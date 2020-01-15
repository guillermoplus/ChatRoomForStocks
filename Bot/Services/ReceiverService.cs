using Bot.ViewModels;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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
                            var jsonMessage = Encoding.UTF8.GetString(body);
                            //var message = ObjectConverter.getObject<MessageViewModel>(body);

                            var message = JsonConvert.DeserializeObject<MessageViewModel>(jsonMessage);

                            message.Content = GetStockPriceMessage(message.Content);

                            Debug.WriteLine("\n\n[x] Mensaje recibido: " + message.Content + "\n");
                        };

                        channel.BasicConsume(
                            queue: "messages",
                            autoAck: true,
                            consumer: consumer
                            );

                        Debug.WriteLine("\n\nServicio Receiver iniciado correctamente.\n");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\nSe detuvo el servicio Receiver debido a lo siguiente: " + ex.Message + "\n");
            }
        }

        public string GetStockPriceMessage(string command)
        {
            var stock_code = command.Split("=").Last();

            var url = "https://stooq.com/q/l/?s=" + stock_code + "&f=sd2t2ohlcv&h&e=csv";

            var data = GetCSVFromUrl(url);

            var data_headers = data.Split("\n")[0].Split(",");

            var price_index = Array.IndexOf(data_headers, "Close");

            var stock_data = data.Split("\n")[1].Split(",");

            var share_price = stock_data[price_index];

            return (stock_code.ToUpper() + " quote is $" + share_price + " per share");
        }

        public string GetCSVFromUrl(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string results = sr.ReadToEnd();

            sr.Close();

            return results;
        }
    }
}
