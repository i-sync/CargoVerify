using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class CBase
    {
        public int result { get; set; }
        public string message { get; set; }
    }
    public class CChild:CBase
    {
        public string sessionId { get; set; }
    }
}
