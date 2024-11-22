using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services;
using BLL.Models;
using BLL.Services.Bases;
using BLL.DAL;

// Generated from Custom Template.

namespace PhotoGalleryApp.Controllers
{
    public class PhotosController : MvcController
    {
        // Service injections:
        private readonly IService<Photo, PhotoModel> _photoService;
        private readonly IPhotoTypesService _photoTypesService;

        /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
        //private readonly IManyToManyRecordService _ManyToManyRecordService;

        public PhotosController(
             IService<Photo, PhotoModel> photoService
            , IPhotoTypesService photoTypesService

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //, IManyToManyRecordService ManyToManyRecordService
        )
        {
            _photoService = photoService;
            _photoTypesService = photoTypesService;
            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //_ManyToManyRecordService = ManyToManyRecordService;
        }

        // GET: Photos
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _photoService.Query().ToList();
            return View(list);
        }

        // GET: Photos/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _photoService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            ViewData["PhotoTypesID"] = new SelectList(_photoTypesService.Query().ToList(), "Record.Id", "Title");
            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //ViewBag.ManyToManyRecordIds = new MultiSelectList(_ManyToManyRecordService.Query().ToList(), "Record.Id", "Name");
        }

        // GET: Photos/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Photos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PhotoModel photo)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _photoService.Create(photo.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = photo.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(photo);
        }

        // GET: Photos/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _photoService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: Photos/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PhotoModel photo)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _photoService.Update(photo.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = photo.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(photo);
        }

        // GET: Photos/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _photoService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: Photos/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _photoService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
