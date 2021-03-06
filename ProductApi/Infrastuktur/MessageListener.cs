using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using ProductApi.Data.Facade;
using ProductApi.Models;
using SharedModel;
using SharedModel.Messages;
using SharedModels;
using System;
using System.Collections.Generic;
using System.Threading;
using static SharedModel.Enums.Enums;

namespace ProductApi.Infrastuktur
{
    public class MessageListener
    {
        IServiceProvider _provider;
        string _connectionString;
        IBus _bus;

        public MessageListener(IServiceProvider provider, string connectionstring)
        {
            _provider = provider;
            _connectionString = connectionstring;
            _bus = RabbitHutch.CreateBus(_connectionString);
        }

        public void Start()
        {
            using(_bus = RabbitHutch.CreateBus(_connectionString))
            {
                _bus.PubSub.Subscribe<ProductRequest>("productApiCreated", HandledOrderCreated);
            

                lock (this)
                {
                    Monitor.Wait(this);
                }
            }
        }

        private void HandledOrderCreated(ProductRequest productRequest)
        {
            using(var scope = _provider.CreateScope())
            {
                var service = scope.ServiceProvider;
                var productrepo = service.GetService<IRepository<Product>>();


                if(ProductItemAvailable(productRequest.Products, productrepo))
                {
                    foreach(var orderline in productRequest.Products)
                    {
                        var product = productrepo.Get(orderline.ProductId);
                        product.ItemsReserved += orderline.Quantity;
                        productrepo.Edit(product);
                    }

                    var replyMessage = new ProductAcceptResponse
                    {
                        Id = productRequest.Id,
                        Status = OrderStatus.Completed
                    };

                    _bus.PubSub.Publish(replyMessage);
                } else
                {
                    var replyMessage = new ProductRejectResponse
                    {
                        Id = productRequest.Id,
                        Status = OrderStatus.Cancelled
                    };

                    _bus.PubSub.Publish(replyMessage);
                }
            }
        }

        private bool ProductItemAvailable(IList<OrderLine> products, IRepository<Product> productrepo)
        {
            if (products != null)
            {
                foreach (var product in products)
                {
                    var productv2 = productrepo.Get(product.ProductId);
                    if (product.Quantity > productv2.ItemsInStock - productv2.ItemsReserved)
                    {
                        return false;
                    }
                }
                return true;
            } else
            {
                return false;
            }
        }
    }
}
