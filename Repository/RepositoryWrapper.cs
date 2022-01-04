using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.DataOperator.Contracts;
using Entities.Models;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {

        private IBookRepository<Catalog, Book> _book;
        private IMusicDiskRepository<MusicCatalog, MusicDisk> _musicDisk;
        private ILibraryUserRepository<LibraryMember, LibraryUser> _libraryUser;
        private ILibraryTransactionRepository<LibraryTransaction, BorrowTransaction> _borrowTransaction;

        private readonly IXMLPath _filePath;
        private readonly IXMLDeserializer  _iXMLDeserializer;
        private readonly IXMLSerializer _iXMLSerializer;

        public RepositoryWrapper(IXMLDeserializer iXMLDeserializer, IXMLSerializer iXMLSerializer, IXMLPath filePath)
        {
            _iXMLDeserializer = iXMLDeserializer;
            _iXMLSerializer = iXMLSerializer;
            _filePath = filePath;
        }

        public IBookRepository<Catalog, Book> Books
        {
            get
            {
                if (_book == null)
                {
                    _book = new BookRepository<Catalog, Book>(_iXMLDeserializer, _iXMLSerializer, _filePath);
                }

                return _book;
            }
        }


        public IMusicDiskRepository<MusicCatalog, MusicDisk> MusicDisks
        {
            get
            {
                if (_musicDisk == null)
                {
                    _musicDisk = new MusicDiskRepository<MusicCatalog, MusicDisk>(_iXMLDeserializer, _iXMLSerializer, _filePath);
                }

                return _musicDisk;
            }
        }

        public ILibraryUserRepository<LibraryMember, LibraryUser> LibraryUsers
        {
            get
            {
                if (_libraryUser == null)
                {
                    _libraryUser = new LibraryUserRepository<LibraryMember, LibraryUser>(_iXMLDeserializer, _iXMLSerializer, _filePath);
                }

                return _libraryUser;
            }
        }

        public ILibraryTransactionRepository<LibraryTransaction, BorrowTransaction> BorrowTransactions
        {
            get
            {
                if (_borrowTransaction == null)
                {
                    _borrowTransaction = new LibraryTransactionRepository<LibraryTransaction, BorrowTransaction>(_iXMLDeserializer, _iXMLSerializer, _filePath);
                }

                return _borrowTransaction;
            }
        }
    }
}
