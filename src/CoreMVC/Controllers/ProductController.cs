using CoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Controllers
{
    public class ProductController : Controller
    {
        private Context _context;
        public ProductController(Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Create(Product p)
        {

            try
            {
                _context.Products.Add(p);
                await _context.SaveChangesAsync();

            }
            catch
            {

                ModelState.AddModelError("", "unable to add Product"
                );

            }
            return View();

        }
        public async Task<IActionResult> Show()
        {
            return View(await _context.Products.ToListAsync());
        }

    }
}
