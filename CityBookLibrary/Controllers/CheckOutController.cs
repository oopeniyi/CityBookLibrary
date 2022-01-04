using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Entities.DataOperator.Contracts;
using Entities.Models;
using Repository;
using Contracts;
using Microsoft.AspNetCore.Authorization;

namespace CityBookLibrary.Controllers
{
    [Authorize]
    public class CheckOutController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly IXMLDeserializer _deserializer;
        private readonly IXMLSerializer _serializer;
        private readonly IXMLPath _xmlPath;
        private ICacheManager<RepositoryWrapper> _cacheManager;

        public CheckOutController(IConfiguration configuration,            
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
               
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(IFormCollection collection)
        {
            var requestingMemberID = collection["MemberId"];
            var catalogId = collection["ID"];

            var checkedOutCatalog = _cacheManager.GetCachedItem().BorrowTransactions.FindBy(x=> x.CatalogID == catalogId && x.IsReturned == false);
            
            if (checkedOutCatalog.Any())
            {               
                var borrowTransaction = checkedOutCatalog.First();
                var borrower = _cacheManager.GetCachedItem().LibraryUsers.FindBy(x => x.ID == borrowTransaction.BorrowedBy).First();

                ViewBag.Status = "ExistingCheckeout";                
                return View(borrowTransaction);
            }
            else
            {
                var borrower = _cacheManager.GetCachedItem().LibraryUsers.FindBy(x => x.ID == requestingMemberID).First();

                var borrowTransaction = new BorrowTransaction
                {
                    BorrowedBy = requestingMemberID,
                    BorrowerFullName = $"{borrower.FirstName} {borrower.LastName}",
                    CatalogID = catalogId,
                    BorrowedDate = DateTime.Now,
                    IsReturned = false,
                    IssuedBy = Request.HttpContext.User.Identity.Name,
                    ID = Guid.NewGuid().ToString("N"),
                    ExpectedReturnDate = DateTime.Now.AddDays(14)
                };

                _cacheManager.GetCachedItem().BorrowTransactions.CheckOut(borrowTransaction);
                ViewBag.Status = "NewCheckout";
                _cacheManager.GetCachedItem().BorrowTransactions.SaveChanges();
                return View(borrowTransaction);
            }                       
        }
    }
}
