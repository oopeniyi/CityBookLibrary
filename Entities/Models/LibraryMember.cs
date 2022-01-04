using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entities.Models
{
    [Serializable()]
    [XmlRoot(Namespace = "", ElementName = "librarymember", IsNullable = false)]
    public class LibraryMember
    {
        [XmlElement("libraryuser")]
        public List<LibraryUser> LibraryUser { get; set; }
    }
}
