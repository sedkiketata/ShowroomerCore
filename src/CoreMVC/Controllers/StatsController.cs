using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using CoreMVC.ApiModels;
using CoreMVC.Models;
using CoreMVC.Infrastructure;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreMVC.Controllers
{
    [Route("api/[controller]")]
    public class StatsController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        public StatsController(IOrderRepository orderRepository, IPurchaseRepository purchaseRepository,
            IProductRepository productRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _purchaseRepository = purchaseRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        // GET: api/values
        [Route("[action]")]
        [HttpGet]
        public IEnumerable<DonutEntity> GetCompanyRevenuePerRegion()
        {
            StringValues headersValues;
            string firstValue = string.Empty;
            if (Request.Headers.TryGetValue("company", out headersValues))
                firstValue = headersValues.FirstOrDefault().Trim();
            string CompanySelected = char.ToUpper(firstValue[0]) + firstValue.Substring(1);

            // List that should be returned and the fun of this method
            List<DonutEntity> ListeDonut = new List<DonutEntity>();

            // Total Revenue for a company
            double TotalRevenue = 0;

            // A dictionnary that contains the total revenue for each region
            Dictionary<string,double> RevenueByRegion = new Dictionary<string, double>(); 
            
            // Here we are going to calculate the TotalRevenue and the RevenueByRegion
            foreach (Order OneOrder in _orderRepository.GetAll())
            {
                Product OrderedProduct = _productRepository.Find(OneOrder.ProductId);
                Purchase Purchased = _purchaseRepository.Find(OneOrder.PurchaseId);
                User Buyer = _userRepository.Find(OneOrder.UserId);
                if (OrderedProduct.Owner == CompanySelected && Purchased.Status == "Order")
                {
                    TotalRevenue += Purchased.Total;
                    string Region = string.Empty;
                    string City = Buyer.City.Trim();
                    Region = char.ToUpper(City[0]) + City.Substring(1);
                    if (RevenueByRegion.ContainsKey(Region))
                    {
                        RevenueByRegion[Region] = RevenueByRegion[Region] + Purchased.Total;
                    }
                    else
                    {
                        RevenueByRegion.Add(Region, Purchased.Total);
                    }
                }

            }

            // Here we are going to calculate the percentage of Revenue of each region

            foreach (KeyValuePair<string,double> entry in RevenueByRegion)
            {
                DonutEntity RegionDonut = new DonutEntity();
                double PercentageOfRevenueByRegion = (entry.Value * 100) / TotalRevenue;
                RegionDonut.Label = entry.Key;
                RegionDonut.Value = Math.Round(PercentageOfRevenueByRegion,2);
                ListeDonut.Add(RegionDonut);
            }

            return ListeDonut;
        }

        

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
