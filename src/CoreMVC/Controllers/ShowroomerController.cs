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
    public class ShowroomerController : Controller
    {
        private readonly IShowroomerRepository _repository;

        #region Contructor
        public ShowroomerController(IShowroomerRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region GetAll Method
        // GET: api/values
        [HttpGet]
        public IEnumerable<Showroomer> GetAll()
        {
            return (IEnumerable<Showroomer>)_repository.GetAll();
        }
        #endregion

        #region Get Method
        // GET api/values/5
        [HttpGet("{id}", Name = "GetShowroomer")]
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
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Showroomer item)
        {
            if (item == null || item.UserId != id)
            {
                return BadRequest();
            }

            var Showroomer = _repository.Find(id);
            if (Showroomer == null)
            {
                return NotFound();
            }

            Showroomer.Username = item.Username;
            Showroomer.Street = item.Street;
            Showroomer.ZipCode = item.ZipCode;
            Showroomer.City = item.City;
            Showroomer.Description = item.Description;
            Showroomer.Latitude = item.Latitude;
            Showroomer.Longitude = item.Longitude;

            _repository.Update(Showroomer);
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
