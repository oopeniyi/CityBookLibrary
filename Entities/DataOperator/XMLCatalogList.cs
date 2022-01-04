using Entities.DataOperator.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataOperator
{
    
    public sealed class XMLCatalogList<T> 
    {
        private static dynamic _catalog = null;

        private static readonly Lazy<XMLCatalogList<T>> lazy = 
            new(() => new XMLCatalogList<T>());

        public static XMLCatalogList<T> Instance { get { return lazy.Value; } }

        private XMLCatalogList()
        {
            
        }

        public static T GetCatalog(IXMLDeserializer xML, IXMLPath xmlPath)
        {
            _catalog = xML.DeserializeXML<T>(xmlPath);
            return (T)_catalog;
        }

      
    }
}
