﻿using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;
using System.Diagnostics;
using SEOP.Framework.Config;
using Sample.Domain.Persistence;
using SEOP.Framework.IoC;
using SEOP.MessageQueue.MSKafka.Config;
using SEOP.Framework.MessageQueue;
using SEOP.Framework.Message;
using SEOP.Framework.Command;
using Sample.Domain;
using System.Threading;

namespace Sample
{
    public class Global : HttpApplication
    {
        IMessagePublisher _messagePublisher;
        ICommandBus _commandBus;
        IMessageConsumer _commandConsumer;

        void Application_Start(object sender, EventArgs e)
        {
            try
            {
                var queue = "Classic.MyCommand";
                var topic = "MyEvent";
                string appName = $"{Environment.MachineName}.ClassicApp";
                string subscription = "subscription";
                Configuration.Instance
                             .RegisterDefaultEventBus()
                             //.UseNoneLogger()
                             .UseLog4Net()
                             .UseMessageQueue(appName)
                             .UseKafka("localhost:2181")
                             .UseCommandBus(appName)
                             .UseMessagePublisher(topic);

                ErrorCodeDictionaryInitializer.Init();

                _messagePublisher = MessageQueueFactory.GetMessagePublisher();
                _messagePublisher.Start();

                _commandBus = MessageQueueFactory.GetCommandBus();
                _commandBus.Start();

                _commandConsumer = MessageQueueFactory.CreateCommandConsumer(queue, appName, "commandHandlerProvider");
                _commandConsumer.Start();

             

                var subscribedTopic = $"{appName}.{topic}";
                // 创建事件订阅器并启动开始消费消息
                var eventSubscriber = MessageQueueFactory.CreateEventSubscriber(subscribedTopic, subscription, appName, "eventHandlerProvider");
                eventSubscriber.Start();

                AreaRegistration.RegisterAllAreas();
                GlobalConfiguration.Configure(WebApiConfig.Register);
                RouteConfig.RegisterRoutes(RouteTable.Routes);
                BundleConfig.RegisterBundles(BundleTable.Bundles);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().Message);
            }

            // Code that runs on application startup

        }

        void Application_End(object sender, EventArgs e)
        {
            _commandConsumer.Stop();
            _commandBus.Stop();
            _messagePublisher.Stop();
            IoCFactory.Instance.CurrentContainer.Dispose();
        }
    }
}