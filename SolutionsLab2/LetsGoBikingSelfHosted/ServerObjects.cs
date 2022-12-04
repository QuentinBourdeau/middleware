using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LetsGoBikingSelfHosted
{
    [DataContract]
    public class Itinerary
    {
        public Itinerary()
        {
            directions = new List<Direction>();
        }

        [DataMember]
        public List<Direction> directions { get; set; }

        [DataMember]
        public double distance { get; set; }

        [DataMember]
        public double duration { get; set; }

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
