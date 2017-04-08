using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreMVC.Infrastructure;
using CoreMVC.Models;
using Microsoft.Extensions.Primitives;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreMVC.Controllers
{
    [Route("api/[controller]")]
    public class PurchaseController : Controller
    {
        private readonly IPurchaseRepository _repository;

        #region Constructor
        public PurchaseController(IPurchaseRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region GetAll Method
        // GET: api/values
        [Route("[action]")]
        [HttpGet]
        public IEnumerable<Purchase> GetAll()
        {
            List<Purchase> ListPurchase = new List<Purchase>();
            foreach (Purchase PurchaseOne in _repository.GetAll())
            {
                Purchase NewPurchase = new Purchase();
                NewPurchase = PurchaseOne;
                NewPurchase.Orders = null;
                ListPurchase.Add(NewPurchase);
            }
            return ListPurchase;
        }
        #endregion

        #region Get Method
        // GET api/values/5
        [HttpGet(Name = "GetPurchase")]
        public IActionResult Get()
        {
            StringValues hearderValues;
            var firstValue = string.Empty;
            if (Request.Headers.TryGetValue("id", out hearderValues))
                firstValue = hearderValues.FirstOrDefault();
            long id = Convert.ToInt64(firstValue);
            var item = _repository.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            item.Orders = null;
            return new ObjectResult(item);
        }
        #endregion

        #region Create Method
        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody] Purchase value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            value.DatePurchase = DateTime.Now;
            _repository.Add(value);
            return CreatedAtRoute("GetPurchase", new { id = value.PurchaseId }, value);
        }
        #endregion

    }
}
