using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

using Contracts;
using Repository;
using Entities.Models;
using Entities.DataOperator.Contracts;
using Constants;

namespace CityBookLibrary.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IXMLDeserializer _deserializer;
        private readonly IXMLSerializer _serializer;
        private readonly IXMLPath _xmlPath;
        private ICacheManager<RepositoryWrapper> _cacheManager;       

        public BookController(IConfiguration configuration,             
            IXMLDeserializer deserializer, 
            IXMLSerializer serializer,
            ICacheManager<RepositoryWrapper> cacheManager,
            IXMLPath xmlPath)
        {
            
            _configuration = configuration;          
            _deserializer = deserializer;
            _serializer = serializer;
            _xmlPath = xmlPath;            

            _xmlPath.SetXMLPath(_configuration["XmlFilePaths:BooksXML"]);
            _cacheManager = cacheManager;
        }

        public ActionResult Index()
        {
            var bookCollection = _cacheManager.GetCachedItem().Books.FindAll();
            return View(bookCollection);
        }

        public ActionResult Details(string id)
        {
            var book = _cacheManager.GetCachedItem().Books.FindBy(x => x.ID == id).FirstOrDefault();     
            return View(book);
        }

        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
           
            Book book = new()
            {
                    Author = collection["Author"],
                    ID = collection["ID"],
                    Description = collection["Description"],
                    Genere = collection["Genere"],
                    Price = Convert.ToDecimal(collection["Price"]),
                    PublishDate = Convert.ToDateTime(collection["PublishDate"]),
                    Title = collection["Title"]
            };

            var existingBook = _cacheManager.GetCachedItem().Books.FindBy(x => x.ID == book.ID);

            if (existingBook.Any())
            {
                ViewBag.ErrorMessage = "Book already exist with same ID";
                return View();
            }

            _cacheManager.GetCachedItem().Books.Create(book);

            _cacheManager.GetCachedItem().Books.SaveChanges();

            return RedirectToAction(nameof(Index));
         
        }
                

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string author)
        {
            try
            {
                var books =_cacheManager.GetCachedItem().Books.FindByAuthor(x=> x.Author.ToLower().Contains(author.ToLower()));
                
                return View(books);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

      
        public ActionResult Edit(string id)
        {
            var book = _cacheManager.GetCachedItem().Books.FindBy(x => x.ID == id).FirstOrDefault();
            
            return View(book);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
        {
            try
            {

                Book book = new()
                {
                    Author = collection["Author"],
                    ID = collection["ID"],
                    Description = collection["Description"],
                    Genere = collection["Genere"],
                    Price = Convert.ToDecimal(collection["Price"]),
                    PublishDate = Convert.ToDateTime(collection["PublishDate"]),
                    Title = collection["Title"]
                };

                var oldBook = _cacheManager.GetCachedItem().Books.FindBy(x => x.ID == id).First();

                _cacheManager.GetCachedItem().Books.Update(oldBook,book);

                _cacheManager.GetCachedItem().Books.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

 
        public ActionResult Delete(string id)
        {
            var book = _cacheManager.GetCachedItem().Books.FindBy(x => x.ID == id).FirstOrDefault();

            _cacheManager.GetCachedItem().Books.SaveChanges();

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                var book = _cacheManager.GetCachedItem().Books.FindBy(x => x.ID == id).FirstOrDefault();
                _cacheManager.GetCachedItem().Books.Delete(book);

                _cacheManager.GetCachedItem().Books.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
