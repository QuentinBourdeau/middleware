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

            // get starting point and ending point using nominatim
            GeoCoordinate startingPoint = openStreet.addressToPoint(origin).Result;
            GeoCoordinate endingPoint = openStreet.addressToPoint(destination).Result;
            string originCity = openStreet.getLocation(openStreet.urlAddress(origin)).Result.address.city;
            string destinationCity = openStreet.getLocation(openStreet.urlAddress(destination)).Result.address.city;

            JCDStation startStation = clientJCDecaux.retrieveClosestStationDeparture(startingPoint, originCity, destinationCity);
            JCDStation endingStation = clientJCDecaux.retrieveClosestStationArrival(startingPoint, originCity, destinationCity);

            GeoCoordinate startStationLocation = utils.posToCoor(startStation.position);
            GeoCoordinate endingStationLocation = utils.posToCoor(endingStation.position);

            List<Rootobject> iti = new List<Rootobject>();
            iti.Add(openStreet.geoToItinerary(startingPoint, startStationLocation, false).Result);
            iti.Add(openStreet.geoToItinerary(startStationLocation, endingStationLocation, true).Result);
            iti.Add(openStreet.geoToItinerary(endingStationLocation, endingPoint, false).Result);

            return utils.calculateItinenary(iti);

        }
    }

}
