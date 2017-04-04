using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreMVC.Infrastructure;
using CoreMVC.Models;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreMVC.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        #region Constructor
        public OrderController(IOrderRepository repository , IUserRepository userRepositoy , IPurchaseRepository purchaseRepository , IProductRepository productRepository)
        {
            _repository = repository;
            _productRepository = productRepository;
            _purchaseRepository = purchaseRepository;
            _userRepository = userRepositoy;
        } 
        #endregion
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        #region GET Method

        // GET: api/values/5
        [HttpGet("{id}", Name = "GetOrder")]
        public IActionResult Get(long id)
        {
            var item = _repository.Find(id);
            if (item == null || item.UserId == null)
            {
                return NotFound();
            }
            //User SelectedUser = new Models.User();
            //SelectedUser.Username = _userRepository.Find(item.UserId).Username;
            //SelectedUser.City = _userRepository.Find(item.UserId).City;
            //SelectedUser.Street = _userRepository.Find(item.UserId).Street;
            //SelectedUser.ZipCode = _userRepository.Find(item.UserId).ZipCode;
            //item.User = SelectedUser;
            //var CommentProduct = _productRepository.Find(item.ProductId);
            //Product Product = new Product();
            //Product.ProductId = CommentProduct.ProductId;
            //Product.Name = CommentProduct.Name;
            //Product.Price = CommentProduct.Price;
            //Product.Quantity = CommentProduct.Quantity;
            //Product.TVA = CommentProduct.TVA;
            //Product.Brand = CommentProduct.Brand;
            //Product.Category = CommentProduct.Category;
            //Product.Discount = CommentProduct.Discount;
            //item.Product = Product;
            return new ObjectResult(item);
        }

        #endregion

        // POST api/values
        [HttpPost]
        public IActionResult Post( [FromBody] Variables variables)
        {
            long userId = variables.UserId;
            long productId = variables.ProductId;
            int qte = variables.Quantity;
            //var qte = variables["qte"].ToObject<int>();
            if (userId == null || productId == null || qte == 0)
            {
                return BadRequest();
            }
            _repository.AddtoCart(userId, productId, qte);
            long idNewOrder = _repository.GetAll().Select(x => x.OrderId).Max();
            Order newOrder = _repository.Find(idNewOrder);
            return CreatedAtRoute("GetOrder", new { id = idNewOrder }, newOrder);

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
