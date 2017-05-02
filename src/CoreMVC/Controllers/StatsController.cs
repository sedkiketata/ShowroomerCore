using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreMVC.Controllers
{
    [Route("api/[controller]")]
    public class StatsController : Controller
    {
        // GET: api/values
        [Route("[action]")]
        [HttpGet]
        public IEnumerable<string> GetCompanyRevenuePerRegion()
        {
            StringValues headersValues;
            string firstValue = string.Empty;
            if (Request.Headers.TryGetValue("company", out headersValues))
                firstValue = headersValues.FirstOrDefault().Trim();
            string CompanySelected = char.ToUpper(firstValue[0]) + firstValue.Substring(1);
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
