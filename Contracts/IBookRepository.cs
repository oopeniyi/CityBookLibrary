using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Contracts
{
    public interface IBookRepository<Catalog, Book> : IRepositoryBase<Catalog, Book>
    {
        public IEnumerable<Book> FindByAuthor(Func<Book, bool> expression);
    }
}
