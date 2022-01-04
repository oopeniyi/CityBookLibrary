
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
    public class XMLFileDeserializer : IXMLDeserializer
    {
       
        public T DeserializeXML<T>(IXMLPath xmlFilePath)
        {
            T deserializedObject = default(T);

            if (String.IsNullOrEmpty(xmlFilePath.GetXMLPath())) return default(T);

            StreamReader objStreamReader = new StreamReader(xmlFilePath.GetXMLPath());
            XmlSerializer objXMLSerializer = new XmlSerializer(typeof(T));
            
            deserializedObject = (T)objXMLSerializer.Deserialize(objStreamReader);
            objStreamReader.Close();

            return (T)deserializedObject;
        }
    }
}
