using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataOperator.Contracts
{
    public interface IXMLSerializer
    {
        public string SerializeXML<T>(T seializableObject);
    }
}
