using Contracts;
using Repository;
using Entities.Models;
using Entities.DataOperator;
using System;
using Xunit;
using Entities.DataOperator.Contracts;
using Microsoft.Extensions.Configuration;
using System.IO;




namespace CityBookLibraryUnitTest
{
    public class BookCheckOutTest
    {
        IXMLDeserializer _deserializer = new XMLFileDeserializer();
        IXMLSerializer _serializer = new XMLSerializer();
        IXMLPath _xmlPath = new XMLPath();
        private readonly IConfiguration _configuration;
        IRepositoryWrapper _wrapper;
        ICacheManager<RepositoryWrapper> _cacheManager;

        public BookCheckOutTest()
        {
            _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            _xmlPath.SetXMLPath(_configuration.GetSection("XmlFilePaths:TransactionXML").Value);
            _wrapper = new RepositoryWrapper(_deserializer, _serializer, _xmlPath);
            _cacheManager = new RepositoryCacheManager(_wrapper);
        }

        [Fact]
        public void Checkout_Book_Success()
        {
            
            var borrowTransaction = new BorrowTransaction
            {
                BorrowedBy = "LBU001",
                BorrowerFullName = "James Smith",
                CatalogID = "bk101",
                BorrowedDate = DateTime.Now,
                IsReturned = false,
                IssuedBy = "James",
                ID = Guid.NewGuid().ToString("N"),
                ExpectedReturnDate = DateTime.Now.AddDays(14)
            };

            Assert.True(_cacheManager.GetCachedItem().BorrowTransactions.CheckOut(borrowTransaction));
        }

        [Fact]
        public void Checkout_Book_Faliure()
        {
            BorrowTransaction borrowTransaction = null;

            Assert.False(_cacheManager.GetCachedItem().BorrowTransactions.CheckOut(borrowTransaction));
        }

    }
}
