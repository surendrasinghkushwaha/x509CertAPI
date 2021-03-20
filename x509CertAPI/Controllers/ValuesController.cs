using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using x509CertAPI.Models;

namespace x509CertAPI.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public Student Get(int id)
        {
            var sdudent = new Student().GetAll().SingleOrDefault(x => x.Id == id);
            return sdudent;
        }
        [RequireHttps]
        // POST api/values
        public void Post([FromBody] string value)
        {
          
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
