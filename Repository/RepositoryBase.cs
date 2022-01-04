using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.DataOperator.Contracts;

namespace Repository
{
    public class RepositoryBase<T1,T2> : IRepositoryBase<T1,T2> 
    {
       
        internal RepositoryContex<T1,T2> _repositoryContex;

        public RepositoryBase(IXMLDeserializer iXMLDeserializer, IXMLSerializer iXMLSerializer, IXMLPath filePath)
        {
            _repositoryContex = new RepositoryContex<T1,T2>(iXMLDeserializer, iXMLSerializer, filePath);
            
        }

        public bool Create(T2 entity)
        {
            if (entity != null)
            {
                _repositoryContex.CatalogItemCollection = _repositoryContex.CatalogItemCollection.Append(entity);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(T2 entity)
        {
            if (entity != null)
            {
                IEnumerable<T2> tempCollection = new T2[] { entity };
                _repositoryContex.CatalogItemCollection = _repositoryContex.CatalogItemCollection.Except(tempCollection);
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<T2> FindAll()
        {
             return _repositoryContex.CatalogItemCollection;          
        }

        public IEnumerable<T2> FindBy(Func<T2, bool> expression)
        {
             return _repositoryContex.CatalogItemCollection.Where(expression);           
        }

        public bool SaveChanges()
        {
            _repositoryContex.SaveChanges();
            return true;
        }

        public bool Update(T2 oldEntity, T2 newEntity)
        {   
            if(oldEntity != null && newEntity != null)
            {
                this.Delete(oldEntity);
                this.Create(newEntity);
                return true;
            }
            else
            {
                return false;
            }            
        }
               
    }
}
