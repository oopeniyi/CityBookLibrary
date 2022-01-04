using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entities.Models
{
    [Serializable()]
    [XmlRoot(Namespace = "", ElementName = "LibraryTransaction", IsNullable = false)]
    public class LibraryTransaction
    {
        [XmlElement("BorrowTransaction")]
        public List<BorrowTransaction> BorrowTransaction { get; set; }
    }
}
