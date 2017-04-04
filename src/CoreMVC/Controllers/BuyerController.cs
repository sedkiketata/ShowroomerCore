using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreMVC.Infrastructure;
using CoreMVC.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreMVC.Controllers
{
    [Route("api/[controller]")]
    public class BuyerController : Controller
    {
        private readonly IBuyerRepository _repository;

        #region Contructor
        public BuyerController(IBuyerRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region GetAll Method
        // GET: api/values
        [HttpGet]
        public IEnumerable<Buyer> GetAll()
        {
            return (IEnumerable<Buyer>)_repository.GetAll();
        }
        #endregion

        #region Get Method
        // GET api/values/5
        [HttpGet("{id}", Name = "GetBuyer")]
        public IActionResult Get(int id)
        {
            var item = _repository.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        #endregion

        #region Create Method
        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody] Buyer value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            _repository.Add(value);
            return CreatedAtRoute("GetBuyer", new { id = value.UserId }, value);
        }
        #endregion

        #region Update Method

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Buyer item)
        {
            if (item == null || item.UserId != id)
            {
                return BadRequest();
            }

            var Buyer = _repository.Find(id);
            if (Buyer == null)
            {
                return NotFound();
            }

            Buyer.Username = item.Username;
            Buyer.Street = item.Street;
            Buyer.ZipCode = item.ZipCode;
            Buyer.City = item.City;
            Buyer.Interactions = item.Interactions;
            Buyer.Orders = item.Orders;
            Buyer.Vouchers = item.Vouchers;
            Buyer.DeliveryAddress = item.DeliveryAddress;

            _repository.Update(Buyer);
            return new NoContentResult();
        }

        #endregion

        #region Delete Method
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var item = _repository.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            _repository.Remove(id);
            return new NoContentResult();
        }
        #endregion
    }
}
