using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.DataOperator.Contracts;
using System.Linq.Expressions;

namespace Repository
{
    public class BookRepository<Catalog, Book> : RepositoryBase<Catalog, Book>, IBookRepository<Catalog, Book>
    {
        public BookRepository(IXMLDeserializer iXMLDeserializer, IXMLSerializer iXMLSerializer, IXMLPath filePath)
            :base(iXMLDeserializer, iXMLSerializer, filePath)
        {

        }

        public IEnumerable<Book> FindByAuthor(Func<Book, bool> expression)
        {                
            return  _repositoryContex.CatalogItemCollection.Where(expression);
        }
    }
}
