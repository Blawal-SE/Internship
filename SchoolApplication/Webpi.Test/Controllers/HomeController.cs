using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Webpi.Test.Controllers
{
    public class HomeController : ApiController
    {
        public IHttpActionResult Get() 
        {
            var list = new List<string>();
            list.Add("blawal");
            list.Add("sarfraz");
            return Ok(list);
        }
    }
}
