using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace KafkaConsumer
{
    public class Consumer<T1, T2>
    {
        ConsumerConfig config;
        string topicName;
        
        // It was not working when the processor in the consumer was made insatnce memebr and was supplied in constructor. Find if closure works in field members
        public Consumer(string groupId, string bootStrapServers, string topicName)
        {
            config = new ConsumerConfig
            {
                GroupId = groupId,
                BootstrapServers = bootStrapServers,
                AutoOffsetReset = AutoOffsetReset.Latest,
                
            };
            this.topicName = topicName;
           
        }

        public async Task StartConsuming(Func<T1, T2, Task> processor)
        {
            Console.WriteLine("Inside StartConsuming ...");
            using (var c = new ConsumerBuilder<T1, T2>(config).Build())
            {
                c.Subscribe(topicName);

                try
                {
                    while (true)
                    {
                        try
                        {
                            var cr = c.Consume();
                            
                            Console.WriteLine($"Consumed message '{cr.Message.Value}'  at: '{cr.TopicPartitionOffset}'");
                           await  processor(cr.Message.Key, cr.Message.Value);
                            c.Commit(cr);
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Error occured: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    c.Close();
                }

            }
        }
    }
}
