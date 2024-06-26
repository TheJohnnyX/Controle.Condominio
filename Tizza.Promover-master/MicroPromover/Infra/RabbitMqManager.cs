using Microsoft.EntityFrameworkCore.Metadata;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Text;

namespace Promocao
{
    public static class RabbitMqManager
    {
        public static void Iniciar()
        {
            var factory = new ConnectionFactory
            {
                HostName = "chimpanzee-01.rmq.cloudamqp.com",
                UserName = "nyoghlda",
                Password = "KVnAk7M2S1t3SVQap8-fNkz1ohQueTQY",
                VirtualHost = "nyoghlda"
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            PublishInserirMovtoDeEntrada.Iniciar(channel);
            PublishInformarPromocaoNaPizza.Iniciar(channel);
            ConsumerResultInformarPromocaoNaPizza.ResultInformarPromocaoNaPizza(channel);
        }
    }
}
