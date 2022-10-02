using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class TagNamesController : Controller
    {
        private ApplicationDbContext _db;
        public TagNamesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<TagNames> objTagNameList = _db.TagNames;

            return View(objTagNameList);
        }

        //Create Get Action method
        public ActionResult Create()
        {
            return View();
        }

        //Create Post Action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TagNames tagNames)
        {
            if (ModelState.IsValid)
            {
                _db.TagNames.Add(tagNames);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tagNames);
        }

        //Edit Get Action method
        public ActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var tagName = _db.TagNames.Find(id);
            if (tagName == null)
            {
                return NotFound();
            }
            return View(tagName);
        }

        //Edit Post Action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TagNames tagNames)
        {
            if (ModelState.IsValid)
            {
                _db.Update(tagNames);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tagNames);
        }

        //Details Get Action method
        public ActionResult Details(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var tagName = _db.TagNames.Find(id);
            if (tagName == null)
            {
                return NotFound();
            }
            return View(tagName);
        }

        //Details Post Action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(TagNames tagNames)
        {
            return RedirectToAction(nameof(Index));
        }

        //Delete Get Action method
        public ActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var tagName = _db.TagNames.Find(id);
            if (tagName == null)
            {
                return NotFound();
            }
            return View(tagName);
        }

        //Delete Post Action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, TagNames tagNames)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            if (id != tagNames.Id)
            {
                return NotFound();
            }
            var tagName = _db.TagNames.Find(id);
            if (tagName == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Remove(tagName);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tagNames);
        }
    }
}
