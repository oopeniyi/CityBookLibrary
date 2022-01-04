using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository;
using Contracts;
using Microsoft.Extensions.Configuration;
using Entities.DataOperator.Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;

namespace CityBookLibrary.Controllers
{
    [Authorize]
    public class MusicDiskController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly IXMLDeserializer _deserializer;
        private readonly IXMLSerializer _serializer;
        private readonly IXMLPath _xmlPath;
        private ICacheManager<RepositoryWrapper> _cacheManager;

        public MusicDiskController(IConfiguration configuration,          
           IXMLDeserializer deserializer,
           IXMLSerializer serializer,
           ICacheManager<RepositoryWrapper> cacheManager,
           IXMLPath xmlPath)
        {

            _configuration = configuration;           
            _deserializer = deserializer;
            _serializer = serializer;
            _xmlPath = xmlPath;

            _xmlPath.SetXMLPath(_configuration["XmlFilePaths:AudioXML"]);
            _cacheManager = cacheManager;
           
        }

       
        public ActionResult Index()
        {
            var musicDiskCollection = _cacheManager.GetCachedItem().MusicDisks.FindAll();
            return View(musicDiskCollection);
        }

        
        public ActionResult Details(string id)
        {
            var musicDisk = _cacheManager.GetCachedItem().MusicDisks.FindBy(x => x.ID == id).FirstOrDefault();
            return View(musicDisk);
        }

        
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {

            MusicDisk musicDisk = new()
            {
                Artist = collection["Artist"],
                ID = collection["ID"],
                Description = collection["Description"],
                Genere = collection["Genere"],
                Price = Convert.ToDecimal(collection["Price"]),
                ReleaseDate = Convert.ToDateTime(collection["ReleaseDate"]),
                Title = collection["Title"]
            };

            var existingMusicDisk = _cacheManager.GetCachedItem().MusicDisks.FindBy(x => x.ID == musicDisk.ID);

            if (existingMusicDisk.Any())
            {
                ViewBag.ErrorMessage = "Music Disk already exist with same ID";
                return View();
            }

            _cacheManager.GetCachedItem().MusicDisks.Create(musicDisk);

            _cacheManager.GetCachedItem().MusicDisks.SaveChanges();

            return RedirectToAction(nameof(Index));

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string artist)
        {
            try
            {
                var musicDisk = _cacheManager.GetCachedItem().MusicDisks.FindByArtist(x => x.Artist.ToLower().Contains(artist.ToLower()));
                return View(musicDisk);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        
        public ActionResult Edit(string id)
        {
            var musicDisk = _cacheManager.GetCachedItem().MusicDisks.FindBy(x => x.ID == id).FirstOrDefault();

            return View(musicDisk);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
        {
            try
            {

                MusicDisk musicDisk = new()
                {
                    Artist = collection["Artist"],
                    ID = collection["ID"],
                    Description = collection["Description"],
                    Genere = collection["Genere"],
                    Price = Convert.ToDecimal(collection["Price"]),
                    ReleaseDate = Convert.ToDateTime(collection["ReleaseDate"]),
                    Title = collection["Title"]
                };

                var oldmusicDisk = _cacheManager.GetCachedItem().MusicDisks.FindBy(x => x.ID == id).First();

                _cacheManager.GetCachedItem().MusicDisks.Update(oldmusicDisk, musicDisk);
                _cacheManager.GetCachedItem().MusicDisks.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(string id)
        {
            var musicDisk = _cacheManager.GetCachedItem().MusicDisks.FindBy(x => x.ID == id).FirstOrDefault();
            return View(musicDisk);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                var musicDisk = _cacheManager.GetCachedItem().MusicDisks.FindBy(x => x.ID == id).FirstOrDefault();
                _cacheManager.GetCachedItem().MusicDisks.Delete(musicDisk);

                _cacheManager.GetCachedItem().MusicDisks.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
