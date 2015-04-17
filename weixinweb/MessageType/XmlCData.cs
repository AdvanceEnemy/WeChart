using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace weixinweb.MessageType
{
     [AttributeUsage(AttributeTargets.Property | AttributeTargets.Enum, Inherited = true, AllowMultiple = true)]
    class XmlCDataAttribute : Attribute
    {
    }
}
