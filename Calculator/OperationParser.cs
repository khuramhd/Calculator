using System;
using System.Collections.Generic;
using System.Text;
using Calculator.Domains;

namespace Calculator
{
    public class OperationParser
    {
        private const string AddOperation = "add";
        private const string MultiOperation = "mult";
        private const string LetOperation = "let";
        private const string DivOperation = "div";
        private const string OpeningBracket = "(";

        public IOperation ParseOperation(string eq)
        {

            // if the equation doesn't end with ')' then it is invalid. There is no need to process it. 
            if (eq.EndsWith(")") == false)
                return null;

            // Idea is simple. Figure out the opeation in the begining and creating the corresponding operation object. 
            if (eq.StartsWith(AddOperation + OpeningBracket))
            {
                // break into two parameter
                var parameters = BreakTheParameter(eq, AddOperation.Length + 1, 1);
                var operation = new AddOperation();
                operation.Left = parameters.Item1;
                operation.Right = parameters.Item2;
                return operation;
            }

            if (eq.StartsWith(MultiOperation + OpeningBracket))
            {
                // break into two parameter
                var parameters = BreakTheParameter(eq, MultiOperation.Length + 1, 1);
                var operation = new MultiplyOperation();
                operation.Left = parameters.Item1;
                operation.Right = parameters.Item2;
                return operation;
            }

            if (eq.StartsWith(DivOperation + OpeningBracket))
            {
                // break into two parameter
                var parameters = BreakTheParameter(eq, DivOperation.Length + 1, 1);
                var operation = new DivideOperation();
                operation.Left = parameters.Item1;
                operation.Right = parameters.Item2;
                return operation;
            }

            if (eq.StartsWith(LetOperation + OpeningBracket))
            {
                var variableNameParameter = BreakTheParameter(eq, LetOperation.Length + 1, 1);
                // Since let has three parameter therefore we are breaking the parameters twice.
                var restOfTheParameter = BreakTheParameter(variableNameParameter.Item2, 0, 0);

                var operation = new LetOperation();
                operation.VariableName = variableNameParameter.Item1;
                operation.ValueOperation = restOfTheParameter.Item1;
                operation.NextOperation = restOfTheParameter.Item2;
                return operation;
            }

            throw new Exception("Invalid Input.");
        }

        // The method takes the operaton like 'add(1,2)' as input and prcess it and returns 1 and 2 in tuple. 
        private Tuple<string, string> BreakTheParameter(string eq, int prefixCount, int postfixCount)
        {
            // Taking out function name like 'Add(' from the equation. 
            eq = eq.Substring(prefixCount, eq.Length - prefixCount);

            // Taking out  ')' from the equation. 
            eq = eq.Substring(0, eq.Length - postfixCount);

            var openBracketCount = 0;
            var foundIndex = -1;
            for (int i = 0; i < eq.Length; i++)
            {
                var c = eq[i];
                if (c == '(')
                {
                    openBracketCount++;
                }
                else if (c == ')')
                {
                    openBracketCount--;
                }
                else if (c == ',' && openBracketCount == 0)
                {
                    foundIndex = i;
                    break;
                }
            }

            // return null if ',' index is not found or the if the comma is the last character
            if (foundIndex == -1 || foundIndex == eq.Length - 1)
            {
                throw new Exception("Invalid Input");
            }

            var left = eq.Substring(0, foundIndex);
            var right = eq.Substring(foundIndex + 1, eq.Length - foundIndex - 1);

            return new Tuple<string, string>(left, right);
        }
    }
}
