using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB05.CORE.Exceptions
{
    public class MISAexception: Exception
    {
        public string? ValidateError { get; set; }
        public MISAexception(string error)
        {
            this.ValidateError = error;
        }
        public override string Message => this.ValidateError;
    }
}
