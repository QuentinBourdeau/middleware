﻿using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    internal class Utils
    {

        public GeoCoordinate posToCoor(Position p)
        {
            return new GeoCoordinate(p.latitude, p.longitude);
        }
        public Itinerary processItinenary(List<Rootobject> route)
        {
            route.Select(o => o.metadata.query.profile);
            Itinerary itinerary = new Itinerary();
            Segment segment = new Segment();
            foreach (Rootobject obj in route)
            {
                foreach (Single[] coor in obj.features.First().geometry.coordinates)
                {
                    itinerary.coordinates.Add(new List<float>() { (float)coor[0], (float)coor[1] });
                }


                Segment seg = obj.features.First().properties.segments.First();
                string profile = obj.metadata.query.profile;
                itinerary.directions.Add(new Direction(seg, profile));
                itinerary.distance += obj.features.First().properties.summary.distance;
                itinerary.duration += obj.features.First().properties.summary.duration;
            }

            return itinerary;
        }

        public void cleanContractsList(List<JCDContract> contracts)
        {
            contracts.RemoveAll(contract => contract.cities == null);

        }
        public double calculateDuration(List<Rootobject> route)
        {
            return route.Select(o => o.features.First().properties.summary.duration).Sum();
        }
    }
}
