using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events; 

namespace Test_API.Messaging
{
    public class RabbitMQReceiver
    {

        private IConnection _connection = null;
        private IModel _channel = null;
        private EventingBasicConsumer _consumer = null;
        private static RabbitMQReceiver _receiver = null;
        private StringBuilder _receivedMessage = null;
        private RabbitMQReceiver()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            this._receivedMessage = new StringBuilder();
            this._connection = factory.CreateConnection();
            this._channel = this._connection.CreateModel();
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
                this._receivedMessage.Append(message);
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

        public string Received()
        {
            return this._receivedMessage.ToString();
        } 
    }
}