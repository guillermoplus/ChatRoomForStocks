using Bot.ViewModels;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Services
{
    public class ReceiverService : IReceiverService
    {
        public void Receive()
        {
            try
            {
                var factory = new ConnectionRabbitMQ();

                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(
                            queue: "messages",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null
                            );

                        var consumer = new EventingBasicConsumer(channel);

                        consumer.Received += (model, ea) =>
                        {
                            var body = ea.Body;
                            //var message = Encoding.UTF8.GetString(body);
                            var message = ObjectConverter.getObject<MessageViewModel>(body);

                            // ACCIÓN AL RECIBIR EL MENSAJE

                            Console.WriteLine(" [x] Mensaje recibido: {0}", message.Content);
                        };

                        channel.BasicConsume(
                            queue: "messages",
                            autoAck: true,
                            consumer: consumer
                            );

                        Console.WriteLine("Servicio Receiver iniciado correctamente.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("No se pudo iniciar el servicio Receiver debido a lo siguiente: {0}", ex.Message);
            }
        }
    }
}
