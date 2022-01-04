using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.DataOperator.Contracts;

namespace Repository
{
    public class LibraryUserRepository<LibraryMember, LibraryUser> : RepositoryBase<LibraryMember, LibraryUser>, ILibraryUserRepository<LibraryMember, LibraryUser>
    {
        public LibraryUserRepository(IXMLDeserializer iXMLDeserializer, IXMLSerializer iXMLSerializer, IXMLPath filePath)
         : base(iXMLDeserializer, iXMLSerializer, filePath)
        {

        }
    }
}
