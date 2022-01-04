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
    public class BorrowTransaction
    {
        
        [XmlAttribute("ID")]
        public string ID { get; set; }

        [XmlElement("BorrowedBy")]
        public string BorrowedBy { get; set; }

        
        [Display(Name = "Borrower")]
        [XmlElement("BorrowerFullName")]
        public string BorrowerFullName { get; set; }

        [XmlElement("IssuedBy")]
        public string IssuedBy { get; set; }

        [XmlElement(ElementName = "ReceivedBy", IsNullable = true, Type = typeof(string))]
        public string? ReceivedBy { get; set; }

        [XmlElement("CatalogID")]
        public string CatalogID { get; set; }       

        [Display(Name = "Borrowed Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [XmlElement("BorrowedDate")]
        public DateTime BorrowedDate { get; set; }

        [XmlElement("IsReturned")]
        public bool IsReturned { get; set; }

        [Display(Name = "Expected Return Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [XmlElement(ElementName = "ExpectedReturnDate",  Type = typeof(DateTime))]
        public DateTime ExpectedReturnDate { get; set; }

       
    }
}
