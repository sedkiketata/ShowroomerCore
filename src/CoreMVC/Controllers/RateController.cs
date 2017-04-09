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
    public class RateController : Controller
    {
        private readonly IRateRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;

        #region Contructor
        public RateController(IRateRepository repository, IUserRepository userRepository, IProductRepository productRepository)
        {
            _repository = repository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }
        #endregion

        #region GetAll Method
        // GET: api/values
        [Route("[action]")]
        [HttpGet]
        public IEnumerable<Rate> GetAll()
        {
            List<Rate> ListRate = new List<Rate>();
            foreach (Rate RateOne in _repository.GetAll())
            {
                Rate NewRate = new Rate();
                NewRate = RateOne;
                NewRate.User = null;
                NewRate.Product = null;
                ListRate.Add(NewRate);
            }
            return ListRate;
        }
        #endregion

        #region Get Method
        // GET api/values/5
        [HttpGet(Name = "GetRate")]
        public IActionResult Get()
        {
            StringValues hearderValues;
            var firstValue = string.Empty;
            if (Request.Headers.TryGetValue("id", out hearderValues))
                firstValue = hearderValues.FirstOrDefault();
            long id = Convert.ToInt64(firstValue);
            var item = _repository.Find(id);
            if (item == null || item.ProductId == null || item.UserId == null)
            {
                return NotFound();
            }

            // Create the user object inside the Rate
            var RateUser = _userRepository.Find(item.UserId);
            User NewUser = new User();
            NewUser = RateUser;
            NewUser.Vouchers = null;
            NewUser.Orders = null;
            NewUser.Interactions = null;
            item.User = NewUser;

            // Create the product object inside the Rate
            var RateProduct = _productRepository.Find(item.ProductId);
            Product Product = new Product();
            Product = RateProduct;
            Product.Images = null;
            Product.Interactions = null;
            Product.Orders = null;
            Product.Showrooms = null;
            item.Product = Product;

            // Unset variables that are unused
            RateUser = null;
            NewUser = null;
            RateProduct = null;
            Product = null ;

            return new ObjectResult(item);
        }
        #endregion

        #region Create Method
        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody] Rate value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            _repository.Add(value);
            return CreatedAtRoute("GetRate", new { id = value.InteractionId }, value);
        }
        #endregion

        #region Update Method

        // PUT api/values/5
        [HttpPut]
        public IActionResult Update([FromBody] Rate item)
        {
            StringValues hearderValues;
            var firstValue = string.Empty;
            if (Request.Headers.TryGetValue("id", out hearderValues))
                firstValue = hearderValues.FirstOrDefault();
            long id = Convert.ToInt64(firstValue);
            if (item == null || item.InteractionId != id)
            {
                return BadRequest();
            }

            var Rate = _repository.Find(id);
            if (Rate == null)
            {
                return NotFound();
            }
            
            Rate.Mark = item.Mark;

            _repository.Update(Rate);
            return new NoContentResult();
        }

        #endregion

        #region Delete Method
        // DELETE api/values/5
        [HttpDelete]
        public IActionResult Delete()
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
            _repository.Remove(id);
            return new NoContentResult();
        }
        #endregion
    }
}

