using System.Web.Http;
using TeX.Models;
using TeX.Business;

namespace TeX.Controllers
{
    public class ValuesController : ApiController
    {
        [HttpPost]
        public Response Post([FromBody] WebHook wh)
        {
            var response = new Response();

            switch (wh.result.metadata.intentName)
            {
                case "ag.ti.desbloquearUsuarioRG":
                    response.speech = GetPerson.UnlockPerson(wh);
                    response.displayText = "O usuário foi desbloqueado com sucesso!";
                    break;
                case "weather.temperatura":
                    string local = wh.result.parameters["local"];
                    string dia = wh.result.parameters["data"];
                    
                    response.speech = Weather.GetWeather(local, dia);
                    response.displayText = response.speech;
                    break;
                case "invite.BuscarAgendamentos":
                    response.speech = GetInvite.GetReunioes(wh);
                    response.displayText = response.speech;
                    break;
                default:
                    response.speech = "não deu";
                    response.displayText = "não deu";
                    break;
            }

            return response;
        }
    }
}
