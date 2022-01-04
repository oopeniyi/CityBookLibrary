using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;


namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IBookRepository<Catalog,Book> Books { get; }

        IMusicDiskRepository<MusicCatalog, MusicDisk> MusicDisks { get; }

        ILibraryUserRepository<LibraryMember, LibraryUser> LibraryUsers { get; }

        ILibraryTransactionRepository<LibraryTransaction, BorrowTransaction> BorrowTransactions { get; }

    }
}
