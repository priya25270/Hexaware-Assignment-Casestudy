using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS_ADO.Model
{
    internal class SISException : Exception
    {
        public SISException() : base() { }
        
            public SISException(string message) : base(message) { }
        }
}
