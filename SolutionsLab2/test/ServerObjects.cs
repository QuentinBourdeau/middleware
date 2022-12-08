using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    [DataContract]
    public class Itinerary
    {
        public Itinerary()
        {
            directions = new List<Direction>();
            coordinates = new List<List<float>>();
            error = "";
        }

        [DataMember]
        public List<Direction> directions { get; set; }

        [DataMember]
        public double distance { get; set; }

        [DataMember]
        public double duration { get; set; }

        [DataMember]
        public List<List<float>> coordinates { get; set; }

        [DataMember]
        public string error { get; set; }

    }

    [DataContract]
    public class Direction
    {
        public Direction(Segment segment, string profile)
        {
            this.segment = segment;
            this.profile = profile;
        }
        [DataMember]
        public Segment segment { get; set; }
        [DataMember]
        public string profile { get; set; }
    }

}
