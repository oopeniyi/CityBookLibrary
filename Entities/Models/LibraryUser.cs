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
    public class LibraryUser
    {
        [XmlAttribute("ID")]
        public string ID { get; set; }

        [XmlElement("firstname")]
        public string FirstName { get; set; }

        [XmlElement("lastname")]
        public string LastName { get; set; }

        [Display(Name = "Registration Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [XmlElement("registrationdate")]
        public DateTime RegistrationDate { get; set; }

        [XmlElement("isactive")]
        public string IsActive { get; set; }
    }
}
