using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryBase<T1, T2>
    {
        public bool Create(T2 entity);

        public bool Delete(T2 entity);

        public IEnumerable<T2> FindAll();

        public IEnumerable<T2> FindBy(Func<T2, bool> expression);       

        public bool Update(T2 oldEntity, T2 newEntity);

        public bool SaveChanges();

    }
}
