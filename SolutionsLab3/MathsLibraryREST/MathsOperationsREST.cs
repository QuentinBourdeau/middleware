using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MathsLibraryREST
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class MathsOperationsREST : IMathsOperationsREST
    {
        public int Add(int left, int right)
        {
            return left + right;
        }

        public int Subtract(int left, int right)
        {
            return left - right;
        }

        public int Multiply(int left, int right)
        {
            return left * right;
        }
    }
}
