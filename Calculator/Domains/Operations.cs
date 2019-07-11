using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Domains
{
    public interface IOperation
    {
    }

    public abstract class TwoParamOperation : IOperation
    {
        public string Left { get; set; }
        public string Right { get; set; }
    }

    public class AddOperation : TwoParamOperation
    {
    }

    public class MultiplyOperation : TwoParamOperation
    {
    }

    public class DivideOperation : TwoParamOperation
    {
    }


}
