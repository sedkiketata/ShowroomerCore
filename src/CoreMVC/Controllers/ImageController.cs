using System;
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
    public class ImageController : Controller
    {
        private readonly IImageRepository _repository;
        private readonly IProductRepository _productRepository;

        #region Contructor
        public ImageController(IImageRepository repository, IProductRepository productRepository)
        {
            _repository = repository;
            _productRepository = productRepository;
        }
        #endregion

        #region GetAll Method
        // GET: api/values
        [Route("[action]")]
        [HttpGet]
        public IEnumerable<Image> GetAll()
        {
            List<Image> ListImages = new List<Image>();
            foreach (Image ImageOne in _repository.GetAll())
            {
                Image NewImage = new Image();
                NewImage = ImageOne;
                NewImage.Product = null;
                ListImages.Add(NewImage);
            }
            return ListImages;
        }
        #endregion

        #region Get Method
        // GET api/values/5
        [HttpGet(Name = "GetImages")]
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
            var CommentProduct = _productRepository.Find(item.ProductId);
            Product Product = new Product();
            Product = CommentProduct;
            Product.Images = null;
            Product.Interactions = null;
            Product.Orders = null;
            Product.Showrooms = null;
            item.Product = Product;

            return new ObjectResult(item);
        }
        #endregion

        #region Create Method
        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody] Image value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            _repository.Add(value);
            //   return CreatedAtRoute("GetImages", new { id = value.ImageId }, value);
            return new NoContentResult();
        }
        #endregion

        #region Update Method

        // PUT api/values/5
        [HttpPut]
        public IActionResult Update([FromBody] Image item)
        {
            StringValues hearderValues;
            var firstValue = string.Empty;
            if (Request.Headers.TryGetValue("id", out hearderValues))
                firstValue = hearderValues.FirstOrDefault();
            long id = Convert.ToInt64(firstValue);
            if (item == null || item.ImageId != id)
            {
                return BadRequest();
            }

            var images = _repository.Find(id);
            if (images == null)
            {
                return NotFound();
            }

            images.Name = item.Name;
            images.Url = item.Url;

            _repository.Update(images);
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
