using Contracts;
using Entities.DataOperator;
using Entities.DataOperator.Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityBookLibrary.Controllers
{
    [Authorize]
    public class CheckInController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly IXMLDeserializer _deserializer;
        private readonly IXMLSerializer _serializer;
        private readonly IXMLPath _xmlPath;
        private ICacheManager<RepositoryWrapper> _cacheManager;


        public CheckInController(IConfiguration configuration,          
          IXMLDeserializer deserializer,
          IXMLSerializer serializer,
          ICacheManager<RepositoryWrapper> cacheManager,
          IXMLPath xmlPath)
        {

            _configuration = configuration;
            _deserializer = deserializer;
            _serializer = serializer;
            _xmlPath = xmlPath;

            _xmlPath.SetXMLPath(_configuration["XmlFilePaths:TransactionXML"]);
            _cacheManager = cacheManager;
        }

        public ActionResult Index(string id)
        {
        
            var catalogId = id;

            var checkedOutCatalog = _cacheManager.GetCachedItem().BorrowTransactions.FindBy(x => x.CatalogID == catalogId && x.IsReturned == false);

            if (checkedOutCatalog.Any())
            {
                var borrowTransaction = checkedOutCatalog.First();        
                return View(borrowTransaction);
            }
            else
            {
               return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkin(IFormCollection collection)
        {
            try
            {
                var id = collection["ID"];

                var checkOutTrnx = _cacheManager.GetCachedItem().BorrowTransactions.FindBy(x => x.CatalogID == id).First();

                var checkinTrnx = CloneHelper.Clone(checkOutTrnx);
                checkinTrnx.IsReturned = true;
                checkinTrnx.ReceivedBy = Request.HttpContext.User.Identity.Name;
               
                _cacheManager.GetCachedItem().BorrowTransactions.CheckIn(checkOutTrnx, checkinTrnx);
                _cacheManager.GetCachedItem().BorrowTransactions.SaveChanges();

                return RedirectToAction(nameof(Index),"Book");
            }
            catch(Exception ex)
            {
                return View(nameof(Index));
            }
        }

    }
}
