using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RevolutionGames.Data
{
    class SurpriseBox
    {
        string cityName;
        string cueType;

        public SurpriseBox(string cityName, string cueType)
        {
            this.cityName = cityName;
            this.cueType = cueType;
        }

        public string CityName { get => cityName; set => cityName = value; }
        public string CueType { get => cueType; set => cueType = value; }
    }

    class City
    {
        string cityName = "";
        string[] cueTypes;

        public City(string cityName, string[] cueTypes)
        {
            this.cityName = cityName;
            this.cueTypes = cueTypes;
        }

        public string CityName { get => cityName; set => cityName = value; }
        public string[] CueTypes { get => cueTypes; set => cueTypes = value; }
    }

    public class CityDataManager : MonoBehaviour
    {
        private List<City> cityList = new List<City>();
        private List<SurpriseBox> surpriseBoxList = new List<SurpriseBox>();

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetSurpriseBoxData(string cityName, string cueType)
        {
            SurpriseBox surpriseBox = new SurpriseBox(cityName, cueType);
            surpriseBoxList.Add(surpriseBox);
        }

        public string GetSurpriseBoxCueType(int index)
        {
            return surpriseBoxList[index].CueType;
        }

        public int GetSurpriseBoxListCount()
        {
            return surpriseBoxList.Count;
        }

        public void SetCityData(string cityName, string[] cueTypes)
        {
            City city = new City(cityName, cueTypes);
            cityList.Add(city);
        }

        public string GetCueType(string cityName)
        {
            for (int i = 0; i < cityList.Count; i++)
            {
                if(cityList[i].CityName == cityName)
                {
                    return cityList[i].CueTypes[Random.Range(0, cityList[i].CueTypes.Length)];
                }
            }
            return "Basic";
        }

    }
}
