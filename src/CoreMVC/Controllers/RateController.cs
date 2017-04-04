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
        [HttpGet]
        public IEnumerable<Rate> GetAll()
        {
            return _repository.GetAll();
        }
        #endregion

        #region Get Method
        // GET api/values/5
        [HttpGet("{id}", Name = "GetRate")]
        public IActionResult Get(int id)
        {
            var item = _repository.Find(id);
            if (item == null || item.ProductId == null || item.UserId == null)
            {
                return NotFound();
            }

            // Create the user object inside the Rate
            var RateUser = _userRepository.Find(item.UserId);
            User NewUser = new User();
            NewUser.UserId = RateUser.UserId;
            NewUser.City = RateUser.City;
            NewUser.Street = RateUser.Street;
            NewUser.ZipCode = RateUser.ZipCode;
            NewUser.Username = RateUser.Username;
            item.User = NewUser;

            // Create the product object inside the Rate
            var RateProduct = _productRepository.Find(item.ProductId);
            Product Product = new Product();
            Product.ProductId = RateProduct.ProductId;
            Product.Name = RateProduct.Name;
            Product.Price = RateProduct.Price;
            Product.Quantity = RateProduct.Quantity;
            Product.TVA = RateProduct.TVA;
            Product.Brand = RateProduct.Brand;
            Product.Category = RateProduct.Category;
            Product.Discount = RateProduct.Discount;
            item.Product = Product;

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
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Rate item)
        {
            if (item == null || item.InteractionId != id)
            {
                return BadRequest();
            }

            var Rate = _repository.Find(id);
            if (Rate == null)
            {
                return NotFound();
            }
            
            Rate.InteractionId = item.InteractionId;
            Rate.Product = item.Product;
            Rate.ProductId = item.ProductId;
            Rate.Mark = item.Mark;
            Rate.User = item.User;
            Rate.UserId = item.UserId;

            _repository.Update(Rate);
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

