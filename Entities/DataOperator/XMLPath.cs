using Entities.DataOperator.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataOperator
{
    public class XMLPath : IXMLPath
    {
        private string _path;
           

        public string GetXMLPath()
        {
            return _path;
        }

        public void SetXMLPath(string path)
        {
            _path = path;
        }
    }
}
