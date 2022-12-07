using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    internal class Utils
    {
        public bool SameCity(Location origin, Location destination)
        {
            return origin.Equals(destination);
        }
    }
}
