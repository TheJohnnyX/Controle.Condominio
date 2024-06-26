using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Threading.Channels;
using System.Text;
using MicroPromover.DTO;
using Newtonsoft.Json;

namespace Promocao
{
    public static class PublishInformarPromocaoNaPizza
    {
        public static IModel _channel;

        public static void Iniciar(IModel channel)
        {
            _channel = channel;
        }

        public static void PublicarInformarPromocaoNaPizza(InformarPromocaoNaPizzaPorFilaDTO promocao)
        {
            var body = Encoding.Default.GetBytes(JsonConvert.SerializeObject(promocao));

            _channel.BasicPublish(exchange: string.Empty,
                     routingKey: "InformarPromocaoNaPizza",
                     body: body);
        }
    }
}
