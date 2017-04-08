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
    public class UserController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly IInteractionRepository _interactionRepository;
        private readonly IVoucherRepository _voucherRepository;
        private readonly IProductRepository _productRepository;

        #region Contructor
        public UserController(IUserRepository repository, IInteractionRepository interactionRepository
            , IVoucherRepository voucherRepository, IProductRepository productRepository)
        {
            _repository = repository;
            _interactionRepository = interactionRepository;
            _voucherRepository = voucherRepository;
            _productRepository = productRepository;
        }
        #endregion

        #region GetAll Method
        // GET: api/values
        [Route("[action]")]
        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            List<User> ListUser = new List<User>();
            foreach (User UserOne in _repository.GetAll())
            {
                User NewUser = new User();
                NewUser = UserOne;
                NewUser.Vouchers = null;
                NewUser.Orders = null;
                NewUser.Interactions = null;
                ListUser.Add(NewUser);
            }
            return ListUser;
        }
        #endregion

        #region Get Method
        // GET api/values/5
        [HttpGet(Name = "GetUser")]
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
                long? idNull = null;
                Interaction UserRate = new Interaction();
                UserRate = interaction;
                UserRate.InteractionId = (long)idNull;
                var InteractedProduct = _productRepository.Find(interaction.ProductId);
                UserRate.Product = InteractedProduct;
                UserRate.User = null;
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
                VoucherList.Add(v);
            }
            item.Vouchers = VoucherList;
            // End Vouchers List 
            #endregion

            // Unset variables that are unused
            InteractionList = null;
            InteractionQuery = null;
            VoucherList = null;
            Query = null;

            return new ObjectResult(item);
        }
        #endregion

        #region Create Method
        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody] User value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            _repository.Add(value);
            return CreatedAtRoute("GetUser", new { id = value.UserId }, value);
        }
        #endregion

        #region Update Method

        // PUT api/values/5
        [HttpPut]
        public IActionResult Update([FromBody] User item)
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

            var User = _repository.Find(id);
            if (User == null)
            {
                return NotFound();
            }

            User = item;
            User.Interactions = null;
            User.Orders = null;
            User.Vouchers = null;

            _repository.Update(User);
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
