using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreMVC.Infrastructure;
using CoreMVC.Models;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Primitives;

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

        #region GetAll Method
        // GET: api/values
        [Route("[action]")]
        [HttpGet]
        public IEnumerable<Order> GetAll()
        {
            List<Order> ListOrder = new List<Order>();
            foreach (Order OrderOne in _repository.GetAll())
            {
                Order NewOrder = new Order();
                NewOrder = OrderOne;
                NewOrder.User = null;
                NewOrder.Purchase = null;
                NewOrder.Product = null;
                ListOrder.Add(NewOrder);
            }
            return ListOrder;
        }
        #endregion

        #region GET Method

        // GET: api/values/5
        [HttpGet(Name = "GetOrder")]
        public IActionResult Get()
        {
            StringValues hearderValues;
            var firstValue = string.Empty;
            if (Request.Headers.TryGetValue("id", out hearderValues))
                firstValue = hearderValues.FirstOrDefault();
            long id = Convert.ToInt64(firstValue);
            var item = _repository.Find(id);
            if (item == null || item.UserId == null)
            {
                return NotFound();
            }
           
           var SelectedUser = _userRepository.Find(item.UserId);
            User User = new Models.User();

            // item.User = SelectedUser;
            User = SelectedUser;
            User.Interactions = null;
            User.Orders = null;
            User.Vouchers = null;
            item.User = User;

            var SelectedProduct = _productRepository.Find(item.ProductId);
            Product Product = new Product();
            Product = SelectedProduct;
            Product.Images = null;
            Product.Interactions = null;
            Product.Orders = null;
            Product.Showrooms = null;
            item.Product = Product;

            var SelectedPurchase = _purchaseRepository.Find(item.PurchaseId);
            Purchase purchase = new Purchase();
            purchase = SelectedPurchase;
            purchase.Orders = null;
            item.Purchase = purchase;

            // Unset variables that are unused
            SelectedUser = null;
            User = null;
            SelectedProduct = null;
            Product = null;
            SelectedPurchase = null;
            purchase = null;

            return new ObjectResult(item);
        }

        #endregion

        #region Create Method
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Variables variables)
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
            long idNewOrder = _repository.GetAll().Select(x => x.OrderId).Last();
            Order newOrder = _repository.Find(idNewOrder);
            return CreatedAtRoute("GetOrder", new { id = idNewOrder }, newOrder);

        } 
        #endregion

        #region Delete item from cart 
        // DELETE api/values/5
        [HttpDelete]
        public void Delete()
        {
            StringValues hearderValues;
            var firstValue = string.Empty;
            if (Request.Headers.TryGetValue("id", out hearderValues))
                firstValue = hearderValues.FirstOrDefault();
            long id = Convert.ToInt64(firstValue);
            _repository.Remove(_repository.Find(id).OrderId);
        } 
        #endregion

        #region ClearAll Method
        // DELETE api/values/5
        [Route("ClearAll")]
        [HttpDelete]
        public void Delete([FromBody] long id)
        {
            _repository.ClearAll(id);
        } 
        #endregion
    }
}
