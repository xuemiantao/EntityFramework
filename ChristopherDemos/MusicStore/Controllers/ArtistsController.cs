using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using MusicStore.Models;
using MusicStore.Models.Repositories;

namespace MusicStore.Controllers
{
    public class ArtistsController : Controller
    {
        //MusicStoreDataContext context = new MusicStoreDataContext();
		ArtistRepository repository = new ArtistRepository();
		
		public ActionResult Details(int id)
		{
			Artist artist = repository.Get(id);
			if (artist == null) {
				return HttpNotFound();
			} else {
				return View(artist);
			}
		}

		// GET: Artists
        public ActionResult Index()
        {
			return View(repository.GetAll());
        }

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost()]
		public ActionResult Create(Artist artist)
		{
			if(!ModelState.IsValid) return View(artist);
			
			repository.Add(artist);
			repository.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult Edit(int id)
		{
			var artist = repository.Get(id);
			if(artist == null) return HttpNotFound();
			else return View(artist);
		}

		[HttpPost()]
		public ActionResult Edit(Artist artist)
		{
			if (!ModelState.IsValid) return View(artist);

			try {
				using (TransactionScope scope = new TransactionScope()) {
					repository.Update(artist);
					repository.SaveChanges();
					scope.Complete();
				} 
				return RedirectToAction("Index");
			} catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException) {
				ViewBag.Message = "Sorry, that didn't work!<br />Adam beat you to the punch";
				return View(artist);
			}
		}

		protected override void Dispose(bool disposing)
		{
			repository.Dispose();
			base.Dispose(disposing);
		}
    }
}