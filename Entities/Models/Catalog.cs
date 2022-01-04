using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entities.Models
{
    [Serializable()]
    [XmlRoot(Namespace = "", ElementName = "catalog",  IsNullable = false)]
    public class Catalog
    {
        [XmlElement("book")]        
        public List<Book> Book { get; set; }      
    }
}
