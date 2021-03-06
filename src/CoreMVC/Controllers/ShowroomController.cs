﻿using System;
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
    public class ShowroomController : Controller
    {

        private readonly IShowroomRepository _repository;
        private readonly IShowroomerRepository _showroomerRepository;
        private readonly IProductRepository _productRepository;

        #region Contructor
        public ShowroomController(IShowroomRepository repository, IShowroomerRepository showroomerRepository,
            IProductRepository productRepository)
        {
            _repository = repository;
            _showroomerRepository = showroomerRepository;
            _productRepository = productRepository;
        }
        #endregion

        #region GetAll Method
        // GET: api/values
        [Route("[action]")]
        [HttpGet]
        public IEnumerable<Showroom> GetAll()
        {
            List<Showroom> Showrooms = new List<Showroom>();
            foreach (Showroom OneShowroom in _repository.GetAll())
            {
                Showroom NewShowroom = new Showroom();
                NewShowroom = OneShowroom;
                NewShowroom.Showroomer = null;
                NewShowroom.Product = null;
                Showrooms.Add(NewShowroom);
            }
            return Showrooms;
        }
        #endregion

        #region Get Method
        // GET api/values/5
        [HttpGet(Name = "GetShowroom")]
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

            var SelectedUser = _showroomerRepository.Find(item.ShowroomerId);
            Showroomer showroomer = new Showroomer();
            showroomer = SelectedUser;
            showroomer.Showrooms = null;
            showroomer.Orders = null;
            showroomer.Vouchers = null;
            showroomer.Interactions = null;

            item.Showroomer = showroomer;

            var SelectedProduct = _productRepository.Find(item.ProductId);
            Product Product = new Product();
            Product = SelectedProduct;
            Product.Showrooms = null;
            Product.Interactions = null;
            Product.Orders = null;
            Product.Images = null;
            item.Product = Product;

            

            // Unset variables that are unused
            SelectedUser = null;
            showroomer = null;
            SelectedProduct = null;
            Product = null;

            return new ObjectResult(item);
        }
        #endregion



        #region Create Method
        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody] Showroom value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            value.Product = null;
            value.Showroomer = null;
            _repository.Add(value);
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
