using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot
{
    public class ConnectionRabbitMQ : ConnectionFactory
    {
        public ConnectionRabbitMQ()
        {
            HostName = "localhost";
            UserName = "guest";
            Password = "guest";
        }
    }
}
