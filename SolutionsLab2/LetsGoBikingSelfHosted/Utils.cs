using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsGoBikingSelfHosted
{
    internal class Utils
    {
        string GetCityName(string address)
        {
            ApiOpenRoute openStreet = new ApiOpenRoute();
            Location[] location = openStreet.addressToPoint(address).Result;
            return location[0].address.city.ToLower(); 
            //On met la ville en minuscules car les contrats sont écrits en minuscules dans JCDecaux
        }

        bool SameCity(string origin, string destination)
        {
            string originCity = GetCityName(origin);
            string destinationCity = GetCityName(destination);
            return originCity.Equals(destinationCity);
        }
    }
}
