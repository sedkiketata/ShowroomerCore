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
    public class ProductController : Controller
    {
            private readonly IProductRepository _repository;
            private readonly IInteractionRepository _interactionRepository;
            private readonly IShowroomRepository _showroomRepository;
            private readonly IImageRepository _imageRepository;

            #region Contructor
            public ProductController(IProductRepository repository, IInteractionRepository interactionRepository,
                IShowroomRepository showroomRepository, IImageRepository imageRepository)
            {
                _repository = repository;
                _interactionRepository = interactionRepository;
                _showroomRepository = showroomRepository;
                _imageRepository = imageRepository;
            } 
            #endregion

            #region GetAll Method
            // GET: api/values
            [HttpGet]
            public IEnumerable<Product> GetAll()
            {
                return _repository.GetAll();
            } 
            #endregion

            #region Get Method
            // GET api/values/5
            [HttpGet("{id}", Name = "GetProduct")]
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
                                   where interaction.ProductId == item.ProductId
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

            #region Showrooms List
            List<Showroom> ShowroomsList = new List<Showroom>();
            var ShowroomQuery = from showroom in _showroomRepository.GetAll()
                                   where showroom.ProductId == item.ProductId
                                   select showroom;
            foreach (var showroom in ShowroomQuery)
            {
                Showroom showrooms = new Showroom();
                showrooms.ShowroomerId = showroom.ShowroomerId;
                showrooms.ProductId = showroom.ProductId;
                showrooms.ShowroomId = showroom.ShowroomId;
                ShowroomsList.Add(showrooms);
            }
            item.Showrooms = ShowroomsList;
            #endregion

            #region Images List
            List<Image> ImagesList = new List<Image>();
            var ImageQuery = from image in _imageRepository.GetAll()
                             where image.ProductId == item.ProductId
                             select image;
            foreach (var image in ImageQuery)
            {
                Image images = new Image();
                images.ImageId = image.ImageId;
                images.Name = image.Name;
                images.ProductId = image.ProductId;
                images.Url = image.Url;
                ImagesList.Add(images);
            }
            item.Images = ImagesList;
            #endregion

            return new ObjectResult(item);
            } 
            #endregion

            #region Create Method
            // POST api/values
            [HttpPost]
            public IActionResult Create([FromBody] Product value)
            {
                if (value == null)
                {
                    return BadRequest();
                }
                _repository.Add(value);
                return CreatedAtRoute("GetProduct", new { id = value.ProductId }, value);
            } 
            #endregion

            #region Update Method

            // PUT api/values/5
            [HttpPut("{id}")]
            public IActionResult Update(long id, [FromBody] Product item)
            {
                if (item == null || item.ProductId != id)
                {
                    return BadRequest();
                }

                var product = _repository.Find(id);
                if (product == null)
                {
                    return NotFound();
                }

                product.Brand = item.Brand;
                product.Category = item.Category;
                product.Discount = item.Discount;
                product.Name = item.Name;
                product.Price = item.Price;
                product.Quantity = item.Quantity;
                product.TVA = item.TVA;

                _repository.Update(product);
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
