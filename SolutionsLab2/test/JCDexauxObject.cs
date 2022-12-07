using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{//here
    public class JCDContract
    {

        public string name { get; set; }
    }

    public class JCDStation
    {

        public int number { get; set; }

        public string name { get; set; }

        public string contractName { get; set; }

        public Position position { get; set; }

        public Totalstands totalStands { get; set; }
    }

    public class Totalstands
    {

        public Availabilities availabilities { get; set; }
    }

    public class Availabilities
    {

        public int bikes { get; set; }

        public int stands { get; set; }
    }

    public class Position
    {

        public double latitude { get; set; }

        public double longitude { get; set; }
    }
}
