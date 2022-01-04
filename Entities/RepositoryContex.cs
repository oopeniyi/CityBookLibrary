using Entities.DataOperator.Contracts;
using Entities.DataOperator;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Entities
{
    public class RepositoryContex<T1, T2> 
    {

        private readonly IXMLDeserializer _iXMLDeserializer;
        private readonly IXMLSerializer _iXMLSerializer;
        private T1 _catalog;
        private readonly IXMLPath _filePath;
        private IEnumerable<T2> _catalogItemCollection;
        
        public RepositoryContex(IXMLDeserializer iXMLDeserializer, IXMLSerializer iXMLSerializer, IXMLPath filePath)
        {
            _iXMLDeserializer = iXMLDeserializer;
            _iXMLSerializer = iXMLSerializer;
            _filePath = filePath;
           
            _catalog = XMLCatalogList<T1>.GetCatalog(_iXMLDeserializer, _filePath);
            _catalogItemCollection = GetPropertyValue<T2>(_catalog, typeof(T2).Name);
            
        }

        private static IEnumerable<T> GetPropertyValue<T>(object obj, string propName) {
            return (IEnumerable<T>)obj.GetType().GetProperty(propName).GetValue(obj, null); 
        }

        public T1 Catalog
        {
            get
            {
                return _catalog;
            }
            set
            {
                 _catalog = value;
            }
        }

        public IEnumerable<T2> CatalogItemCollection
        {
            get
            {
                return _catalogItemCollection;
            }

            set
            {
                _catalogItemCollection = value;

                Type objectType = typeof(T1);
                dynamic newEntity = (T1)Activator.CreateInstance(typeof(T1));

                PropertyInfo piInstance = objectType.GetProperty(typeof(T2).Name);
                piInstance.SetValue(newEntity, _catalogItemCollection.ToList());
                _catalog = newEntity;
            }
        }

        
        /// <summary>
        /// Persist Memory Content to file. Method implementation can be commented out 
        /// </summary>
        public void SaveChanges()
        {           
            System.IO.File.WriteAllText(_filePath.GetXMLPath(), _iXMLSerializer.SerializeXML(this.Catalog));
        }
    }
}
