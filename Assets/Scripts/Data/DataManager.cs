using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RevolutionGames.Data
{
    public class DataManager : MonoBehaviour
    {
        public Sprite[] cueSprites;

        private static DataManager instance = null;
        private CueStickDataManager cueStickDataManager;
        private CityDataManager cityDataManager;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                cueStickDataManager = this.GetComponent<CueStickDataManager>();
                cityDataManager = this.GetComponent<CityDataManager>();
                StartCoroutine(Delay());
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(this);

        }

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        public static DataManager Instance()
        {
            return instance;
        }

        public void SetCueStickDataManager()
        {
            /*cueStickDataManager.SetCueStickData(cueSprites[0], "Standard Cue", "Standard", "NA", "3k", 3000, "200", 200, 0, 0, 5, new int[] { 4, 1, 2, 3, 4}, new int[] { 0, 0, 1, 0, 2},
                new int[] { 0, 0, 1, 2, 0, 0 }, new int[] { 0, 0, 1, 0, 0, 1 }, new int[] { 0, 0, 0, 1, 2, 3 }, 2, 2, "Basic", 1, 0, 3, 3, 3, 0, 50);
            cueStickDataManager.SetCueStickData(cueSprites[1], "The Gunman Cue", "Standard", "NA", "1.8k", 1800, "175", 175, 0, 0, 5, new int[] { 4, 1, 2, 3, 4, 5 }, new int[] { 0, 0, 1, 0, 2, 3 },
                new int[] { 0, 0, 1, 2, 0, 0 }, new int[] { 0, 0, 1, 0, 0, 1 }, new int[] { 0, 0, 0, 1, 2, 3 }, 2, 2, "Basic",1, 0, 4, 2, 3, 0, 50);*/
            /*cueStickDataManager.SetCueStickData(cueSprites[3], "Owned Cue", "Owned", "1k", "NA", 1000, "10", 10, 0, 0, 5, new int[] { 4, 1, 2, 3, 4, 5 }, new int[] { 0, 0, 1, 0, 2, 3 },
                new int[] { 0, 0, 1, 2, 0, 0 }, new int[] { 0, 0, 1, 0, 0, 1 }, new int[] { 0, 0, 0, 1, 2, 3 }, 2, 2, "Basic", 0, 0, 3, 4, 1, 4, 2);
            cueStickDataManager.SetCueStickData(cueSprites[2], "Victory Cue", "Victory", "London", "0", 0, "0", 0, 0, 4, 5, new int[] {4, 1, 2, 3, 4, 5}, new int[] {0, 0, 1, 0, 2, 3},
                new int[] { 0, 0, 1, 2, 0, 0}, new int[] { 0, 0, 1, 0, 0, 1}, new int[] { 0, 0, 0, 1, 2, 3}, 0, 0, "Basic", 0, 0, 1, 3, 0, 1, 50);
            cueStickDataManager.SetCueStickData(cueSprites[4], "Pine Cue", "Victory", "Toronto", "0", 0, "0", 0, 2, 4, 1, new int[] { 4, 5, 5 }, new int[] { 0, 0, 1 },
                new int[] { 0, 0, 1 }, new int[] { 0, 0, 1 }, new int[] { 0, 0, 0 }, 0, 0, "Basic", 0, 0, 3, 1, 1, 0, 50);
            
            cueStickDataManager.SetCueStickData(cueSprites[4], "Archangel Cue", "Surprise", "NA", "1k", 0, "100", 100, 0, 4, 5, new int[] { 4, 1, 2, 3, 4, 5 }, new int[] { 0, 0, 1, 0, 2, 3 },
                new int[] { 0, 0, 1, 2, 0, 0 }, new int[] { 0, 0, 1, 0, 0, 1 }, new int[] { 0, 0, 0, 1, 2, 3 }, 0, 0, "Rare", 0, 0, 9, 9, 8, 8, 50);
            cueStickDataManager.SetCueStickData(cueSprites[4], "Dagger Cue", "Surprise", "NA", "1k", 0, "0", 0, 1, 4, 5, new int[] { 4, 1, 2, 3, 4, 5 }, new int[] { 0, 0, 1, 0, 2, 3 },
                new int[] { 0, 0, 1, 2, 0, 0 }, new int[] { 0, 0, 1, 0, 0, 1 }, new int[] { 0, 0, 0, 1, 2, 3 }, 0, 0, "Legendary", 0, 0, 3, 2, 4, 4, 50);
            cueStickDataManager.SetCueStickData(cueSprites[4], "Albania Cue", "Country", "Las Vegas", "1k", 1000, "160", 160, 0, 0, 5, new int[] { 4, 1, 2, 3, 4, 5 }, new int[] { 1, 0, 1, 0, 2, 3 },
                new int[] { 0, 0, 1, 2, 0, 0 }, new int[] { 0, 0, 1, 0, 0, 1 }, new int[] { 0, 1, 0, 1, 2, 3 }, 0, 0, "Advanced", 0, 0, 5, 5, 5, 4, 50);
            cueStickDataManager.SetCueStickData(cueSprites[4], "Algeria Cue", "Country", "Singapore", "1k", 1000, "160", 160, 0, 0, 5, new int[] { 4, 1, 2, 3, 4, 5 }, new int[] { 0, 0, 1, 0, 2, 3 },
                new int[] { 0, 0, 1, 2, 0, 0 }, new int[] { 0, 0, 1, 0, 0, 1 }, new int[] { 0, 0, 0, 1, 2, 3 }, 0, 0, "Advanced", 0, 0, 5, 5, 5, 4, 50);*/
        }

        public void SetCityData()
        {
            cityDataManager.SetCityData("Tokyo", new string[] { "Basic", "Advanced" });
            cityDataManager.SetCityData("Amsterdam", new string[] { "Advanced" });
            cityDataManager.SetCityData("Bangkok", new string[] { "Expert" });
            cityDataManager.SetCityData("Barcelona", new string[] { "Expert" });
            cityDataManager.SetCityData("Las Vegas", new string[] { "Basic", "Advanced" });
            cityDataManager.SetCityData("London", new string[] { "Basic" });
            cityDataManager.SetCityData("London", new string[] { "Advanced" });
        }

        IEnumerator Delay()
        {
            yield return new WaitForSeconds(2f);
            SetCueStickDataManager();
        }

        public int GetRareCueStickIndex()
        {
            return cueStickDataManager.GetSurpriseRareCueStickIndex();
        }

        public int GetEpicCueStickIndex()
        {
            return cueStickDataManager.GetSurpriseEpicCueStickIndex();
        }

        public int GetLegendaryCueStickIndex()
        {
            return cueStickDataManager.GetSurpriseLegendaryCueStickIndex();
        }

        public string GetRareCueStickName(int index)
        {
            return cueStickDataManager.GetSurpriseCueStickName(index);
        }

        public int GetRareCueStickCurrentSubLevel(int index)
        {
            return cueStickDataManager.GetSurpriseCueStickCurrentSubLevel(index);
        }

        public int GetRareCueStickMaxSubLevel(int index)
        {
            return cueStickDataManager.GetSurpriseCueStickMaxSubLevel(index);
        }

        public int GetRareCueStickIsUnlockedFlag(int index)
        {
            return cueStickDataManager.GetSurpriseCueStickIsUnlockedFlag(index);
        }

        public int GetRareCueStickUnlockedPieces(int index)
        {
            return cueStickDataManager.GetSurpriseCueStickUnlockedPieces(index);
        }

        public Sprite GetRareCueStickImage(int index)
        {
            return cueStickDataManager.GetSurpriseCueStickImage(index);
        }

        public string GetEpicCueStickName(int index)
        {
            return cueStickDataManager.GetSurpriseCueStickName(index);
        }

        public int GetEpicCueStickCurrentSubLevel(int index)
        {
            return cueStickDataManager.GetSurpriseCueStickCurrentSubLevel(index);
        }

        public int GetEpicCueStickMaxSubLevel(int index)
        {
            return cueStickDataManager.GetSurpriseCueStickMaxSubLevel(index);
        }

        public int GetEpicCueStickIsUnlockedFlag(int index)
        {
            return cueStickDataManager.GetSurpriseCueStickIsUnlockedFlag(index);
        }

        public int GetEpicCueStickUnlockedPieces(int index)
        {
            return cueStickDataManager.GetSurpriseCueStickUnlockedPieces(index);
        }

        public Sprite GetEpicCueStickImage(int index)
        {
            return cueStickDataManager.GetSurpriseCueStickImage(index);
        }

        public string GetLegendaryCueStickName(int index)
        {
            return cueStickDataManager.GetSurpriseCueStickName(index);
        }

        public int GetLegendaryCueStickCurrentSubLevel(int index)
        {
            return cueStickDataManager.GetSurpriseCueStickCurrentSubLevel(index);
        }

        public int GetLegendaryCueStickMaxSubLevel(int index)
        {
            return cueStickDataManager.GetSurpriseCueStickMaxSubLevel(index);
        }

        public int GetLegendaryCueStickIsUnlockedFlag(int index)
        {
            return cueStickDataManager.GetSurpriseCueStickIsUnlockedFlag(index);
        }

        public int GetLegendaryCueStickUnlockedPieces(int index)
        {
            return cueStickDataManager.GetSurpriseCueStickUnlockedPieces(index);
        }

        public Sprite GetLegendaryCueStickImage(int index)
        {
            return cueStickDataManager.GetSurpriseCueStickImage(index);
        }

        public void UpdateSurpriseCueStickLevel(int index)
        {
            if (cueStickDataManager.GetSurpriseCueStickIsUnlockedFlag(index) == 1)
            {
                if (cueStickDataManager.GetSurpriseCueStickCurrentLevel(index) > cueStickDataManager.GetSurpriseCueStickMaxLevel(index))
                {
                    //DO Nothing
                }
                else
                {
                    cueStickDataManager.SetSurpriseCueStickCurrentSubLevel(index, cueStickDataManager.GetSurpriseCueStickCurrentSubLevel(index) + 1);
                    if (cueStickDataManager.GetSurpriseCueStickCurrentSubLevel(index) >= cueStickDataManager.GetSurpriseCueStickMaxSubLevel(index))
                    {
                        cueStickDataManager.SetSurpriseCueStickCurrentLevel(index, cueStickDataManager.GetSurpriseCueStickCurrentLevel(index) + 1);
                        if (cueStickDataManager.GetSurpriseCueStickCurrentLevel(index) + 1 <= cueStickDataManager.GetSurpriseCueStickMaxLevel(index))
                        {
                            cueStickDataManager.SetSurpriseCueStickCurrentSubLevel(index, 0);
                        }
                    }
                }
            }
            else
            {
                cueStickDataManager.SetSurpriseCueStickUnlockedPieces(index, cueStickDataManager.GetSurpriseCueStickUnlockedPieces(index) + 1);
                if (cueStickDataManager.GetSurpriseCueStickUnlockedPieces(index) >= cueStickDataManager.GetSurpriseCueStickTotalPieces(index))
                {
                    cueStickDataManager.SetSurpriseCueStickCurrentSubLevel(index, 0);
                    cueStickDataManager.SetSurpriseCueStickIsUnlockedFlag(index, 1);
                    cueStickDataManager.ObtainedSurpriseCueStick(index);
                }
            }
        }


        public void SpawnSurpriseBox()
        {
            string city = "London";
            string cueType = cityDataManager.GetCueType(city);
            cityDataManager.SetSurpriseBoxData(city, cueType);
        }

        public int GetSurpriseBoxListCount()
        {
            return cityDataManager.GetSurpriseBoxListCount();
        }

        public int OpenVictorySurpriseBox(int index)
        {
            if (cityDataManager.GetSurpriseBoxCueType(index) == "Basic")
            {
                return cueStickDataManager.GetVictoryBasicCueStickIndex();
            }
            else if (cityDataManager.GetSurpriseBoxCueType(index) == "Advanced")
            {
                return cueStickDataManager.GetVictoryAdvancedCueStickIndex();
            }
            else if (cityDataManager.GetSurpriseBoxCueType(index) == "Expert")
            {
                return cueStickDataManager.GetVictoryExpertCueStickIndex();
            }
            return 0;
        }

        public int OpenCountrySurpriseBox(int index)
        {
            return cueStickDataManager.GetCountryAdvancedCueStickIndex();

        }

        public string GetVictoryBoxCueStickName(int index)
        {
            return cueStickDataManager.GetVictoryCueStickName(index);
        }

        public Sprite GetVictoryBoxCueStickImage(int index)
        {
            return cueStickDataManager.GetVictoryCueStickImage(index);
        }

        public string GetCountryBoxCueStickName(int index)
        {
            return cueStickDataManager.GetCountryCueStickName(index);
        }

        public Sprite GetCountryBoxCueStickImage(int index)
        {
            return cueStickDataManager.GetCountryCueStickImage(index);
        }

        public void UpdateVictoryCueStickLevel(int index)
        {
            if (cueStickDataManager.GetVictoryCueStickIsUnlockedFlag(index) == 1)
            {
                if (cueStickDataManager.GetVictoryCueStickCurrentLevel(index) > cueStickDataManager.GetVictoryCueStickMaxLevel(index))
                {
                    //DO Nothing
                }
                else
                {
                    cueStickDataManager.SetVictoryCueStickCurrentSubLevel(index, cueStickDataManager.GetVictoryCueStickCurrentSubLevel(index) + 1);
                    if (cueStickDataManager.GetVictoryCueStickCurrentSubLevel(index) >= cueStickDataManager.GetVictoryCueStickMaxSubLevel(index))
                    {
                        cueStickDataManager.SetVictoryCueStickCurrentLevel(index, cueStickDataManager.GetVictoryCueStickCurrentLevel(index) + 1);
                        if (cueStickDataManager.GetVictoryCueStickCurrentLevel(index) + 1 <= cueStickDataManager.GetVictoryCueStickMaxLevel(index))
                        {
                            cueStickDataManager.SetVictoryCueStickCurrentSubLevel(index, 0);
                        }
                    }
                }
            }
            else
            {
                cueStickDataManager.SetVictoryCueStickUnlockedPieces(index, cueStickDataManager.GetVictoryCueStickUnlockedPieces(index) + 1);
                if (cueStickDataManager.GetVictoryCueStickUnlockedPieces(index) >= cueStickDataManager.GetVictoryCueStickTotalPieces(index))
                {
                    cueStickDataManager.SetVictoryCueStickCurrentSubLevel(index, 0);
                    cueStickDataManager.SetVictoryCueStickIsUnlockedFlag(index, 1);
                    cueStickDataManager.ObtainedVictoryCueStick(index);
                }
            }
        }

        public void UpdateCountryCueStickLevel(int index)
        {
            if(cueStickDataManager.GetCountryCueStickIsUnlockedFlag(index) == 1)
            {
                if (cueStickDataManager.GetCountryCueStickCurrentLevel(index) > cueStickDataManager.GetCountryCueStickMaxLevel(index))
                {
                    //DO Nothing
                }
                else
                {
                    cueStickDataManager.SetCountryCueStickCurrentSubLevel(index, cueStickDataManager.GetCountryCueStickCurrentSubLevel(index) + 1);
                    if (cueStickDataManager.GetCountryCueStickCurrentSubLevel(index) >= cueStickDataManager.GetCountryCueStickMaxSubLevel(index))
                    {
                        cueStickDataManager.SetCountryCueStickCurrentLevel(index, cueStickDataManager.GetCountryCueStickCurrentLevel(index) + 1);
                        if (cueStickDataManager.GetCountryCueStickCurrentLevel(index) + 1 <= cueStickDataManager.GetCountryCueStickMaxLevel(index))
                        {
                            cueStickDataManager.SetCountryCueStickCurrentSubLevel(index, 0);
                        }
                    }
                }
            }
            else
            {
                cueStickDataManager.SetCountryCueStickUnlockedPieces(index, cueStickDataManager.GetCountryCueStickUnlockedPieces(index) + 1);
                if (cueStickDataManager.GetCountryCueStickUnlockedPieces(index) >= cueStickDataManager.GetCountryCueStickTotalPieces(index))
                {
                    cueStickDataManager.SetCountryCueStickCurrentSubLevel(index, 0);
                    cueStickDataManager.SetCountryCueStickIsUnlockedFlag(index, 1);
                    cueStickDataManager.ObtainedCountryCueStick(index);
                }
            }
        }

        /*public int GetTapToAimFlag()
        {
            //0 - disabled property
            //1- enabled
            return cueStickDataManager.TapToAimFlag;
        }

        public void SetTapToAimFlag(int val)
        {
            cueStickDataManager.TapToAimFlag = val;
        }

        public int GetDisableGuidelineInLocalFlag()
        {
            //0 - disabled property
            //1- enabled
            return cueStickDataManager.DisableGuidelineInLocalFlag;
        }

        public void SetDisableGuidelineInLocalFlag(int val)
        {
            cueStickDataManager.DisableGuidelineInLocalFlag = val;
        }

        public int GetAimingWheelFlag()
        {
            //0 - disabled property
            //1- enabled
            return cueStickDataManager.AimingWheelFlag;
        }

        public void SetAimingWheelFlag(int val)
        {
            cueStickDataManager.AimingWheelFlag = val;
        }

        public int GetCueSensitivity()
        {
            //0 - slow
            //1- normal
            //2 - fast
            return cueStickDataManager.CueSensitivity;
        }

        public void SetCueSensitivity(int val)
        {
            cueStickDataManager.CueSensitivity = val;
        }

        public int GetPowerBarLocationFlag()
        {
            //0 - left
            //1- right
            return cueStickDataManager.PowerBarLocationFlag;
        }

        public void SetPowerBarLocationFlag(int val)
        {
            cueStickDataManager.PowerBarLocationFlag = val;
        }

        public int GetPowerBarOrientationFlag()
        {
            //0 - vertical
            //1- horizontal
            return cueStickDataManager.PowerBarOrientationFlag;
        }

        public void SetPowerBarOrientationFlag(int val)
        {
            cueStickDataManager.PowerBarOrientationFlag = val;
        }*/
    }
}
