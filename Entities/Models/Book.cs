using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    [Serializable()]     
    public class Book: CatalogBase
    {
        [XmlElement("author")]
        public string Author { get; set; }

        [Display(Name ="Published")]
        [DisplayFormat(DataFormatString ="{0:dd MMM yyyy}")]
        [XmlElement("publish_date")]
        public DateTime PublishDate { get; set; }
    }
}
