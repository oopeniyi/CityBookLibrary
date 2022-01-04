using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entities.Models
{
    [Serializable()]
    [XmlRoot(Namespace = "", ElementName = "musiccatalog", IsNullable = false)]
    public class MusicCatalog
    {
        [XmlElement("musicdisk")]
        public List<MusicDisk> MusicDisk { get; set; }
    }
}
