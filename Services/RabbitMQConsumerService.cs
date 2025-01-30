using CalculatorApi.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace CalculatorApi.Services
{
    public class RabbitMQConsumerService : IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        private readonly CalculationService _calculationService;

        public RabbitMQConsumerService(CalculationService calculationService)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "calculation",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            _calculationService = calculationService;
        }

        public void StartConsuming()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"[Consumer] Received: {message}");

                var calculation = JsonSerializer.Deserialize<Calculation>(message);
                if (calculation != null)
                {
                    calculation.Result = EvaluateExpression(calculation.Expression);
                    calculation.Id = Guid.NewGuid();
                    _calculationService.CreateCalculationAsync(calculation).Wait();
                }

                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            _channel.BasicConsume(queue: "calculation",
                                 autoAck: false,
                                 consumer: consumer);
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }

        static double EvaluateExpression(string expression)
        {
            try
            {
                return Convert.ToDouble(new System.Data.DataTable().Compute(expression, null));
            }
            catch
            {
                Console.WriteLine("[Consumer] Error evaluating expression.");
                return double.NaN;
            }
        }
    }
}