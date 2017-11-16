using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeX.Models
{
    public class WebHook
    {
        public string Speech { get; set; }
        public string ContextOut { get; set; }
        public string Data { get; set; }
        public string DisplayText { get; set; }
        public string Source { get; set; }
    }
}