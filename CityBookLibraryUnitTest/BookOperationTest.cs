using Contracts;
using Repository;
using Entities.Models;
using Entities.DataOperator;
using System;
using Xunit;
using Entities.DataOperator.Contracts;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using CityBookLibrary.Controllers;
using System.Collections.Generic;


namespace CityBookLibraryUnitTest
{
    public class BookOperationTest
    {
        IXMLDeserializer _deserializer = new XMLFileDeserializer();
        IXMLSerializer _serializer = new XMLSerializer();
        IXMLPath _xmlPath = new XMLPath();
        private readonly IConfiguration _configuration;
        IRepositoryWrapper _wrapper;
        ICacheManager<RepositoryWrapper> _cacheManager;

        public BookOperationTest()
        {
            _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            _xmlPath.SetXMLPath(_configuration.GetSection("XmlFilePaths:BooksXML").Value);
            _wrapper = new RepositoryWrapper(_deserializer, _serializer, _xmlPath);
            _cacheManager = new RepositoryCacheManager(_wrapper);
        }

        [Fact]
        public void Book_FindAll_Success()
        {
            Assert.NotEmpty(_cacheManager.GetCachedItem().Books.FindAll());
        }

        [Fact]
        public void Book_FindBy_Success()
        {                
            Assert.NotEmpty(_cacheManager.GetCachedItem().Books.FindBy(x=> x.ID == "bk101"));
        }

        [Fact]
        public void Book_FindBy_Faliure()
        {
            Assert.Empty(_cacheManager.GetCachedItem().Books.FindBy(x => x.ID == ""));
        }

        [Fact]
        public void Delete_Book_Success()
        {            
            var bookItem = _cacheManager.GetCachedItem().Books.FindBy(x => x.ID == "bk102").First();

            Assert.True(_cacheManager.GetCachedItem().Books.Delete(bookItem));
        }

        [Fact]
        public void Edith_Book_Success()
        {
            var oldBook = _cacheManager.GetCachedItem().Books.FindBy(x => x.ID == "bk101").First();

            var newBook = CloneHelper.Clone<Book>(oldBook);
            newBook.PublishDate = DateTime.Now;

            Assert.True(_cacheManager.GetCachedItem().Books.Update(oldBook, newBook));
        }

        [Fact]
        public void Edith_Book_Faliure()
        {
            var oldBook = _cacheManager.GetCachedItem().Books.FindBy(x => x.ID == "bk101").First();

            Assert.False(_cacheManager.GetCachedItem().Books.Update(oldBook, null));
        }

        [Fact]
        public void Delete_Book_Faliure()
        {
            Book book = null;

            Assert.False(_cacheManager.GetCachedItem().Books.Delete(book));
        }

        [Fact]
        public void Create_Book_Success()
        {           
            Book book = new Book
            {
                Author = "Max Marcus",
                Description = "The Art of Technology Testing",
                Genere = "Technology",
                ID = "bk200",
                Price = 4.99m,
                PublishDate = DateTime.Now,
                Title = "The Art of Technology Testing"

            };
            
            Assert.True(_cacheManager.GetCachedItem().Books.Create(book));
        }

        [Fact]
        public void Create_Book_Faliure()
        {
            Book book = null;
          
            Assert.False(_cacheManager.GetCachedItem().Books.Create(book));           
        }
         
        //Controller Test

        [Fact]
        public void Book_Controller_Success()
        {
            _xmlPath.SetXMLPath(_configuration.GetSection("XmlFilePaths:BooksXML").Value);
            IRepositoryWrapper _wrapper = new RepositoryWrapper(_deserializer, _serializer, _xmlPath);
            ICacheManager<RepositoryWrapper> cacheManager = new RepositoryCacheManager(_wrapper);

            BookController bookController = new BookController(_configuration, _deserializer, _serializer, cacheManager, _xmlPath);

            var result = bookController.Index();

            var viewResult = Assert.IsType<Microsoft.AspNetCore.Mvc.ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Book>>(viewResult.ViewData.Model);
            Assert.NotEmpty(model);
        }

    }
}
