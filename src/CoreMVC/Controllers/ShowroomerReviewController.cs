using CoreMVC.Infrastructure;
using CoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Controllers
{
    [Route("api/[controller]")]
    public class ShowroomerReviewController : Controller
    {
        private readonly IShowroomerReviewRepository _repository ;
        private readonly IUserRepository _userRepository;
        private readonly IShowroomerRepository _showroomerRepository;
        public ShowroomerReviewController(IShowroomerReviewRepository repository , IUserRepository userrepository, IShowroomerRepository showroomerrepository)
        {
            _repository = repository;
            _userRepository = userrepository;
            _showroomerRepository = showroomerrepository;

        }
        #region GetAll Method
        // GET: api/values
        [Route("[action]")]
        [HttpGet]
        public IEnumerable<ShowroomerReview> GetAll()
        {
            StringValues hearderValues;
            var firstValue = string.Empty;
            if (Request.Headers.TryGetValue("id", out hearderValues))
                firstValue = hearderValues.FirstOrDefault();
            long id = Convert.ToInt64(firstValue);
            List<ShowroomerReview> ListRate = new List<ShowroomerReview>();
            foreach (ShowroomerReview shr in _repository.GetAllByShowroomer(id))
            {
                ShowroomerReview NewShr = new ShowroomerReview();
                NewShr = shr;
                NewShr.User = null;
                NewShr.Showroomer = null;
                ListRate.Add(NewShr);
            }
            return ListRate;
        }
        #endregion
        #region Create Method
        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody] ShowroomerReview value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            value.date = DateTime.Now;
            _repository.Add(value);
            
            return new NoContentResult();
        }
        #endregion
        #region Update Method

        // PUT api/values/5
        [HttpPut]
        public IActionResult Update([FromBody] ShowroomerReview item)
        {
            StringValues hearderValues;
            var firstValue = string.Empty;
            if (Request.Headers.TryGetValue("id", out hearderValues))
                firstValue = hearderValues.FirstOrDefault();
            long id = Convert.ToInt64(firstValue);
            if (item == null || item.ShowroomerReviewId != id)
            {
                return BadRequest();
            }

            var Review = _repository.Find(id);
            if (Review == null)
            {
                return NotFound();
            }

            Review.Mark = item.Mark;
            Review.Comment = item.Comment;

            _repository.Update(Review);
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
