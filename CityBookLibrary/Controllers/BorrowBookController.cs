using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Entities.DataOperator.Contracts;
using Repository;
using Contracts;
using Microsoft.AspNetCore.Authorization;

namespace CityBookLibrary.Controllers
{
    [Authorize]
    public class BorrowBookController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IXMLDeserializer _deserializer;
        private readonly IXMLSerializer _serializer;
        private readonly IXMLPath _xmlPath;
        private ICacheManager<RepositoryWrapper> _cacheManager;

        public BorrowBookController(IConfiguration configuration,            
            IXMLDeserializer deserializer,
            IXMLSerializer serializer,
            ICacheManager<RepositoryWrapper> cacheManager,
            IXMLPath xmlPath)
        {

            _configuration = configuration;            
            _deserializer = deserializer;
            _serializer = serializer;
            _xmlPath = xmlPath;

            _xmlPath.SetXMLPath(_configuration["XmlFilePaths:MemberXML"]);
            _cacheManager = cacheManager;
            
        }
            

        public ActionResult Index(string id)
        {
            var book = _cacheManager.GetCachedItem().Books.FindBy(x => x.ID == id).FirstOrDefault();
            ViewBag.MemberList = _cacheManager.GetCachedItem().LibraryUsers.FindBy(x => x.IsActive == "Yes").Select(y=> new { y.ID, FullName = y.FirstName + " " + y.LastName});
           
            return View(book);
        }
   
    }
}
