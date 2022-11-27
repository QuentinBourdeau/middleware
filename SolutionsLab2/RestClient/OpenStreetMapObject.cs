using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RestClient
{
    [Serializable]
    public class Geopoints
    {
        public Geopoints()
        {
                
        }
        public Location[] loc { get; set; }
    }

    [Serializable]
    public class Location
    {
        public Location()
        {

        }
        public int place_id { get; set; }
        public string licence { get; set; }
        public string osm_type { get; set; }
        public int osm_id { get; set; }
        public string[] boundingbox { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string display_name { get; set; }
        public string _class { get; set; }
        public string type { get; set; }
        public float importance { get; set; }
        public string icon { get; set; }
        public Address address { get; set; }
    }

    [Serializable]
    public class Address
    {
        public Address()
        {
                
        }
        public string shop { get; set; }
        public string road { get; set; }
        public string neighbourhood { get; set; }
        public string suburb { get; set; }
        public string borough { get; set; }
        public string city { get; set; }
        public string ISO31662lvl4 { get; set; }
        public string postcode { get; set; }
        public string country { get; set; }
        public string country_code { get; set; }
    }

    [Serializable]
    public class Itinerary
    {
        public Itinerary()
        {
                
        }
        public Geocoding geocoding { get; set; }
        public string type { get; set; }
        public List<Feature> features { get; set; }
        public List<double> bbox { get; set; }
    }

    [Serializable]
    public class Geocoding
    {
        public Geocoding()
        {
                
        }
        public string version { get; set; }
        public string attribution { get; set; }
        public Query query { get; set; }
        public Engine engine { get; set; }
        public long timestamp { get; set; }
    }

    [Serializable]
    public class Query
    {
        public Query()
        {
                
        }
        public int size { get; set; }
        public bool _private { get; set; }
        public double pointlat { get; set; }
        public double pointlon { get; set; }
        public double boundarycirclelat { get; set; }
        public double boundarycirclelon { get; set; }
        public Lang lang { get; set; }
        public int querySize { get; set; }
    }

    [Serializable]
    public class Lang
    {
        public Lang()
        {
                
        }
        public string name { get; set; }
        public string iso6391 { get; set; }
        public string iso6393 { get; set; }
        public string via { get; set; }
        public bool defaulted { get; set; }
    }

    [Serializable]
    public class Engine
    {
        public Engine()
        {
                
        }
        public string name { get; set; }
        public string author { get; set; }
        public string version { get; set; }
    }
    [Serializable]
    public class Feature
    {
        public Feature()
        {
                
        }
        public string type { get; set; }
        public Geometry geometry { get; set; }
        public Properties properties { get; set; }
        public List<double> bbox { get; set; }
    }
    [Serializable]
    public class Geometry
    {
        public Geometry()
        {
                
        }
        public string type { get; set; }
        public List<List<double>> coordinates { get; set; }
    }
    [Serializable]
    public class Properties
    {
        public Properties()
        {
                
        }
        public string id { get; set; }
        public string gid { get; set; }
        public string layer { get; set; }
        public string source { get; set; }
        public string source_id { get; set; }
        public string name { get; set; }
        public double confidence { get; set; }
        public double distance { get; set; }
        public string accuracy { get; set; }
        public string country { get; set; }
        public string country_gid { get; set; }
        public string country_a { get; set; }
        public string macroregion { get; set; }
        public string macroregion_gid { get; set; }
        public string macroregion_a { get; set; }
        public string region { get; set; }
        public string region_gid { get; set; }
        public string region_a { get; set; }
        public string localadmin { get; set; }
        public string localadmin_gid { get; set; }
        public string locality { get; set; }
        public string locality_gid { get; set; }
        public string borough { get; set; }
        public string borough_gid { get; set; }
        public string neighbourhood { get; set; }
        public string neighbourhood_gid { get; set; }
        public string continent { get; set; }
        public string continent_gid { get; set; }
        public string label { get; set; }
    }


}


