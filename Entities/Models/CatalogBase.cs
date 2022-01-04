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
    public abstract class CatalogBase
    {
        [XmlAttribute("id")]
        public string ID { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("genre")]
        public string Genere { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }
    }
}
