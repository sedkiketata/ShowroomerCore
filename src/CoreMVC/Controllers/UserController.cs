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
    public class UserController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly IInteractionRepository _interactionRepository;
        private readonly IVoucherRepository _voucherRepository;

        #region Contructor
        public UserController(IUserRepository repository, IInteractionRepository interactionRepository
            , IVoucherRepository voucherRepository)
        {
            _repository = repository;
            _interactionRepository = interactionRepository;
            _voucherRepository = voucherRepository;
        }
        #endregion

        #region GetAll Method
        // GET: api/values
        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            return _repository.GetAll();
        }
        #endregion

        #region Get Method
        // GET api/values/5
        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult Get(int id)
        {
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
                UserRate.InteractionId = interaction.InteractionId;
                UserRate.ProductId = interaction.ProductId;
                UserRate.UserId = interaction.UserId;
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
                v.Amount = voucher.Amount;
                v.Description = voucher.Description;
                v.Name = voucher.Name;
                v.Reference = voucher.Reference;
                v.UserId = voucher.UserId;
                v.VoucherId = voucher.VoucherId;
                VoucherList.Add(v);
            }
            item.Vouchers = VoucherList;
            // End Vouchers List 
            #endregion

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
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] User item)
        {
            if (item == null || item.UserId != id)
            {
                return BadRequest();
            }

            var User = _repository.Find(id);
            if (User == null)
            {
                return NotFound();
            }

            User.Username = item.Username;
            User.Street = item.Street;
            User.ZipCode = item.ZipCode;
            User.City = item.City;

            _repository.Update(User);
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
