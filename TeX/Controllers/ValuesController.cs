using Newtonsoft.Json;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Web.Http;
using TeX.Models;

namespace TeX.Controllers
{
    public class ValuesController : ApiController
    {
        [HttpPost]
        public Response Post([FromBody] WebHook wh)
        {
            var response = new Response();
            string cpf = string.Empty;
            string idade = string.Empty;
            string rg = string.Empty;
            for (int i = 0; i < wh.result.contexts.Count; i++)
            {
                if (wh.result.contexts[i].ToString().Contains("cpf.original"))
                {
                    dynamic data = JsonConvert.DeserializeObject(wh.result.contexts[i].ToString());
                    cpf = data.parameters.cpf;
                    rg = data.parameters.rg;
                    idade = data.parameters.idade;
                    continue;
                }
            }
            switch (wh.result.metadata.intentName)
            {
                case "ag.ti.desbloquearUsuarioRG":
                    //TODO METHOD RESPONSE
                    InfoReset.Usuarios users = new InfoReset.Usuarios();
                    var reset = users.ConfirmarInformacoesParaReset("", cpf, rg, idade);
                    switch (reset.Codigo)
                    {
                        case 0:
                            response.speech = "BIIIIIP... de acordo com meus cálculos, o usuário " + reset.Mensagem.Substring(9) + " agora está desbloquedo!";
                            //PrincipalContext ctx = new PrincipalContext(ContextType.Domain,
                            //             "AGDOMAIN",
                            //             null,
                            //             "_srvcUnlockAccount",
                            //             "*DesbloqueadordeContasAG17");

                            //UserPrincipal usr = UserPrincipal.FindByIdentity(ctx, IdentityType.SamAccountName, reset.Mensagem.Substring(9));
                            //if (usr != null)
                            //{
                            //    if (usr.IsAccountLockedOut())
                            //    {
                            //        usr.UnlockAccount();
                            //        response.speech = "BIIIIIP... de acordo com meus cálculos, o usuário " + reset.Mensagem.Substring(9) + " agora está desbloquedo!";
                            //    }
                            //    else
                            //    {
                            //        response.speech = "BEEEEHHHH! O usuário " + reset.Mensagem.Substring(9) + " não estava bloquedo!";
                            //    }
                            //    usr.Dispose();
                            //}
                            //ctx.Dispose();
                            break;
                        case 1:
                            response.speech = "BEH! Alguma das informações não foram reconhecidas!";
                            break;
                        case 2:
                            response.speech = "Não tente se passar por outra pessoa... Esse usuário não está mais entre nós!";
                            break;
                        case 3:
                            response.speech = "HMM... Essa pessoa não está com um e-mail cadastrado... Eu vejo inconsistências... Com que frequência?! Todo o tempo... =P";
                            break;
                        case 4:
                            response.speech = "Faltou alguma informação pra eu identificar o usuário. Ainda não tenho bola de cristal, sabia?";
                            break;
                        default:
                            response.speech = "MINHA CABEÇA DÓI!";
                            break;
                    }
                   
                    response.displayText = "O usuário " + reset.Mensagem + " foi desbloqueado com sucesso!";
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
