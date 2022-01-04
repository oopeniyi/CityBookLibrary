using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entities.Models
{
    [Serializable()]
    public class MusicDisk: CatalogBase
    {
        [XmlElement("artist")]
        public string Artist { get; set; }

        [Display(Name = "Released")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [XmlElement("releasedate")]
        public DateTime ReleaseDate { get; set; }
    }
}
