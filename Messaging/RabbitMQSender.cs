using System;
using RabbitMQ.Client;

namespace Test_API.Messaging
{
    public class RabbitMQSender
    {
        public RabbitMQSender()
        {
            var factory = new ConnectionFactory(){ HostName = "localhost"};
            
        }
        
    }
}