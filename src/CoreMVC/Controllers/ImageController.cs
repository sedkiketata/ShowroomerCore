using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreMVC.Models;
using CoreMVC.Infrastructure;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreMVC.Controllers
{
    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        private readonly IImageRepository _repository;

        #region Contructor
        public ImageController(IImageRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region GetAll Method
        // GET: api/values
        [HttpGet]
        public IEnumerable<Image> GetAll()
        {
            return _repository.GetAll();
        }
        #endregion

        #region Get Method
        // GET api/values/5
        [HttpGet("{id}", Name = "GetImages")]
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
        public IActionResult Create([FromBody] Image value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            _repository.Add(value);
            return CreatedAtRoute("GetImages", new { id = value.ImageId }, value);
        }
        #endregion

        #region Update Method

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Image item)
        {
            if (item == null || item.ImageId != id)
            {
                return BadRequest();
            }

            var images = _repository.Find(id);
            if (images == null)
            {
                return NotFound();
            }

            images.ImageId = item.ImageId;
            images.Name = item.Name;
            images.ProductId = item.ProductId;
            images.Url = item.Url;

            _repository.Update(images);
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
