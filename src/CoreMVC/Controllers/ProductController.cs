using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreMVC.Infrastructure;
using CoreMVC.Models;
using Microsoft.Extensions.Primitives;
using System.Reflection;

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
        private readonly IUserRepository _userRespository;

        #region Contructor
        public ProductController(IProductRepository repository, IInteractionRepository interactionRepository,
            IShowroomRepository showroomRepository, IImageRepository imageRepository,
            IUserRepository userRepository)
        {
            _repository = repository;
            _interactionRepository = interactionRepository;
            _showroomRepository = showroomRepository;
            _imageRepository = imageRepository;
            _userRespository = userRepository;
        } 
        #endregion

        #region GetAll Method
        // GET: api/values
        [Route("[action]")]
        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            List<Product> ListProduct = new List<Product>();
            foreach (Product ProductOne in _repository.GetAll())
            {
                Product NewProduct = new Product();
                NewProduct = ProductOne;
                NewProduct.Showrooms = null;
                NewProduct.Interactions = null;
                NewProduct.Orders = null;
                NewProduct.Images = null;
                ListProduct.Add(NewProduct);
            }
            return ListProduct;
        }
        #endregion

        #region GetAllWithRate Method
        // GET: api/values
        [Route("[action]")]
        [HttpGet]
        public IEnumerable<Product> GetAllWithRate()
        {
            List<Product> ListProductWithRate = new List<Product>();
            foreach (Product ProductOne in _repository.GetAll())
            {
                Product NewProduct = new Product();
                NewProduct = ProductOne;

                #region Rate List
                List<Interaction> RateList = new List<Interaction>();
                var RateQuery = from rate in _interactionRepository.GetAll()
                            where rate.ProductId == NewProduct.ProductId
                            select rate;
                foreach (Interaction rated in RateQuery)
                {
                    if (rated is Rate)
                    {
                        Interaction RateToAdd = new Interaction();
                        RateToAdd = rated;
                        RateList.Add(RateToAdd);
                    }
                }
                #endregion

                NewProduct.Interactions = RateList;
                NewProduct.Showrooms = null;
                //NewProduct.Interactions = null;
                NewProduct.Orders = null;
                NewProduct.Images = null;
                ListProductWithRate.Add(NewProduct);
            }
            return ListProductWithRate;
        }
        #endregion

        #region Get Method
        // GET api/values/5
        [HttpGet(Name = "GetProduct")]
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
                                where interaction.ProductId == item.ProductId
                                select interaction;
        foreach (var interaction in InteractionQuery)
        {
            Interaction UserRate = new Interaction();
            UserRate = interaction;
            UserRate.User = null;
            UserRate.Product = null;
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
            showrooms = showroom;
            showrooms.Product = null;
            showrooms.Showroomer = null;
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
            images = image;
            images.Product = null;
            ImagesList.Add(images);
        }
        item.Images = ImagesList;
        #endregion

        // Unset variables that are unused
        InteractionList = null;
        InteractionQuery = null;
        ShowroomsList = null;
        ShowroomQuery = null;
        ImagesList = null;
        ImageQuery = null;


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
            value.CreationDate = DateTime.Now;
            _repository.Add(value);
            //  return CreatedAtRoute("GetProduct", new { id = value.ProductId }, value);
            return new NoContentResult();
        } 
        #endregion

        #region Update Method

        // PUT api/values/5
        [HttpPut]
        public IActionResult Update([FromBody] Product item)
        {
            StringValues hearderValues;
            var firstValue = string.Empty;
            if (Request.Headers.TryGetValue("id", out hearderValues))
                firstValue = hearderValues.FirstOrDefault();
            long id = Convert.ToInt64(firstValue);
            if (item == null || item.ProductId != id )
            {
                return BadRequest();
            }
            var product = _repository.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            // Editing editable properties
            product.FacebookId = item.FacebookId;
            product.Brand = item.Brand;
            product.Category = item.Category;
            product.Discount = item.Discount;
            product.Name = item.Name;
            product.Price = item.Price;
            product.Quantity = item.Quantity;
            product.TVA = item.TVA;
            product.Description = item.Description;
            _repository.Update(product);
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
