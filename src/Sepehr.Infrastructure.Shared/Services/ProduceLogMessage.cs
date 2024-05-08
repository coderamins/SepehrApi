using Confluent.Kafka;
using Sepehr.Application.Interfaces;

namespace Sepehr.Infrastructure.Shared.Services
{
    public class ProduceLogMessage:IProduceLogMessage
    {
        public async Task CreateMessage(string tableName, object data)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
                ClientId = "my-app",
                BrokerAddressFamily = BrokerAddressFamily.V4,
            };

            using var producer = new ProducerBuilder<Null, string>(config).Build();
            while (true)
            {
                Console.WriteLine("Please Enter a message you wanna send :");
                var input = Console.ReadLine();

                var message = new Message<Null, string>
                {
                    Value = input
                };

                var deliveryReport = await producer.ProduceAsync("my-topic", message);
                Console.WriteLine($"delivery: {deliveryReport.TopicPartitionOffset}");

            }
        }

    }
}
