using EasyNetQ;
using System;

namespace OrderApi.Infrastructure
{
    public class MessageListener
    {
        IServiceProvider _service;
        string _connectionString;
        IBus _bus;

        public MessageListener(IServiceProvider service, string connectionString)
        {
            _service = service;
            _connectionString = connectionString;
        }

        public void Start()
        {
            using(_bus = RabbitHutch.CreateBus(_connectionString)
            {
                _bus.PubSub.Subscribe<>
            }
        }
    }
}
