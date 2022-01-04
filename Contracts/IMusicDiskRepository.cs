using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
   public interface IMusicDiskRepository<MusicDiskCatalog, MusicDisk> : IRepositoryBase<MusicDiskCatalog, MusicDisk>
    {
        public IEnumerable<MusicDisk> FindByArtist(Func<MusicDisk, bool> expression);
    }
}
