using System;
using System.Collections.Generic;
using Calculator.Domains;

namespace Calculator
{
    public class Calc
    {
        OperationParser _parser;
        public Calc()
        {
            _parser = new OperationParser();
        }

        private IDictionary<string, double> variables = new Dictionary<string, double>();

        public double ProcessEquation(string eq)
        {
            // Return null if the input is null or empty
            if (string.IsNullOrWhiteSpace(eq))
            {
                throw new Exception("Invalid Input");
            }

            // Trim the extra spaces
            eq = eq.Trim();

            // if the input the constant number. Then return it without processing it. 
            if (double.TryParse(eq, out double constant))
            {
                return constant;
            }

            // If the input the variable then return the value against it. 
            if (variables.ContainsKey(eq))
            {
                return variables[eq];
            }

            // Parse the operation
            var operation = _parser.ParseOperation(eq);

            // Process the parsed operation
            if (operation is AddOperation)
            {
                var addOperation = operation as AddOperation;
                var leftResult = ProcessEquation(addOperation.Left);
                var rightResult = ProcessEquation(addOperation.Right);
                return leftResult + rightResult;
            }

            if (operation is MultiplyOperation)
            {
                var multiOperation = operation as MultiplyOperation;
                var leftResult = ProcessEquation(multiOperation.Left);
                var rightResult = ProcessEquation(multiOperation.Right);
                return leftResult * rightResult;
            }

            if (operation is DivideOperation)
            {
                var divOperation = operation as DivideOperation;
                var leftResult = ProcessEquation(divOperation.Left);
                var rightResult = ProcessEquation(divOperation.Right);
                return leftResult / rightResult;
            }

            if (operation is LetOperation)
            {
                var letOperation = operation as LetOperation;
                var variableValue = ProcessEquation(letOperation.ValueOperation);
                // setting the variable and its value in the dictionary so that it could be used by other operatons. 
                variables[letOperation.VariableName] = variableValue;
                // processing the next operation in the let command
                return ProcessEquation(letOperation.NextOperation);
            }

            // unexpected operation object. 
            throw new Exception($"{operation?.GetType()?.Name ?? "One of the operations "} is not supported");
        }
    }
}
