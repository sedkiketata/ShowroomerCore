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
    public class BuyerController : Controller
    {
        private readonly IBuyerRepository _repository;
        private readonly IInteractionRepository _interactionRepository;

        #region Contructor
        public BuyerController(IBuyerRepository repository, IInteractionRepository interactionRepository)
        {
            _repository = repository;
            _interactionRepository = interactionRepository;
        }
        #endregion

        #region GetAll Method
        // GET: api/values
        [Route("[action]")]
        [HttpGet]
        public IEnumerable<Buyer> GetAll()
        {
            List<Buyer> ListBuyer = new List<Buyer>();
            foreach (Buyer OneBuyer in _repository.GetAll())
            {
                Buyer NewBuyer = new Buyer();
                NewBuyer = OneBuyer;
                NewBuyer.Vouchers = null;
                NewBuyer.Orders = null;
                NewBuyer.Interactions = null;
                ListBuyer.Add(NewBuyer);
            }
            return ListBuyer;
        }
        #endregion

        #region Get Method
        // GET api/values/5
        [HttpGet(Name = "GetBuyer")]
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
                UserRate.UserId = (long)idNull;
                UserRate.Product = null;
                UserRate.User = null;
                InteractionList.Add(UserRate);
            }
            item.Interactions = InteractionList;
            #endregion

            // Unset variables that are unused
            InteractionList = null;
            InteractionQuery = null;

            return new ObjectResult(item);
        }
        #endregion

        #region Create Method
        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody] Buyer value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            _repository.Add(value);
            return CreatedAtRoute("GetBuyer", new { id = value.UserId }, value);
        }
        #endregion

        #region Update Method

        // PUT api/values/5
        [HttpPut]
        public IActionResult Update([FromBody] Buyer item)
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

            var Buyer = _repository.Find(id);
            if (Buyer == null)
            {
                return NotFound();
            }

            Buyer = item;
            Buyer.Interactions = null;
            Buyer.Orders = null;
            Buyer.Vouchers = null;

            _repository.Update(Buyer);
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
