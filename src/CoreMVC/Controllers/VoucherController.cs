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
    public class VoucherController : Controller
    {
        private readonly IVoucherRepository _repository;
        private readonly IUserRepository _userRepository;

        #region Construtor
        public VoucherController(IVoucherRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }
        #endregion

        #region GetAll Method

        // GET: api/values
        [Route("[action]")]
        [HttpGet]
        public IEnumerable<Voucher> GetAll()
        {
            List<Voucher> ListVoucher = new List<Voucher>();
            foreach (Voucher VoucherOne in _repository.GetAll())
            {
                Voucher NewVoucher = new Voucher();
                NewVoucher = VoucherOne;
                NewVoucher.User = null;
                ListVoucher.Add(NewVoucher);
            }
            return ListVoucher;
        }

        #endregion

        #region GET Method

        // GET: api/values/5
        [HttpGet(Name = "GetVoucher")]
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

            User SelectedUser = new Models.User();
            SelectedUser = _userRepository.Find(item.UserId);
            SelectedUser.Interactions = null;
            SelectedUser.Orders = null;
            SelectedUser.Vouchers = null;
            item.User = SelectedUser;

            // Unset variables that are unused
            SelectedUser = null;

            return new ObjectResult(item);
        }

        #endregion

        #region Create Method

        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody] Voucher voucher)
        {
            if (voucher == null || voucher.UserId == null)
            {
                return BadRequest();
            }
           
            _repository.Add(voucher);
            return CreatedAtRoute("GetVoucher", new { id = voucher.VoucherId }, voucher);
        }
        #endregion

        #region Update Method

        // PUT api/values/5
        [HttpPut]
        public IActionResult Update([FromBody] Voucher voucher)
        {
            StringValues hearderValues;
            var firstValue = string.Empty;
            if (Request.Headers.TryGetValue("id", out hearderValues))
                firstValue = hearderValues.FirstOrDefault();
            long id = Convert.ToInt64(firstValue);
            if (voucher == null || voucher.VoucherId != id)
            {
                return BadRequest();
            }

            var v = _repository.Find(id);
            if (v == null)
            {
                return NotFound();
            }

            v.Amount = voucher.Amount;
            v.Description = voucher.Description;
            v.Name = voucher.Name;
       
            _repository.Update(v);
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
