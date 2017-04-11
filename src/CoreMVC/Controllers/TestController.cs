using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreMVC.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return "GET: Test message";/*new HttpResponseMessage()*/
            //{
            //    Content = new StringContent("GET: Test message")
            //};
        }

        public string Post()
        {
            return "POST: Test message";
            //return new HttpResponseMessage()
            //{
            //    Content = new StringContent("POST: Test message")
            //};
        }

        public string Put()
        {
            return "POST: Test message";
            //return new HttpResponseMessage()
            //{
            //    Content = new StringContent("PUT: Test message")
            //};
        }
    }
}
