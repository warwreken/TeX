using TeX.Models;
using Newtonsoft.Json;
using System.DirectoryServices.AccountManagement;

namespace TeX.Business
{
    public class GetPerson
    {
        public static string UnlockPerson(WebHook wh)
        {
            string cpf = string.Empty;
            string idade = string.Empty;
            string rg = string.Empty;
            string response = string.Empty;
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
            InfoReset.Usuarios users = new InfoReset.Usuarios();
            var reset = users.ConfirmarInformacoesParaReset("", cpf, rg, idade);
            switch (reset.Codigo)
            {
                case 0:
                    response = "BIIIIIP... de acordo com meus cálculos, o usuário " + reset.Mensagem.Substring(9) + " agora está desbloquedo!";
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
                    response = "BEH! Alguma das informações não foram reconhecidas!";
                    break;
                case 2:
                    response = "Não tente se passar por outra pessoa... Esse usuário não está mais entre nós!";
                    break;
                case 3:
                    response = "HMM... Essa pessoa não está com um e-mail cadastrado... Eu vejo inconsistências... Com que frequência?! Todo o tempo... =P";
                    break;
                case 4:
                    response = "Faltou alguma informação pra eu identificar o usuário. Ainda não tenho bola de cristal, sabia?";
                    break;
                default:
                    response = "MINHA CABEÇA DÓI!";
                    break;
            }

            return response;
        }
    }
}