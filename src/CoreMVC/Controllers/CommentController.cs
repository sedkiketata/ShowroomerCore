using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreMVC.Models;
using CoreMVC.Infrastructure;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreMVC.Controllers
{
    [Route("api/[controller]")]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;

        #region Contructor
        public CommentController(ICommentRepository repository, IUserRepository userRepository, IProductRepository productRepository)
        {
            _repository = repository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }
        #endregion

        #region GetAll Method
        // GET: api/values
        [HttpGet]
        public IEnumerable<Comment> GetAll()
        {
            return _repository.GetAll();
        }
        #endregion

        #region Get Method
        // GET api/values/5
        [HttpGet("{id}", Name = "GetComment")]
        public IActionResult Get(int id)
        {
            var item = _repository.Find(id);
            if (item == null || item.ProductId == null || item.UserId == null)
            {
                return NotFound();
            }

            // Create the user object inside the Comment
            var CommentUser = _userRepository.Find(item.UserId);
            User NewUser = new User();
            NewUser.UserId = CommentUser.UserId;
            NewUser.City = CommentUser.City;
            NewUser.Street = CommentUser.Street;
            NewUser.ZipCode = CommentUser.ZipCode;
            NewUser.Username = CommentUser.Username;
            item.User = NewUser;

            // Create the product object inside the Comment
            var CommentProduct = _productRepository.Find(item.ProductId);
            Product Product = new Product();
            Product.ProductId = CommentProduct.ProductId;
            Product.Name = CommentProduct.Name;
            Product.Price = CommentProduct.Price;
            Product.Quantity = CommentProduct.Quantity;
            Product.TVA = CommentProduct.TVA;
            Product.Brand = CommentProduct.Brand;
            Product.Category = CommentProduct.Category;
            Product.Discount = CommentProduct.Discount;
            item.Product = Product;

            return new ObjectResult(item);
        }
        #endregion

        #region Create Method
        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody] Comment value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            value.Date = DateTime.Now;
            _repository.Add(value);
            return CreatedAtRoute("GetComment", new { id = value.InteractionId }, value);
        }
        #endregion

        #region Update Method

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Comment item)
        {
            if (item == null || item.InteractionId != id)
            {
                return BadRequest();
            }

            var Comment = _repository.Find(id);
            if (Comment == null)
            {
                return NotFound();
            }

            Comment.Date = DateTime.Now;
            Comment.InteractionId = item.InteractionId;
            Comment.Product = item.Product;
            Comment.ProductId = item.ProductId;
            Comment.Text = item.Text;
            Comment.User = item.User;
            Comment.UserId = item.UserId;

            _repository.Update(Comment);
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
