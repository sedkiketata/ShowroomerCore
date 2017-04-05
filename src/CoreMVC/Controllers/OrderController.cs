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
        #region GetAll Method
        // GET: api/values
        [HttpGet]
        public IEnumerable<Order> GetAll()
        {
            return _repository.GetAll();
        }
        #endregion

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
           
           var SelectedUser = _userRepository.Find(item.UserId);
            User User = new Models.User();

            // item.User = SelectedUser;
            User.UserId = SelectedUser.UserId;
            User.Username = SelectedUser.Username;
            User.City = SelectedUser.City;
            User.Street = SelectedUser.Street;
            User.ZipCode = SelectedUser.ZipCode;
            item.User = User;
            var SelectedProduct = _productRepository.Find(item.ProductId);
            Product Product = new Product();
            Product.ProductId = SelectedProduct.ProductId;
            Product.Name = SelectedProduct.Name;
            Product.Price = SelectedProduct.Price;
            Product.Quantity = SelectedProduct.Quantity;
            Product.TVA = SelectedProduct.TVA;
            Product.Brand = SelectedProduct.Brand;
            Product.Category = SelectedProduct.Category;
            Product.Discount = SelectedProduct.Discount;
            item.Product = Product;

            var SelectedPurchase = _purchaseRepository.Find(item.PurchaseId);
            Purchase purchase = new Purchase();
            purchase.PurchaseId = SelectedPurchase.PurchaseId;
            purchase.Status = SelectedPurchase.Status;
            purchase.Total = SelectedPurchase.Total;
            purchase.DatePurchase = SelectedPurchase.DatePurchase;
            item.Purchase = purchase;


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

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        #region Delete item from cart 
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            _repository.Remove(_repository.Find(id).OrderId);
        } 
        #endregion

        #region ClearAll Method
        // DELETE api/values/5
        [HttpDelete]
        public void Delete([FromBody] Order order)
        {
            _repository.ClearAll(order.UserId);
        } 
        #endregion
    }
}
