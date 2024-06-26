using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Threading.Channels;
using System.Text;
using Newtonsoft.Json;
using MicroPromover.DTO;
using Promocao;

namespace Promocao
{
    public static class ConsumerResultInformarPromocaoNaPizza
    {
        public static void ResultInformarPromocaoNaPizza(IModel channel)
        {
            var nomeFila = "ResultInformarPromocaoPizza";

            channel.QueueDeclare(queue: nomeFila, durable: true, exclusive: false, autoDelete: false);

            var consumer = new EventingBasicConsumer(channel);


            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var resultInformarPromocaoDto = JsonConvert.DeserializeObject<ResultInformarPromoverPizza>(message);

                var servPromover = GeradorDeServicos.ServiceProvider.GetService<IServPromover>();


                servPromover.ReceberResultInformarResultadoPromocaoAssincrono(resultInformarPromocaoDto);
            };

            channel.BasicConsume(queue: nomeFila,
                     autoAck: true,
                     consumer: consumer);
        }
    }
}
