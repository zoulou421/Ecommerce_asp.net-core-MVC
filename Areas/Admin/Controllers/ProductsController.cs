using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using IHostingEnvironment= Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using IHostingEnvironment =  Microsoft.AspNetCore.Hosting.IWebHostEnvironment;

using OnlineShop.Data;
using OnlineShop.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


using Microsoft.AspNetCore.Authorization;

namespace OnlineShop.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize]
    public class ProductsController : Controller
    {
        private ApplicationDbContext _db;
        private IHostingEnvironment _he;
        public ProductsController(ApplicationDbContext db, IHostingEnvironment he)
        {
            _db = db;
            _he = he;
        }
        public IActionResult Index()
        {
            return View(_db.Products.Include(c=>c.ProductTypes).Include(f=>f.TagNames).ToList());
        }
        //Post Index action Method
        [HttpPost]
        public IActionResult Index(int? lowAmount, int? largeAmount)
        {
            var products = _db.Products.Include(c => c.ProductTypes).Include(c => c.TagNames)
                .Where(c => c.Price >= lowAmount && c.Price <= largeAmount).ToList();
            if(lowAmount==null || largeAmount == null)
            {
                products = _db.Products.Include(c => c.ProductTypes).Include(c => c.TagNames).ToList();
            }
            return View(products);
        }

        //Get Create Method
        public IActionResult Create()
        {
            ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["tagNameId"] = new SelectList(_db.TagNames.ToList(), "Id", "TagName");
            return View();
        }
        //Post Create Method
        [HttpPost]
        public async Task<IActionResult> Create(Products products, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var searchProduct = _db.Products.FirstOrDefault(c => c.Name == products.Name);
                if (searchProduct != null)
                {
                    ViewBag.message = "This Product already exists";
                    ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
                    ViewData["tagNameId"] = new SelectList(_db.TagNames.ToList(), "Id", "TagName");
                    return View(products);
                }
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    products.Image = "/Images/" + image.FileName;

                }
                if (image == null)
                {
                    products.Image = "/Images/noim.jpg";
                }

                _db.Products.Add(products);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        //Get Edit Method
        public ActionResult Edit(int? id)
        {
            ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["tagNameId"] = new SelectList(_db.TagNames.ToList(), "Id", "TagName");
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products.Include(c => c.ProductTypes).Include(c => c.TagNames).
                FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //Postt Edit Method
        [HttpPost]
        public async Task<IActionResult> Edit(Products products, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    products.Image = "/Images/" + image.FileName;

                }
                if (image == null)
                {
                    products.Image = "/Images/noim.jpg";
                }

                _db.Products.Update(products);
                await _db.SaveChangesAsync();
                TempData["Edit"] = "successfully edited!";
                return RedirectToAction(nameof(Index));
            }
            return View(products);

        }

        //Get Detail Action Method

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products.Include(c => c.ProductTypes).Include(c => c.TagNames)
                .FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //Get Delete Action Method
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products.Include(c => c.TagNames).Include(c => c.ProductTypes)
                .Where(c => c.Id == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        //Post Delete Action Method
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _db.Products.FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
