using System;
using System.Collections.Generic;

namespace TeX.Models
{
    public class WebHook
    {
        public string id { get; set; }
        public DateTime timestamp { get; set; }
        public string lang { get; set; }
        public Result result { get; set; }
        public Status status { get; set; }
        public string sessionId { get; set; }
    }

    public class Result
    {
        public string source { get; set; }
        public string resolvedQuery { get; set; }
        public string action { get; set; }
        public bool actionIncomplete { get; set; }
        public Dictionary<string, string> parameters { get; set; }
        public List<object> contexts { get; set; }
        public Metadata metadata { get; set; }
        public Fulfillment fulfillment { get; set; }
        public double score { get; set; }
    }

    public class Metadata
    {
        public string intentId { get; set; }
        public string webhookUsed { get; set; }
        public string webhookForSlotFillingUsed { get; set; }
        public string intentName { get; set; }
    }

    public class Fulfillment
    {
        public string speech { get; set; }
        public List<Message> messages { get; set; }
    }

    public class Message
    {
        public int type { get; set; }
        public string speech { get; set; }
    }

    public class Status
    {
        public int code { get; set; }
        public string errorType { get; set; }
        public string errorDetails { get; set; }
    }

    public class Response
    {
        public string sessionId { get; set; }
        public string speech { get; set; }
        public string displayText { get; set; }
        public object data { get; set; }
        public List<object> contextOut { get; set; }
        public string source { get; set; }
    }
}