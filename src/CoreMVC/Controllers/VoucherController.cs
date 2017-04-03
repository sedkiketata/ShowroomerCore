﻿using System;
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
    public class VoucherController : Controller
    {
        private readonly IVoucherRepository _repository;

        #region Construtor
        public VoucherController(IVoucherRepository repository)
        {
            _repository = repository;

        }
        #endregion

        #region GetAll Method
        // GET: api/values
        [HttpGet]
        public IEnumerable<Voucher> GetAll()
        {
            return _repository.GetAll();
        }
        #endregion

        // GET: api/values/5
        #region GET Method
        [HttpGet("{id}", Name = "GetVoucher")]
        public IActionResult Get(int id)
        {
            var item = _repository.Find(id);
            if (item == null)
            {
                return NotFound();
            }
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
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Voucher voucher)
        {
            if (voucher == null || voucher.VoucherId != id)
            {
                return BadRequest();
            }

            var v = _repository.Find(id);
            if (v == null)
            {
                return NotFound();
            }

            v.Reference = voucher.Reference;
            v.Name = voucher.Name;
            v.Description = voucher.Description;
            v.Amount = voucher.Amount;
            v.UserId = voucher.UserId;
       
            _repository.Update(v);
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