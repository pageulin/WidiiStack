using System;
using RabbitMQ.Client;
using System.Text;

namespace Test_API.Messaging
{
    public class RabbitMQSender
    {

        private IConnection _connection = null;
        private IModel _channel = null;
        private static RabbitMQSender _sender = null;

        private RabbitMQSender()
        {
            var factory = new ConnectionFactory(){ HostName = "localhost"};
            this._connection = factory.CreateConnection();
            this._channel = this._connection.CreateModel();
            this._channel.QueueDeclare(queue: "hello",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            
        }

        public static RabbitMQSender GetInstance()
        {
            if(_sender == null)
            {
                _sender = new RabbitMQSender();
            }
            return _sender;
        }

        public void sendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            this._channel.BasicPublish(exchange: "",
                routingKey: "hello",
                basicProperties: null,
                body: body);
        }        
    }
}