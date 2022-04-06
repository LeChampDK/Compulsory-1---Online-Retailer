using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using SharedModel.Messages.LoginComponent;
using System;
using System.Threading;

namespace LoginService
{
    public class MessageListener
    {
        IServiceProvider _provider;
        string _connectionString;
        IBus _bus;

        public MessageListener(IServiceProvider provider, string connectionString)
        {
            _provider = provider;
            _connectionString = connectionString;
            _bus = RabbitHutch.CreateBus(_connectionString);
        }

        public void Start()
        {
            using (_bus = RabbitHutch.CreateBus(_connectionString))
            {
                _bus.PubSub.Subscribe<LoginRequest>("loginApi", HandleLogin);
                _bus.PubSub.Subscribe<LoginCreate>("loginApi", HandleCreate);
                

                lock (this)
                {
                    Monitor.Wait(this);
                }
            }
        }

        private void HandleCreate(LoginCreate loginCreate)
        {
            using (var scope = _provider.CreateScope())
            {
                var service = scope.ServiceProvider;
                var loginService = service.GetService<ILoginService>();

                var login = loginService.CreateLogin(loginCreate.email, loginCreate.password);

                if (login)
                {
                    var reply = new LoginCreated
                    {
                        Created = true
                    };
                    _bus.PubSub.Publish(reply);
                }

                else
                {
                    var replyReject = new LoginCreateFailed
                    {
                        LoginCreateRejected = true
                    };

                    _bus.PubSub.Publish(replyReject);
                }
            }
        }

        private void HandleLogin(LoginRequest loginRequest)
        {
            using (var scope = _provider.CreateScope())
            {
                var service = scope.ServiceProvider;
                var loginService = service.GetService<ILoginService>();

                var login = loginService.Login(loginRequest.email, loginRequest.password);

                if (login)
                {
                    var reply = new LoginSuccess
                    {
                        Success = true
                    };
                    _bus.PubSub.Publish(reply);
                }

                else
                {
                    var replyReject = new LoginRejected
                    {
                        Rejected = true
                    };

                    _bus.PubSub.Publish(replyReject);
                }
            }
        }
    }
}
