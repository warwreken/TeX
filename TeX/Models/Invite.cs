using System;
using System.Collections.Generic;

namespace TeX.Models
{
    public class CalendarioEvento
    {
        public string Id { get; set; }
        public string Local { get; set; }
        public string Assunto { get; set; }
        public bool Livre { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
    }

    public class HorarioModel : CalendarioEvento
    {
        public int SalaId { get; set; }
        public String Body { get; set; }
        public bool VCF { get; set; }
        public String Observacao { get; set; }
        public bool Intersects(HorarioModel target)
        {
            //return (((Inicio <= target.Inicio) && (Fim >= target.Inicio)) ||
            //    ((Inicio <= target.Fim) && (Fim >= target.Fim)) ||
            //    ((Inicio >= target.Inicio) && (Fim <= target.Fim)));

            return (((Inicio < target.Inicio) && (Fim > target.Inicio)) ||
                ((Inicio < target.Fim) && (Fim > target.Fim)) ||
                ((Inicio >= target.Inicio) && (Fim <= target.Fim)));
        }

        public override string ToString()
        {
            return String.Format("{0} - {1}", this.Inicio.ToString(), this.Livre);
        }
    }

    public class ParticipanteModel
    {
        public int ParticipanteId { get; set; }
        public String Email { get; set; }
        public String Nome { get; set; }
        public MeetingAttendeeType Tipo { get; set; }
        public bool Organizador { get; set; }
    }

    public class AgendaConsultaModel : HorarioModel
    {
        public bool Dono { get; set; }
        public ParticipanteModel Organizador { get; set; }
        public SalaModel Sala { get; set; }
        public List<ParticipanteModel> Participantes { get; set; }
        public int AgendamentoId { get; set; }
        public override string ToString()
        {
            return String.Format("{0} - {1}", this.Inicio.ToString(), this.Livre);
        }
    }

    public class SalaModel
    {
        public int SalaId { get; set; }
        public String Escritorio { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Email { get; set; }
        public int Lotacao { get; set; }
        public override string ToString()
        {
            return Nome;
        }
    }

    namespace Microsoft.Exchange.WebServices.Data
    {
        //
        // Summary:
        //     Defines the type of a meeting attendee.
        public enum MeetingAttendeeType
        {
            //
            // Summary:
            //     The attendee is the organizer of the meeting.
            Organizer = 0,
            //
            // Summary:
            //     The attendee is required.
            Required = 1,
            //
            // Summary:
            //     The attendee is optional.
            Optional = 2,
            //
            // Summary:
            //     The attendee is a room.
            Room = 3,
            //
            // Summary:
            //     The attendee is a resource.
            Resource = 4
        }
    }
    //     Defines the type of a meeting attendee.
    public enum MeetingAttendeeType
    {
        //
        // Summary:
        //     The attendee is the organizer of the meeting.
        Organizer = 0,
        //
        // Summary:
        //     The attendee is required.
        Required = 1,
        //
        // Summary:
        //     The attendee is optional.
        Optional = 2,
        //
        // Summary:
        //     The attendee is a room.
        Room = 3,
        //
        // Summary:
        //     The attendee is a resource.
        Resource = 4
    }
}