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
    public class ProductController : Controller
    {
        private readonly IProductRepository _repository;

        #region Contructor
        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        } 
        #endregion

        #region GetAll Method
        // GET: api/values
        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            return _repository.GetAll();
        } 
        #endregion

        #region Get Method
        // GET api/values/5
        [HttpGet("{id}", Name = "GetProduct")]
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
        public IActionResult Create([FromBody] Product value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            _repository.Add(value);
            return CreatedAtRoute("GetProduct", new { id = value.ProductId }, value);
        } 
        #endregion

        #region Update Method

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Product item)
        {
            if (item == null || item.ProductId != id)
            {
                return BadRequest();
            }

            var product = _repository.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            product.Brand = item.Brand;
            product.Category = item.Category;
            product.Discount = item.Discount;
            product.Name = item.Name;
            product.Price = item.Price;
            product.Quantity = item.Quantity;
            product.TVA = item.TVA;

            _repository.Update(product);
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
