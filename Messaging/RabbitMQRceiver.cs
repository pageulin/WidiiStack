using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events; 

namespace Test_API.Messaging
{
    public class RabbitMQReceiver
    {

        private IModel _channel = null;
        private EventingBasicConsumer _consumer = null;
        private static RabbitMQReceiver _receiver = new RabbitMQReceiver();

        private RabbitMQReceiver()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using(var connection = factory.CreateConnection())
            this._channel = connection.CreateModel();
            this._channel.QueueDeclare(queue: "hello",
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

            this._consumer = new EventingBasicConsumer(this._channel);
            this._consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);
            };
            this._channel.BasicConsume(queue: "hello",
                                autoAck: true,
                                consumer: this._consumer); 
        }

        public static RabbitMQReceiver GetInstance()
        {
            if(_receiver != null)
            {
                _receiver = new RabbitMQReceiver();
            }
            return _receiver;
        
        }

        public void AddConsumer(EventHandler<BasicDeliverEventArgs> eventHandler)
        {
              this._consumer.Received += eventHandler;
        } 
    }
}