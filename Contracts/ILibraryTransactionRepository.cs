using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface ILibraryTransactionRepository<LibraryTransaction, BorrowTransaction> : IRepositoryBase<LibraryTransaction, BorrowTransaction>
    {
        public bool CheckOut(BorrowTransaction transaction);

        public bool CheckIn(BorrowTransaction transaction, BorrowTransaction reurnTransaction);
    }
}
