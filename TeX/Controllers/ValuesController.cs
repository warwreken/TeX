using System.Collections.Generic;
using System.Web.Http;
using TeX.Models;

namespace TeX.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public WebHook Post([FromBody]string value)
        {
            var wh = new WebHook();

            wh.Speech = "Funcionou o Speech";
            wh.DisplayText = "Funcionou o Display";
            wh.Source = "WebHookSource";

            return wh;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
