using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gfl
{
    public class APIException : Exception 
    {
        public APIException()
            : base()
        {
        }

        public APIException(string message)
            :base(message)
        {
        }
    }
}
