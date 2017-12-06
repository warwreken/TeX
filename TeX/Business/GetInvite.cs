using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
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
                results = wc.DownloadString("https://appshom.agnet.com.br/INVITE/api/MinhasReunioes/" + wh.result.parameters["login"] + "@agnet.com.br/");
            }

            agenda = JsonConvert.DeserializeObject<List<AgendaConsultaModel>>(results);

            if (agenda.Count > 0)
            {
                response = "Essa pessoa tem atualmente " + agenda.Count + " reuniões agendadas para os próximos dias.";
                if (agenda.Count >= 3)
                {
                    response += "Abaixo, seguem as próximas 3 reuniões.</br>";
                    for (int i = 0; i < 3; i++)
                    {
                        response += "Reunião dia " + agenda[i].Inicio.Day + "/" + agenda[i].Inicio.Month + " as " + agenda[i].Inicio.Hour + ":" + agenda[i].Inicio.Minute + " até as " + agenda[i].Fim.Hour + ":" + agenda[i].Fim.Minute + " na sala " + agenda[i].Sala + ". Assunto: " + agenda[i].Assunto + "</br>";
                    }
                }
                else
                {
                    response += "Abaixo, seguem as próximas " + agenda.Count + " reuniões.</br>";
                    for (int i = 0; i < agenda.Count; i++)
                    {
                        response += "Reunião dia " + agenda[i].Inicio.Day + "/" + agenda[i].Inicio.Month + " as " + agenda[i].Inicio.Hour + ":" + agenda[i].Inicio.Minute + " até as " + agenda[i].Fim.Hour + ":" + agenda[i].Fim.Minute + " na sala " + agenda[i].Sala + ". Assunto: " + agenda[i].Assunto + "</br>";
                    }
                }
            }

            return response;
        }
    }
}