using Entities.Models;
using Contracts;
using Entities.DataOperator.Contracts;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Repository
{
    public class MusicDiskRepository<MusicCatalog, MusicDisk> : RepositoryBase<MusicCatalog, MusicDisk>, IMusicDiskRepository<MusicCatalog, MusicDisk>
    {
        public MusicDiskRepository(IXMLDeserializer iXMLDeserializer, IXMLSerializer iXMLSerializer, IXMLPath filePath)
            :base(iXMLDeserializer, iXMLSerializer, filePath)
        {

        }


        public IEnumerable<MusicDisk> FindByArtist(Func<MusicDisk, bool> expression)
        {
            return _repositoryContex.CatalogItemCollection.Where(expression);
        }
    }
}
