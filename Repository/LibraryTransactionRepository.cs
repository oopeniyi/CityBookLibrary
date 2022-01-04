using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.DataOperator.Contracts;

namespace Repository
{
    public class LibraryTransactionRepository<LibraryTransaction, BorrowTransaction> : RepositoryBase<LibraryTransaction, BorrowTransaction>, ILibraryTransactionRepository<LibraryTransaction, BorrowTransaction>
    {

        public LibraryTransactionRepository(IXMLDeserializer iXMLDeserializer, IXMLSerializer iXMLSerializer, IXMLPath filePath)
          : base(iXMLDeserializer, iXMLSerializer, filePath)
        {

        }

        public bool CheckIn(BorrowTransaction transaction, BorrowTransaction reurnTransaction)
        {           
            if(transaction !=null && reurnTransaction != null)
            {
                return  this.Update(transaction, reurnTransaction);
            }
            else
            {
                return false;
            }
                     
        }

        public bool CheckOut(BorrowTransaction transaction)
        {
            if(transaction != null)
            {
                return this.Create(transaction);
            }
            else
            {
                return false;
            }
          
        }
    }
}
