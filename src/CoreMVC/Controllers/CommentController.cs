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

        [Route("[action]")]
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
            NewUser.UserId = CommentUser.UserId;
            NewUser.City = CommentUser.City;
            NewUser.Street = CommentUser.Street;
            NewUser.ZipCode = CommentUser.ZipCode;
            NewUser.Username = CommentUser.Username;

            #region  Interactions List
            // Rate list for the user that he commented for this product
            List<Interaction> RateUserList = new List<Interaction>();
            var InteractionQuery = from interaction in _rateRepository.GetAll()
                                   where interaction.UserId == CommentUser.UserId
                                   && interaction.ProductId == item.ProductId
                                   select interaction;
            foreach (var interaction in InteractionQuery)
            {
                Interaction UserRate = new Interaction();
                UserRate.InteractionId = interaction.InteractionId;
                UserRate.ProductId = interaction.ProductId;
                UserRate.UserId = interaction.UserId;
                RateUserList.Add(UserRate);
            }
            NewUser.Interactions = RateUserList;
            #endregion

            #region Vouchers List
            // Vouchers List
            var Query = from voucher in _voucherRepository.GetAll()
                        where voucher.UserId == CommentUser.UserId
                        select voucher;
            List<Voucher> VoucherList = new List<Voucher>();
            foreach (var voucher in Query)
            {
                Voucher v = new Voucher();
                v.Amount = voucher.Amount;
                v.Description = voucher.Description;
                v.Name = voucher.Name;
                v.Reference = voucher.Reference;
                v.UserId = voucher.UserId;
                v.VoucherId = voucher.VoucherId;
                VoucherList.Add(v);
            }
            NewUser.Vouchers = VoucherList;
            // End Vouchers List 
            #endregion

            item.User = NewUser;

            // -- End of Create the user object inside the Comment -- 

            #region Product List
            // -- Create the product object inside the Comment -- 
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

            // -- End of Create the product object inside the Comment --  
            #endregion

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

            Comment.Date = DateTime.Now;
            Comment.InteractionId = item.InteractionId;
            Comment.ProductId = item.ProductId;
            Comment.Text = item.Text;
            Comment.UserId = item.UserId;

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
