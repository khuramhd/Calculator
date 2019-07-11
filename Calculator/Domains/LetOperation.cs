using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Domains
{
    public class LetOperation : IOperation
    {
        public string VariableName { get; set; }
        public string ValueOperation { get; set; }
        public string NextOperation { get; set; }
    }
}
