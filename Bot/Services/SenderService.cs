using Bot.ViewModels;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

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

                        var jsonMessage = JsonConvert.SerializeObject(message);

                        var body = Encoding.UTF8.GetBytes(jsonMessage);
                        //var body = ObjectConverter.getBytes<MessageViewModel>(message);

                        if (body == null) throw new Exception("El objeto no pudo ser convertido a bytes");

                        channel.BasicPublish(
                        exchange: "",
                        routingKey: "messages",
                        basicProperties: null,
                        body: body
                        );

                        Debug.WriteLine("\nMensaje enviado: " + jsonMessage + "\n");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\nNo se pudo enviar el mensaje \"" + message.Content + "\" debido a la siguiente excepción: " + ex.Message + "\n");
                return false;
            }

            return true;
        }
    }
}
