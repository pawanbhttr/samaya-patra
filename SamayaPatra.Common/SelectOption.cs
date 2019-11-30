using System;
using System.Collections.Generic;
using System.Text;

namespace SamayaPatra.Common
{
    public class SelectOption
    {
        public string TableName { get; set; }
        public string ValueField { get; set; }
        public string TextField { get; set; }
        public string OrderBy { get; set; }
        public string Condition { get; set; }
    }
}
