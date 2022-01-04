using Entities.DataOperator.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entities.DataOperator
{
    public class XMLSerializer : IXMLSerializer
    {
        public string SerializeXML<T>(T seializableObject)
        {
            using StringWriter writer = new StringWriter();
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(writer, seializableObject, null);
            return writer.ToString();
        }
    }
}
