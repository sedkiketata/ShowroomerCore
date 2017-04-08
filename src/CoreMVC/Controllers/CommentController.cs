using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreMVC.Models;
using CoreMVC.Infrastructure;
using Microsoft.Extensions.Primitives;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreMVC.Controllers
{
    [Route("api/[controller]")]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IVoucherRepository _voucherRepository;
        private readonly IRateRepository _rateRepository;

        #region Contructor
        public CommentController(ICommentRepository repository, IUserRepository userRepository, 
            IProductRepository productRepository,IVoucherRepository voucherRepository,
            IRateRepository rateRepository)
        {
            _repository = repository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _voucherRepository = voucherRepository;
            _rateRepository = rateRepository;
        }
        #endregion

        #region GetAll Method
        // GET: api/values
        [Route("[action]")]
        [HttpGet]
        public IEnumerable<Comment> GetAll()
        {
            List<Comment> ListComment = new List<Comment>();
            foreach (Comment CommentOne in _repository.GetAll())
            {
                Comment NewComment = new Comment();
                NewComment = CommentOne;
                NewComment.User = null;
                NewComment.Product = null;
                ListComment.Add(NewComment);
            }
            return ListComment;
        }
        #endregion

        #region Get Method
        // GET api/values/5
        [HttpGet( Name = "GetComment")]
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

            // -- Create the user object inside the Comment -- 
            var CommentUser = _userRepository.Find(item.UserId);

            User NewUser = new User();
            NewUser = CommentUser;
            NewUser.Interactions = null;
            NewUser.Orders = null;
            NewUser.Vouchers = null;
            item.User = NewUser;
            
            // -- End of Create the user object inside the Comment -- 

            #region Product List
            // -- Create the product object inside the Comment -- 
            var CommentProduct = _productRepository.Find(item.ProductId);
            Product Product = new Product();
            Product = CommentProduct;
            Product.Images = null;
            Product.Interactions = null;
            Product.Orders = null;
            Product.Showrooms = null; 
            item.Product = Product;

            // -- End of Create the product object inside the Comment --  
            #endregion

            // Unset variables that are unused
            NewUser = null;
            Product = null;
            CommentProduct = null;
            

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
        [HttpPut]
        public IActionResult Update([FromBody] Comment item)
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

            var Comment = _repository.Find(id);
            if (Comment == null)
            {
                return NotFound();
            }

            Comment = item;
            Comment.Date = DateTime.Now;
            Comment.Product = null;
            Comment.User = null;

            _repository.Update(Comment);
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
