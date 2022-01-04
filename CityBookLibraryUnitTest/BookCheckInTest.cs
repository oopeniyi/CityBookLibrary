using Contracts;
using Repository;
using Entities.DataOperator;
using Xunit;
using Entities.DataOperator.Contracts;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;



namespace CityBookLibraryUnitTest
{
    public class BookCheckInTest
    {

        IXMLDeserializer _deserializer = new XMLFileDeserializer();
        IXMLSerializer _serializer = new XMLSerializer();
        IXMLPath _xmlPath = new XMLPath();
        private readonly IConfiguration _configuration;
        IRepositoryWrapper _wrapper;
        ICacheManager<RepositoryWrapper> _cacheManager;

        public BookCheckInTest()
        {
            _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            _xmlPath.SetXMLPath(_configuration.GetSection("XmlFilePaths:TransactionXML").Value);
            _wrapper = new RepositoryWrapper(_deserializer, _serializer, _xmlPath);
            _cacheManager = new RepositoryCacheManager(_wrapper);
        }

        [Fact]
        public void Validate_CheckIn_Book_Faliuree()
        {
          
            var checkOutTrnx = _cacheManager.GetCachedItem().BorrowTransactions.FindBy(x => x.CatalogID == "bk101" && x.IsReturned == false).First();

            var checkinTrnx = CloneHelper.Clone(checkOutTrnx);
            checkinTrnx.IsReturned = true;
            checkinTrnx.ReceivedBy = "James";

            Assert.True(_cacheManager.GetCachedItem().BorrowTransactions.CheckIn(checkOutTrnx, checkinTrnx));
        }

        [Fact]
        public void Validate_CheckIn_Book_Faliure()
        {            
            Assert.False(_cacheManager.GetCachedItem().BorrowTransactions.CheckIn(null, null));
        }
    }
}
