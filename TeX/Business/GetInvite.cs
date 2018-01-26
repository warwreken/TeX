using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Text;
using TeX.Models;

namespace TeX.Business
{
    public class GetInvite
    {
        public static string GetReunioes(WebHook wh)
        {
            string results = string.Empty;
            string response = "Não achei nenhum agendamento para o login informado...";
            List<AgendaConsultaModel> agenda = new List<AgendaConsultaModel>();

            using (WebClient wc = new WebClient())
            {
                results = wc.DownloadString("https://apps.agnet.com.br/INVITE/api/MinhasReunioes/" + wh.result.parameters["login"] + "@agnet.com.br/");
            }

            agenda = JsonConvert.DeserializeObject<List<AgendaConsultaModel>>(results);

            if (agenda.Count > 0)
            {
                response = "Essa pessoa tem atualmente " + agenda.Count + " reunião(ões) agendadas para os próximos dias.";
                if (agenda.Count >= 3)
                {
                    response += "Abaixo segue(m) as próximas 3 reuniões.\n";
                    for (int i = 0; i < 3; i++)
                    {
                        byte[] b1 = Encoding.Default.GetBytes(agenda[i].Assunto);
                        var assunto = Encoding.UTF8.GetString(b1);
                        response += "[" + agenda[i].Inicio.Day + "/" + agenda[i].Inicio.Month + "] das " + agenda[i].Inicio.Hour + ":" + agenda[i].Inicio.Minute + " até as " + agenda[i].Fim.Hour + ":" + agenda[i].Fim.Minute + " na sala " + agenda[i].Sala + ". Assunto: " + assunto + "\n";
                    }
                }
                else
                {
                    response += "Abaixo seguem as próximas " + agenda.Count + " reuniões.\n";
                    for (int i = 0; i < agenda.Count; i++)
                    {
                        byte[] b2 = Encoding.Default.GetBytes(agenda[i].Assunto);
                        var assunto = Encoding.UTF8.GetString(b2);
                        response += "[" + agenda[i].Inicio.Day + "/" + agenda[i].Inicio.Month + "] das " + agenda[i].Inicio.Hour + ":" + agenda[i].Inicio.Minute + " até as " + agenda[i].Fim.Hour + ":" + agenda[i].Fim.Minute + " na sala " + agenda[i].Sala + ". Assunto: " + assunto + "\n";
                    }
                }
            }
            
            return response;
        }
    }
}