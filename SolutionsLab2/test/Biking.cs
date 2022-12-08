using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Text.Json;
using test.ProxyCacheRef;

namespace test
{
    internal class Biking : IBiking
    {
        Utils utils = new Utils();
        ApiOpenRoute openStreet = new ApiOpenRoute();
        ProxyCacheClient genericProxyCache = new ProxyCacheClient();
        ClientJCDecauxAPI clientJCDecaux;
        public Biking()
        {
            clientJCDecaux = new ClientJCDecauxAPI(genericProxyCache);
        }

        public Itinerary GetItinerary(string origin, string destination)
        {
            GeoCoordinate startingPoint;
            GeoCoordinate endingPoint;
            // get starting point and ending point using nominatim
            try {
                startingPoint = openStreet.addressToPoint(origin).Result;
                endingPoint = openStreet.addressToPoint(destination).Result;
            }
            catch(Exception ex)
            {
                Itinerary errorIti = new Itinerary();
                errorIti.error += "wrong address";
                return errorIti;
            }

            string originCity = openStreet.getLocation(openStreet.urlAddress(origin)).Result.address.city;
            string destinationCity = openStreet.getLocation(openStreet.urlAddress(destination)).Result.address.city;

            JCDStation startStation = clientJCDecaux.retrieveClosestStationDeparture(startingPoint);
            JCDContract jCDContract = clientJCDecaux.contractFromChosenStation(startStation);


            JCDStation endingStation = clientJCDecaux.retrieveClosestStationArrival(endingPoint, jCDContract);

            GeoCoordinate startStationLocation = utils.posToCoor(startStation.position);
            GeoCoordinate endingStationLocation = utils.posToCoor(endingStation.position);

            List<Rootobject> bikeiti = new List<Rootobject>();
            List<Rootobject> walkiti = new List<Rootobject>();
                bikeiti.Add(openStreet.geoToItinerary(startingPoint, startStationLocation, false).Result);
                bikeiti.Add(openStreet.geoToItinerary(startStationLocation, endingStationLocation, true).Result);
                bikeiti.Add(openStreet.geoToItinerary(endingStationLocation, endingPoint, false).Result);

                walkiti.Add(openStreet.geoToItinerary(startingPoint, endingPoint, false).Result);

            double walkingTime;
            double bikingTime;
            try
            {
                walkingTime = utils.calculateDuration(walkiti);
                bikingTime = utils.calculateDuration(bikeiti);
            }
            catch (Exception ex)
            {
                Itinerary errorIti = new Itinerary();
                errorIti.error += "Jesus";
                return errorIti;
            }


            if (walkingTime < bikingTime) {
                Itinerary ret = utils.calculateItinenary(walkiti);
                ret.error += "FullFoot";
                return ret;
            }
            else
            {
                return utils.calculateItinenary(bikeiti);
            }

        }
    }

}
