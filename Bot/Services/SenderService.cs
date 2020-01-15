using Bot.ViewModels;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Services
{
    public class SenderService : ISenderService
    {
        public bool SendMessage(MessageViewModel message)
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
                            arguments: null);

                        //var body = Encoding.UTF8.GetBytes(message);
                        var body = ObjectConverter.getBytes(message);

                        channel.BasicPublish(
                        exchange: "",
                        routingKey: "messages",
                        basicProperties: null,
                        body: body
                        );

                        Console.WriteLine("Mensaje enviado: {0}", message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("No se pudo enviar el mensaje [{0}] debido a la siguiente excepción: {1}", [message.Content, ex.Message]);
                return false;
            }

            return true;
        }
    }
}
