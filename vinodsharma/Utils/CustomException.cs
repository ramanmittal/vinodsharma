using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vinodsharma.Utils
{
    public class CustomException:Exception
    {
        public CustomException(string msg):base(msg)
        {

        }
    }
}