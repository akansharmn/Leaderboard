using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;

namespace KafkaProducer
{
    public class Producer<T1, T2>
    {
        ProducerConfig config;
        Action<DeliveryReport<T1, T2>> handler;
        string topicName;

        public Producer(string brokerList, string topicName)
        {
            this.topicName = topicName;
             this.config = new ProducerConfig { BootstrapServers = brokerList };
            this.handler = r =>
                Console.WriteLine(!r.Error.IsError ? $"Delivered message to {r.TopicPartitionOffset}" : $"Delivery error: {r.Error.Reason}");
        }

        public void Ingest(T1 key, T2 value)
        {
           

            using (var producer = new ProducerBuilder<T1, T2>(config).Build())
            {
                Console.WriteLine($"Putting record into topic {topicName} {value}");
                    producer.Produce(topicName, new Message<T1, T2> { Key = key, Value = value }, handler);

            }

            
        }
    }
}
