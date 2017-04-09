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
    public class ShowroomerController : Controller
    {
        private readonly IShowroomerRepository _repository;
        private readonly IInteractionRepository _interactionRepository;
        private readonly IVoucherRepository _voucherRepository;
        private readonly IShowroomRepository _showroomRepository;
        private readonly IProductRepository _productRepository;

        #region Contructor
        public ShowroomerController(IShowroomerRepository repository, IVoucherRepository voucherRepository,
            IInteractionRepository interactionRepository, IShowroomRepository showroomRepository,
            IProductRepository productRepository)
        {
            _repository = repository;
            _voucherRepository = voucherRepository;
            _interactionRepository = interactionRepository;
            _showroomRepository = showroomRepository;
            _productRepository = productRepository;
        }
        #endregion

        #region GetAll Method
        // GET: api/values
        [Route("[action]")]
        [HttpGet]
        public IEnumerable<Showroomer> GetAll()
        {
            List<Showroomer> ListShowroomer = new List<Showroomer>();
            foreach (Showroomer ShowroomerOne in _repository.GetAll())
            {
                Showroomer NewShowroomer = new Showroomer();
                NewShowroomer = ShowroomerOne;
                NewShowroomer.Vouchers = null;
                NewShowroomer.Showrooms = null;
                NewShowroomer.Orders = null;
                NewShowroomer.Interactions = null;
                ListShowroomer.Add(NewShowroomer);
            }
            return ListShowroomer;
        }
        #endregion

        #region Get Method
        // GET api/values/5
        [HttpGet(Name = "GetShowroomer")]
        public IActionResult Get()
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

            #region  Interactions List
            // Rate list for the user that he commented for this product
            List<Interaction> InteractionList = new List<Interaction>();
            var InteractionQuery = from interaction in _interactionRepository.GetAll()
                                   where interaction.UserId == item.UserId
                                   select interaction;
            foreach (var interaction in InteractionQuery)
            {
                Interaction UserRate = new Interaction();
                UserRate = interaction;
                UserRate.User = null;
                UserRate.Product = null;
                InteractionList.Add(UserRate);
            }
            item.Interactions = InteractionList;
            #endregion

            #region Vouchers List
            // Vouchers List
            var Query = from voucher in _voucherRepository.GetAll()
                        where voucher.UserId == item.UserId
                        select voucher;
            List<Voucher> VoucherList = new List<Voucher>();
            foreach (var voucher in Query)
            {
                Voucher v = new Voucher();
                v = voucher;
                v.User = null;
                VoucherList.Add(v);
            }
            item.Vouchers = VoucherList;
            // End Vouchers List 
            #endregion

            #region  Showrooms List
            // Rate list for the user that he commented for this product
            List<Showroom> ShowroomList = new List<Showroom>();
            var ShowroomQuery = from showroom in _showroomRepository.GetAll()
                                   where showroom.ShowroomerId == item.UserId
                                   select showroom;
            foreach (var showroom in ShowroomQuery)
            {
                Showroom UserShowroom  = new Showroom();
                UserShowroom = showroom;

                // Product that this user accept to be showroom
                var ProductQuery = from product in _productRepository.GetAll()
                                   where product.ProductId == showroom.ProductId
                                   select product;
                if (ProductQuery == null)
                    UserShowroom.Product = null;
                else
                {
                    Product NewProduct = new Product();
                    var OldProduct = ProductQuery.First();
                    NewProduct = OldProduct;
                    NewProduct.Images = null;
                    NewProduct.Interactions = null;
                    NewProduct.Orders = null;
                    NewProduct.Showrooms = null;
                    UserShowroom.Product = NewProduct;
                }
                UserShowroom.Showroomer = null;
                ShowroomList.Add(UserShowroom);
            }
            item.Showrooms = ShowroomList;
            #endregion

            // Unset variables that are unused
            InteractionList = null;
            InteractionQuery = null;
            VoucherList = null;
            Query = null;
            ShowroomList = null;
            ShowroomQuery = null;

            return new ObjectResult(item);
        }
        #endregion

        #region Create Method
        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody] Showroomer value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            _repository.Add(value);
            return CreatedAtRoute("GetShowroomer", new { id = value.UserId }, value);
        }
        #endregion

        #region Update Method

        // PUT api/values/5
        [HttpPut]
        public IActionResult Update([FromBody] Showroomer item)
        {
            StringValues hearderValues;
            var firstValue = string.Empty;
            if (Request.Headers.TryGetValue("id", out hearderValues))
                firstValue = hearderValues.FirstOrDefault();
            long id = Convert.ToInt64(firstValue);
            if (item == null || item.UserId != id)
            {
                return BadRequest();
            }

            var Showroomer = _repository.Find(id);
            if (Showroomer == null)
            {
                return NotFound();
            }

            Showroomer.City = item.City;
            Showroomer.Description = item.Description;
            Showroomer.Latitude = item.Latitude;
            Showroomer.Longitude = item.Longitude;
            Showroomer.Street = item.Street;
            Showroomer.Username = item.Username;
            Showroomer.ZipCode = item.ZipCode;

            _repository.Update(Showroomer);
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
