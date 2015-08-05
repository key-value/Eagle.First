using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Eagle.Web.Areas.Channel.Controllers
{
    public class ReplyController : ApiController
    {
        // GET: api/Reply
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Reply/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Reply
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Reply/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Reply/5
        public void Delete(int id)
        {
        }
    }
}
