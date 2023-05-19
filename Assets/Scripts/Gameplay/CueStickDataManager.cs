using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RevolutionGames.Data
{
    //The data classes should not be accessed outside this script. Only through the main class
    class CueStickLocalData //PlayerPrefs data
    {
        public int tapToAimFlag = 0;
        public int disableGuidelineInLocalFlag = 0;
        public int aimingWheelFlag = 0;
        public int cueSensitivity = 0; //0 or 1 or 2
        public int powerBarLocationFlag = 0;
        public int powerBarOrientationFlag = 0;
    }

    class CueStickProperties //APIData
    {
        private int force = 0;
        private int spin = 0;
        private int aim = 0;
        private int time = 0;
        private int charge = 0;

        public CueStickProperties(int force, int aim, int spin, int time, int charge)
        {
            this.force = force;
            this.aim = aim;
            this.spin = spin;
            this.time = time;
            this.charge = charge;
        }

        public int Force { get => force; set => force = value; }
        public int Aim { get => aim; set => aim = value; }
        public int Spin { get => spin; set => spin = value; }
        public int Time { get => time; set => time = value; }
        public int Charge { get => charge; set => charge = value; }
    }

    class CueStickLevel
    {
        private int levelNumber;
        private int maxSubLevel; //maximum pieces required
        private int currentSubLevel;
        private int forceUpgradeValue;
        private int aimUpgradeValue;
        private int spinUpgradeValue;
        private int timeUpgradeValue;

        public CueStickLevel(int levelNumber, int maxSubLevel, int currentSubLevel, 
            int forceUpgradeValue, int aimUpgradeValue, int spinUpgradeValue, int timeUpgradeValue)
        {
            this.levelNumber = levelNumber;
            this.maxSubLevel = maxSubLevel;
            this.currentSubLevel = currentSubLevel;
            this.forceUpgradeValue = forceUpgradeValue;
            this.aimUpgradeValue = aimUpgradeValue;
            this.spinUpgradeValue = spinUpgradeValue;
            this.timeUpgradeValue = timeUpgradeValue;
        }

        public int LevelNumber { get => levelNumber; set => levelNumber = value; }
        public int MaxSubLevel { get => maxSubLevel; set => maxSubLevel = value; }
        public int CurrentSubLevel { get => currentSubLevel; set => currentSubLevel = value; }
        public int ForceUpgradeValue { get => forceUpgradeValue; set => forceUpgradeValue = value; }
        public int AimUpgradeValue { get => aimUpgradeValue; set => aimUpgradeValue = value; }
        public int SpinUpgradeValue { get => spinUpgradeValue; set => spinUpgradeValue = value; }
        public int TimeUpgradeValue { get => timeUpgradeValue; set => timeUpgradeValue = value; }
    }

    class CueStick //APIData
    {
        private Sprite image;
        private string name; //primary key
        private string category; // Standard, premium, country, victory, owned
        private string city;
        private string price;
        private long priceInNumbers;
        private string rechargePrice;
        private long rechargePriceInNumbers;
        private int maxLevel;
        private int unlockedPieces;
        private int totalPieces;
        private List<CueStickLevel> cueStickLevels;
        private CueStickLevel currentLevel;
        private string cueType; // Basic, advanced, legendary
        private int isUnlocked; //0 - locked, 1 - unlocked
        private int autoRecharge; //0 - no 1 - yes
        private CueStickProperties cueStickProperties;

        public CueStick(Sprite image, string name, CueStickProperties cueStickProperties)
        {
            this.image = image;
            this.name = name;
            this.cueStickProperties = cueStickProperties;
        }

        public CueStick(Sprite image, string name, string category, string city, string price, long priceInNumbers, string rechargePrice, long rechargePriceInNumbers,
            int unlockedPieces, int totalPieces,
            int maxLevel, List<CueStickLevel> cueStickLevels, string cueType, int isUnlocked, int autoRecharge, CueStickProperties cueStickProperties)
        {
            this.image = image;
            this.name = name;
            this.category = category;
            this.city = city;
            this.price = price;
            this.priceInNumbers = priceInNumbers;
            this.rechargePrice = rechargePrice;
            this.rechargePriceInNumbers = rechargePriceInNumbers;
            this.unlockedPieces = unlockedPieces;
            this.totalPieces = totalPieces;
            this.maxLevel = maxLevel;
            this.cueStickLevels = cueStickLevels;
            this.cueType = cueType;
            this.isUnlocked = isUnlocked;
            this.autoRecharge = autoRecharge;
            this.cueStickProperties = cueStickProperties;
        }

        public Sprite Image { get => image; set => image = value; }
        public string Name { get => name; set => name = value; }
        public string Category { get => category; set => category = value; }
        public string Price { get => price; set => price = value; }
        public long PriceInNumbers { get => priceInNumbers; set => priceInNumbers = value; }
        public string RechargePrice { get => rechargePrice; set => rechargePrice = value; }
        public long RechargePriceInNumbers { get => rechargePriceInNumbers; set => rechargePriceInNumbers = value; }
        public string CueType { get => cueType; set => cueType = value; }
        public string City { get => city; set => city = value; }
        public int MaxLevel { get => maxLevel; set => maxLevel = value; }
        public int IsUnlocked { get => isUnlocked; set => isUnlocked = value; }
        public int AutoRecharge { get => autoRecharge; set => autoRecharge = value; }
        public int UnlockedPieces { get => unlockedPieces; set => unlockedPieces = value; }
        public int TotalPieces { get => totalPieces; set => totalPieces = value; }
        internal CueStickProperties CueStickProperties { get => cueStickProperties; set => cueStickProperties = value; }
        internal CueStickLevel CurrentLevel { get => currentLevel; set => currentLevel = value; }
        internal List<CueStickLevel> CueStickLevels { get => cueStickLevels; set => cueStickLevels = value; }
    }

    public class CueStickDataManager : MonoBehaviour
    {
        public Sprite[] cueSprites;

        //private static CueStickDataManager instance = null;
        private CueStickLocalData cueStickLocalData;
        private CueStick playerCueStick;
        private List<CueStick> standardCueStickList;
        private List<CueStick> victoryCueStickList;
        private List<int> victoryBasicCueStickList;
        private List<int> victoryAdvancedCueStickList;
        private List<int> victoryExpertCueStickList;

        private List<CueStick> surpriseCueStickList;
        private List<int> surpriseRareCueStickList;
        private List<int> surpriseEpicCueStickList;
        private List<int> surpriseLegendaryCueStickList;

        private List<CueStick> countryCueStickList;
        private List<int> countryUnlockedCueStickList;

        private List<CueStick> ownedCueStickList;

        private int randomIndex = 0;

        void Awake()
        {
            /*if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(this);*/

            cueStickLocalData = new CueStickLocalData();
            standardCueStickList = new List<CueStick>();
            victoryCueStickList = new List<CueStick>();
            victoryBasicCueStickList = new List<int>();
            victoryAdvancedCueStickList = new List<int>();
            victoryExpertCueStickList = new List<int>();

            surpriseCueStickList = new List<CueStick>();
            surpriseRareCueStickList = new List<int>();
            surpriseEpicCueStickList = new List<int>();
            surpriseLegendaryCueStickList = new List<int>();

            countryCueStickList = new List<CueStick>();
            countryUnlockedCueStickList = new List<int>();

            ownedCueStickList = new List<CueStick>();
            /*SetCueStickData(cueSprites[0], "Standard Cue", "Standard", "NA", "2k", 2000, 200, 200, 5, new int[] { 4, 1, 2, 3, 4, 5 }, new int[] { 0, 0, 1, 0, 2, 3 },
                new int[] { 0, 0, 1, 2, 0, 0 }, new int[] { 0, 0, 1, 0, 0, 1 }, new int[] { 0, 0, 0, 1, 2, 3 }, 2, 2, "Basic", 1, 0, 3, 3, 3, 0, 50);
            SetCueStickData(cueSprites[1], "The Gunman Cue", "Standard", "NA", "1.8k", 1800, 175, 175, 5, new int[] { 4, 1, 2, 3, 4, 5 }, new int[] { 0, 0, 1, 0, 2, 3 },
                new int[] { 0, 0, 1, 2, 0, 0 }, new int[] { 0, 0, 1, 0, 0, 1 }, new int[] { 0, 0, 0, 1, 2, 3 }, 2, 2, "Basic", 1, 0, 4, 2, 3, 0, 50);
            SetCueStickData(cueSprites[3], "Owned Cue", "Owned", "1k", "NA", 1000, 10, 10, 5, new int[] { 4, 1, 2, 3, 4, 5 }, new int[] { 0, 0, 1, 0, 2, 3 },
                new int[] { 0, 0, 1, 2, 0, 0 }, new int[] { 0, 0, 1, 0, 0, 1 }, new int[] { 0, 0, 0, 1, 2, 3 }, 2, 2, "Basic", 1, 0, 3, 4, 1, 4, 2);
            SetCueStickData(cueSprites[2], "Victory Cue", "Victory", "London", "0", 0, 0, 0, 5, new int[] { 4, 1, 2, 3, 4, 5 }, new int[] { 0, 0, 1, 0, 2, 3 },
                new int[] { 0, 0, 1, 2, 0, 0 }, new int[] { 0, 0, 1, 0, 0, 1 }, new int[] { 0, 0, 0, 1, 2, 3 }, 1, 0, "Basic", 1, 0, 1, 3, 0, 1, 50);
            SetCueStickData(cueSprites[4], "Pine Cue", "Victory", "Toronto", "0", 0, 0, 0, 0, new int[] { 4, 5, 5 }, new int[] { 0, 0, 1 },
                new int[] { 0, 0, 1 }, new int[] { 0, 0, 1 }, new int[] { 0, 0, 0 }, 0, 2, "Basic", 0, 0, 3, 1, 1, 0, 50);

            SetCueStickData(cueSprites[4], "Archangel Cue", "Surprise", "NA", "1k", 0, 100, 100, 5, new int[] { 4, 1, 2, 3, 4, 5 }, new int[] { 0, 0, 1, 0, 2, 3 },
                new int[] { 0, 0, 1, 2, 0, 0 }, new int[] { 0, 0, 1, 0, 0, 1 }, new int[] { 0, 0, 0, 1, 2, 3 }, 2, 2, "Legendary", 1, 0, 9, 9, 8, 8, 50);
            SetCueStickData(cueSprites[4], "Survivalist Cue", "Surprise", "NA", "1k", 0, 0, 0, 5, new int[] { 4, 1, 2, 3, 4, 5 }, new int[] { 0, 0, 1, 0, 2, 3 },
                new int[] { 0, 0, 1, 2, 0, 0 }, new int[] { 0, 0, 1, 0, 0, 1 }, new int[] { 0, 0, 0, 1, 2, 3 }, 2, 2, "Rare", 1, 0, 3, 2, 4, 4, 50);
            SetCueStickData(cueSprites[4], "Albania Cue", "Country", "NA", "1k", 1000, 160, 160, 5, new int[] { 4, 1, 2, 3, 4, 5 }, new int[] { 0, 0, 1, 0, 2, 3 },
                new int[] { 0, 0, 1, 2, 0, 0 }, new int[] { 0, 0, 1, 0, 0, 1 }, new int[] { 0, 0, 0, 1, 2, 3 }, 2, 2, "Advanced", 1, 0, 5, 5, 5, 4, 50);
            SetCueStickData(cueSprites[4], "Algeria Cue", "Country", "NA", "1k", 1000, 160, 160, 5, new int[] { 4, 1, 2, 3, 4, 5 }, new int[] { 0, 0, 1, 0, 2, 3 },
                new int[] { 0, 0, 1, 2, 0, 0 }, new int[] { 0, 0, 1, 0, 0, 1 }, new int[] { 0, 0, 0, 1, 2, 3 }, 2, 2, "Advanced", 1, 0, 5, 5, 5, 4, 50);
            SetPlayerCueStick("Owned Cue");*/
        }

        // Start is called before the first frame update
        void Start()
        {

            /* List<CueStickLevel> cueStickLevels = new List<CueStickLevel>();
             CueStickLevel cueStickLevel1 = new CueStickLevel(1, 6, 0, 1, 0, 0, 1);
             CueStickLevel cueStickLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
             CueStickLevel cueStickLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 1, 0);
             CueStickLevel cueStickLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 1, 0);
             CueStickLevel cueStickLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 1);
             cueStickLevels.Add(cueStickLevel1);
             cueStickLevels.Add(cueStickLevel2);
             cueStickLevels.Add(cueStickLevel3);
             cueStickLevels.Add(cueStickLevel4);
             cueStickLevels.Add(cueStickLevel5);

             CueStickProperties cueStickProperties = new CueStickProperties(1, 3, 0, 1, 9);
             CueStick cueStick = new CueStick(null, "victory cue", cueStickProperties);
             CueStick cueStick1 = new CueStick(null, "victory cue", "", "London", "", 0, 0,0,0,4,5, cueStickLevels, "Basic",0,0, cueStickProperties);

             print("Force" + cueStickProperties.Force + "  " + "Aim" + cueStickProperties.Aim + " " + "Spin" + cueStickProperties.Spin + " " + "Time" + cueStickProperties.Time);
             print("image" + cueStick.Image + " " + "name " + cueStick.Name);
             print("image" + cueStick1.Image + "name" + cueStick1.Name + "category" + cueStick1.Category + "city" + cueStick1.City + "price " + cueStick1.Price + "recharge price" +
                 cueStick1.RechargePrice + "recharge price in numbers" + cueStick1.RechargePriceInNumbers + "max level" +"unlocked pieces"+cueStick1.UnlockedPieces+"total pieces"+cueStick1.TotalPieces+
                   cueStick1.MaxLevel + "cue type" + cueStick1.CueType + "is unlocked" + cueStick1.IsUnlocked + "auto recharge" + cueStick1.AutoRecharge);
             for (int i = 0; i < cueStickLevels.Count; i++)
             {
                 print("level number" + i + cueStickLevels[i].LevelNumber);
                 print("max sub level" + i + cueStickLevels[i].MaxSubLevel);
                 print("current sub level" + i + cueStickLevels[i].CurrentSubLevel);
                 print("force upgrade" + i + cueStickLevels[i].ForceUpgradeValue);
                 print("aim upgrade" + i + cueStickLevels[i].AimUpgradeValue);
                 print("spin upgrade" + i + cueStickLevels[i].SpinUpgradeValue);
                 print("time upgrade" + i + cueStickLevels[i].TimeUpgradeValue);
             }*/

            StandardCueDetails();
            VictoryCueDetails();
            SurpriseCueDetails();
            CountryCueDetails();
            //print("standard count" + standardCueStickList.Count);
            //print("victory count" + victoryCueStickList.Count);
            //print("surprise count" + surpriseCueStickList.Count);
            //print("country count" + countryCueStickList.Count);
            //for(int i=0;i<standardCueStickList.Count;i++)
            //{
            //    print("standardCueStickList"+ standardCueStickList);
            //    if (standardCueStickList[i].Name == "Multimillionaire Cue")
            //    {
            //        print("price  " + standardCueStickList[i].PriceInNumbers);
            //        print("force  " + standardCueStickList[i].CueStickProperties.Force);
            //    }
            //}
            //VictoryCurDetails();
            CueStickProperties theBeginnerCueStickProperties = new CueStickProperties(0, 0, 0, 0, 0);
            CueStick theBeginnerCue = new CueStick(cueSprites[0], "Beginner Cue", "Owned", null, "0", 0, "0", 0, 0, 0, 0, null, null, 0, 0, theBeginnerCueStickProperties);
            ownedCueStickList.Add(theBeginnerCue);
            SetPlayerCueStick(ownedCueStickList[0].Name);
        }

        // Update is called once per frame
        void Update()
        {

        }

        /*public static CueStickDataManager Instance()
        {
            return instance;
        }*/

        public void SetCueStickData(Sprite image, string name, string category, string city, string price, long priceInNumbers, string rechargePrice,
            long rechargePriceInNumbers, int unlockedPieces, int totalPieces, int maxLevel, int[] cueStickMaxSubLevels, int[] forceUpgradeValues, int[] aimUpgradeValues,
            int[] spinUpgradeValues, int[] timeUpgradeValues, int currentLevelNumber, int currentSubLevelNumber,
            string cueType, int isUnlocked, int autoRecharge,
            int force, int aim, int spin, int time, int charge)
        {
            List<CueStickLevel> cueStickLevels = new List<CueStickLevel>();
            for (int i = 0; i < maxLevel; i++)
            {
                CueStickLevel cueStickLevel = new CueStickLevel(i + 1, cueStickMaxSubLevels[i], 0, forceUpgradeValues[i], aimUpgradeValues[i],
                    spinUpgradeValues[i], timeUpgradeValues[i]);
                cueStickLevels.Add(cueStickLevel);
            }
            CueStickProperties cueStickProperties =
                new CueStickProperties(force + forceUpgradeValues[currentLevelNumber], aim + aimUpgradeValues[currentLevelNumber],
                spin + spinUpgradeValues[currentLevelNumber], time + timeUpgradeValues[currentLevelNumber], charge);
            CueStick cueStick = new CueStick(image, name, category, city, price, priceInNumbers, rechargePrice, rechargePriceInNumbers,
                unlockedPieces, totalPieces,
                maxLevel, cueStickLevels, cueType, isUnlocked, autoRecharge, cueStickProperties);
            cueStick.CurrentLevel = cueStick.CueStickLevels[currentLevelNumber];
            cueStick.CurrentLevel.CurrentSubLevel = currentSubLevelNumber;
            if (category == "Standard")
            {
                standardCueStickList.Add(cueStick);
                if (isUnlocked == 1)
                {
                    ownedCueStickList.Add(cueStick);
                }
            }
            else if (category == "Victory")
            {
                victoryCueStickList.Add(cueStick);
                if (cueType == "Basic")
                {
                    victoryBasicCueStickList.Add(victoryCueStickList.Count - 1);
                }
                else if (cueType == "Advanced")
                {
                    victoryAdvancedCueStickList.Add(victoryCueStickList.Count - 1);
                }
                else if (cueType == "Expert")
                {
                    victoryExpertCueStickList.Add(victoryCueStickList.Count - 1);
                }
                if (isUnlocked == 1)
                {
                    ownedCueStickList.Add(cueStick);
                }
            }
            else if (category == "Surprise")
            {
                surpriseCueStickList.Add(cueStick);
                if (cueType == "Rare")
                {
                    surpriseRareCueStickList.Add(surpriseCueStickList.Count - 1);
                }
                else if (cueType == "Epic")
                {
                    surpriseEpicCueStickList.Add(surpriseCueStickList.Count - 1);
                }
                else if (cueType == "Legendary")
                {
                    surpriseLegendaryCueStickList.Add(surpriseCueStickList.Count - 1);
                }
                if (isUnlocked == 1)
                {
                    ownedCueStickList.Add(cueStick);
                }
            }
            else if (category == "Country")
            {
                countryCueStickList.Add(cueStick);
                if (isUnlocked == 1)
                {
                    countryUnlockedCueStickList.Add(countryCueStickList.Count - 1);
                    ownedCueStickList.Add(cueStick);
                }
            }
        }

        //Standard cues
        public int GetStandardCueStickCount()
        {
            return standardCueStickList.Count;
        }

        public void ObtainedStandardCueStick(int index)
        {
            UsePlayerCueStick(standardCueStickList[index]);
            SetStandardCueStickIsUnlockedFlag(index, 1);
            AddOwnedCueStick(standardCueStickList[index]);
            standardCueStickList.RemoveAt(index);
        }

        public Sprite GetStandardCueStickImage(int index)
        {
            return standardCueStickList[index].Image;
        }

        public string GetStandardCueStickName(int index)
        {
            return standardCueStickList[index].Name;
        }

        public string GetStandardCueStickCategory(int index)
        {
            return standardCueStickList[index].Category;
        }

        public string GetStandardCueStickPrice(int index)
        {
            return standardCueStickList[index].Price;
        }

        public long GetStandardCueStickPriceInNumber(int index)
        {
            return standardCueStickList[index].PriceInNumbers;
        }

        public string GetStandardCueStickRechargePrice(int index)
        {
            return standardCueStickList[index].RechargePrice;
        }

        public long GetStandardCueStickRechargePriceInNumbers(int index)
        {
            return standardCueStickList[index].RechargePriceInNumbers;
        }

        public int GetStandardCueStickCurrentLevelNumber(int index)
        {
            return standardCueStickList[index].CurrentLevel.LevelNumber;
        }

        public int GetStandardCueStickCurrentMaxSubLevel(int index)
        {
            return standardCueStickList[index].CurrentLevel.MaxSubLevel;
        }

        public int GetStandardCueStickCurrentSubLevel(int index)
        {
            return standardCueStickList[index].CurrentLevel.CurrentSubLevel;
        }

        public int GetStandardCueStickMaxLevel(int index)
        {
            return standardCueStickList[index].MaxLevel;
        }

        public string GetStandardCueStickType(int index)
        {
            return standardCueStickList[index].CueType;
        }

        public int GetStandardCueStickForce(int index)
        {
            return standardCueStickList[index].CueStickProperties.Force;
        }

        public int GetStandardCueStickAim(int index)
        {
            return standardCueStickList[index].CueStickProperties.Aim;
        }

        public int GetStandardCueStickSpin(int index)
        {
            return standardCueStickList[index].CueStickProperties.Spin;
        }

        public int GetStandardCueStickTime(int index)
        {
            return standardCueStickList[index].CueStickProperties.Time;
        }

        public int GetStandardCueStickIsUnlockedFlag(int index)
        {
            return standardCueStickList[index].IsUnlocked;
        }

        public void SetStandardCueStickIsUnlockedFlag(int index, int flag)
        {
            standardCueStickList[index].IsUnlocked = flag;
        }

        public int GetStandardCueStickCharge(int index)
        {
            return standardCueStickList[index].CueStickProperties.Charge;
        }

        //Victory cues
        public int GetVictoryCueStickCount()
        {
            return victoryCueStickList.Count;
        }

        public void ObtainedVictoryCueStick(int index)
        {
            UsePlayerCueStick(victoryCueStickList[index]);
            AddOwnedCueStick(victoryCueStickList[index]);
            //standardCueStickList.RemoveAt(index);
        }

        public int GetVictoryBasicCueStickIndex()
        {
            return victoryBasicCueStickList[Random.Range(0, victoryBasicCueStickList.Count)];
        }

        public int GetVictoryAdvancedCueStickIndex()
        {
            return victoryAdvancedCueStickList[Random.Range(0, victoryAdvancedCueStickList.Count)];
        }

        public int GetVictoryExpertCueStickIndex()
        {
            return victoryExpertCueStickList[Random.Range(0, victoryExpertCueStickList.Count)];
        }

        public Sprite GetVictoryCueStickImage(int index)
        {
            return victoryCueStickList[index].Image;
        }

        public string GetVictoryCueStickName(int index)
        {
            return victoryCueStickList[index].Name;
        }

        public string GetVictoryCueStickCategory(int index)
        {
            return victoryCueStickList[index].Category;
        }

        public string GetVictoryCueStickCity(int index)
        {
            return victoryCueStickList[index].City;
        }

        public string GetVictoryCueStickRechargePrice(int index)
        {
            return victoryCueStickList[index].RechargePrice;
        }

        public long GetVictoryCueStickRechargePriceInNumbers(int index)
        {
            return victoryCueStickList[index].RechargePriceInNumbers;
        }

        public string GetVictoryCueStickType(int index)
        {
            return victoryCueStickList[index].CueType;
        }

        public int GetVictoryCueStickUnlockedPieces(int index)
        {
            return victoryCueStickList[index].UnlockedPieces;
        }

        public void SetVictoryCueStickUnlockedPieces(int index, int val)
        {
            victoryCueStickList[index].UnlockedPieces = val;
        }

        public int GetVictoryCueStickTotalPieces(int index)
        {
            return victoryCueStickList[index].TotalPieces;
        }

        public int GetVictoryCueStickForce(int index)
        {
            return victoryCueStickList[index].CueStickProperties.Force;
        }

        public int GetVictoryCueStickAim(int index)
        {
            return victoryCueStickList[index].CueStickProperties.Aim;
        }

        public int GetVictoryCueStickSpin(int index)
        {
            return victoryCueStickList[index].CueStickProperties.Spin;
        }

        public int GetVictoryCueStickTime(int index)
        {
            return victoryCueStickList[index].CueStickProperties.Time;
        }

        public int GetVictoryCueStickCharge(int index)
        {
            return victoryCueStickList[index].CueStickProperties.Charge;
        }

        public int GetVictoryCueStickAutoRecharge(int index)
        {
            return victoryCueStickList[index].AutoRecharge;
        }

        public int GetVictoryCueStickIsUnlockedFlag(int index)
        {
            return victoryCueStickList[index].IsUnlocked;
        }

        public void SetVictoryCueStickIsUnlockedFlag(int index, int flag)
        {
            print(victoryCueStickList[index].Name);
            victoryCueStickList[index].IsUnlocked = flag;
        }

        public int GetVictoryCueStickCurrentLevel(int index)
        {
            return victoryCueStickList[index].CurrentLevel.LevelNumber;
        }

        public int GetVictoryCueStickCurrentSubLevel(int index)
        {
            return victoryCueStickList[index].CurrentLevel.CurrentSubLevel;
        }

        public void SetVictoryCueStickCurrentSubLevel(int index, int level)
        {
            victoryCueStickList[index].CurrentLevel.CurrentSubLevel = level;
        }

        public int GetVictoryCueStickMaxSubLevel(int index)
        {
            return victoryCueStickList[index].CurrentLevel.MaxSubLevel;
        }

        public int GetVictoryCueStickMaxLevel(int index)
        {
            return victoryCueStickList[index].MaxLevel;
        }

        public void SetVictoryCueStickCurrentLevel(int index, int level)
        {
            if (level <= victoryCueStickList[index].MaxLevel)
            {
                victoryCueStickList[index].CurrentLevel = victoryCueStickList[index].CueStickLevels[level - 1];
                victoryCueStickList[index].CueStickProperties.Force += victoryCueStickList[index].CurrentLevel.ForceUpgradeValue;
                victoryCueStickList[index].CueStickProperties.Aim += victoryCueStickList[index].CurrentLevel.AimUpgradeValue;
                victoryCueStickList[index].CueStickProperties.Spin += victoryCueStickList[index].CurrentLevel.SpinUpgradeValue;
                victoryCueStickList[index].CueStickProperties.Time += victoryCueStickList[index].CurrentLevel.TimeUpgradeValue;
            }
            else
            {
                victoryCueStickList[index].CurrentLevel.LevelNumber = victoryCueStickList[index].MaxLevel + 1;
            }
        }

        public void ResetVictoryCueStickCharge(int index)
        {
            victoryCueStickList[index].CueStickProperties.Charge = 50;
        }

        public void SetVictoryCueStickAutoRecharge(int index, int val)
        {
            victoryCueStickList[index].AutoRecharge = val;
        }

        //Surprise cues
        public int GetSurpriseCueStickCount()
        {
            return surpriseCueStickList.Count;
        }

        public int GetSurpriseRareCueStickIndex()
        {
            return surpriseRareCueStickList[Random.Range(0, surpriseRareCueStickList.Count)];
        }

        public int GetSurpriseEpicCueStickIndex()
        {
            return surpriseEpicCueStickList[Random.Range(0, surpriseEpicCueStickList.Count)];
        }

        public int GetSurpriseLegendaryCueStickIndex()
        {
            return surpriseLegendaryCueStickList[Random.Range(0, surpriseLegendaryCueStickList.Count)];
        }

        public void ObtainedSurpriseCueStick(int index)
        {
            UsePlayerCueStick(surpriseCueStickList[index]);
            AddOwnedCueStick(surpriseCueStickList[index]);
            //standardCueStickList.RemoveAt(index);
        }

        public Sprite GetSurpriseCueStickImage(int index)
        {
            return surpriseCueStickList[index].Image;
        }

        public string GetSurpriseCueStickName(int index)
        {
            return surpriseCueStickList[index].Name;
        }

        public string GetSurpriseCueStickCategory(int index)
        {
            return surpriseCueStickList[index].Category;
        }

        public string GetSurpriseCueStickPrice(int index)
        {
            return surpriseCueStickList[index].Price;
        }

        public long GetSurpriseCueStickPriceInNumber(int index)
        {
            return surpriseCueStickList[index].PriceInNumbers;
        }

        public string GetSurpriseCueStickRechargePrice(int index)
        {
            return surpriseCueStickList[index].RechargePrice;
        }

        public long GetSurpriseCueStickRechargePriceInNumbers(int index)
        {
            return surpriseCueStickList[index].RechargePriceInNumbers;
        }

        public string GetSurpriseCueStickType(int index)
        {
            return surpriseCueStickList[index].CueType;
        }

        public int GetSurpriseCueStickUnlockedPieces(int index)
        {
            return surpriseCueStickList[index].UnlockedPieces;
        }

        public void SetSurpriseCueStickUnlockedPieces(int index, int val)
        {
            surpriseCueStickList[index].UnlockedPieces = val;
        }

        public int GetSurpriseCueStickTotalPieces(int index)
        {
            return surpriseCueStickList[index].TotalPieces;
        }

        public int GetSurpriseCueStickForce(int index)
        {
            return surpriseCueStickList[index].CueStickProperties.Force;
        }

        public int GetSurpriseCueStickAim(int index)
        {
            return surpriseCueStickList[index].CueStickProperties.Aim;
        }

        public int GetSurpriseCueStickSpin(int index)
        {
            return surpriseCueStickList[index].CueStickProperties.Spin;
        }

        public int GetSurpriseCueStickTime(int index)
        {
            return surpriseCueStickList[index].CueStickProperties.Time;
        }

        public int GetSurpriseCueStickCharge(int index)
        {
            return surpriseCueStickList[index].CueStickProperties.Charge;
        }

        public void ResetSurpriseCueStickCharge(int index)
        {
            surpriseCueStickList[index].CueStickProperties.Charge = 50;
        }

        public int GetSurpriseCueStickAutoRecharge(int index)
        {
            return surpriseCueStickList[index].AutoRecharge;
        }

        public void SetSurpriseCueStickAutoRecharge(int index, int val)
        {
            surpriseCueStickList[index].AutoRecharge = val;
        }

        public int GetSurpriseCueStickIsUnlockedFlag(int index)
        {
            return surpriseCueStickList[index].IsUnlocked;
        }

        public void SetSurpriseCueStickIsUnlockedFlag(int index, int flag)
        {
            print(surpriseCueStickList[index].Name);
            surpriseCueStickList[index].IsUnlocked = flag;
        }

        public int GetSurpriseCueStickCurrentLevel(int index)
        {
            return surpriseCueStickList[index].CurrentLevel.LevelNumber;
        }

        public int GetSurpriseCueStickCurrentSubLevel(int index)
        {
            return surpriseCueStickList[index].CurrentLevel.CurrentSubLevel;
        }

        public void SetSurpriseCueStickCurrentSubLevel(int index, int level)
        {
            surpriseCueStickList[index].CurrentLevel.CurrentSubLevel = level;
        }

        public int GetSurpriseCueStickMaxSubLevel(int index)
        {
            return surpriseCueStickList[index].CurrentLevel.MaxSubLevel;
        }

        public int GetSurpriseCueStickMaxLevel(int index)
        {
            return surpriseCueStickList[index].MaxLevel;
        }

        public void SetSurpriseCueStickCurrentLevel(int index, int level)
        {
            //surpriseCueStickList[index].CurrentLevel.LevelNumber = level;

            if (level <= surpriseCueStickList[index].MaxLevel)
            {
                surpriseCueStickList[index].CurrentLevel = surpriseCueStickList[index].CueStickLevels[level - 1];
                surpriseCueStickList[index].CueStickProperties.Force += surpriseCueStickList[index].CurrentLevel.ForceUpgradeValue;
                surpriseCueStickList[index].CueStickProperties.Aim += surpriseCueStickList[index].CurrentLevel.AimUpgradeValue;
                surpriseCueStickList[index].CueStickProperties.Spin += surpriseCueStickList[index].CurrentLevel.SpinUpgradeValue;
                surpriseCueStickList[index].CueStickProperties.Time += surpriseCueStickList[index].CurrentLevel.TimeUpgradeValue;
            }
            else
            {
                surpriseCueStickList[index].CurrentLevel.LevelNumber = surpriseCueStickList[index].MaxLevel + 1;
            }
        }


        //Country cues
        public int GetCountryCueStickCount()
        {
            return countryCueStickList.Count;
        }

        public int GetCountryAdvancedCueStickIndex()
        {
            return countryUnlockedCueStickList[Random.Range(0, countryUnlockedCueStickList.Count)];
        }

        public void ObtainedCountryCueStick(int index)
        {
            countryUnlockedCueStickList.Add(index);
            UsePlayerCueStick(countryCueStickList[index]);
            SetCountryCueStickCurrentLevel(index, 1);
            SetCountryCueStickCurrentSubLevel(index, 0);
            SetCountryCueStickIsUnlockedFlag(index, 1);
            AddOwnedCueStick(countryCueStickList[index]);
            //standardCueStickList.RemoveAt(index);
        }

        public Sprite GetCountryCueStickImage(int index)
        {
            return countryCueStickList[index].Image;
        }

        public string GetCountryCueStickName(int index)
        {
            return countryCueStickList[index].Name;
        }

        public string GetCountryCueStickCategory(int index)
        {
            return countryCueStickList[index].Category;
        }

        public string GetCountryCueStickCity(int index)
        {
            return countryCueStickList[index].City;
        }

        public string GetCountryCueStickPrice(int index)
        {
            return countryCueStickList[index].Price;
        }

        public long GetCountryCueStickPriceInNumber(int index)
        {
            return countryCueStickList[index].PriceInNumbers;
        }

        public string GetCountryCueStickRechargePrice(int index)
        {
            return countryCueStickList[index].RechargePrice;
        }

        public long GetCountryCueStickRechargePriceInNumbers(int index)
        {
            return countryCueStickList[index].RechargePriceInNumbers;
        }

        public string GetCountryCueStickType(int index)
        {
            return countryCueStickList[index].CueType;
        }

        public int GetCountryCueStickAutoRecharge(int index)
        {
            return countryCueStickList[index].AutoRecharge;
        }

        public int GetCountryCueStickUnlockedPieces(int index)
        {
            return countryCueStickList[index].UnlockedPieces;
        }

        public void SetCountryCueStickUnlockedPieces(int index, int val)
        {
            countryCueStickList[index].UnlockedPieces = val;
        }

        public int GetCountryCueStickTotalPieces(int index)
        {
            return countryCueStickList[index].TotalPieces;
        }

        public int GetCountryCueStickForce(int index)
        {
            return countryCueStickList[index].CueStickProperties.Force;
        }

        public int GetCountryCueStickAim(int index)
        {
            return countryCueStickList[index].CueStickProperties.Aim;
        }

        public int GetCountryCueStickSpin(int index)
        {
            return countryCueStickList[index].CueStickProperties.Spin;
        }

        public int GetCountryCueStickTime(int index)
        {
            return countryCueStickList[index].CueStickProperties.Time;
        }

        public int GetCountryCueStickCharge(int index)
        {
            return countryCueStickList[index].CueStickProperties.Charge;
        }

        public void ResetCountryCueStickCharge(int index)
        {
            countryCueStickList[index].CueStickProperties.Charge = 50;
        }

        public void SetCountryCueStickAutoRecharge(int index, int val)
        {
            countryCueStickList[index].AutoRecharge = val;
        }

        public int GetCountryCueStickIsUnlockedFlag(int index)
        {
            return countryCueStickList[index].IsUnlocked;
        }

        public void SetCountryCueStickIsUnlockedFlag(int index, int flag)
        {
            countryCueStickList[index].IsUnlocked = flag;
        }

        public int GetCountryCueStickCurrentLevel(int index)
        {
            return countryCueStickList[index].CurrentLevel.LevelNumber;
        }

        public int GetCountryCueStickCurrentSubLevel(int index)
        {
            return countryCueStickList[index].CurrentLevel.CurrentSubLevel;
        }

        public void SetCountryCueStickCurrentSubLevel(int index, int level)
        {
            countryCueStickList[index].CurrentLevel.CurrentSubLevel = level;
        }

        public int GetCountryCueStickMaxSubLevel(int index)
        {
            return countryCueStickList[index].CurrentLevel.MaxSubLevel;
        }

        public int GetCountryCueStickMaxLevel(int index)
        {
            return countryCueStickList[index].MaxLevel;
        }

        public void SetCountryCueStickCurrentLevel(int index, int level)
        {
            //countryCueStickList[index].CurrentLevel.LevelNumber = level;
            if (level <= countryCueStickList[index].MaxLevel)
            {
                countryCueStickList[index].CurrentLevel = countryCueStickList[index].CueStickLevels[level - 1];
                countryCueStickList[index].CueStickProperties.Force += countryCueStickList[index].CurrentLevel.ForceUpgradeValue;
                countryCueStickList[index].CueStickProperties.Aim += countryCueStickList[index].CurrentLevel.AimUpgradeValue;
                countryCueStickList[index].CueStickProperties.Spin += countryCueStickList[index].CurrentLevel.SpinUpgradeValue;
                countryCueStickList[index].CueStickProperties.Time += countryCueStickList[index].CurrentLevel.TimeUpgradeValue;
            }
            else
            {
                countryCueStickList[index].CurrentLevel.LevelNumber = countryCueStickList[index].MaxLevel + 1;
            }

        }

        //Owned cues
        public int GetOwnedCueStickCount()
        {
            return ownedCueStickList.Count;
        }

        private void AddOwnedCueStick(CueStick cueStick)
        {
            List<CueStick> emptyCueStickList = new List<CueStick>();
            for (int i = 0; i < ownedCueStickList.Count; i++)
            {
                emptyCueStickList.Add(ownedCueStickList[i]);
            }
            ownedCueStickList.Clear();
            ownedCueStickList.Add(cueStick);
            for (int i = 0; i < emptyCueStickList.Count; i++)
            {
                ownedCueStickList.Add(emptyCueStickList[i]);
            }
            emptyCueStickList.Clear();
            //ownedCueStickList.Add(cueStick);
        }

        public Sprite GetOwnedCueStickImage(int index)
        {
            return ownedCueStickList[index].Image;
        }

        public string GetOwnedCueStickName(int index)
        {
            return ownedCueStickList[index].Name;
        }

        public string GetOwnedCueStickCategory(int index)
        {
            return ownedCueStickList[index].Category;
        }

        public string GetOwnedCueStickPrice(int index)
        {
            return ownedCueStickList[index].Price;
        }

        public long GetOwnedCueStickPriceInNumber(int index)
        {
            return ownedCueStickList[index].PriceInNumbers;
        }

        public string GetOwnedCueStickRechargePrice(int index)
        {
            return ownedCueStickList[index].RechargePrice;
        }

        public long GetOwnedCueStickRechargePriceInNumbers(int index)
        {
            return ownedCueStickList[index].RechargePriceInNumbers;
        }

        public int GetOwnedCueStickIsUnlockedFlag(int index)
        {
            return ownedCueStickList[index].IsUnlocked;
        }

        public string GetOwnedCueStickType(int index)
        {
            return ownedCueStickList[index].CueType;
        }

        public int GetOwnedCueStickAutoRecharge(int index)
        {
            return ownedCueStickList[index].AutoRecharge;
        }

        public int GetOwnedCueStickForce(int index)
        {
            return ownedCueStickList[index].CueStickProperties.Force;
        }

        public int GetOwnedCueStickAim(int index)
        {
            return ownedCueStickList[index].CueStickProperties.Aim;
        }

        public int GetOwnedCueStickSpin(int index)
        {
            return ownedCueStickList[index].CueStickProperties.Spin;
        }

        public int GetOwnedCueStickTime(int index)
        {
            return ownedCueStickList[index].CueStickProperties.Time;
        }

        public int GetOwnedCueStickCharge(int index)
        {
            return ownedCueStickList[index].CueStickProperties.Charge;
        }

        public int GetOwnedCueStickCurrentLevel(int index)
        {
            return ownedCueStickList[index].CurrentLevel.LevelNumber;
        }

        public int GetOwnedCueStickCurrentSubLevel(int index)
        {
            return ownedCueStickList[index].CurrentLevel.CurrentSubLevel;
        }

        public int GetOwnedCueStickMaxLevel(int index)
        {
            return ownedCueStickList[index].MaxLevel;
        }

        public int GetOwnedCueStickMaxSubLevel(int index)
        {
            return ownedCueStickList[index].CurrentLevel.MaxSubLevel;
        }

        public void SetOwnedCueStickCurrentLevel(int index, int level)
        {
            ownedCueStickList[index].CurrentLevel.LevelNumber = level;
        }

        public void ResetOwnedCueStickCharge(int index)
        {
            ownedCueStickList[index].CueStickProperties.Charge = 50;
        }

        public void SetOwnedCueStickAutoRecharge(int index, int val)
        {
            ownedCueStickList[index].AutoRecharge = val;
        }

        private void UsePlayerCueStick(CueStick cueStick)
        {
            playerCueStick = cueStick;
        }

        public void SetPlayerCueStick(string name)
        {
            for (int i = 0; i < ownedCueStickList.Count; i++)
            {
                if (name == ownedCueStickList[i].Name)
                {
                    //playerCueStick = new CueStick(cueStickList[i].Name, cueStickList[i].Category, cueStickList[i].Price,
                    //    cueStickList[i].PriceInNumbers, cueStickList[i].RechargePrice, cueStickList[i].UnlockedPieces, cueStickList[i].TotalPieces,
                    //    cueStickList[i].Level, cueStickList[i].SubLevel, cueStickList[i].CueType, cueStickList[i].CueStickProperties.Force,
                    //    cueStickList[i].CueStickProperties.Spin, cueStickList[i].CueStickProperties.Aim, cueStickList[i].CueStickProperties.Time,
                    //    cueStickList[i].Name);
                    playerCueStick = ownedCueStickList[i];
                    break;
                }
            }
        }

        public int GetPlayerCueStick()
        {
            for (int i = 0; i < ownedCueStickList.Count; i++)
            {
                if (playerCueStick.Name == ownedCueStickList[i].Name)
                {
                    //playerCueStick = new CueStick(cueStickList[i].Name, cueStickList[i].Category, cueStickList[i].Price,
                    //    cueStickList[i].PriceInNumbers, cueStickList[i].RechargePrice, cueStickList[i].UnlockedPieces, cueStickList[i].TotalPieces,
                    //    cueStickList[i].Level, cueStickList[i].SubLevel, cueStickList[i].CueType, cueStickList[i].CueStickProperties.Force,
                    //    cueStickList[i].CueStickProperties.Spin, cueStickList[i].CueStickProperties.Aim, cueStickList[i].CueStickProperties.Time,
                    //    cueStickList[i].Name);

                    return i;
                }
            }
            return 0;
        }

        public string GetPlayerCueStickName()
        {
            return playerCueStick.Name;
        }

        public void ReducePlayerCueStickCharge()
        {
            playerCueStick.CueStickProperties.Charge -= 1;
        }

        public void ResetPlayerCueStickCharge()
        {
            playerCueStick.CueStickProperties.Charge = 50;
        }

        public Sprite GetPlayerCueStickSprite()
        {
            return playerCueStick.Image;
        }

        public int GetForce()
        {
            return playerCueStick.CueStickProperties.Force;
        }

        public int GetSpin()
        {
            return playerCueStick.CueStickProperties.Spin;
        }

        public int GetAim()
        {
            return playerCueStick.CueStickProperties.Aim;
        }

        public int GetTime()
        {
            return playerCueStick.CueStickProperties.Time;
        }

        public int GetCharge()
        {
            return playerCueStick.CueStickProperties.Charge;
        }

        public int GetAutoRecharge()
        {
            return playerCueStick.AutoRecharge;
        }

        public long GetRechargePriceInNumbers()
        {
            return playerCueStick.RechargePriceInNumbers;
        }


        public int TapToAimFlag { get => cueStickLocalData.tapToAimFlag; set => cueStickLocalData.tapToAimFlag = value; }
        public int DisableGuidelineInLocalFlag { get => cueStickLocalData.disableGuidelineInLocalFlag; set => cueStickLocalData.disableGuidelineInLocalFlag = value; }
        public int AimingWheelFlag { get => cueStickLocalData.aimingWheelFlag; set => cueStickLocalData.aimingWheelFlag = value; }
        public int CueSensitivity { get => cueStickLocalData.cueSensitivity; set => cueStickLocalData.cueSensitivity = value; }
        public int PowerBarLocationFlag { get => cueStickLocalData.powerBarLocationFlag; set => cueStickLocalData.powerBarLocationFlag = value; }
        public int PowerBarOrientationFlag { get => cueStickLocalData.powerBarOrientationFlag; set => cueStickLocalData.powerBarOrientationFlag = value; }

        //standard cues data details

        public void StandardCueDetails()
        {
            /*CueStickProperties lunarCueStickProperties = new CueStickProperties(4,4,5,7,50);
            CueStick lunarNewYearCue = new CueStick(cueSprites[0], "Lunar New Year Cue","Standard", null, null, 0, null, 0, 0, 0, 0, null, null, 0,0, lunarCueStickProperties);
            standardCueStickList.Add(lunarNewYearCue);*/
            CueStickProperties theGunmanCueStickProperties = new CueStickProperties(4,2,3,0,50);
            CueStick theGunmanCue = new CueStick(cueSprites[1], "The Gunman Cue", "Standard", null, "1.8K", 1800, "175", 175, 0, 0, 0, null, null, 0, 0, theGunmanCueStickProperties);
            standardCueStickList.Add(theGunmanCue);
            CueStickProperties standardCueStickProperties = new CueStickProperties(3, 3, 3, 0, 50);
            CueStick standardCue = new CueStick(cueSprites[2], "Standard Cue", "Standard", null, "2k", 2000, "200", 200, 0, 0, 0, null, null, 0, 0, standardCueStickProperties);
            standardCueStickList.Add(standardCue);
            CueStickProperties millionarieCueStickProperties = new CueStickProperties(8, 7, 6, 3, 50);
            CueStick millionarieCue = new CueStick(cueSprites[3], "Millionarie Cue", "Standard", null, "1M", 1000000, "100K", 100000, 0, 0, 0, null, null, 0, 0, millionarieCueStickProperties);
            standardCueStickList.Add(millionarieCue);
            CueStickProperties multimillionaireCueStickProperties = new CueStickProperties(9,7,8,4,50);
            CueStick multimillionaireCue = new CueStick(cueSprites[4], "Multimillionaire Cue", "Standard", null, "5M", 5000000, "350K", 350000, 0, 0, 0, null, null, 0, 0, multimillionaireCueStickProperties);
            standardCueStickList.Add(multimillionaireCue);
            CueStickProperties billionaireCueStickProperties = new CueStickProperties(8, 9, 8, 5, 50);
            CueStick billionaireCue = new CueStick(cueSprites[0], "Billionaire Cue","Standard",null,"1B",1000000000,"5M",5000000,0,0,0,null,null,0,0, billionaireCueStickProperties);
            standardCueStickList.Add(billionaireCue);
            CueStickProperties billionCueStickProperties = new CueStickProperties(9,8,7,7,50);
            CueStick billionCue = new CueStick(cueSprites[1], "Billion Cue", "Standard", null, "1.5B", 1500000000, "7.5M", 7500000, 0, 0, 0, null, null, 0, 0, billionCueStickProperties);
            standardCueStickList.Add(billionCue);

            for(int i = 0;i<standardCueStickList.Count;i++)
            {
                if(GetStandardCueStickIsUnlockedFlag(i) == 1)
                {
                    ownedCueStickList.Add(standardCueStickList[i]);
                    standardCueStickList.RemoveAt(i);
                }
            }
        }
        //Victory Cue Data details

        public void VictoryCueDetails()
        {
            //Victory Cue
            CueStickProperties victoryCueStickProperties = new CueStickProperties(1, 3, 0, 1, 9);
            List<CueStickLevel> victoryCueStickLevels = new List<CueStickLevel>();
            CueStickLevel victoryLevel1 = new CueStickLevel(1, 6, 0, 1, 0, 0, 1);
            CueStickLevel victoryLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel victoryLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 1, 0);
            CueStickLevel victoryLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 1, 0);
            CueStickLevel victoryLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 1);
            victoryCueStickLevels.Add(victoryLevel1);
            victoryCueStickLevels.Add(victoryLevel2);
            victoryCueStickLevels.Add(victoryLevel3);
            victoryCueStickLevels.Add(victoryLevel4);
            victoryCueStickLevels.Add(victoryLevel5);
            CueStick victoryCue = new CueStick(cueSprites[1], "Victory Cue", "Victory", "London", "NA", 0, "0", 0, 0, 4, 5, victoryCueStickLevels, "Basic", 0, 0, victoryCueStickProperties);
            victoryCueStickList.Add(victoryCue);
            //Pine Cue
            CueStickProperties pineCueStickProperties = new CueStickProperties(2, 0, 1, 0, 50);
            List<CueStickLevel> pineCueStickLevels = new List<CueStickLevel>();
            CueStickLevel pineLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel pineLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 1, 0);
            CueStickLevel pineLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 0);
            CueStickLevel pineLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 1);
            CueStickLevel pineLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 1);
            pineCueStickLevels.Add(pineLevel1);
            pineCueStickLevels.Add(pineLevel2);
            pineCueStickLevels.Add(pineLevel3);
            pineCueStickLevels.Add(pineLevel4);
            pineCueStickLevels.Add(pineLevel5);
            CueStick pineCue = new CueStick(cueSprites[1], "Pine Cue", "Victory", "London", "NA", 0, "0", 0, 0, 4, 5, pineCueStickLevels, "Basic", 0, 0, pineCueStickProperties);
            victoryCueStickList.Add(pineCue);
            //Cow Cue
            CueStickProperties cowCueStickProperties = new CueStickProperties(1, 1, 1, 0, 50);
            List<CueStickLevel> cowCueStickLevels = new List<CueStickLevel>();
            CueStickLevel cowCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 1);
            CueStickLevel cowCueLevel2 = new CueStickLevel(2, 0, 0, 1, 0, 0, 1);
            CueStickLevel cowCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel cowCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 1, 0);
            CueStickLevel cowCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            cowCueStickLevels.Add(cowCueLevel1);
            cowCueStickLevels.Add(cowCueLevel2);
            cowCueStickLevels.Add(cowCueLevel3);
            cowCueStickLevels.Add(cowCueLevel4);
            cowCueStickLevels.Add(cowCueLevel5);
            CueStick cowCue = new CueStick(cueSprites[1], "Cow Cue", "Victory", "London", "NA", 0, "0", 0, 0, 4, 5, cowCueStickLevels, "Basic", 0, 0, cowCueStickProperties);
            victoryCueStickList.Add(cowCue);
            //Modern Cue
            CueStickProperties modernCueStickProperties = new CueStickProperties(3, 1, 0, 1, 50);
            List<CueStickLevel> modernCueStickLevels = new List<CueStickLevel>();
            CueStickLevel modernCueLevel1 = new CueStickLevel(1, 0, 0, 1, 1, 1, 0);
            CueStickLevel modernCueLevel2 = new CueStickLevel(2, 0, 0, 1, 0, 0, 0);
            CueStickLevel modernCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 1);
            CueStickLevel modernCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel modernCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 1, 0);
            modernCueStickLevels.Add(modernCueLevel1);
            modernCueStickLevels.Add(modernCueLevel2);
            modernCueStickLevels.Add(modernCueLevel3);
            modernCueStickLevels.Add(modernCueLevel4);
            modernCueStickLevels.Add(modernCueLevel5);
            CueStick modernCue = new CueStick(cueSprites[1], "Modern Cue", "Victory", "Sydney", "NA", 0, "0", 0, 0, 4, 5, modernCueStickLevels, "Basic", 0, 0, modernCueStickProperties);
            victoryCueStickList.Add(modernCue);
            //Royal Blue Cue
            CueStickProperties royalBlueCueStickProperties = new CueStickProperties(2, 4, 4, 0, 50);
            List<CueStickLevel> royalBlueCueStickLevels = new List<CueStickLevel>();
            CueStickLevel royalBlueCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel royalBlueCueLevel2 = new CueStickLevel(2, 0, 0, 1, 1, 0, 0);
            CueStickLevel royalBlueCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 1, 1);
            CueStickLevel royalBlueCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel royalBlueCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 1, 0);
            royalBlueCueStickLevels.Add(royalBlueCueLevel1);
            royalBlueCueStickLevels.Add(royalBlueCueLevel2);
            royalBlueCueStickLevels.Add(royalBlueCueLevel3);
            royalBlueCueStickLevels.Add(royalBlueCueLevel4);
            royalBlueCueStickLevels.Add(royalBlueCueLevel5);
            CueStick royalBlueCue = new CueStick(cueSprites[1], "Royal Blue Cue", "Victory", "Tokyo", "NA", 0, "220", 220, 0, 4, 5, royalBlueCueStickLevels, "Basic", 0, 0, royalBlueCueStickProperties);
            victoryCueStickList.Add(royalBlueCue);
            //Ash Cue
            CueStickProperties ashCueStickProperties = new CueStickProperties(5, 4, 7, 2, 50);
            List<CueStickLevel> ashCueStickLevels = new List<CueStickLevel>();
            CueStickLevel ashCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel ashCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel ashCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel ashCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel ashCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            ashCueStickLevels.Add(ashCueLevel1);
            ashCueStickLevels.Add(ashCueLevel2);
            ashCueStickLevels.Add(ashCueLevel3);
            ashCueStickLevels.Add(ashCueLevel4);
            ashCueStickLevels.Add(ashCueLevel5);
            CueStick ashCue = new CueStick(cueSprites[1], "Ash Cue", "Victory", "Shanghai", "NA", 0, "0", 0, 0, 4, 5, ashCueStickLevels, "Basic", 0, 0, ashCueStickProperties);
            victoryCueStickList.Add(ashCue);
            //Crocodile Cue
            CueStickProperties crocodileCueStickProperties = new CueStickProperties(3, 3, 5, 0, 12);
            List<CueStickLevel> crocodileCueStickLevels = new List<CueStickLevel>();
            CueStickLevel crocodileCueLevel1 = new CueStickLevel(1, 0, 0, 2, 2, 2, 1);
            CueStickLevel crocodileCueLevel2 = new CueStickLevel(2, 0, 0, 1, 1, 0, 3);
            CueStickLevel crocodileCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 1);
            CueStickLevel crocodileCueLevel4 = new CueStickLevel(4, 0, 0, 1, 1, 0, 1);
            CueStickLevel crocodileCueLevel5 = new CueStickLevel(5, 0, 0, 1, 1, 0, 0);
            crocodileCueStickLevels.Add(crocodileCueLevel1);
            crocodileCueStickLevels.Add(crocodileCueLevel2);
            crocodileCueStickLevels.Add(crocodileCueLevel3);
            crocodileCueStickLevels.Add(crocodileCueLevel4);
            crocodileCueStickLevels.Add(crocodileCueLevel5);
            CueStick crocodileCue = new CueStick(cueSprites[1], "Crocodile Cue", "Victory", "Tokyo", "NA", 0, "300", 300, 0, 4, 5, crocodileCueStickLevels, "Advanced", 0, 0, crocodileCueStickProperties);
            victoryCueStickList.Add(crocodileCue);
            //Elven Cue
            CueStickProperties elvenCueStickProperties = new CueStickProperties(4, 5, 4, 2, 50);
            List<CueStickLevel> elvenCueStickLevels = new List<CueStickLevel>();
            CueStickLevel elvenCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel elvenCueLevel2 = new CueStickLevel(2, 0, 0, 1, 0, 0, 1);
            CueStickLevel elvenCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 1);
            CueStickLevel elvenCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel elvenCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            elvenCueStickLevels.Add(elvenCueLevel1);
            elvenCueStickLevels.Add(elvenCueLevel2);
            elvenCueStickLevels.Add(elvenCueLevel3);
            elvenCueStickLevels.Add(elvenCueLevel4);
            elvenCueStickLevels.Add(elvenCueLevel5);
            CueStick elvenCue = new CueStick(cueSprites[1], "Elven Cue", "Victory", "Cairo", "NA", 0, "2.1K", 2100, 0, 4, 5, elvenCueStickLevels, "Advanced", 0, 0, elvenCueStickProperties);
            victoryCueStickList.Add(elvenCue);
            //Neon Cue
            CueStickProperties neonCueStickProperties = new CueStickProperties(6, 5, 4, 1, 50);
            List<CueStickLevel> neonCueStickLevels = new List<CueStickLevel>();
            CueStickLevel neonCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 2);
            CueStickLevel neonCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel neonCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel neonCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel neonCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            neonCueStickLevels.Add(neonCueLevel1);
            neonCueStickLevels.Add(neonCueLevel2);
            neonCueStickLevels.Add(neonCueLevel3);
            neonCueStickLevels.Add(neonCueLevel4);
            neonCueStickLevels.Add(neonCueLevel5);
            CueStick neonCue = new CueStick(cueSprites[1], "Neon Cue", "Victory", "Cairo", "NA", 0, "0", 0, 0, 4, 5, neonCueStickLevels, "Advanced", 0, 0, neonCueStickProperties);
            victoryCueStickList.Add(neonCue);
            //Pharaoh Cue
            CueStickProperties pharaohCueStickProperties = new CueStickProperties(5, 2, 4, 2, 50);
            List<CueStickLevel> pharaohCueStickLevels = new List<CueStickLevel>();
            CueStickLevel pharaohCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 1);
            CueStickLevel pharaohCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 1, 0);
            CueStickLevel pharaohCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel pharaohCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 1, 0);
            CueStickLevel pharaohCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            pharaohCueStickLevels.Add(pharaohCueLevel1);
            pharaohCueStickLevels.Add(pharaohCueLevel2);
            pharaohCueStickLevels.Add(pharaohCueLevel3);
            pharaohCueStickLevels.Add(pharaohCueLevel4);
            pharaohCueStickLevels.Add(pharaohCueLevel5);
            CueStick pharaohCue = new CueStick(cueSprites[1], "Pharaoh Cue", "Victory", "Jakarta", "NA", 0, "0", 0, 0, 4, 5, pharaohCueStickLevels, "Advanced", 0, 0, pharaohCueStickProperties);
            victoryCueStickList.Add(pharaohCue);
            //RoseWood Cue
            CueStickProperties rosewoodCueStickProperties = new CueStickProperties(1, 2, 2, 1, 50);
            List<CueStickLevel> rosewoodCueStickLevels = new List<CueStickLevel>();
            CueStickLevel rosewoodCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel rosewoodCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel rosewoodCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel rosewoodCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel rosewoodCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 1);
            rosewoodCueStickLevels.Add(rosewoodCueLevel1);
            rosewoodCueStickLevels.Add(rosewoodCueLevel2);
            rosewoodCueStickLevels.Add(rosewoodCueLevel3);
            rosewoodCueStickLevels.Add(rosewoodCueLevel4);
            rosewoodCueStickLevels.Add(rosewoodCueLevel5);
            CueStick rosewoodCue = new CueStick(cueSprites[1], "Rosewood Cue", "Victory", "Sydney", "NA", 0, "0", 0, 0, 4, 5, rosewoodCueStickLevels, "Basic", 0, 0, rosewoodCueStickProperties);
            victoryCueStickList.Add(rosewoodCue);
            //Traditional Cue
            CueStickProperties traditionalCueStickProperties = new CueStickProperties(0, 1, 2, 0, 50);
            List<CueStickLevel> traditionalCueStickLevels = new List<CueStickLevel>();
            CueStickLevel traditionalCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 1);
            CueStickLevel traditionalCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel traditionalCueLevel3 = new CueStickLevel(3, 0, 0, 1, 1, 0, 0);
            CueStickLevel traditionalCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel traditionalCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            traditionalCueStickLevels.Add(traditionalCueLevel1);
            traditionalCueStickLevels.Add(traditionalCueLevel2);
            traditionalCueStickLevels.Add(traditionalCueLevel3);
            traditionalCueStickLevels.Add(traditionalCueLevel4);
            traditionalCueStickLevels.Add(traditionalCueLevel5);
            CueStick traditionalCue = new CueStick(cueSprites[1], "Traditional Cue", "Victory", "London", "NA", 0, "0", 0, 0, 4, 5, traditionalCueStickLevels, "Basic", 0, 0, traditionalCueStickProperties);
            victoryCueStickList.Add(traditionalCue);
            //Majestic Cue
            CueStickProperties majesticCueStickProperties = new CueStickProperties(6, 5, 3, 5, 50);
            List<CueStickLevel> majesticCueStickLevels = new List<CueStickLevel>();
            CueStickLevel majesticCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel majesticCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel majesticCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel majesticCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel majesticCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 1, 0);
            pharaohCueStickLevels.Add(majesticCueLevel1);
            pharaohCueStickLevels.Add(majesticCueLevel2);
            pharaohCueStickLevels.Add(majesticCueLevel3);
            pharaohCueStickLevels.Add(majesticCueLevel4);
            pharaohCueStickLevels.Add(majesticCueLevel5);
            CueStick majesticCue = new CueStick(cueSprites[1], "Majestic Cue", "Victory", "Shanghai", "NA", 0, "0", 0, 0, 4, 5, majesticCueStickLevels, "Advanced", 0, 0, majesticCueStickProperties);
            victoryCueStickList.Add(majesticCue);
            //Antique Cue
            CueStickProperties antiqueCueStickProperties = new CueStickProperties(1, 0, 1, 3, 50);
            List<CueStickLevel> antiqueCueStickLevels = new List<CueStickLevel>();
            CueStickLevel antiqueCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel antiqueCueLevel2 = new CueStickLevel(2, 0, 0, 1, 0, 0, 0);
            CueStickLevel antiqueCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel antiqueCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel antiqueCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            antiqueCueStickLevels.Add(antiqueCueLevel1);
            antiqueCueStickLevels.Add(antiqueCueLevel2);
            antiqueCueStickLevels.Add(antiqueCueLevel3);
            antiqueCueStickLevels.Add(antiqueCueLevel4);
            antiqueCueStickLevels.Add(antiqueCueLevel5);
            CueStick antiqueCue = new CueStick(cueSprites[1], "Antique Cue", "Victory", "Sydney", "NA", 0, "0", 0, 0, 4, 5, antiqueCueStickLevels, "Basic", 0, 0, antiqueCueStickProperties);
            victoryCueStickList.Add(antiqueCue);
            //Composite Cue
            CueStickProperties compositeCueStickProperties = new CueStickProperties(2, 0, 0, 2, 50);
            List<CueStickLevel> compositeCueStickLevels = new List<CueStickLevel>();
            CueStickLevel compositeCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 1, 0);
            CueStickLevel compositeCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel compositeCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel compositeCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 1);
            CueStickLevel compositeCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            compositeCueStickLevels.Add(compositeCueLevel1);
            compositeCueStickLevels.Add(compositeCueLevel2);
            compositeCueStickLevels.Add(compositeCueLevel3);
            compositeCueStickLevels.Add(compositeCueLevel4);
            compositeCueStickLevels.Add(compositeCueLevel5);
            CueStick compositeCue = new CueStick(cueSprites[1], "Composite Cue", "Victory", "London", "NA", 0, "0", 0, 0, 4, 5, compositeCueStickLevels, "Basic", 0, 0, compositeCueStickProperties);
            victoryCueStickList.Add(compositeCue);
            //Black Hole Cue
            CueStickProperties blackHoleCueStickProperties = new CueStickProperties(8, 8, 9, 3, 50);
            List<CueStickLevel> blackHoleCueStickLevels = new List<CueStickLevel>();
            CueStickLevel blackHoleCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel blackHoleCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel blackHoleCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel blackHoleCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel blackHoleCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            blackHoleCueStickLevels.Add(blackHoleCueLevel1);
            blackHoleCueStickLevels.Add(blackHoleCueLevel2);
            blackHoleCueStickLevels.Add(blackHoleCueLevel3);
            blackHoleCueStickLevels.Add(blackHoleCueLevel4);
            blackHoleCueStickLevels.Add(blackHoleCueLevel5);
            CueStick blackHoleCue = new CueStick(cueSprites[1], "Blac Hole Cue", "Victory", "Mumbai", "NA", 0, "0", 0, 0, 4, 5, blackHoleCueStickLevels, "Expert", 0, 0, blackHoleCueStickProperties);
            victoryCueStickList.Add(blackHoleCue);
            //Czar cue
            CueStickProperties czarCueStickProperties = new CueStickProperties(6, 8, 6, 7, 50);
            List<CueStickLevel> czarCueStickLevels = new List<CueStickLevel>();
            CueStickLevel czarCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel czarCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel czarCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel czarCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel czarCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            czarCueStickLevels.Add(czarCueLevel1);
            czarCueStickLevels.Add(czarCueLevel2);
            czarCueStickLevels.Add(czarCueLevel3);
            czarCueStickLevels.Add(czarCueLevel4);
            czarCueStickLevels.Add(czarCueLevel5);
            CueStick czarCue = new CueStick(cueSprites[1], "Czar Cue", "Victory", "Mumbai", "NA", 0, "0", 0, 0, 4, 5, czarCueStickLevels, "Expert", 0, 0, czarCueStickProperties);
            victoryCueStickList.Add(czarCue);
            //Diamond Cue
            CueStickProperties diamondCueStickProperties = new CueStickProperties(8, 8, 6, 3, 50);
            List<CueStickLevel> diamondCueStickLevels = new List<CueStickLevel>();
            CueStickLevel diamondCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 1);
            CueStickLevel diamondCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel diamondCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 2);
            CueStickLevel diamondCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel diamondCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            diamondCueStickLevels.Add(diamondCueLevel1);
            diamondCueStickLevels.Add(diamondCueLevel2);
            diamondCueStickLevels.Add(diamondCueLevel3);
            diamondCueStickLevels.Add(diamondCueLevel4);
            diamondCueStickLevels.Add(diamondCueLevel5);
            CueStick diamondCue = new CueStick(cueSprites[1], "Diamond Cue", "Victory", "Seoul", "NA", 0, "0", 0, 0, 4, 5, diamondCueStickLevels, "Expert", 0, 0, diamondCueStickProperties);
            victoryCueStickList.Add(diamondCue);
            //Emerald Cue
            CueStickProperties emeraldCueStickProperties = new CueStickProperties(7, 6, 8, 8, 50);
            List<CueStickLevel> emeraldCueStickLevels = new List<CueStickLevel>();
            CueStickLevel emeraldCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel emeraldCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel emeraldCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel emeraldCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel emeraldCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            emeraldCueStickLevels.Add(emeraldCueLevel1);
            emeraldCueStickLevels.Add(emeraldCueLevel2);
            emeraldCueStickLevels.Add(emeraldCueLevel3);
            emeraldCueStickLevels.Add(emeraldCueLevel4);
            emeraldCueStickLevels.Add(emeraldCueLevel5);
            CueStick emeraldCue = new CueStick(cueSprites[1], "Emerald Cue", "Victory", "Mumbai", "NA", 0, "0", 0, 0, 4, 5, emeraldCueStickLevels, "Expert", 0, 0, emeraldCueStickProperties);
            victoryCueStickList.Add(emeraldCue);
            //Franken Cue 
            CueStickProperties frankenCueStickProperties = new CueStickProperties(7, 5, 7, 2, 50);
            List<CueStickLevel> frankenCueStickLevels = new List<CueStickLevel>();
            CueStickLevel frankenCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel frankenCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel frankenCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel frankenCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel frankenCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 1);
            frankenCueStickLevels.Add(frankenCueLevel1);
            frankenCueStickLevels.Add(frankenCueLevel2);
            frankenCueStickLevels.Add(frankenCueLevel3);
            frankenCueStickLevels.Add(frankenCueLevel4);
            frankenCueStickLevels.Add(frankenCueLevel5);
            CueStick frankenCue = new CueStick(cueSprites[1], "Franken Cue", "Victory", "Rome", "NA", 0, "0", 0, 0, 4, 5, frankenCueStickLevels, "Expert", 0, 0, frankenCueStickProperties);
            victoryCueStickList.Add(frankenCue);
            //Galaxy Cue
            CueStickProperties galaxyCueStickProperties = new CueStickProperties(9, 8, 9, 8, 50);
            List<CueStickLevel> galaxyCueStickLevels = new List<CueStickLevel>();
            CueStickLevel galaxyCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel galaxyCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel galaxyCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 0);
            CueStickLevel galaxyCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel galaxyCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            galaxyCueStickLevels.Add(galaxyCueLevel1);
            galaxyCueStickLevels.Add(galaxyCueLevel2);
            galaxyCueStickLevels.Add(galaxyCueLevel3);
            galaxyCueStickLevels.Add(galaxyCueLevel4);
            galaxyCueStickLevels.Add(galaxyCueLevel5);
            CueStick galaxyCue = new CueStick(cueSprites[1], "Galaxy Cue", "Victory", "Berlin", "NA", 0, "0", 0, 0, 4, 5, galaxyCueStickLevels, "Advanced", 0, 0, galaxyCueStickProperties);
            victoryCueStickList.Add(galaxyCue);
            //Gold  Cue
            CueStickProperties goldCueStickProperties = new CueStickProperties(7, 5, 8, 4, 50);
            List<CueStickLevel> goldCueStickLevels = new List<CueStickLevel>();
            CueStickLevel goldCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel goldCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel goldCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel goldCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel goldCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 1);
            goldCueStickLevels.Add(goldCueLevel1);
            goldCueStickLevels.Add(goldCueLevel2);
            goldCueStickLevels.Add(goldCueLevel3);
            goldCueStickLevels.Add(goldCueLevel4);
            goldCueStickLevels.Add(goldCueLevel5);
            CueStick goldCue = new CueStick(cueSprites[1], "Gold Cue", "Victory", "Seoul", "NA", 0, "0", 0, 0, 4, 5, goldCueStickLevels, "Expert", 0, 0, goldCueStickProperties);
            victoryCueStickList.Add(goldCue);
            //Banbkok Cue
            CueStickProperties iceCueStickProperties = new CueStickProperties(8, 5, 5, 5, 50);
            List<CueStickLevel> iceCueStickLevels = new List<CueStickLevel>();
            CueStickLevel iceCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 1, 1);
            CueStickLevel iceCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 1);
            CueStickLevel iceCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel iceCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 1, 1);
            CueStickLevel iceCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 1, 0);
            iceCueStickLevels.Add(iceCueLevel1);
            iceCueStickLevels.Add(iceCueLevel2);
            iceCueStickLevels.Add(iceCueLevel3);
            iceCueStickLevels.Add(iceCueLevel4);
            iceCueStickLevels.Add(iceCueLevel5);
            CueStick iceCue = new CueStick(cueSprites[1], "Ice Cue", "Victory", "Bangkok", "NA", 0, "0", 0, 0, 4, 5, iceCueStickLevels, "Expert", 0, 0, iceCueStickProperties);
            victoryCueStickList.Add(iceCue);
            //Jade Cue
            CueStickProperties jadeCueStickProperties = new CueStickProperties(5, 7, 6, 3, 50);
            List<CueStickLevel> jadeCueStickLevels = new List<CueStickLevel>();
            CueStickLevel jadeCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel jadeCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel jadeCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel jadeCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel jadeCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            jadeCueStickLevels.Add(jadeCueLevel1);
            jadeCueStickLevels.Add(jadeCueLevel2);
            jadeCueStickLevels.Add(jadeCueLevel3);
            jadeCueStickLevels.Add(jadeCueLevel4);
            jadeCueStickLevels.Add(jadeCueLevel5);
            CueStick jadeCue = new CueStick(cueSprites[1], "Jade Cue", "Victory", "Rome", "NA", 0, "0", 0, 0, 4, 5, jadeCueStickLevels, "Expert", 0, 0, jadeCueStickProperties);
            victoryCueStickList.Add(jadeCue);
            //Leopard Cue
            CueStickProperties leopardCueStickProperties = new CueStickProperties(7, 6, 5, 6, 50);
            List<CueStickLevel> leopardCueStickLevels = new List<CueStickLevel>();
            CueStickLevel leopardCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel leopardCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel leopardCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel leopardCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel leopardCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 1, 0);
            leopardCueStickLevels.Add(leopardCueLevel1);
            leopardCueStickLevels.Add(leopardCueLevel2);
            leopardCueStickLevels.Add(leopardCueLevel3);
            leopardCueStickLevels.Add(leopardCueLevel4);
            leopardCueStickLevels.Add(leopardCueLevel5);
            CueStick leopardCue = new CueStick(cueSprites[1], "Leopard Cue", "Victory", "Seoul", "NA", 0, "0", 0, 0, 4, 5, leopardCueStickLevels, "Expert", 0, 0, leopardCueStickProperties);
            victoryCueStickList.Add(leopardCue);
            //Lightining Cue
            CueStickProperties lightiningCueStickProperties = new CueStickProperties(9, 7, 7, 7, 50);
            List<CueStickLevel> lightiningCueStickLevels = new List<CueStickLevel>();
            CueStickLevel lightiningCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel lightiningCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel lightiningCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel lightiningCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel lightiningCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            lightiningCueStickLevels.Add(lightiningCueLevel1);
            lightiningCueStickLevels.Add(lightiningCueLevel2);
            lightiningCueStickLevels.Add(lightiningCueLevel3);
            lightiningCueStickLevels.Add(lightiningCueLevel4);
            lightiningCueStickLevels.Add(lightiningCueLevel5);
            CueStick lightiningCue = new CueStick(cueSprites[1], "Lightining Cue", "Victory", "Berlin", "NA", 0, "0", 0, 0, 4, 5, lightiningCueStickLevels, "Expert", 0, 0, lightiningCueStickProperties);
            victoryCueStickList.Add(lightiningCue);
            //Ninja Cue
            CueStickProperties ninjaCueStickProperties = new CueStickProperties(6, 5, 6, 3, 50);
            List<CueStickLevel> ninjaCueStickLevels = new List<CueStickLevel>();
            CueStickLevel ninjaCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel ninjaCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel ninjaCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel ninjaCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel ninjaCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            ninjaCueStickLevels.Add(ninjaCueLevel1);
            ninjaCueStickLevels.Add(ninjaCueLevel2);
            ninjaCueStickLevels.Add(ninjaCueLevel3);
            ninjaCueStickLevels.Add(ninjaCueLevel4);
            ninjaCueStickLevels.Add(ninjaCueLevel5);
            CueStick ninjaCue = new CueStick(cueSprites[1], "Ninja Cue", "Victory", "Rome", "NA", 0, "0", 0, 0, 4, 5, ninjaCueStickLevels, "Expert", 0, 0, ninjaCueStickProperties);
            victoryCueStickList.Add(ninjaCue);
            //Plainum Cue
            CueStickProperties platinumCueStickProperties = new CueStickProperties(7, 8, 9, 8, 50);
            List<CueStickLevel> platinumCueStickLevels = new List<CueStickLevel>();
            CueStickLevel platinumCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel platinumCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel platinumCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel platinumCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel platinumCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            platinumCueStickLevels.Add(platinumCueLevel1);
            platinumCueStickLevels.Add(platinumCueLevel2);
            platinumCueStickLevels.Add(platinumCueLevel3);
            platinumCueStickLevels.Add(platinumCueLevel4);
            platinumCueStickLevels.Add(platinumCueLevel5);
            CueStick platinumCue = new CueStick(cueSprites[1], "Platinum Cue", "Victory", "Berlin", "NA", 0, "0", 0, 0, 4, 5, platinumCueStickLevels, "Expert", 0, 0, platinumCueStickProperties);
            victoryCueStickList.Add(platinumCue);
            //Potter Cue
            CueStickProperties potterCueStickProperties = new CueStickProperties(8, 7, 8, 7, 50);
            List<CueStickLevel> potterCueStickLevels = new List<CueStickLevel>();
            CueStickLevel potterCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel potterCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel potterCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel potterCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel potterCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            potterCueStickLevels.Add(potterCueLevel1);
            potterCueStickLevels.Add(potterCueLevel2);
            potterCueStickLevels.Add(potterCueLevel3);
            potterCueStickLevels.Add(potterCueLevel4);
            potterCueStickLevels.Add(potterCueLevel5);
            CueStick potterCue = new CueStick(cueSprites[1], "Potter Cue", "Victory", "Berlin", "NA", 0, "0", 0, 0, 4, 5, potterCueStickLevels, "Expert", 0, 0, potterCueStickProperties);
            victoryCueStickList.Add(potterCue);
            //Shanghai Dragon Cue
            CueStickProperties shanghaiDragonCueStickProperties = new CueStickProperties(8, 7, 9, 7, 50);
            List<CueStickLevel> shanghaiDragonCueStickLevels = new List<CueStickLevel>();
            CueStickLevel shanghaiDragonCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel shanghaiDragonCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 0);
            CueStickLevel shanghaiDragonCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 0);
            CueStickLevel shanghaiDragonCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel shanghaiDragonCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 4);
            shanghaiDragonCueStickLevels.Add(shanghaiDragonCueLevel1);
            shanghaiDragonCueStickLevels.Add(shanghaiDragonCueLevel2);
            shanghaiDragonCueStickLevels.Add(shanghaiDragonCueLevel3);
            shanghaiDragonCueStickLevels.Add(shanghaiDragonCueLevel4);
            shanghaiDragonCueStickLevels.Add(shanghaiDragonCueLevel5);
            CueStick shanghaiDragonCue = new CueStick(cueSprites[1], "Shanghai Dragon Cue", "Victory", "Berlin", "NA", 0, "0", 0, 0, 4, 5, shanghaiDragonCueStickLevels, "Expert", 0, 0, shanghaiDragonCueStickProperties);
            victoryCueStickList.Add(shanghaiDragonCue);
            //Shark Cue
            CueStickProperties sharkCueStickProperties = new CueStickProperties(6, 7, 8, 7, 50);
            List<CueStickLevel> sharkCueStickLevels = new List<CueStickLevel>();
            CueStickLevel sharkCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel sharkCueLevel2 = new CueStickLevel(2, 0, 0, 1, 0, 0, 0);
            CueStickLevel sharkCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel sharkCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel sharkCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            sharkCueStickLevels.Add(sharkCueLevel1);
            sharkCueStickLevels.Add(sharkCueLevel2);
            sharkCueStickLevels.Add(sharkCueLevel3);
            sharkCueStickLevels.Add(sharkCueLevel4);
            sharkCueStickLevels.Add(sharkCueLevel5);
            CueStick sharkCue = new CueStick(cueSprites[1], "Shark Cue", "Victory", "Mumbai", "NA", 0, "0", 0, 0, 4, 5, sharkCueStickLevels, "Expert", 0, 0, sharkCueStickProperties);
            victoryCueStickList.Add(sharkCue);
            //Solar System
            CueStickProperties solarSystemCueStickProperties = new CueStickProperties(8, 5, 9, 4, 50);
            List<CueStickLevel> solarSystemCueStickLevels = new List<CueStickLevel>();
            CueStickLevel solarSystemCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel solarSystemCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel solarSystemCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel solarSystemCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel solarSystemCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            solarSystemCueStickLevels.Add(solarSystemCueLevel1);
            solarSystemCueStickLevels.Add(solarSystemCueLevel2);
            solarSystemCueStickLevels.Add(solarSystemCueLevel3);
            solarSystemCueStickLevels.Add(solarSystemCueLevel4);
            solarSystemCueStickLevels.Add(solarSystemCueLevel5);
            CueStick solaySystemCue = new CueStick(cueSprites[1], "Solar System Cue", "Victory", "Mumbai", "NA", 0, "0", 0, 0, 4, 5, solarSystemCueStickLevels, "Expert", 0, 0, solarSystemCueStickProperties);
            victoryCueStickList.Add(solaySystemCue);
            //The bounty Hunter
            CueStickProperties thebountyHunterCueStickProperties = new CueStickProperties(6, 5, 7, 4, 50);
            List<CueStickLevel> thebountyHunterCueStickLevels = new List<CueStickLevel>();
            CueStickLevel thebountyHunterCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel thebountyHunterCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel thebountyHunterCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel thebountyHunterCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel thebountyHunterCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 1, 0);
            thebountyHunterCueStickLevels.Add(thebountyHunterCueLevel1);
            thebountyHunterCueStickLevels.Add(thebountyHunterCueLevel2);
            thebountyHunterCueStickLevels.Add(thebountyHunterCueLevel3);
            thebountyHunterCueStickLevels.Add(thebountyHunterCueLevel4);
            thebountyHunterCueStickLevels.Add(thebountyHunterCueLevel5);
            CueStick thebountyHunterCue = new CueStick(cueSprites[1], "The Bounty Hunter Cue", "Victory", "Bangkok", "NA", 0, "0", 0, 0, 4, 5, thebountyHunterCueStickLevels, "Expert", 0, 0, thebountyHunterCueStickProperties);
            victoryCueStickList.Add(thebountyHunterCue);
            //Titan Cue
            CueStickProperties titanCueStickProperties = new CueStickProperties(8, 7, 5, 6, 50);
            List<CueStickLevel> titanCueStickLevels = new List<CueStickLevel>();
            CueStickLevel titanCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel titanCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel titanCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel titanCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel titanCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            titanCueStickLevels.Add(titanCueLevel1);
            titanCueStickLevels.Add(titanCueLevel2);
            titanCueStickLevels.Add(titanCueLevel3);
            titanCueStickLevels.Add(titanCueLevel4);
            titanCueStickLevels.Add(titanCueLevel5);
            CueStick titanCue = new CueStick(cueSprites[1], "Titan Cue", "Victory", "Seoul", "NA", 0, "0", 0, 0, 4, 5, titanCueStickLevels, "Expert", 0, 0, titanCueStickProperties);
            victoryCueStickList.Add(titanCue);
            //Tungsten
            CueStickProperties tungstenCueStickProperties = new CueStickProperties(7, 4, 5, 6, 50);
            List<CueStickLevel> tungstenCueStickLevels = new List<CueStickLevel>();
            CueStickLevel tungstenCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel tungstenCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel tungstenCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel tungstenCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel tungstenCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 1, 0);
            tungstenCueStickLevels.Add(tungstenCueLevel1);
            tungstenCueStickLevels.Add(tungstenCueLevel2);
            tungstenCueStickLevels.Add(tungstenCueLevel3);
            tungstenCueStickLevels.Add(tungstenCueLevel4);
            tungstenCueStickLevels.Add(tungstenCueLevel5);
            CueStick tungstenCue = new CueStick(cueSprites[1], "Tungsten Cue", "Victory", "Bangkok", "NA", 0, "0", 0, 0, 4, 5, tungstenCueStickLevels, "Expert", 0, 0, tungstenCueStickProperties);
            victoryCueStickList.Add(tungstenCue);
            //Winter Cue
            CueStickProperties winterCueStickProperties = new CueStickProperties(7, 6, 7, 3, 50);
            List<CueStickLevel> winterCueStickLevels = new List<CueStickLevel>();
            CueStickLevel winterCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel winterCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel winterCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel winterCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel winterCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            winterCueStickLevels.Add(winterCueLevel1);
            winterCueStickLevels.Add(winterCueLevel2);
            winterCueStickLevels.Add(winterCueLevel3);
            winterCueStickLevels.Add(winterCueLevel4);
            winterCueStickLevels.Add(winterCueLevel5);
            CueStick winterCue = new CueStick(cueSprites[1], "Winter Cue", "Victory", "Bangkok", "NA", 0, "0", 0, 0, 4, 5, winterCueStickLevels, "Expert", 0, 0, winterCueStickProperties);
            victoryCueStickList.Add(winterCue);
            //Amber Cue
            CueStickProperties amberCueStickProperties = new CueStickProperties(3, 5, 5, 2, 50);
            List<CueStickLevel> amberCueStickLevels = new List<CueStickLevel>();
            CueStickLevel amberCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel amberCueLevel2 = new CueStickLevel(2, 0, 0, 1, 0, 0, 0);
            CueStickLevel amberCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel amberCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel amberCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            amberCueStickLevels.Add(amberCueLevel1);
            amberCueStickLevels.Add(amberCueLevel2);
            amberCueStickLevels.Add(amberCueLevel3);
            amberCueStickLevels.Add(amberCueLevel4);
            amberCueStickLevels.Add(amberCueLevel5);
            CueStick amberCue = new CueStick(cueSprites[1], "Amber Cue", "Victory", "Cairo", "NA", 0, "0", 0, 0, 4, 5, amberCueStickLevels, "Advanced", 0, 0, amberCueStickProperties);
            victoryCueStickList.Add(amberCue);
            //Amsterdam
            CueStickProperties amsterdamCueStickProperties = new CueStickProperties(3, 2, 3, 0, 50);
            List<CueStickLevel> amsterdamCueStickLevels = new List<CueStickLevel>();
            CueStickLevel amsterdamCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 2);
            CueStickLevel amsterdamCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel amsterdamCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel amsterdamCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel amsterdamCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 1, 0);
            amsterdamCueStickLevels.Add(amsterdamCueLevel1);
            amsterdamCueStickLevels.Add(amsterdamCueLevel2);
            amsterdamCueStickLevels.Add(amsterdamCueLevel3);
            amsterdamCueStickLevels.Add(amsterdamCueLevel4);
            amsterdamCueStickLevels.Add(amsterdamCueLevel5);
            CueStick amsterdamCue = new CueStick(cueSprites[1], "Amsterdam Cue", "Victory", "Amsterdam", "NA", 0, "0", 0, 0, 4, 5, amsterdamCueStickLevels, "Advanced", 0, 0, amsterdamCueStickProperties);
            victoryCueStickList.Add(amsterdamCue);
            //Amethyst Cue
            CueStickProperties amehystCueStickProperties = new CueStickProperties(5, 4, 6, 2, 50);
            List<CueStickLevel> amehystCueStickLevels = new List<CueStickLevel>();
            CueStickLevel amehystCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel amehystCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel amehystCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel amehystCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel amehystCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            amehystCueStickLevels.Add(amehystCueLevel1);
            amehystCueStickLevels.Add(amehystCueLevel2);
            amehystCueStickLevels.Add(amehystCueLevel3);
            amehystCueStickLevels.Add(amehystCueLevel4);
            amehystCueStickLevels.Add(amehystCueLevel5);
            CueStick amehystCue = new CueStick(cueSprites[1], "Amehyst Cue", "Victory", "Dubai", "NA", 0, "0", 0, 0, 4, 5, amehystCueStickLevels, "Advanced", 0, 0, amehystCueStickProperties);
            victoryCueStickList.Add(amehystCue);
            //Atom Cue
            CueStickProperties atomCueStickProperties = new CueStickProperties(6, 4, 7, 0, 50);
            List<CueStickLevel> atomCueStickLevels = new List<CueStickLevel>();
            CueStickLevel atomCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 1);
            CueStickLevel atomCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel atomCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel atomCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel atomCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            atomCueStickLevels.Add(atomCueLevel1);
            atomCueStickLevels.Add(atomCueLevel2);
            atomCueStickLevels.Add(atomCueLevel3);
            atomCueStickLevels.Add(atomCueLevel4);
            atomCueStickLevels.Add(atomCueLevel5);
            CueStick atomCue = new CueStick(cueSprites[1], "Atom Cue", "Victory", "Dubai", "NA", 0, "0", 0, 0, 4, 5, atomCueStickLevels, "Advanced", 0, 0, atomCueStickProperties);
            victoryCueStickList.Add(atomCue);
            //Barbaric Cue
            CueStickProperties barbaricCueStickProperties = new CueStickProperties(5, 2, 5, 3, 50);
            List<CueStickLevel> barbaricCueStickLevels = new List<CueStickLevel>();
            CueStickLevel barbaricCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel barbaricCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel barbaricCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel barbaricCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel barbaricCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 1);
            barbaricCueStickLevels.Add(barbaricCueLevel1);
            barbaricCueStickLevels.Add(barbaricCueLevel2);
            barbaricCueStickLevels.Add(barbaricCueLevel3);
            barbaricCueStickLevels.Add(barbaricCueLevel4);
            barbaricCueStickLevels.Add(barbaricCueLevel5);
            CueStick barbaricCue = new CueStick(cueSprites[1], "Barbaric Cue", "Victory", "Toronto", "NA", 0, "0", 0, 0, 4, 5, barbaricCueStickLevels, "Advanced", 0, 0, barbaricCueStickProperties);
            victoryCueStickList.Add(barbaricCue);
            //Barcelona Cue
            CueStickProperties barcelonaCueStickProperties = new CueStickProperties(1, 0, 1, 0, 50);
            List<CueStickLevel> barcelonaCueStickLevels = new List<CueStickLevel>();
            CueStickLevel barcelonaCueLevel1 = new CueStickLevel(1, 2, 0, 1, 1, 1, 1);
            CueStickLevel barcelonaCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 1);
            CueStickLevel barcelonaCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 1, 0);
            CueStickLevel barcelonaCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 1, 0);
            CueStickLevel barcelonaCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 2);
            barcelonaCueStickLevels.Add(barcelonaCueLevel1);
            barcelonaCueStickLevels.Add(barcelonaCueLevel2);
            barcelonaCueStickLevels.Add(barcelonaCueLevel3);
            barcelonaCueStickLevels.Add(barcelonaCueLevel4);
            barcelonaCueStickLevels.Add(barcelonaCueLevel5);
            CueStick barcelonaCue = new CueStick(cueSprites[1], "Barcelona Cue", "Victory", "Barcelona", "NA", 0, "20", 20, 0, 4, 5, barcelonaCueStickLevels, "Advanced", 0, 0, barcelonaCueStickProperties);
            victoryCueStickList.Add(barcelonaCue);
            //Beech Cue
            CueStickProperties beechCueStickProperties = new CueStickProperties(3, 5, 3, 2, 50);
            List<CueStickLevel> beechCueStickLevels = new List<CueStickLevel>();
            CueStickLevel beechCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel beechCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel beechCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel beechCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel beechCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 1, 1);
            beechCueStickLevels.Add(beechCueLevel1);
            beechCueStickLevels.Add(beechCueLevel2);
            beechCueStickLevels.Add(beechCueLevel3);
            beechCueStickLevels.Add(beechCueLevel4);
            beechCueStickLevels.Add(beechCueLevel5);
            CueStick beechCue = new CueStick(cueSprites[1], "Beech Cue", "Victory", "Jakarta", "NA", 0, "0", 0, 0, 4, 5, beechCueStickLevels, "Advanced", 0, 0, beechCueStickProperties);
            victoryCueStickList.Add(beechCue);
            //Binder Cue
            CueStickProperties binderCueStickProperties = new CueStickProperties(5, 3, 5, 1, 50);
            List<CueStickLevel> binderCueStickLevels = new List<CueStickLevel>();
            CueStickLevel binderCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel binderCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 1);
            CueStickLevel binderCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 2);
            CueStickLevel binderCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel binderCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            binderCueStickLevels.Add(binderCueLevel1);
            binderCueStickLevels.Add(binderCueLevel2);
            binderCueStickLevels.Add(binderCueLevel3);
            binderCueStickLevels.Add(binderCueLevel4);
            binderCueStickLevels.Add(binderCueLevel5);
            CueStick binderCue = new CueStick(cueSprites[1], "Binder Cue", "Victory", "Toronto", "NA", 0, "0", 0, 0, 4, 5, binderCueStickLevels, "Advanced", 0, 0, binderCueStickProperties);
            victoryCueStickList.Add(binderCue);
            //Blackmattte Cue
            CueStickProperties blackmatteCueStickProperties = new CueStickProperties(6, 4, 5, 2, 50);
            List<CueStickLevel> blackmatteCueStickLevels = new List<CueStickLevel>();
            CueStickLevel blackmatteCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel blackmatteCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel blackmatteCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel blackmatteCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel blackmatteCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 1);
            blackmatteCueStickLevels.Add(blackmatteCueLevel1);
            blackmatteCueStickLevels.Add(blackmatteCueLevel2);
            blackmatteCueStickLevels.Add(blackmatteCueLevel3);
            blackmatteCueStickLevels.Add(blackmatteCueLevel4);
            blackmatteCueStickLevels.Add(blackmatteCueLevel5);
            CueStick blackmattteCue = new CueStick(cueSprites[1], "Blackmattte Cue", "Victory", "Dubai", "NA", 0, "0", 0, 0, 4, 5, blackmatteCueStickLevels, "Advanced", 0, 0, blackmatteCueStickProperties);
            victoryCueStickList.Add(blackmattteCue);
            //Blue Hope Cue
            CueStickProperties blueHopeCueStickProperties = new CueStickProperties(6, 3, 4, 0, 50);
            List<CueStickLevel> blueHopeCueStickLevels = new List<CueStickLevel>();
            CueStickLevel blueHopeCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 1);
            CueStickLevel blueHopeCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel blueHopeCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel blueHopeCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel blueHopeCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 1, 1);
            blueHopeCueStickLevels.Add(blueHopeCueLevel1);
            blueHopeCueStickLevels.Add(blueHopeCueLevel2);
            blueHopeCueStickLevels.Add(blueHopeCueLevel3);
            blueHopeCueStickLevels.Add(blueHopeCueLevel4);
            blueHopeCueStickLevels.Add(blueHopeCueLevel5);
            CueStick blueHopeCue = new CueStick(cueSprites[1], "Blue Hope Cue", "Victory", "Jakarta", "NA", 0, "0", 0, 0, 4, 5, blueHopeCueStickLevels, "Advanced", 0, 0, blueHopeCueStickProperties);
            victoryCueStickList.Add(blueHopeCue);
            //Buenos Aires Cue
            CueStickProperties buenosAiresCueStickProperties = new CueStickProperties(1, 2, 3, 0, 50);
            List<CueStickLevel> buenosAiresCueStickLevels = new List<CueStickLevel>();
            CueStickLevel buenosAiresCueLevel1 = new CueStickLevel(1, 0, 0, 1, 1, 0, 2);
            CueStickLevel buenosAiresCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel buenosAiresCueLevel3 = new CueStickLevel(3, 0, 0, 1, 1, 1, 0);
            CueStickLevel buenosAiresCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel buenosAiresCueLevel5 = new CueStickLevel(5, 0, 0, 2, 1, 0, 1);
            buenosAiresCueStickLevels.Add(buenosAiresCueLevel1);
            buenosAiresCueStickLevels.Add(buenosAiresCueLevel2);
            buenosAiresCueStickLevels.Add(buenosAiresCueLevel3);
            buenosAiresCueStickLevels.Add(buenosAiresCueLevel4);
            buenosAiresCueStickLevels.Add(buenosAiresCueLevel5);
            CueStick buenosAiresCue = new CueStick(cueSprites[1], "Buenos Aires Cue", "Victory", "Buenos Aires", "NA", 0, "0", 0, 0, 4, 5, buenosAiresCueStickLevels, "Advanced", 0, 0, buenosAiresCueStickProperties);
            victoryCueStickList.Add(buenosAiresCue);
            //Camouflage Cue
            CueStickProperties camouflageCueStickProperties = new CueStickProperties(4, 3, 5, 0, 50);
            List<CueStickLevel> camouflageCueStickLevels = new List<CueStickLevel>();
            CueStickLevel camouflageCueLevel1 = new CueStickLevel(1, 2, 0, 0, 1, 0, 0);
            CueStickLevel camouflageCueLevel2 = new CueStickLevel(2, 0, 0, 1, 0, 0, 1);
            CueStickLevel camouflageCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 1);
            CueStickLevel camouflageCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel camouflageCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            camouflageCueStickLevels.Add(camouflageCueLevel1);
            camouflageCueStickLevels.Add(camouflageCueLevel2);
            camouflageCueStickLevels.Add(camouflageCueLevel3);
            camouflageCueStickLevels.Add(camouflageCueLevel4);
            camouflageCueStickLevels.Add(camouflageCueLevel5);
            CueStick camouflageCue = new CueStick(cueSprites[1], "Camouflage Cue", "Victory", "Las Vegas", "NA", 0, "450", 450, 0, 4, 5, camouflageCueStickLevels, "Advanced", 0, 0, camouflageCueStickProperties);
            victoryCueStickList.Add(camouflageCue);
            //Crystal Cue
            CueStickProperties crystalCueStickProperties = new CueStickProperties(7, 4, 7, 1, 50);
            List<CueStickLevel> crystalCueStickLevels = new List<CueStickLevel>();
            CueStickLevel crystalCueLevel1 = new CueStickLevel(1, 2, 0, 0, 0, 0, 1);
            CueStickLevel crystalCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel crystalCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel crystalCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 1);
            CueStickLevel crystalCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            crystalCueStickLevels.Add(crystalCueLevel1);
            crystalCueStickLevels.Add(crystalCueLevel2);
            crystalCueStickLevels.Add(crystalCueLevel3);
            crystalCueStickLevels.Add(crystalCueLevel4);
            crystalCueStickLevels.Add(crystalCueLevel5);
            CueStick crystalCue = new CueStick(cueSprites[1], "Crystal Cue", "Victory", "Paris", "NA", 0, "0", 0, 0, 4, 5, crystalCueStickLevels, "Advanced", 0, 0, crystalCueStickProperties);
            victoryCueStickList.Add(crystalCue);
            //Digitized Cue
            CueStickProperties digitizedCueStickProperties = new CueStickProperties(3, 3, 3, 0, 50);
            List<CueStickLevel> digitizedCueStickLevels = new List<CueStickLevel>();
            CueStickLevel digitizedCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 2);
            CueStickLevel digitizedCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 1);
            CueStickLevel digitizedCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 1);
            CueStickLevel digitizedCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel digitizedCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            digitizedCueStickLevels.Add(digitizedCueLevel1);
            digitizedCueStickLevels.Add(digitizedCueLevel2);
            digitizedCueStickLevels.Add(digitizedCueLevel3);
            digitizedCueStickLevels.Add(digitizedCueLevel4);
            digitizedCueStickLevels.Add(digitizedCueLevel5);
            CueStick digitizedCue = new CueStick(cueSprites[1], "Digitized Cue", "Victory", "Las Vegas", "NA", 0, "0", 0, 0, 4, 5, digitizedCueStickLevels, "Advanced", 0, 0, digitizedCueStickProperties);
            victoryCueStickList.Add(digitizedCue);
            //Ebony Cue
            CueStickProperties ebonyCueStickProperties = new CueStickProperties(5, 6, 4, 5, 50);
            List<CueStickLevel> ebonyCueStickLevels = new List<CueStickLevel>();
            CueStickLevel ebonyCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel ebonyCueLevel2 = new CueStickLevel(2, 0, 0, 1, 0, 0, 0);
            CueStickLevel ebonyCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel ebonyCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel ebonyCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            ebonyCueStickLevels.Add(ebonyCueLevel1);
            ebonyCueStickLevels.Add(ebonyCueLevel2);
            ebonyCueStickLevels.Add(ebonyCueLevel3);
            ebonyCueStickLevels.Add(ebonyCueLevel4);
            ebonyCueStickLevels.Add(ebonyCueLevel5);
            CueStick ebonyCue = new CueStick(cueSprites[1], "Ebony Cue", "Victory", "Paris", "NA", 0, "0", 0, 0, 4, 5, ebonyCueStickLevels, "Advanced", 0, 0, ebonyCueStickProperties);
            victoryCueStickList.Add(ebonyCue);
            //Flame Cue
            CueStickProperties flameCueStickProperties = new CueStickProperties(5, 5, 7, 2, 50);
            List<CueStickLevel> flameCueStickLevels = new List<CueStickLevel>();
            CueStickLevel flameCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel flameCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel flameCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 1);
            CueStickLevel flameCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 1);
            CueStickLevel flameCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            flameCueStickLevels.Add(flameCueLevel1);
            flameCueStickLevels.Add(flameCueLevel2);
            flameCueStickLevels.Add(flameCueLevel3);
            flameCueStickLevels.Add(flameCueLevel4);
            flameCueStickLevels.Add(flameCueLevel5);
            CueStick flameCue = new CueStick(cueSprites[1], "Flame Cue", "Victory", "Paris", "NA", 0, "0", 0, 0, 4, 5, flameCueStickLevels, "Advanced", 0, 0, flameCueStickProperties);
            victoryCueStickList.Add(flameCue);
            //Insane Cue
            CueStickProperties insaneCueStickProperties = new CueStickProperties(5, 6, 7, 1, 50);
            List<CueStickLevel> insaneCueStickLevels = new List<CueStickLevel>();
            CueStickLevel insaneCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel insaneCueLevel2 = new CueStickLevel(2, 0, 0, 1, 0, 0, 1);
            CueStickLevel insaneCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 1);
            CueStickLevel insaneCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel insaneCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            insaneCueStickLevels.Add(insaneCueLevel1);
            insaneCueStickLevels.Add(insaneCueLevel2);
            insaneCueStickLevels.Add(insaneCueLevel3);
            insaneCueStickLevels.Add(insaneCueLevel4);
            insaneCueStickLevels.Add(insaneCueLevel5);
            CueStick insaneCue = new CueStick(cueSprites[1], "Insane Cue", "Victory", "Paris", "NA", 0, "0", 0, 0, 4, 5, insaneCueStickLevels, "Advanced", 0, 0, insaneCueStickProperties);
            victoryCueStickList.Add(insaneCue);
            //Norseman Cue
            CueStickProperties norsemanCueStickProperties = new CueStickProperties(3, 4, 5, 2, 50);
            List<CueStickLevel> norsemanCueStickLevels = new List<CueStickLevel>();
            CueStickLevel norsemanCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 1);
            CueStickLevel norsemanCueLevel2 = new CueStickLevel(2, 0, 0, 1, 1, 0, 1);
            CueStickLevel norsemanCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 1);
            CueStickLevel norsemanCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel norsemanCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 1);
            norsemanCueStickLevels.Add(norsemanCueLevel1);
            norsemanCueStickLevels.Add(norsemanCueLevel2);
            norsemanCueStickLevels.Add(norsemanCueLevel3);
            norsemanCueStickLevels.Add(norsemanCueLevel4);
            norsemanCueStickLevels.Add(norsemanCueLevel5);
            CueStick norsemanCue = new CueStick(cueSprites[1], "Norseman Cue", "Victory", "Toronto", "NA", 0, "0", 0, 0, 4, 5, norsemanCueStickLevels, "Advanced", 0, 0, norsemanCueStickProperties);
            victoryCueStickList.Add(norsemanCue);
            //Oak Cue
            CueStickProperties oakCueStickProperties = new CueStickProperties(3, 4, 4, 0, 50);
            List<CueStickLevel> oakCueStickLevels = new List<CueStickLevel>();
            CueStickLevel oakCueLevel1 = new CueStickLevel(1, 2, 0, 1, 0, 0, 1);
            CueStickLevel oakCueLevel2 = new CueStickLevel(2, 0, 0, 1, 1, 0, 1);
            CueStickLevel oakCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 1);
            CueStickLevel oakCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel oakCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            oakCueStickLevels.Add(oakCueLevel1);
            oakCueStickLevels.Add(oakCueLevel2);
            oakCueStickLevels.Add(oakCueLevel3);
            oakCueStickLevels.Add(oakCueLevel4);
            oakCueStickLevels.Add(oakCueLevel5);
            CueStick oakCue = new CueStick(cueSprites[1], "Oak Cue", "Victory", "Tokyo", "NA", 0, "300", 300, 0, 4, 5, oakCueStickLevels, "Advanced", 0, 0, oakCueStickProperties);
            victoryCueStickList.Add(oakCue);
            //Palladium Cue
            CueStickProperties palladiumCueStickProperties = new CueStickProperties(4, 7, 6, 4, 50);
            List<CueStickLevel> palladiumCueStickLevels = new List<CueStickLevel>();
            CueStickLevel palladiumCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel palladiumCueLevel2 = new CueStickLevel(2, 0, 0, 1, 0, 0, 0);
            CueStickLevel palladiumCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel palladiumCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel palladiumCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            palladiumCueStickLevels.Add(palladiumCueLevel1);
            palladiumCueStickLevels.Add(palladiumCueLevel2);
            palladiumCueStickLevels.Add(palladiumCueLevel3);
            palladiumCueStickLevels.Add(palladiumCueLevel4);
            palladiumCueStickLevels.Add(palladiumCueLevel5);
            CueStick palladiumCue = new CueStick(cueSprites[1], "Palladium Cue", "Victory", "Rome", "NA", 0, "0", 0, 0, 4, 5, palladiumCueStickLevels, "Advanced", 0, 0, palladiumCueStickProperties);
            victoryCueStickList.Add(palladiumCue);
            //Phantom Cue
            CueStickProperties phantomCueStickProperties = new CueStickProperties(3, 4, 3, 4, 50);
            List<CueStickLevel> phantomCueStickLevels = new List<CueStickLevel>();
            CueStickLevel phantomCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel phantomCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel phantomCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel phantomCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel phantomCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 1, 0);
            phantomCueStickLevels.Add(phantomCueLevel1);
            phantomCueStickLevels.Add(phantomCueLevel2);
            phantomCueStickLevels.Add(phantomCueLevel3);
            phantomCueStickLevels.Add(phantomCueLevel4);
            phantomCueStickLevels.Add(phantomCueLevel5);
            CueStick phantomCue = new CueStick(cueSprites[1], "Phantom Cue", "Victory", "Jakarta", "NA", 0, "0", 0, 0, 4, 5, phantomCueStickLevels, "Advanced", 0, 0, phantomCueStickProperties);
            victoryCueStickList.Add(phantomCue);
            //Platinum Viper Cue
            CueStickProperties platinumViperCueStickProperties = new CueStickProperties(4, 4, 3, 3, 50);
            List<CueStickLevel> platinumViperCueStickLevels = new List<CueStickLevel>();
            CueStickLevel platinumViperCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 1, 0);
            CueStickLevel platinumViperCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 1);
            CueStickLevel platinumViperCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 0);
            CueStickLevel platinumViperCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 1);
            CueStickLevel platinumViperCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 1, 0);
            platinumViperCueStickLevels.Add(platinumViperCueLevel1);
            platinumViperCueStickLevels.Add(platinumViperCueLevel2);
            platinumViperCueStickLevels.Add(platinumViperCueLevel3);
            platinumViperCueStickLevels.Add(platinumViperCueLevel4);
            platinumViperCueStickLevels.Add(platinumViperCueLevel5);
            CueStick platinumViperCue = new CueStick(cueSprites[1], "Platinum Viper Cue", "Victory", "Jakarta", "NA", 0, "0", 0, 0, 4, 5, platinumViperCueStickLevels, "Advanced", 0, 0, platinumViperCueStickProperties);
            victoryCueStickList.Add(platinumViperCue);
            //Posh Cue
            CueStickProperties poshCueStickProperties = new CueStickProperties(5, 4, 2, 1, 50);
            List<CueStickLevel> poshCueStickLevels = new List<CueStickLevel>();
            CueStickLevel poshCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 1);
            CueStickLevel poshCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel poshCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel poshCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel poshCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 2);
            poshCueStickLevels.Add(poshCueLevel1);
            poshCueStickLevels.Add(poshCueLevel2);
            poshCueStickLevels.Add(poshCueLevel3);
            poshCueStickLevels.Add(poshCueLevel4);
            poshCueStickLevels.Add(poshCueLevel5);
            CueStick poshCue = new CueStick(cueSprites[1], "Posh Cue", "Victory", "Las Vegas", "NA", 0, "0", 0, 0, 4, 5, poshCueStickLevels, "Advanced", 0, 0, poshCueStickProperties);
            victoryCueStickList.Add(poshCue);
            //Rio Cue
            CueStickProperties rioCueStickProperties = new CueStickProperties(2, 1, 2, 0, 50);
            List<CueStickLevel> rioCueStickLevels = new List<CueStickLevel>();
            CueStickLevel rioCueLevel1 = new CueStickLevel(1, 0, 0, 1, 1, 1, 1);
            CueStickLevel rioCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 2);
            CueStickLevel rioCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel rioCueLevel4 = new CueStickLevel(4, 0, 0, 1, 1, 1, 1);
            CueStickLevel rioCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 1, 1);
            rioCueStickLevels.Add(rioCueLevel1);
            rioCueStickLevels.Add(rioCueLevel2);
            rioCueStickLevels.Add(rioCueLevel3);
            rioCueStickLevels.Add(rioCueLevel4);
            rioCueStickLevels.Add(rioCueLevel5);
            CueStick rioCue = new CueStick(cueSprites[1], "Rio Cue", "Victory", "Rio", "NA", 0, "0", 0, 0, 4, 5, rioCueStickLevels, "Advanced", 0, 0, rioCueStickProperties);
            victoryCueStickList.Add(rioCue);
            //Ruby Cue
            CueStickProperties rubyCueStickProperties = new CueStickProperties(4, 5, 5, 0, 50);
            List<CueStickLevel> rubyCueStickLevels = new List<CueStickLevel>();
            CueStickLevel rubyCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel rubyCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel rubyCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel rubyCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel rubyCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 1);
            rubyCueStickLevels.Add(rubyCueLevel1);
            rubyCueStickLevels.Add(rubyCueLevel2);
            rubyCueStickLevels.Add(rubyCueLevel3);
            rubyCueStickLevels.Add(rubyCueLevel4);
            rubyCueStickLevels.Add(rubyCueLevel5);
            CueStick rubyCue = new CueStick(cueSprites[1], "Ruby Cue", "Victory", "Shanghai", "NA", 0, "0", 0, 0, 4, 5, rubyCueStickLevels, "Advanced", 0, 0, rubyCueStickProperties);
            victoryCueStickList.Add(rubyCue);
            //Sapphire Cue
            CueStickProperties sapphireCueStickProperties = new CueStickProperties(4, 5, 4, 3, 50);
            List<CueStickLevel> sapphireCueStickLevels = new List<CueStickLevel>();
            CueStickLevel sapphireCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel sapphireCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel sapphireCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 0);
            CueStickLevel sapphireCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel sapphireCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            sapphireCueStickLevels.Add(sapphireCueLevel1);
            sapphireCueStickLevels.Add(sapphireCueLevel2);
            sapphireCueStickLevels.Add(sapphireCueLevel3);
            sapphireCueStickLevels.Add(sapphireCueLevel4);
            sapphireCueStickLevels.Add(sapphireCueLevel5);
            CueStick sapphireCue = new CueStick(cueSprites[1], "Sapphire Cue", "Victory", "Cairo", "NA", 0, "0", 0, 0, 4, 5, sapphireCueStickLevels, "Advanced", 0, 0, sapphireCueStickProperties);
            victoryCueStickList.Add(sapphireCue);
            //Silver Cue
            CueStickProperties silverCueStickProperties = new CueStickProperties(7, 5, 5, 1, 50);
            List<CueStickLevel> silverCueStickLevels = new List<CueStickLevel>();
            CueStickLevel silverCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel silverCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel silverCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel silverCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel silverCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            silverCueStickLevels.Add(silverCueLevel1);
            silverCueStickLevels.Add(silverCueLevel2);
            silverCueStickLevels.Add(silverCueLevel3);
            silverCueStickLevels.Add(silverCueLevel4);
            silverCueStickLevels.Add(silverCueLevel5);
            CueStick silverCue = new CueStick(cueSprites[1], "Silver Cue", "Victory", "Shanghai", "NA", 0, "0", 0, 0, 4, 5, silverCueStickLevels, "Advanced", 0, 0, silverCueStickProperties);
            victoryCueStickList.Add(silverCue);
            //Singapore Cue
            CueStickProperties singaporeCueStickProperties = new CueStickProperties(4, 3, 4, 0, 50);
            List<CueStickLevel> singaporeCueStickLevels = new List<CueStickLevel>();
            CueStickLevel singaporeCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel singaporeCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel singaporeCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 1);
            CueStickLevel singaporeCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 2);
            CueStickLevel singaporeCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            singaporeCueStickLevels.Add(singaporeCueLevel1);
            singaporeCueStickLevels.Add(singaporeCueLevel2);
            singaporeCueStickLevels.Add(singaporeCueLevel3);
            singaporeCueStickLevels.Add(singaporeCueLevel4);
            singaporeCueStickLevels.Add(singaporeCueLevel5);
            CueStick singaporeCue = new CueStick(cueSprites[1], "Singapore Cue", "Victory", "Singapore", "NA", 0, "0", 0, 0, 4, 5, singaporeCueStickLevels, "Advanced", 0, 0, singaporeCueStickProperties);
            victoryCueStickList.Add(singaporeCue);
            //Steampunk Cue
            CueStickProperties steampunkCueStickProperties = new CueStickProperties(3, 5, 4, 2, 50);
            List<CueStickLevel> steampunkCueStickLevels = new List<CueStickLevel>();
            CueStickLevel steampunkCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel steampunkCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 0);
            CueStickLevel steampunkCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel steampunkCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel steampunkCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            steampunkCueStickLevels.Add(steampunkCueLevel1);
            steampunkCueStickLevels.Add(steampunkCueLevel2);
            steampunkCueStickLevels.Add(steampunkCueLevel3);
            steampunkCueStickLevels.Add(steampunkCueLevel4);
            steampunkCueStickLevels.Add(steampunkCueLevel5);
            CueStick steampunkCue = new CueStick(cueSprites[1], "Steampunk Cue", "Victory", "Toronto", "NA", 0, "0", 0, 0, 4, 5, steampunkCueStickLevels, "Advanced", 0, 0, steampunkCueStickProperties);
            victoryCueStickList.Add(steampunkCue);
            //Teak Cue
            CueStickProperties teakCueStickProperties = new CueStickProperties(5, 5, 6, 0, 50);
            List<CueStickLevel> teakCueStickLevels = new List<CueStickLevel>();
            CueStickLevel teakCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel teakCueLevel2 = new CueStickLevel(2, 0, 0, 1, 0, 0, 1);
            CueStickLevel teakCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 1);
            CueStickLevel teakCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel teakCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            teakCueStickLevels.Add(teakCueLevel1);
            teakCueStickLevels.Add(teakCueLevel2);
            teakCueStickLevels.Add(teakCueLevel3);
            teakCueStickLevels.Add(teakCueLevel4);
            teakCueStickLevels.Add(teakCueLevel5);
            CueStick teakCue = new CueStick(cueSprites[1], "Teak Cue", "Victory", "Dubai", "NA", 0, "0", 0, 0, 4, 5, teakCueStickLevels, "Advanced", 0, 0, teakCueStickProperties);
            victoryCueStickList.Add(teakCue);
            //Test Tube Cue
            CueStickProperties testTubeCueStickProperties = new CueStickProperties(4, 2, 5, 2, 50);
            List<CueStickLevel> testTubeCueStickLevels = new List<CueStickLevel>();
            CueStickLevel testTubeCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 1);
            CueStickLevel testTubeCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel testTubeCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 1);
            CueStickLevel testTubeCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel testTubeCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 1);
            testTubeCueStickLevels.Add(testTubeCueLevel1);
            testTubeCueStickLevels.Add(testTubeCueLevel2);
            testTubeCueStickLevels.Add(testTubeCueLevel3);
            testTubeCueStickLevels.Add(testTubeCueLevel4);
            testTubeCueStickLevels.Add(testTubeCueLevel5);
            CueStick testTubeCue = new CueStick(cueSprites[1], "Test Tube Cue", "Victory", "Las Vegas", "NA", 0, "0", 0, 0, 4, 5, testTubeCueStickLevels, "Advanced", 0, 0, testTubeCueStickProperties);
            victoryCueStickList.Add(testTubeCue);
            //The Sheriff Cue
            CueStickProperties theSheriffCueStickProperties = new CueStickProperties(4, 6, 3, 4, 50);
            List<CueStickLevel> theSherifCueStickLevels = new List<CueStickLevel>();
            CueStickLevel theSherifCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel theSherifCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel theSherifCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel theSherifCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel theSherifCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            theSherifCueStickLevels.Add(theSherifCueLevel1);
            theSherifCueStickLevels.Add(theSherifCueLevel2);
            theSherifCueStickLevels.Add(theSherifCueLevel3);
            theSherifCueStickLevels.Add(theSherifCueLevel4);
            theSherifCueStickLevels.Add(theSherifCueLevel5);
            CueStick theSherifCue = new CueStick(cueSprites[1], "The Sherif Cue", "Victory", "Dubai", "NA", 0, "0", 0, 0, 4, 5, theSherifCueStickLevels, "Advanced", 0, 0, theSheriffCueStickProperties);
            victoryCueStickList.Add(theSherifCue);
            //Tiger Cue
            CueStickProperties tigerCueStickProperties = new CueStickProperties(5, 5, 5, 0, 50);
            List<CueStickLevel> tigerCueStickLevels = new List<CueStickLevel>();
            CueStickLevel tigerCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel tigerCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel tigerCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 1);
            CueStickLevel tigerCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel tigerCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            tigerCueStickLevels.Add(tigerCueLevel1);
            tigerCueStickLevels.Add(tigerCueLevel2);
            tigerCueStickLevels.Add(tigerCueLevel3);
            tigerCueStickLevels.Add(tigerCueLevel4);
            tigerCueStickLevels.Add(tigerCueLevel5);
            CueStick tigerCue = new CueStick(cueSprites[1], "Tiger Cue", "Victory", "Cairo", "NA", 0, "0", 0, 0, 4, 5, tigerCueStickLevels, "Advanced", 0, 0, tigerCueStickProperties);
            victoryCueStickList.Add(tigerCue);
            //Titanium Cue
            CueStickProperties titaniumCueStickProperties = new CueStickProperties(4, 5, 2, 4, 50);
            List<CueStickLevel> titaniumCueStickLevels = new List<CueStickLevel>();
            CueStickLevel titaniumCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel titaniumCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel titaniumCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 0);
            CueStickLevel titaniumCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 2);
            CueStickLevel titaniumCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            titaniumCueStickLevels.Add(titaniumCueLevel1);
            titaniumCueStickLevels.Add(titaniumCueLevel2);
            titaniumCueStickLevels.Add(titaniumCueLevel3);
            titaniumCueStickLevels.Add(titaniumCueLevel4);
            titaniumCueStickLevels.Add(titaniumCueLevel5);
            CueStick titaniumCue = new CueStick(cueSprites[1], "Titanium Cue", "Victory", "Toronto", "NA", 0, "0", 0, 0, 4, 5, titaniumCueStickLevels, "Advanced", 0, 0, titaniumCueStickProperties);
            victoryCueStickList.Add(titaniumCue);
            //Walnut Cue
            CueStickProperties walnutCueStickProperties = new CueStickProperties(6, 7, 7, 0, 50);
            List<CueStickLevel> walnutCueStickLevels = new List<CueStickLevel>();
            CueStickLevel walnutCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel walnutCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel walnutCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel walnutCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel walnutCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 1);
            walnutCueStickLevels.Add(walnutCueLevel1);
            walnutCueStickLevels.Add(walnutCueLevel2);
            walnutCueStickLevels.Add(walnutCueLevel3);
            walnutCueStickLevels.Add(walnutCueLevel4);
            walnutCueStickLevels.Add(walnutCueLevel5);
            CueStick walnutCue = new CueStick(cueSprites[1], "Walnut Cue", "Victory", "Rome", "NA", 0, "0", 0, 0, 4, 5, walnutCueStickLevels, "Advanced", 0, 0, walnutCueStickProperties);
            victoryCueStickList.Add(walnutCue);
            //i Cue
            CueStickProperties iCueStickProperties = new CueStickProperties(4, 6, 7, 3, 50);
            List<CueStickLevel> iCueStickLevels = new List<CueStickLevel>();
            CueStickLevel iCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel iCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel iCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel iCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel iCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            iCueStickLevels.Add(iCueLevel1);
            iCueStickLevels.Add(iCueLevel2);
            iCueStickLevels.Add(iCueLevel3);
            iCueStickLevels.Add(iCueLevel4);
            iCueStickLevels.Add(iCueLevel5);
            CueStick iCue = new CueStick(cueSprites[1], "I Cue", "Victory", "Paris", "NA", 0, "0", 0, 0, 4, 5, iCueStickLevels, "Advanced", 0, 0, iCueStickProperties);
            victoryCueStickList.Add(iCue);
            //Zombie Cue
            CueStickProperties zombieCueStickProperties = new CueStickProperties(6, 6, 6, 0, 50);
            List<CueStickLevel> zombieCueStickLevels = new List<CueStickLevel>();
            CueStickLevel zombieCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel zombieCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel zombieCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 1);
            CueStickLevel zombieCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel zombieCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 1);
            zombieCueStickLevels.Add(zombieCueLevel1);
            zombieCueStickLevels.Add(zombieCueLevel2);
            zombieCueStickLevels.Add(zombieCueLevel3);
            zombieCueStickLevels.Add(zombieCueLevel4);
            zombieCueStickLevels.Add(zombieCueLevel5);
            CueStick zombieCue = new CueStick(cueSprites[1], "Zombie Cue", "Victory", "Shanghai", "NA", 0, "0", 0, 0, 4, 5, zombieCueStickLevels, "Advanced", 0, 0, zombieCueStickProperties);
            victoryCueStickList.Add(zombieCue);
            //Bearpaw Cue
            CueStickProperties bearpawCueStickProperties = new CueStickProperties(2, 3, 4, 1, 50);
            List<CueStickLevel> bearpawCueStickLevels = new List<CueStickLevel>();
            CueStickLevel bearpawCueLevel1 = new CueStickLevel(1, 2, 0, 1, 0, 0, 1);
            CueStickLevel bearpawCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel bearpawCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 1);
            CueStickLevel bearpawCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel bearpawCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            bearpawCueStickLevels.Add(bearpawCueLevel1);
            bearpawCueStickLevels.Add(bearpawCueLevel2);
            bearpawCueStickLevels.Add(bearpawCueLevel3);
            bearpawCueStickLevels.Add(bearpawCueLevel4);
            bearpawCueStickLevels.Add(bearpawCueLevel5);
            CueStick bearpawCue = new CueStick(cueSprites[1], "Bearpaw Cue", "Victory", "Moscow", "NA", 0, "220", 220, 0, 4, 5, bearpawCueStickLevels, "Basic", 0, 0, bearpawCueStickProperties);
            victoryCueStickList.Add(bearpawCue);
            //Bronze Cue
            CueStickProperties bronzeCueStickProperties = new CueStickProperties(3, 1, 2, 0, 50);
            List<CueStickLevel> bronzeCueStickLevels = new List<CueStickLevel>();
            CueStickLevel bronzeCueLevel1 = new CueStickLevel(1, 2, 0, 1, 0, 0, 0);
            CueStickLevel bronzeCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel bronzeCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel bronzeCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel bronzeCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            bronzeCueStickLevels.Add(bronzeCueLevel1);
            bronzeCueStickLevels.Add(bronzeCueLevel2);
            bronzeCueStickLevels.Add(bronzeCueLevel3);
            bronzeCueStickLevels.Add(bronzeCueLevel4);
            bronzeCueStickLevels.Add(bronzeCueLevel5);
            CueStick bronzeCue = new CueStick(cueSprites[1], "Bronze Cue", "Victory", "Sydney", "NA", 0, "0", 0, 0, 4, 5, bronzeCueStickLevels, "Basic", 0, 0, bronzeCueStickProperties);
            victoryCueStickList.Add(bronzeCue);
            //Carbon Fiber Cue
            CueStickProperties carbonFiberCueStickProperties = new CueStickProperties(3, 4, 1, 3, 19);
            List<CueStickLevel> carbonFiberCueStickLevels = new List<CueStickLevel>();
            CueStickLevel carbonFiberCueLevel1 = new CueStickLevel(1, 2, 0, 0, 0, 1, 0);
            CueStickLevel carbonFiberCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel carbonFiberCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 0);
            CueStickLevel carbonFiberCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel carbonFiberCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            carbonFiberCueStickLevels.Add(carbonFiberCueLevel1);
            carbonFiberCueStickLevels.Add(carbonFiberCueLevel2);
            carbonFiberCueStickLevels.Add(carbonFiberCueLevel3);
            carbonFiberCueStickLevels.Add(carbonFiberCueLevel4);
            carbonFiberCueStickLevels.Add(carbonFiberCueLevel5);
            CueStick carbonFiberCue = new CueStick(cueSprites[1], "Carbon Fiber Cue", "Victory", "Tokyo", "NA", 0, "300", 300, 0, 4, 5, carbonFiberCueStickLevels, "Basic", 0, 0, carbonFiberCueStickProperties);
            victoryCueStickList.Add(carbonFiberCue);
            //Classic Cue
            CueStickProperties classicCueStickProperties = new CueStickProperties(2, 3, 2, 2, 50);
            List<CueStickLevel> classicCueStickLevels = new List<CueStickLevel>();
            CueStickLevel classicCueLevel1 = new CueStickLevel(1, 2, 0, 1, 0, 0, 0);
            CueStickLevel classicCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel classicCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel classicCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel classicCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 1, 0);
            classicCueStickLevels.Add(classicCueLevel1);
            classicCueStickLevels.Add(classicCueLevel2);
            classicCueStickLevels.Add(classicCueLevel3);
            classicCueStickLevels.Add(classicCueLevel4);
            classicCueStickLevels.Add(classicCueLevel5);
            CueStick classicCue = new CueStick(cueSprites[1], "Classic Cue", "Victory", "Moscow", "NA", 0, "160", 160, 0, 4, 5, classicCueStickLevels, "Basic", 0, 0, classicCueStickProperties);
            victoryCueStickList.Add(classicCue);
            //Shaman Cue
            CueStickProperties shamanCueStickProperties = new CueStickProperties(2, 4, 2, 2, 13);
            List<CueStickLevel> shamanCueStickLevels = new List<CueStickLevel>();
            CueStickLevel shamanCueLevel1 = new CueStickLevel(1, 2, 0, 0, 0, 1, 0);
            CueStickLevel shamanCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel shamanCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 0);
            CueStickLevel shamanCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel shamanCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            shamanCueStickLevels.Add(shamanCueLevel1);
            shamanCueStickLevels.Add(shamanCueLevel2);
            shamanCueStickLevels.Add(shamanCueLevel3);
            shamanCueStickLevels.Add(shamanCueLevel4);
            shamanCueStickLevels.Add(shamanCueLevel5);
            CueStick shamanCue = new CueStick(cueSprites[1], "Shaman Cue", "Victory", "Tokyo", "NA", 0, "220", 220, 0, 4, 5, shamanCueStickLevels, "Basic", 0, 0, shamanCueStickProperties);
            victoryCueStickList.Add(shamanCue);
            //Snake Cue
            CueStickProperties snakeCueStickProperties = new CueStickProperties(4, 4, 2, 2, 50);
            List<CueStickLevel> snakeCueStickLevels = new List<CueStickLevel>();
            CueStickLevel snakeCueLevel1 = new CueStickLevel(1, 2, 0, 0, 0, 0, 1);
            CueStickLevel snakeCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel snakeCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel snakeCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel snakeCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 1, 0);
            snakeCueStickLevels.Add(snakeCueLevel1);
            snakeCueStickLevels.Add(snakeCueLevel2);
            snakeCueStickLevels.Add(snakeCueLevel3);
            snakeCueStickLevels.Add(snakeCueLevel4);
            snakeCueStickLevels.Add(snakeCueLevel5);
            CueStick snakeCue = new CueStick(cueSprites[1], "Snake Cue", "Victory", "Las Vegas", "NA", 0, "450", 450, 0, 4, 5, snakeCueStickLevels, "Basic", 0, 0, snakeCueStickProperties);
            victoryCueStickList.Add(snakeCue);
            //The Deputy
            CueStickProperties theDeputyCueStickProperties = new CueStickProperties(4, 3, 3, 1, 50);
            List<CueStickLevel> theDeputyCueStickLevels = new List<CueStickLevel>();
            CueStickLevel theDeputyCueLevel1 = new CueStickLevel(1, 2, 0, 1, 0, 0, 0);
            CueStickLevel theDeputyCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel theDeputyCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel theDeputyCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel theDeputyCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            theDeputyCueStickLevels.Add(theDeputyCueLevel1);
            theDeputyCueStickLevels.Add(theDeputyCueLevel2);
            theDeputyCueStickLevels.Add(theDeputyCueLevel3);
            theDeputyCueStickLevels.Add(theDeputyCueLevel4);
            theDeputyCueStickLevels.Add(theDeputyCueLevel5);
            CueStick theDeputyCue = new CueStick(cueSprites[1], "The Deputy Cue", "Victory", "Tokyo", "NA", 0, "300", 300, 0, 4, 5, theDeputyCueStickLevels, "Basic", 0, 0, theDeputyCueStickProperties);
            victoryCueStickList.Add(theDeputyCue);
            //Vintage Cue
            CueStickProperties vintageCueStickProperties = new CueStickProperties(2, 1, 2, 2, 50);
            List<CueStickLevel> vintageCueStickLevels = new List<CueStickLevel>();
            CueStickLevel vintageCueLevel1 = new CueStickLevel(1, 2, 0, 0, 1, 0, 0);
            CueStickLevel vintageCueLevel2 = new CueStickLevel(2, 0, 0, 1, 0, 0, 0);
            CueStickLevel vintageCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel vintageCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel vintageCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 1, 0);
            vintageCueStickLevels.Add(vintageCueLevel1);
            vintageCueStickLevels.Add(vintageCueLevel2);
            vintageCueStickLevels.Add(vintageCueLevel3);
            vintageCueStickLevels.Add(vintageCueLevel4);
            vintageCueStickLevels.Add(vintageCueLevel5);
            CueStick vintageCue = new CueStick(cueSprites[1], "Vintage Cue", "Victory", "Moscow", "NA", 0, "50", 50, 0, 4, 5, vintageCueStickLevels, "Basic", 0, 0, vintageCueStickProperties);
            victoryCueStickList.Add(vintageCue);
            //Zebra Cue
            CueStickProperties zebraCueStickProperties = new CueStickProperties(2, 3, 3, 1, 50);
            List<CueStickLevel> zebraCueStickLevels = new List<CueStickLevel>();
            CueStickLevel zebraCueLevel1 = new CueStickLevel(1, 2, 0, 0, 0, 0, 1);
            CueStickLevel zebraCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel zebraCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 0);
            CueStickLevel zebraCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel zebraCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            zebraCueStickLevels.Add(zebraCueLevel1);
            zebraCueStickLevels.Add(zebraCueLevel2);
            zebraCueStickLevels.Add(zebraCueLevel3);
            zebraCueStickLevels.Add(zebraCueLevel4);
            zebraCueStickLevels.Add(zebraCueLevel5);
            CueStick zebraCue = new CueStick(cueSprites[1], "Zebra Cue", "Victory", "Moscow", "NA", 0, "160", 160, 0, 4, 5, zebraCueStickLevels, "Basic", 0, 0, zebraCueStickProperties);
            victoryCueStickList.Add(zebraCue);

            for (int i = 0; i < victoryCueStickList.Count; i++)
            {

                if (GetVictoryCueStickType(i) == "Basic")
                {
                    victoryBasicCueStickList.Add(i);
                }
                else if (GetVictoryCueStickType(i) == "Advanced")
                {
                    victoryAdvancedCueStickList.Add(i);
                }
                else if (GetVictoryCueStickType(i) == "Expert")
                {
                    victoryExpertCueStickList.Add(i);
                }
                if (GetVictoryCueStickIsUnlockedFlag(i) == 1)
                {
                    ownedCueStickList.Add(victoryCueStickList[i]);
                }
            }

        }

        //Surprise Cue data details

        public void SurpriseCueDetails()
        {
            //Survivalist cue
            CueStickProperties survivalistCueStickProperties = new CueStickProperties(3, 2, 4, 4, 50);
            List<CueStickLevel> survivalistCueStickLevels = new List<CueStickLevel>();
            CueStickLevel survivalistCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel survivalistCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel survivalistCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel survivalistCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel survivalistCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            survivalistCueStickLevels.Add(survivalistCueLevel1);
            survivalistCueStickLevels.Add(survivalistCueLevel2);
            survivalistCueStickLevels.Add(survivalistCueLevel3);
            survivalistCueStickLevels.Add(survivalistCueLevel4);
            survivalistCueStickLevels.Add(survivalistCueLevel5);
            CueStick survivalistCue = new CueStick(cueSprites[2], "survivalist Cue", "Surprise", null, "NA", 0, "100", 100, 0, 4, 5, survivalistCueStickLevels, "Rare", 0, 0, survivalistCueStickProperties);
            surpriseCueStickList.Add(survivalistCue);
            //Archangel Cue
            CueStickProperties archangelCueStickProperties = new CueStickProperties(9, 9, 8, 8, 50);
            List<CueStickLevel> archangelCueStickLevels = new List<CueStickLevel>();
            CueStickLevel archangelCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel archangelCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel archangelCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel archangelCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel archangelCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            archangelCueStickLevels.Add(archangelCueLevel1);
            archangelCueStickLevels.Add(archangelCueLevel2);
            archangelCueStickLevels.Add(archangelCueLevel3);
            archangelCueStickLevels.Add(archangelCueLevel4);
            archangelCueStickLevels.Add(archangelCueLevel5);
            CueStick archangelCue = new CueStick(cueSprites[2], "Archangel Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, archangelCueStickLevels, "Legendary", 0, 0, archangelCueStickProperties);
            surpriseCueStickList.Add(archangelCue);
            //Archon Cue
            CueStickProperties archonCueStickProperties = new CueStickProperties(10, 8, 9, 8, 50);
            List<CueStickLevel> archonCueStickLevels = new List<CueStickLevel>();
            CueStickLevel archonCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel archonCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel archonCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel archonCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel archonCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            archonCueStickLevels.Add(archonCueLevel1);
            archonCueStickLevels.Add(archonCueLevel2);
            archonCueStickLevels.Add(archonCueLevel3);
            archonCueStickLevels.Add(archonCueLevel4);
            archonCueStickLevels.Add(archonCueLevel5);
            CueStick archonCue = new CueStick(cueSprites[2], "Archon Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, archonCueStickLevels, "Legendary", 0, 0, archonCueStickProperties);
            surpriseCueStickList.Add(archonCue);
            //Atlantis Cue
            CueStickProperties atlantisCueStickProperties = new CueStickProperties(9, 8, 7, 8, 50);
            List<CueStickLevel> atlantisCueStickLevels = new List<CueStickLevel>();
            CueStickLevel atlantisCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel atlantisCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel atlantisCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel atlantisCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel atlantisCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            atlantisCueStickLevels.Add(atlantisCueLevel1);
            atlantisCueStickLevels.Add(atlantisCueLevel2);
            atlantisCueStickLevels.Add(atlantisCueLevel3);
            atlantisCueStickLevels.Add(atlantisCueLevel4);
            atlantisCueStickLevels.Add(atlantisCueLevel5);
            CueStick atlantisCue = new CueStick(cueSprites[2], "Atlantis Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, atlantisCueStickLevels, "Legendary", 0, 0, atlantisCueStickProperties);
            surpriseCueStickList.Add(atlantisCue);
            //Axiom Cue
            CueStickProperties axiomCueStickProperties = new CueStickProperties(8, 7, 6, 7, 50);
            List<CueStickLevel> axiomCueStickLevels = new List<CueStickLevel>();
            CueStickLevel axiomCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel axiomCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel axiomCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel axiomCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel axiomCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            axiomCueStickLevels.Add(axiomCueLevel1);
            axiomCueStickLevels.Add(axiomCueLevel2);
            axiomCueStickLevels.Add(axiomCueLevel3);
            axiomCueStickLevels.Add(axiomCueLevel4);
            axiomCueStickLevels.Add(axiomCueLevel5);
            CueStick axiomCue = new CueStick(cueSprites[2], "Axiom Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, axiomCueStickLevels, "Legendary", 0, 0, axiomCueStickProperties);
            surpriseCueStickList.Add(axiomCue);
            //Crystal Blade Cue
            CueStickProperties crystalBladeCueStickProperties = new CueStickProperties(9, 7, 9, 7, 50);
            List<CueStickLevel> crystalBladeCueStickLevels = new List<CueStickLevel>();
            CueStickLevel crystalBladeCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel crystalBladeCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel crystalBladeCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel crystalBladeCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel crystalBladeCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            crystalBladeCueStickLevels.Add(crystalBladeCueLevel1);
            crystalBladeCueStickLevels.Add(crystalBladeCueLevel2);
            crystalBladeCueStickLevels.Add(crystalBladeCueLevel3);
            crystalBladeCueStickLevels.Add(crystalBladeCueLevel4);
            crystalBladeCueStickLevels.Add(crystalBladeCueLevel5);
            CueStick crystalBladeCue = new CueStick(cueSprites[2], "Brystal Blade Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, crystalBladeCueStickLevels, "Legendary", 0, 0, crystalBladeCueStickProperties);
            surpriseCueStickList.Add(crystalBladeCue);
            //Excalibur Cue
            CueStickProperties excaliburCueStickProperties = new CueStickProperties(8, 8, 7, 7, 50);
            List<CueStickLevel> excaliburCueStickLevels = new List<CueStickLevel>();
            CueStickLevel excaliburCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel excaliburCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel excaliburCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 0);
            CueStickLevel excaliburCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel excaliburCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 1, 0);
            excaliburCueStickLevels.Add(excaliburCueLevel1);
            excaliburCueStickLevels.Add(excaliburCueLevel2);
            excaliburCueStickLevels.Add(excaliburCueLevel3);
            excaliburCueStickLevels.Add(excaliburCueLevel4);
            excaliburCueStickLevels.Add(excaliburCueLevel5);
            CueStick excaliburCue = new CueStick(cueSprites[2], "Excalibur Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, excaliburCueStickLevels, "Legendary", 0, 0, excaliburCueStickProperties);
            surpriseCueStickList.Add(excaliburCue);
            //Firestorm Cue
            CueStickProperties firestormCueStickProperties = new CueStickProperties(9, 8, 8, 9, 50);
            List<CueStickLevel> firestormCueStickLevels = new List<CueStickLevel>();
            CueStickLevel firestormCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel firestormCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel firestormCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel firestormCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel firestormCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            firestormCueStickLevels.Add(firestormCueLevel1);
            firestormCueStickLevels.Add(firestormCueLevel2);
            firestormCueStickLevels.Add(firestormCueLevel3);
            firestormCueStickLevels.Add(firestormCueLevel4);
            firestormCueStickLevels.Add(firestormCueLevel5);
            CueStick firestormCue = new CueStick(cueSprites[2], "Firestorm Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, firestormCueStickLevels, "Legendary", 0, 0, firestormCueStickProperties);
            surpriseCueStickList.Add(firestormCue);
            //Inferno Cue
            CueStickProperties infernoCueStickProperties = new CueStickProperties(8, 8, 6, 5, 50);
            List<CueStickLevel> infernoCueStickLevels = new List<CueStickLevel>();
            CueStickLevel infernoCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel infernoCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel infernoCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel infernoCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel infernoCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 1);
            infernoCueStickLevels.Add(infernoCueLevel1);
            infernoCueStickLevels.Add(infernoCueLevel2);
            infernoCueStickLevels.Add(infernoCueLevel3);
            infernoCueStickLevels.Add(infernoCueLevel4);
            infernoCueStickLevels.Add(infernoCueLevel5);
            CueStick infernoCue = new CueStick(cueSprites[2], "Inferno Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, infernoCueStickLevels, "Legendary", 0, 0, infernoCueStickProperties);
            surpriseCueStickList.Add(infernoCue);
            //Kraken Cue
            CueStickProperties krakenCueStickProperties = new CueStickProperties(7, 7, 6, 8, 50);
            List<CueStickLevel> krakenCueStickLevels = new List<CueStickLevel>();
            CueStickLevel krakenCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel krakenCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel krakenCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel krakenCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel krakenCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 1);
            krakenCueStickLevels.Add(krakenCueLevel1);
            krakenCueStickLevels.Add(krakenCueLevel2);
            krakenCueStickLevels.Add(krakenCueLevel3);
            krakenCueStickLevels.Add(krakenCueLevel4);
            krakenCueStickLevels.Add(krakenCueLevel5);
            CueStick krakenCue = new CueStick(cueSprites[2], "Kraken Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, krakenCueStickLevels, "Legendary", 0, 0, krakenCueStickProperties);
            surpriseCueStickList.Add(krakenCue);
            //Laser Cue
            CueStickProperties laserCueStickProperties = new CueStickProperties(7, 8, 5, 9, 50);
            List<CueStickLevel> laserCueStickLevels = new List<CueStickLevel>();
            CueStickLevel laserCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel laserCueLevel2 = new CueStickLevel(2, 0, 0, 1, 0, 0, 0);
            CueStickLevel laserCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel laserCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel laserCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 1, 0);
            laserCueStickLevels.Add(laserCueLevel1);
            laserCueStickLevels.Add(laserCueLevel2);
            laserCueStickLevels.Add(laserCueLevel3);
            laserCueStickLevels.Add(laserCueLevel4);
            laserCueStickLevels.Add(laserCueLevel5);
            CueStick laserCue = new CueStick(cueSprites[2], "Laser Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, laserCueStickLevels, "Legendary", 0, 0, laserCueStickProperties);
            surpriseCueStickList.Add(laserCue);
            //Mechatronic ue
            CueStickProperties mechatronicCueStickProperties = new CueStickProperties(9, 6, 7, 8, 50);
            List<CueStickLevel> mechatronicCueStickLevels = new List<CueStickLevel>();
            CueStickLevel mechatronicCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel mechatronicCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel mechatronicCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel mechatronicCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel mechatronicCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            mechatronicCueStickLevels.Add(mechatronicCueLevel1);
            mechatronicCueStickLevels.Add(mechatronicCueLevel2);
            mechatronicCueStickLevels.Add(mechatronicCueLevel3);
            mechatronicCueStickLevels.Add(mechatronicCueLevel4);
            mechatronicCueStickLevels.Add(mechatronicCueLevel5);
            CueStick mechatronicCue = new CueStick(cueSprites[2], "Mechatronic Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, mechatronicCueStickLevels, "Legendary", 0, 0, mechatronicCueStickProperties);
            surpriseCueStickList.Add(mechatronicCue);
            //Medusa Cue
            CueStickProperties medusaCueStickProperties = new CueStickProperties(8, 6, 7, 7, 50);
            List<CueStickLevel> medusaCueStickLevels = new List<CueStickLevel>();
            CueStickLevel medusaCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel medusaCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel medusaCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel medusaCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel medusaCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            medusaCueStickLevels.Add(medusaCueLevel1);
            medusaCueStickLevels.Add(medusaCueLevel2);
            medusaCueStickLevels.Add(medusaCueLevel3);
            medusaCueStickLevels.Add(medusaCueLevel4);
            medusaCueStickLevels.Add(medusaCueLevel5);
            CueStick medusaCue = new CueStick(cueSprites[2], "Medusa Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, medusaCueStickLevels, "Legendary", 0, 0, medusaCueStickProperties);
            surpriseCueStickList.Add(medusaCue);
            //Minotaur Cue
            CueStickProperties minotaurCueStickProperties = new CueStickProperties(8, 5, 8, 8, 50);
            List<CueStickLevel> minotaurCueStickLevels = new List<CueStickLevel>();
            CueStickLevel minotaurCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel minotaurCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel minotaurCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel minotaurCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel minotaurCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 1, 0);
            minotaurCueStickLevels.Add(minotaurCueLevel1);
            minotaurCueStickLevels.Add(minotaurCueLevel2);
            minotaurCueStickLevels.Add(minotaurCueLevel3);
            minotaurCueStickLevels.Add(minotaurCueLevel4);
            minotaurCueStickLevels.Add(minotaurCueLevel5);
            CueStick minotaurCue = new CueStick(cueSprites[2], "Minotaur Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, minotaurCueStickLevels, "Legendary", 0, 0, minotaurCueStickProperties);
            surpriseCueStickList.Add(minotaurCue);
            //Mythical Cue
            CueStickProperties mythicalCueStickProperties = new CueStickProperties(6, 7, 7, 8, 50);
            List<CueStickLevel> mythicalCueStickLevels = new List<CueStickLevel>();
            CueStickLevel mythicalCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel mythicalCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel mythicalCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel mythicalCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel mythicalCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            mythicalCueStickLevels.Add(mythicalCueLevel1);
            mythicalCueStickLevels.Add(mythicalCueLevel2);
            mythicalCueStickLevels.Add(mythicalCueLevel3);
            mythicalCueStickLevels.Add(mythicalCueLevel4);
            mythicalCueStickLevels.Add(mythicalCueLevel5);
            CueStick mythicalCue = new CueStick(cueSprites[2], "Mythical Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, mythicalCueStickLevels, "Legendary", 0, 0, mythicalCueStickProperties);
            surpriseCueStickList.Add(mythicalCue);
            //Necromancer Cue
            CueStickProperties necromancerCueStickProperties = new CueStickProperties(9, 6, 8, 6, 50);
            List<CueStickLevel> necromancerCueStickLevels = new List<CueStickLevel>();
            CueStickLevel necromancerCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel necromancerCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel necromancerCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel necromancerCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel necromancerCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 1, 0);
            necromancerCueStickLevels.Add(necromancerCueLevel1);
            necromancerCueStickLevels.Add(necromancerCueLevel2);
            necromancerCueStickLevels.Add(necromancerCueLevel3);
            necromancerCueStickLevels.Add(necromancerCueLevel4);
            necromancerCueStickLevels.Add(necromancerCueLevel5);
            CueStick necromancerCue = new CueStick(cueSprites[2], "Necromancer Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, necromancerCueStickLevels, "Legendary", 0, 0, necromancerCueStickProperties);
            surpriseCueStickList.Add(necromancerCue);
            //Phoenix Cue
            CueStickProperties phoenixCueStickProperties = new CueStickProperties(7, 8, 8, 4, 50);
            List<CueStickLevel> phoenixCueStickLevels = new List<CueStickLevel>();
            CueStickLevel phoenixCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel phoenixCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel phoenixCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel phoenixCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel phoenixCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            phoenixCueStickLevels.Add(phoenixCueLevel1);
            phoenixCueStickLevels.Add(phoenixCueLevel2);
            phoenixCueStickLevels.Add(phoenixCueLevel3);
            phoenixCueStickLevels.Add(phoenixCueLevel4);
            phoenixCueStickLevels.Add(phoenixCueLevel5);
            CueStick phoenixCue = new CueStick(cueSprites[2], "Phoenix Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, phoenixCueStickLevels, "Legendary", 0, 0, phoenixCueStickProperties);
            surpriseCueStickList.Add(phoenixCue);
            //Plasma Cue
            CueStickProperties plasmaCueStickProperties = new CueStickProperties(8, 8, 7, 8, 50);
            List<CueStickLevel> plasmaCueStickLevels = new List<CueStickLevel>();
            CueStickLevel plasmaCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel plasmaCueLevel2 = new CueStickLevel(2, 0, 0, 1, 0, 0, 0);
            CueStickLevel plasmaCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel plasmaCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel plasmaCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            plasmaCueStickLevels.Add(plasmaCueLevel1);
            plasmaCueStickLevels.Add(plasmaCueLevel2);
            plasmaCueStickLevels.Add(plasmaCueLevel3);
            plasmaCueStickLevels.Add(plasmaCueLevel4);
            plasmaCueStickLevels.Add(plasmaCueLevel5);
            CueStick plasmaCue = new CueStick(cueSprites[2], " Plasma Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, plasmaCueStickLevels, "Legendary", 0, 0, plasmaCueStickProperties);
            surpriseCueStickList.Add(plasmaCue);
            //Shangri La Cue
            CueStickProperties shangriLaCueStickProperties = new CueStickProperties(9, 7, 8, 8, 50);
            List<CueStickLevel> shangriLaCueStickLevels = new List<CueStickLevel>();
            CueStickLevel shangriLaCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel shangriLaCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel shangriLaCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel shangriLaCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel shangriLaCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            shangriLaCueStickLevels.Add(shangriLaCueLevel1);
            shangriLaCueStickLevels.Add(shangriLaCueLevel2);
            shangriLaCueStickLevels.Add(shangriLaCueLevel3);
            shangriLaCueStickLevels.Add(shangriLaCueLevel4);
            shangriLaCueStickLevels.Add(shangriLaCueLevel5);
            CueStick shangriLaCue = new CueStick(cueSprites[2], "Shangri La Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, shangriLaCueStickLevels, "Legendary", 0, 0, shangriLaCueStickProperties);
            surpriseCueStickList.Add(shangriLaCue);
            //Thor Hammer Cue
            CueStickProperties thorHammerCueStickProperties = new CueStickProperties(8, 7, 8, 8, 50);
            List<CueStickLevel> thorHammerCueStickLevels = new List<CueStickLevel>();
            CueStickLevel thorHammerCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel thorHammerCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel thorHammerCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel thorHammerCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel thorHammerCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            thorHammerCueStickLevels.Add(thorHammerCueLevel1);
            thorHammerCueStickLevels.Add(thorHammerCueLevel2);
            thorHammerCueStickLevels.Add(thorHammerCueLevel3);
            thorHammerCueStickLevels.Add(thorHammerCueLevel4);
            thorHammerCueStickLevels.Add(thorHammerCueLevel5);
            CueStick thorHammerCue = new CueStick(cueSprites[2], "Thor Hammer Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, thorHammerCueStickLevels, "Legendary", 0, 0, thorHammerCueStickProperties);
            surpriseCueStickList.Add(thorHammerCue);
            //Valkyrie Cue
            CueStickProperties valkyrieCueStickProperties = new CueStickProperties(8, 9, 9, 8, 50);
            List<CueStickLevel> valkyrierCueStickLevels = new List<CueStickLevel>();
            CueStickLevel valkyrieCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel valkyrieCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel valkyrieCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 0);
            CueStickLevel valkyrieCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel valkyrieCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 1, 0);
            valkyrierCueStickLevels.Add(valkyrieCueLevel1);
            valkyrierCueStickLevels.Add(valkyrieCueLevel2);
            valkyrierCueStickLevels.Add(valkyrieCueLevel3);
            valkyrierCueStickLevels.Add(valkyrieCueLevel4);
            valkyrierCueStickLevels.Add(valkyrieCueLevel5);
            CueStick valkyrieCue = new CueStick(cueSprites[2], "Valkyrie Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, valkyrierCueStickLevels, "Legendary", 0, 0, valkyrieCueStickProperties);
            surpriseCueStickList.Add(valkyrieCue);
            //Aztec Cue
            CueStickProperties aztecCueStickProperties = new CueStickProperties(7, 5, 6, 5, 50);
            List<CueStickLevel> aztecCueStickLevels = new List<CueStickLevel>();
            CueStickLevel aztecCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel aztecCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel aztecCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 0);
            CueStickLevel aztecCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel aztecCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 1, 0);
            aztecCueStickLevels.Add(aztecCueLevel1);
            aztecCueStickLevels.Add(aztecCueLevel2);
            aztecCueStickLevels.Add(aztecCueLevel3);
            aztecCueStickLevels.Add(aztecCueLevel4);
            aztecCueStickLevels.Add(aztecCueLevel5);
            CueStick aztecCue = new CueStick(cueSprites[2], "Aztec Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, aztecCueStickLevels, "Epic", 0, 0, aztecCueStickProperties);
            surpriseCueStickList.Add(aztecCue);
            //Emerald Cue
            CueStickProperties emeraldCueStickProperties = new CueStickProperties(7, 6, 5, 5, 50);
            List<CueStickLevel> emeraldCueStickLevels = new List<CueStickLevel>();
            CueStickLevel emeraldCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel emeraldCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel emeraldCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel emeraldCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel emeraldCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            emeraldCueStickLevels.Add(emeraldCueLevel1);
            emeraldCueStickLevels.Add(emeraldCueLevel2);
            emeraldCueStickLevels.Add(emeraldCueLevel3);
            emeraldCueStickLevels.Add(emeraldCueLevel4);
            emeraldCueStickLevels.Add(emeraldCueLevel5);
            CueStick emeraldCue = new CueStick(cueSprites[2], "Emerald Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, emeraldCueStickLevels, "Epic", 0, 0, emeraldCueStickProperties);
            surpriseCueStickList.Add(emeraldCue);
            //Everlasting Cue
            CueStickProperties everlastingCueStickProperties = new CueStickProperties(7, 7, 6, 7, 50);
            List<CueStickLevel> everlastingCueStickLevels = new List<CueStickLevel>();
            CueStickLevel everlastingCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel everlastingCueLevel2 = new CueStickLevel(2, 0, 0, 1, 0, 0, 0);
            CueStickLevel everlastingCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel everlastingCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel everlastingCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 1, 0);
            everlastingCueStickLevels.Add(everlastingCueLevel1);
            everlastingCueStickLevels.Add(everlastingCueLevel2);
            everlastingCueStickLevels.Add(everlastingCueLevel3);
            everlastingCueStickLevels.Add(everlastingCueLevel4);
            everlastingCueStickLevels.Add(everlastingCueLevel5);
            CueStick everlastingCue = new CueStick(cueSprites[2], "Everlasting Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, everlastingCueStickLevels, "Epic", 0, 0, everlastingCueStickProperties);
            surpriseCueStickList.Add(everlastingCue);
            //Frost Bite Cue
            CueStickProperties frostBiteCueStickProperties = new CueStickProperties(7, 4, 3, 7, 50);
            List<CueStickLevel> frostBiteCueStickLevels = new List<CueStickLevel>();
            CueStickLevel frostBiteCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel frostBiteCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel frostBiteCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel frostBiteCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel frostBiteCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            frostBiteCueStickLevels.Add(frostBiteCueLevel1);
            frostBiteCueStickLevels.Add(frostBiteCueLevel2);
            frostBiteCueStickLevels.Add(frostBiteCueLevel3);
            frostBiteCueStickLevels.Add(frostBiteCueLevel4);
            frostBiteCueStickLevels.Add(frostBiteCueLevel5);
            CueStick frostBiteCue = new CueStick(cueSprites[2], "Frost Bite Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, frostBiteCueStickLevels, "Epic", 0, 0, frostBiteCueStickProperties);
            surpriseCueStickList.Add(frostBiteCue);
            //Fusion Drive Cue
            CueStickProperties fusionDriveCueStickProperties = new CueStickProperties(6, 5, 5, 3, 50);
            List<CueStickLevel> fusionDriveCueStickLevels = new List<CueStickLevel>();
            CueStickLevel fusionDriveCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel fusionDriveCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel fusionDriveCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel fusionDriveCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel fusionDriveCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            fusionDriveCueStickLevels.Add(fusionDriveCueLevel1);
            fusionDriveCueStickLevels.Add(fusionDriveCueLevel2);
            fusionDriveCueStickLevels.Add(fusionDriveCueLevel3);
            fusionDriveCueStickLevels.Add(fusionDriveCueLevel4);
            fusionDriveCueStickLevels.Add(fusionDriveCueLevel5);
            CueStick fusionDriveCue = new CueStick(cueSprites[2], "Fusion Drive Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, fusionDriveCueStickLevels, "Epic", 0, 0, fusionDriveCueStickProperties);
            surpriseCueStickList.Add(fusionDriveCue);
            //Gengis Khan
            CueStickProperties gengisKhanCueStickProperties = new CueStickProperties(6, 5, 6, 5, 50);
            List<CueStickLevel> gengisKhanCueStickLevels = new List<CueStickLevel>();
            CueStickLevel gengisKhanCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel gengisKhanCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel gengisKhanCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 0);
            CueStickLevel gengisKhanCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel gengisKhanCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            gengisKhanCueStickLevels.Add(gengisKhanCueLevel1);
            gengisKhanCueStickLevels.Add(gengisKhanCueLevel2);
            gengisKhanCueStickLevels.Add(gengisKhanCueLevel3);
            gengisKhanCueStickLevels.Add(gengisKhanCueLevel4);
            gengisKhanCueStickLevels.Add(gengisKhanCueLevel5);
            CueStick gengisKhanCue = new CueStick(cueSprites[2], "Gengis Khan Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, gengisKhanCueStickLevels, "Epic", 0, 0, gengisKhanCueStickProperties);
            surpriseCueStickList.Add(gengisKhanCue);
            //Greatsword Cue
            CueStickProperties greatswordCueStickProperties = new CueStickProperties(6, 5, 5, 5, 50);
            List<CueStickLevel> greatswordCueStickLevels = new List<CueStickLevel>();
            CueStickLevel greatswordCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel greatswordCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel greatswordCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel greatswordCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel greatswordCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            greatswordCueStickLevels.Add(greatswordCueLevel1);
            greatswordCueStickLevels.Add(greatswordCueLevel2);
            greatswordCueStickLevels.Add(greatswordCueLevel3);
            greatswordCueStickLevels.Add(greatswordCueLevel4);
            greatswordCueStickLevels.Add(greatswordCueLevel5);
            CueStick greatswordCue = new CueStick(cueSprites[2], "Greatsword Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, greatswordCueStickLevels, "Epic", 0, 0, greatswordCueStickProperties);
            surpriseCueStickList.Add(greatswordCue);
            //Guardian Spirit Cue
            CueStickProperties guardianSpiritCueStickProperties = new CueStickProperties(6, 5, 4, 6, 50);
            List<CueStickLevel> guardianSpiritCueStickLevels = new List<CueStickLevel>();
            CueStickLevel guardianSpiritCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel guardianSpiritCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel guardianSpiritCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel guardianSpiritCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel guardianSpiritCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            guardianSpiritCueStickLevels.Add(guardianSpiritCueLevel1);
            guardianSpiritCueStickLevels.Add(guardianSpiritCueLevel2);
            guardianSpiritCueStickLevels.Add(guardianSpiritCueLevel3);
            guardianSpiritCueStickLevels.Add(guardianSpiritCueLevel4);
            guardianSpiritCueStickLevels.Add(guardianSpiritCueLevel5);
            CueStick guardianSpiritCue = new CueStick(cueSprites[2], "Guardian Spirit Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, guardianSpiritCueStickLevels, "Epic", 0, 0, guardianSpiritCueStickProperties);
            surpriseCueStickList.Add(guardianSpiritCue);
            //Hercules cue
            CueStickProperties herculesCueStickProperties = new CueStickProperties(6, 6, 7, 7, 50);
            List<CueStickLevel> herculesCueStickLevels = new List<CueStickLevel>();
            CueStickLevel herculesCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel herculesCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel herculesCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel herculesCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel herculesCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            herculesCueStickLevels.Add(herculesCueLevel1);
            herculesCueStickLevels.Add(herculesCueLevel2);
            herculesCueStickLevels.Add(herculesCueLevel3);
            herculesCueStickLevels.Add(herculesCueLevel4);
            herculesCueStickLevels.Add(herculesCueLevel5);
            CueStick herculesCue = new CueStick(cueSprites[2], "Hercules Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, herculesCueStickLevels, "Epic", 0, 0, herculesCueStickProperties);
            surpriseCueStickList.Add(herculesCue);
            //Invader Cue
            CueStickProperties invaderCueStickProperties = new CueStickProperties(5, 7, 5, 5, 50);
            List<CueStickLevel> invaderCueStickLevels = new List<CueStickLevel>();
            CueStickLevel invaderCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel invaderCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel invaderCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 0);
            CueStickLevel invaderCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel invaderCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            invaderCueStickLevels.Add(invaderCueLevel1);
            invaderCueStickLevels.Add(invaderCueLevel2);
            invaderCueStickLevels.Add(invaderCueLevel3);
            invaderCueStickLevels.Add(invaderCueLevel4);
            invaderCueStickLevels.Add(invaderCueLevel5);
            CueStick invaderCue = new CueStick(cueSprites[2], "Invader Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, invaderCueStickLevels, "Epic", 0, 0, invaderCueStickProperties);
            surpriseCueStickList.Add(invaderCue);
            //Jade Relic Cue
            CueStickProperties jadeRelicCueStickProperties = new CueStickProperties(6, 6, 6, 6, 50);
            List<CueStickLevel> jadeRelicCueStickLevels = new List<CueStickLevel>();
            CueStickLevel jadeRelicCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel jadeRelicCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel jadeRelicCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel jadeRelicCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel jadeRelicCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            jadeRelicCueStickLevels.Add(jadeRelicCueLevel1);
            jadeRelicCueStickLevels.Add(jadeRelicCueLevel2);
            jadeRelicCueStickLevels.Add(jadeRelicCueLevel3);
            jadeRelicCueStickLevels.Add(jadeRelicCueLevel4);
            jadeRelicCueStickLevels.Add(jadeRelicCueLevel5);
            CueStick jadeRelicCue = new CueStick(cueSprites[2], "Jade Relic Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, jadeRelicCueStickLevels, "Epic", 0, 0, jadeRelicCueStickProperties);
            surpriseCueStickList.Add(jadeRelicCue);
            //Jaguar Cue
            CueStickProperties jaguarCueStickProperties = new CueStickProperties(6, 4, 5, 5, 50);
            List<CueStickLevel> jaguarCueStickLevels = new List<CueStickLevel>();
            CueStickLevel jaguarCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel jaguarCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel jaguarCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel jaguarCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel jaguarCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            jaguarCueStickLevels.Add(jaguarCueLevel1);
            jaguarCueStickLevels.Add(jaguarCueLevel2);
            jaguarCueStickLevels.Add(jaguarCueLevel3);
            jaguarCueStickLevels.Add(jaguarCueLevel4);
            jaguarCueStickLevels.Add(jaguarCueLevel5);
            CueStick jaguarCue = new CueStick(cueSprites[2], "Jaguar Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, jaguarCueStickLevels, "Epic", 0, 0, jaguarCueStickProperties);
            surpriseCueStickList.Add(jaguarCue);
            //Lavaforged Cue
            CueStickProperties lavaforgedCueStickProperties = new CueStickProperties(8, 6, 4, 7, 50);
            List<CueStickLevel> lavaforgedCueStickLevels = new List<CueStickLevel>();
            CueStickLevel lavaforgedCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel lavaforgedCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel lavaforgedCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel lavaforgedCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel lavaforgedCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            lavaforgedCueStickLevels.Add(lavaforgedCueLevel1);
            lavaforgedCueStickLevels.Add(lavaforgedCueLevel2);
            lavaforgedCueStickLevels.Add(lavaforgedCueLevel3);
            lavaforgedCueStickLevels.Add(lavaforgedCueLevel4);
            lavaforgedCueStickLevels.Add(lavaforgedCueLevel5);
            CueStick lavaforgedCue = new CueStick(cueSprites[2], "Lavaforged Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, lavaforgedCueStickLevels, "Epic", 0, 0, lavaforgedCueStickProperties);
            surpriseCueStickList.Add(lavaforgedCue);
            //Moonstone Cue
            CueStickProperties moonstoneCueStickProperties = new CueStickProperties(8, 6, 7, 5, 50);
            List<CueStickLevel> moonstoneCueStickLevels = new List<CueStickLevel>();
            CueStickLevel moonstoneCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel moonstoneCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel moonstoneCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel moonstoneCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel moonstoneCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 1, 0);
            moonstoneCueStickLevels.Add(moonstoneCueLevel1);
            moonstoneCueStickLevels.Add(moonstoneCueLevel2);
            moonstoneCueStickLevels.Add(moonstoneCueLevel3);
            moonstoneCueStickLevels.Add(moonstoneCueLevel4);
            moonstoneCueStickLevels.Add(moonstoneCueLevel5);
            CueStick moonstoneCue = new CueStick(cueSprites[2], "Moonstone Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, moonstoneCueStickLevels, "Epic", 0, 0, moonstoneCueStickProperties);
            surpriseCueStickList.Add(moonstoneCue);
            //Persia Cue
            CueStickProperties persiaCueStickProperties = new CueStickProperties(7, 5, 7, 6, 50);
            List<CueStickLevel> persiaCueStickLevels = new List<CueStickLevel>();
            CueStickLevel persiaCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel persiaCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel persiaCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 0);
            CueStickLevel persiaCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel persiaCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            persiaCueStickLevels.Add(persiaCueLevel1);
            persiaCueStickLevels.Add(persiaCueLevel2);
            persiaCueStickLevels.Add(persiaCueLevel3);
            persiaCueStickLevels.Add(persiaCueLevel4);
            persiaCueStickLevels.Add(persiaCueLevel5);
            CueStick persiaCue = new CueStick(cueSprites[2], "Persia Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, persiaCueStickLevels, "Epic", 0, 0, persiaCueStickProperties);
            surpriseCueStickList.Add(persiaCue);
            //Scimitar Cue
            CueStickProperties scimitarCueStickProperties = new CueStickProperties(6, 4, 6, 4, 50);
            List<CueStickLevel> scimitarCueStickLevels = new List<CueStickLevel>();
            CueStickLevel scimitarCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel scimitarCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel scimitarCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel scimitarCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel scimitarCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 1, 0);
            scimitarCueStickLevels.Add(scimitarCueLevel1);
            scimitarCueStickLevels.Add(scimitarCueLevel2);
            scimitarCueStickLevels.Add(scimitarCueLevel3);
            scimitarCueStickLevels.Add(scimitarCueLevel4);
            scimitarCueStickLevels.Add(scimitarCueLevel5);
            CueStick scimitarCue = new CueStick(cueSprites[2], "Scimitar Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, scimitarCueStickLevels, "Epic", 0, 0, scimitarCueStickProperties);
            surpriseCueStickList.Add(scimitarCue);
            //Scorpion Cue
            CueStickProperties scorpionCueStickProperties = new CueStickProperties(7, 6, 5, 1, 50);
            List<CueStickLevel> scorpionCueStickLevels = new List<CueStickLevel>();
            CueStickLevel scorpionCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel scorpionCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel scorpionCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel scorpionCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel scorpionCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            scorpionCueStickLevels.Add(scorpionCueLevel1);
            scorpionCueStickLevels.Add(scorpionCueLevel2);
            scorpionCueStickLevels.Add(scorpionCueLevel3);
            scorpionCueStickLevels.Add(scorpionCueLevel4);
            scorpionCueStickLevels.Add(scorpionCueLevel5);
            CueStick scorpionCue = new CueStick(cueSprites[2], "Scorpion Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, scorpionCueStickLevels, "Epic", 0, 0, scorpionCueStickProperties);
            surpriseCueStickList.Add(scorpionCue);
            //Sparta Cue
            CueStickProperties spartaCueStickProperties = new CueStickProperties(7, 6, 7, 4, 50);
            List<CueStickLevel> spartaCueStickLevels = new List<CueStickLevel>();
            CueStickLevel spartaCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel spartaCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel spartaCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel spartaCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel spartaCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            spartaCueStickLevels.Add(spartaCueLevel1);
            spartaCueStickLevels.Add(spartaCueLevel2);
            spartaCueStickLevels.Add(spartaCueLevel3);
            spartaCueStickLevels.Add(spartaCueLevel4);
            spartaCueStickLevels.Add(spartaCueLevel5);
            CueStick spartaCue = new CueStick(cueSprites[2], "Sparta Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, spartaCueStickLevels, "Epic", 0, 0, spartaCueStickProperties);
            surpriseCueStickList.Add(spartaCue);
            //Tarantula Cue
            CueStickProperties tarantulaCueStickProperties = new CueStickProperties(5, 1, 6, 7, 50);
            List<CueStickLevel> tarantulaCueStickLevels = new List<CueStickLevel>();
            CueStickLevel tarantulaCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel tarantulaCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel tarantulaCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 0);
            CueStickLevel tarantulaCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel tarantulaCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            tarantulaCueStickLevels.Add(tarantulaCueLevel1);
            tarantulaCueStickLevels.Add(tarantulaCueLevel2);
            tarantulaCueStickLevels.Add(tarantulaCueLevel3);
            tarantulaCueStickLevels.Add(tarantulaCueLevel4);
            tarantulaCueStickLevels.Add(tarantulaCueLevel5);
            CueStick tarantulaCue = new CueStick(cueSprites[2], "Tarantula Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, tarantulaCueStickLevels, "Epic", 0, 0, tarantulaCueStickProperties);
            surpriseCueStickList.Add(tarantulaCue);
            //Trident Cue
            CueStickProperties tridentCueStickProperties = new CueStickProperties(7, 6, 3, 6, 50);
            List<CueStickLevel> tridentCueStickLevels = new List<CueStickLevel>();
            CueStickLevel tridentCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel tridentCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel tridentCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel tridentCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel tridentCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 1, 0);
            tridentCueStickLevels.Add(tridentCueLevel1);
            tridentCueStickLevels.Add(tridentCueLevel2);
            tridentCueStickLevels.Add(tridentCueLevel3);
            tridentCueStickLevels.Add(tridentCueLevel4);
            tridentCueStickLevels.Add(tridentCueLevel5);
            CueStick tridentlaCue = new CueStick(cueSprites[2], "Trident Cue", "Surprise", null, "NA", 0, "0", 0, 0, 4, 5, tridentCueStickLevels, "Epic", 0, 0, tridentCueStickProperties);
            surpriseCueStickList.Add(tridentlaCue);
            //Apache
            CueStickProperties apacheCueStickProperties = new CueStickProperties(4, 5, 3, 4, 50);
            List<CueStickLevel> apacheCueStickLevels = new List<CueStickLevel>();
            CueStickLevel apacheCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel apacheCueLevel2 = new CueStickLevel(2, 0, 0, 1, 0, 0, 0);
            CueStickLevel apacheCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel apacheCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel apacheCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            apacheCueStickLevels.Add(apacheCueLevel1);
            apacheCueStickLevels.Add(apacheCueLevel2);
            apacheCueStickLevels.Add(apacheCueLevel3);
            apacheCueStickLevels.Add(apacheCueLevel4);
            apacheCueStickLevels.Add(apacheCueLevel5);
            CueStick apacheCue = new CueStick(cueSprites[2], "Apache Cue", "Surprise", null, "NA", 0, "160", 160, 0, 4, 5, apacheCueStickLevels, "Rare", 0, 0, apacheCueStickProperties);
            surpriseCueStickList.Add(apacheCue);
            //Aristocrat Cue
            CueStickProperties aristocratCueStickProperties = new CueStickProperties(5, 3, 4, 4, 50);
            List<CueStickLevel> aristocratCueStickLevels = new List<CueStickLevel>();
            CueStickLevel aristocratCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel aristocratCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel aristocratCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel aristocratCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel aristocratCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            aristocratCueStickLevels.Add(aristocratCueLevel1);
            aristocratCueStickLevels.Add(aristocratCueLevel2);
            aristocratCueStickLevels.Add(aristocratCueLevel3);
            aristocratCueStickLevels.Add(aristocratCueLevel4);
            aristocratCueStickLevels.Add(aristocratCueLevel5);
            CueStick aristocratCue = new CueStick(cueSprites[2], "Aristocrat Cue", "Surprise", null, "NA", 0, "160", 160, 0, 4, 5, aristocratCueStickLevels, "Rare", 0, 0, aristocratCueStickProperties);
            surpriseCueStickList.Add(aristocratCue);
            //Bear Cue
            CueStickProperties bearCueStickProperties = new CueStickProperties(5, 3, 2, 3, 50);
            List<CueStickLevel> bearCueStickLevels = new List<CueStickLevel>();
            CueStickLevel bearCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel bearCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel bearCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel bearCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel bearCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 1);
            bearCueStickLevels.Add(bearCueLevel1);
            bearCueStickLevels.Add(bearCueLevel2);
            bearCueStickLevels.Add(bearCueLevel3);
            bearCueStickLevels.Add(bearCueLevel4);
            bearCueStickLevels.Add(bearCueLevel5);
            CueStick bearCue = new CueStick(cueSprites[2], "Bear Cue", "Surprise", null, "NA", 0, "160", 160, 0, 4, 5, bearCueStickLevels, "Rare", 0, 0, bearCueStickProperties);
            surpriseCueStickList.Add(bearCue);
            //Broadsword Cue
            CueStickProperties broadswordCueStickProperties = new CueStickProperties(4, 3, 3, 4, 50);
            List<CueStickLevel> broadswordCueStickLevels = new List<CueStickLevel>();
            CueStickLevel broadswordCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel broadswordCueLevel2 = new CueStickLevel(2, 0, 0, 1, 0, 0, 0);
            CueStickLevel broadswordCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel broadswordCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel broadswordCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 1, 0);
            broadswordCueStickLevels.Add(broadswordCueLevel1);
            broadswordCueStickLevels.Add(broadswordCueLevel2);
            broadswordCueStickLevels.Add(broadswordCueLevel3);
            broadswordCueStickLevels.Add(broadswordCueLevel4);
            broadswordCueStickLevels.Add(broadswordCueLevel5);
            CueStick broadswordCue = new CueStick(cueSprites[2], "Broadsword Cue", "Surprise", null, "NA", 0, "120", 120, 0, 4, 5, broadswordCueStickLevels, "Rare", 0, 0, broadswordCueStickProperties);
            surpriseCueStickList.Add(broadswordCue);
            //Cobra Cue
            CueStickProperties cobraCueStickProperties = new CueStickProperties(5, 3, 5, 2, 50);
            List<CueStickLevel> cobraCueStickLevels = new List<CueStickLevel>();
            CueStickLevel cobraCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel cobraCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel cobraCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel cobraCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel cobraCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            cobraCueStickLevels.Add(cobraCueLevel1);
            cobraCueStickLevels.Add(cobraCueLevel2);
            cobraCueStickLevels.Add(cobraCueLevel3);
            cobraCueStickLevels.Add(cobraCueLevel4);
            cobraCueStickLevels.Add(cobraCueLevel5);
            CueStick cobraCue = new CueStick(cueSprites[2], "Cobra Cue", "Surprise", null, "NA", 0, "140", 140, 0, 4, 5, cobraCueStickLevels, "Rare", 0, 0, cobraCueStickProperties);
            surpriseCueStickList.Add(cobraCue);
            //Dagger Cue
            CueStickProperties daggerCueStickProperties = new CueStickProperties(4, 4, 3, 2, 50);
            List<CueStickLevel> daggerStickLevels = new List<CueStickLevel>();
            CueStickLevel daggerCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel daggerCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel daggerCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 0);
            CueStickLevel daggerCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel daggerCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            daggerStickLevels.Add(daggerCueLevel1);
            daggerStickLevels.Add(daggerCueLevel2);
            daggerStickLevels.Add(daggerCueLevel3);
            daggerStickLevels.Add(daggerCueLevel4);
            daggerStickLevels.Add(daggerCueLevel5);
            CueStick daggerCue = new CueStick(cueSprites[2], "Dagger Cue", "Surprise", null, "NA", 0, "100", 100, 0, 4, 5, daggerStickLevels, "Rare", 0, 0, daggerCueStickProperties);
            surpriseCueStickList.Add(daggerCue);
            //Eagle Cue
            CueStickProperties eagleCueStickProperties = new CueStickProperties(3, 3, 3, 3, 50);
            List<CueStickLevel> eagleStickLevels = new List<CueStickLevel>();
            CueStickLevel eagleCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel eagleCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel eagleCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel eagleCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel eagleCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            eagleStickLevels.Add(eagleCueLevel1);
            eagleStickLevels.Add(eagleCueLevel2);
            eagleStickLevels.Add(eagleCueLevel3);
            eagleStickLevels.Add(eagleCueLevel4);
            eagleStickLevels.Add(eagleCueLevel5);
            CueStick eagleCue = new CueStick(cueSprites[2], "Eagle Cue", "Surprise", null, "NA", 0, "80", 80, 0, 4, 5, eagleStickLevels, "Rare", 0, 0, eagleCueStickProperties);
            surpriseCueStickList.Add(eagleCue);
            //Guerrilla Cue
            CueStickProperties guerrillaCueStickProperties = new CueStickProperties(5, 3, 4, 2, 50);
            List<CueStickLevel> guerrillaStickLevels = new List<CueStickLevel>();
            CueStickLevel guerrillaCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel guerrillaCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel guerrillaCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel guerrillaCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel guerrillaCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 1, 0);
            guerrillaStickLevels.Add(guerrillaCueLevel1);
            guerrillaStickLevels.Add(guerrillaCueLevel2);
            guerrillaStickLevels.Add(guerrillaCueLevel3);
            guerrillaStickLevels.Add(guerrillaCueLevel4);
            guerrillaStickLevels.Add(guerrillaCueLevel5);
            CueStick guerrillaCue = new CueStick(cueSprites[2], "Guerrilla Cue", "Surprise", null, "NA", 0, "120", 120, 0, 4, 5, guerrillaStickLevels, "Rare", 0, 0, guerrillaCueStickProperties);
            surpriseCueStickList.Add(guerrillaCue);
            //Hopelite Cue
            CueStickProperties hopliteCueStickProperties = new CueStickProperties(4, 4, 4, 5, 50);
            List<CueStickLevel> hopliteStickLevels = new List<CueStickLevel>();
            CueStickLevel hopliteCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel hopliteCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel hopliteCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 2);
            CueStickLevel hopliteCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel hopliteCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            hopliteStickLevels.Add(hopliteCueLevel1);
            hopliteStickLevels.Add(hopliteCueLevel2);
            hopliteStickLevels.Add(hopliteCueLevel3);
            hopliteStickLevels.Add(hopliteCueLevel4);
            hopliteStickLevels.Add(hopliteCueLevel5);
            CueStick hopliteCue = new CueStick(cueSprites[2], "Hoplite Cue", "Surprise", null, "NA", 0, "180", 180, 0, 4, 5, hopliteStickLevels, "Rare", 0, 0, hopliteCueStickProperties);
            surpriseCueStickList.Add(hopliteCue);
            //Jackal Cue
            CueStickProperties jackalCueStickProperties = new CueStickProperties(4, 4, 2, 4, 50);
            List<CueStickLevel> jackalStickLevels = new List<CueStickLevel>();
            CueStickLevel jackalCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel jackalCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel jackalCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 0);
            CueStickLevel jackalCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel jackalCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            jackalStickLevels.Add(jackalCueLevel1);
            jackalStickLevels.Add(jackalCueLevel2);
            jackalStickLevels.Add(jackalCueLevel3);
            jackalStickLevels.Add(jackalCueLevel4);
            jackalStickLevels.Add(jackalCueLevel5);
            CueStick jackalCue = new CueStick(cueSprites[2], "Jackal Cue", "Surprise", null, "NA", 0, "120", 120, 0, 4, 5, jackalStickLevels, "Rare", 0, 0, jackalCueStickProperties);
            surpriseCueStickList.Add(jackalCue);
            //Ming Cue
            CueStickProperties mingCueStickProperties = new CueStickProperties(5, 4, 5, 1, 50);
            List<CueStickLevel> mingStickLevels = new List<CueStickLevel>();
            CueStickLevel mingCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel mingCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel mingCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel mingCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel mingCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            mingStickLevels.Add(mingCueLevel1);
            mingStickLevels.Add(mingCueLevel2);
            mingStickLevels.Add(mingCueLevel3);
            mingStickLevels.Add(mingCueLevel4);
            mingStickLevels.Add(mingCueLevel5);
            CueStick mingCue = new CueStick(cueSprites[2], "Ming Cue", "Surprise", null, "NA", 0, "140", 140, 0, 4, 5, mingStickLevels, "Rare", 0, 0, mingCueStickProperties);
            surpriseCueStickList.Add(mingCue);
            //Mongol Cue
            CueStickProperties mongolCueStickProperties = new CueStickProperties(4, 5, 4, 5, 50);
            List<CueStickLevel> mongolStickLevels = new List<CueStickLevel>();
            CueStickLevel mongolCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel mongolCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel mongolCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel mongolCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel mongolCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 1);
            mongolStickLevels.Add(mongolCueLevel1);
            mongolStickLevels.Add(mongolCueLevel2);
            mongolStickLevels.Add(mongolCueLevel3);
            mongolStickLevels.Add(mongolCueLevel4);
            mongolStickLevels.Add(mongolCueLevel5);
            CueStick mongolCue = new CueStick(cueSprites[2], "Mongol Cue", "Surprise", null, "NA", 0, "200", 200, 0, 4, 5, mongolStickLevels, "Rare", 0, 0, mongolCueStickProperties);
            surpriseCueStickList.Add(mongolCue);
            //Nightowl Cue
            CueStickProperties nightowlCueStickProperties = new CueStickProperties(3, 4, 5,3, 50);
            List<CueStickLevel> nightowlStickLevels = new List<CueStickLevel>();
            CueStickLevel nightowlCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel nightowlCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel nightowlCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel nightowlCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel nightowlCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 1, 0);
            nightowlStickLevels.Add(nightowlCueLevel1);
            nightowlStickLevels.Add(nightowlCueLevel2);
            nightowlStickLevels.Add(nightowlCueLevel3);
            nightowlStickLevels.Add(nightowlCueLevel4);
            nightowlStickLevels.Add(nightowlCueLevel5);
            CueStick nightowlCue = new CueStick(cueSprites[2], "Nightowl Cue", "Surprise", null, "NA", 0, "140", 140, 0, 4, 5, nightowlStickLevels, "Rare", 0, 0, nightowlCueStickProperties);
            surpriseCueStickList.Add(nightowlCue);
            //Ottoman
            CueStickProperties ottomanCueStickProperties = new CueStickProperties(5, 4, 4, 4, 50);
            List<CueStickLevel> ottomanStickLevels = new List<CueStickLevel>();
            CueStickLevel ottomanCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel ottomanCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel ottomanCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel ottomanCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel ottomanCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            ottomanStickLevels.Add(ottomanCueLevel1);
            ottomanStickLevels.Add(ottomanCueLevel2);
            ottomanStickLevels.Add(ottomanCueLevel3);
            ottomanStickLevels.Add(ottomanCueLevel4);
            ottomanStickLevels.Add(ottomanCueLevel5);
            CueStick ottomanCue = new CueStick(cueSprites[2], "Ottoman Cue", "Surprise", null, "NA", 0, "180", 180, 0, 4, 5, ottomanStickLevels, "Rare", 0, 0, ottomanCueStickProperties);
            surpriseCueStickList.Add(ottomanCue);
            //Rapier Cue
            CueStickProperties rapierCueStickProperties = new CueStickProperties(5, 1, 5, 3, 50);
            List<CueStickLevel> rapierStickLevels = new List<CueStickLevel>();
            CueStickLevel rapierCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel rapierCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel rapierCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel rapierCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel rapierCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            rapierStickLevels.Add(rapierCueLevel1);
            rapierStickLevels.Add(rapierCueLevel2);
            rapierStickLevels.Add(rapierCueLevel3);
            rapierStickLevels.Add(rapierCueLevel4);
            rapierStickLevels.Add(rapierCueLevel5);
            CueStick rapierCue = new CueStick(cueSprites[2], "Rapier Cue", "Surprise", null, "NA", 0, "120", 120, 0, 4, 5, rapierStickLevels, "Rare", 0, 0, rapierCueStickProperties);
            surpriseCueStickList.Add(rapierCue);
            //Raven Cue
            CueStickProperties ravenCueStickProperties = new CueStickProperties(4, 3, 3, 3, 50);
            List<CueStickLevel> ravenStickLevels = new List<CueStickLevel>();
            CueStickLevel ravenCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel ravenCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel ravenCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel ravenCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel ravenCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 1);
            ravenStickLevels.Add(ravenCueLevel1);
            ravenStickLevels.Add(ravenCueLevel2);
            ravenStickLevels.Add(ravenCueLevel3);
            ravenStickLevels.Add(ravenCueLevel4);
            ravenStickLevels.Add(ravenCueLevel5);
            CueStick ravenCue = new CueStick(cueSprites[2], "Raven Cue", "Surprise", null, "NA", 0, "100", 100, 0, 4, 5, ravenStickLevels, "Rare", 0, 0, ravenCueStickProperties);
            surpriseCueStickList.Add(ravenCue);
            //Robinhood Cue
            CueStickProperties robinhoodCueStickProperties = new CueStickProperties(5, 5, 5, 3, 50);
            List<CueStickLevel> robinhoodStickLevels = new List<CueStickLevel>();
            CueStickLevel robinhoodCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel robinhoodCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel robinhoodCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 0);
            CueStickLevel robinhoodCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel robinhoodCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 1, 0);
            robinhoodStickLevels.Add(robinhoodCueLevel1);
            robinhoodStickLevels.Add(robinhoodCueLevel2);
            robinhoodStickLevels.Add(robinhoodCueLevel3);
            robinhoodStickLevels.Add(robinhoodCueLevel4);
            robinhoodStickLevels.Add(robinhoodCueLevel5);
            CueStick robinhoodCue = new CueStick(cueSprites[2], "Robinhood Cue", "Surprise", null, "NA", 0, "200", 200, 0, 4, 5, robinhoodStickLevels, "Rare", 0, 0, robinhoodCueStickProperties);
            surpriseCueStickList.Add(robinhoodCue);
            //Swordfish Cue
            CueStickProperties swordfishCueStickProperties = new CueStickProperties(3, 4, 3, 2, 50);
            List<CueStickLevel> swordfishStickLevels = new List<CueStickLevel>();
            CueStickLevel swordfishCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel swordfishCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel swordfishCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel swordfishCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel swordfishCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            swordfishStickLevels.Add(swordfishCueLevel1);
            swordfishStickLevels.Add(swordfishCueLevel2);
            swordfishStickLevels.Add(swordfishCueLevel3);
            swordfishStickLevels.Add(swordfishCueLevel4);
            swordfishStickLevels.Add(swordfishCueLevel5);
            CueStick swordfishCue = new CueStick(cueSprites[2], "Swordfish Cue", "Surprise", null, "NA", 0, "80", 80, 0, 4, 5, swordfishStickLevels, "Rare", 0, 0, swordfishCueStickProperties);
            surpriseCueStickList.Add(swordfishCue);
            //Voodoo Cue
            CueStickProperties voodooCueStickProperties = new CueStickProperties(5, 6, 4, 4, 50);
            List<CueStickLevel> voodooStickLevels = new List<CueStickLevel>();
            CueStickLevel voodooCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel voodooCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel voodooCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 0);
            CueStickLevel voodooCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel voodooCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 1, 0);
            voodooStickLevels.Add(voodooCueLevel1);
            voodooStickLevels.Add(voodooCueLevel2);
            voodooStickLevels.Add(voodooCueLevel3);
            voodooStickLevels.Add(voodooCueLevel4);
            voodooStickLevels.Add(voodooCueLevel5);
            CueStick voodooCue = new CueStick(cueSprites[2], "Voodoo Cue", "Surprise", null, "NA", 0, "220", 220, 0, 4, 5, voodooStickLevels, "Rare", 0, 0, voodooCueStickProperties);
            surpriseCueStickList.Add(voodooCue);

            for (int i = 0; i < surpriseCueStickList.Count; i++)
            {
                if (GetSurpriseCueStickType(i) == "Rare")
                {
                    surpriseRareCueStickList.Add(i);
                }
                else if (GetSurpriseCueStickType(i) == "Epic")
                {
                    surpriseEpicCueStickList.Add(i);
                }
                else if (GetSurpriseCueStickType(i) == "Legendary")
                {
                    surpriseLegendaryCueStickList.Add(i);
                }
                if (GetSurpriseCueStickIsUnlockedFlag(i) == 1)
                {
                    ownedCueStickList.Add(surpriseCueStickList[i]);
                }
            }


        }
        //Country Cue data details
        public void CountryCueDetails()
        {
            //Albania Cue
            CueStickProperties albaniaCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> albaniaStickLevels = new List<CueStickLevel>();
            CueStickLevel albaniaCueLevel1 = new CueStickLevel(1, 2, 0, 1, 0, 0, 0);
            CueStickLevel albaniaCueLevel2 = new CueStickLevel(2, 4, 0, 0, 0, 1, 0);
            CueStickLevel albaniaCueLevel3 = new CueStickLevel(3, 4, 0, 0, 1, 0, 0);
            CueStickLevel albaniaCueLevel4 = new CueStickLevel(4, 4, 0, 1, 0, 0, 0);
            CueStickLevel albaniaCueLevel5 = new CueStickLevel(5, 5, 0, 0, 0, 0, 1);
            albaniaStickLevels.Add(albaniaCueLevel1);
            albaniaStickLevels.Add(albaniaCueLevel2);
            albaniaStickLevels.Add(albaniaCueLevel3);
            albaniaStickLevels.Add(albaniaCueLevel4);
            albaniaStickLevels.Add(albaniaCueLevel5);
            CueStick albaniaCue = new CueStick(cueSprites[4], "Albania Cue", "Country","Las Vegas", "40", 40, "160",160, 0, 4, 5, albaniaStickLevels, "Advanced", 0, 0, albaniaCueStickProperties);
            countryCueStickList.Add(albaniaCue);
            //Algeria Cue
            CueStickProperties algeriaCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> algeriaStickLevels = new List<CueStickLevel>();
            CueStickLevel algeriaCueLevel1 = new CueStickLevel(1, 2, 0, 0, 1, 0, 0);
            CueStickLevel algeriaCueLevel2 = new CueStickLevel(2, 3, 0, 0, 0, 0, 1);
            CueStickLevel algeriaCueLevel3 = new CueStickLevel(3, 4, 0, 1, 0, 0, 0);
            CueStickLevel algeriaCueLevel4 = new CueStickLevel(4, 4, 0, 0, 1, 0, 0);
            CueStickLevel algeriaCueLevel5 = new CueStickLevel(5, 5, 0, 0, 0, 0, 1);
            algeriaStickLevels.Add(algeriaCueLevel1);
            algeriaStickLevels.Add(algeriaCueLevel2);
            algeriaStickLevels.Add(algeriaCueLevel3);
            algeriaStickLevels.Add(algeriaCueLevel4);
            algeriaStickLevels.Add(algeriaCueLevel5);
            CueStick algeriaCue = new CueStick(cueSprites[4], "Algeria Cue", "Country", "Singapore", "40", 40, "160", 160, 0, 4, 5, algeriaStickLevels, "Advanced", 0, 0, algeriaCueStickProperties);
            countryCueStickList.Add(algeriaCue);
            //Argentina Cue
            CueStickProperties argentinaCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> argentinaStickLevels = new List<CueStickLevel>();
            CueStickLevel argentinaCueLevel1 = new CueStickLevel(1, 2, 0, 1, 0, 0, 0);
            CueStickLevel argentinaCueLevel2 = new CueStickLevel(2, 3, 0, 0, 0, 1, 0);
            CueStickLevel argentinaCueLevel3 = new CueStickLevel(3, 4, 0, 0, 0, 0, 1);
            CueStickLevel argentinaCueLevel4 = new CueStickLevel(4, 5, 0, 0, 0, 0, 1);
            CueStickLevel argentinaCueLevel5 = new CueStickLevel(5, 5, 0, 0, 1, 0, 0);
            argentinaStickLevels.Add(argentinaCueLevel1);
            argentinaStickLevels.Add(argentinaCueLevel2);
            argentinaStickLevels.Add(argentinaCueLevel3);
            argentinaStickLevels.Add(argentinaCueLevel4);
            argentinaStickLevels.Add(argentinaCueLevel5);
            CueStick argentinaCue = new CueStick(cueSprites[4], "Argentina Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, argentinaStickLevels, "Advanced", 0, 0, argentinaCueStickProperties);
            countryCueStickList.Add(argentinaCue);
            //Australia Cue
            CueStickProperties australiaCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> australiaStickLevels = new List<CueStickLevel>();
            CueStickLevel australiaCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel australiaCueLevel2 = new CueStickLevel(2, 0, 0, 1, 0, 0, 0);
            CueStickLevel australiaCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel australiaCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel australiaCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 1, 0);
            australiaStickLevels.Add(australiaCueLevel1);
            australiaStickLevels.Add(australiaCueLevel2);
            australiaStickLevels.Add(australiaCueLevel3);
            australiaStickLevels.Add(australiaCueLevel4);
            australiaStickLevels.Add(australiaCueLevel5);
            CueStick australiaCue = new CueStick(cueSprites[4], "Australia Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, australiaStickLevels, "Advanced", 0, 0, australiaCueStickProperties);
            countryCueStickList.Add(australiaCue);
            //Austria
            CueStickProperties austriaCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> austriaStickLevels = new List<CueStickLevel>();
            CueStickLevel austriaCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel austriaCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel austriaCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 0);
            CueStickLevel austriaCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel austriaCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            austriaStickLevels.Add(austriaCueLevel1);
            austriaStickLevels.Add(austriaCueLevel2);
            austriaStickLevels.Add(austriaCueLevel3);
            austriaStickLevels.Add(austriaCueLevel4);
            austriaStickLevels.Add(austriaCueLevel5);
            CueStick austriaCue = new CueStick(cueSprites[4], "Austria Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, austriaStickLevels, "Advanced", 0, 0, austriaCueStickProperties);
            countryCueStickList.Add(austriaCue);
            //Bahrain Cue
            CueStickProperties bahrainCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> bahrainStickLevels = new List<CueStickLevel>();
            CueStickLevel bahrainCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel bahrainCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel bahrainCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel bahrainCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel bahrainCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            bahrainStickLevels.Add(bahrainCueLevel1);
            bahrainStickLevels.Add(bahrainCueLevel2);
            bahrainStickLevels.Add(bahrainCueLevel3);
            bahrainStickLevels.Add(bahrainCueLevel4);
            bahrainStickLevels.Add(bahrainCueLevel5);
            CueStick bahrainCue = new CueStick(cueSprites[4], "Bahrain Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, bahrainStickLevels, "Advanced", 0, 0, bahrainCueStickProperties);
            countryCueStickList.Add(bahrainCue);
            //Bangladesh Cue
            CueStickProperties bangladeshCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> bangladeshStickLevels = new List<CueStickLevel>();
            CueStickLevel bangladeshCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel bangladeshCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel bangladeshCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel bangladeshCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel bangladeshCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            bangladeshStickLevels.Add(bangladeshCueLevel1);
            bangladeshStickLevels.Add(bangladeshCueLevel2);
            bangladeshStickLevels.Add(bangladeshCueLevel3);
            bangladeshStickLevels.Add(bangladeshCueLevel4);
            bangladeshStickLevels.Add(bangladeshCueLevel5);
            CueStick bangladeshCue = new CueStick(cueSprites[4], "Bangladesh Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, bangladeshStickLevels, "Advanced", 0, 0, bangladeshCueStickProperties);
            countryCueStickList.Add(bangladeshCue);
            //Belgium Cue
            CueStickProperties belgiumCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> belgiumStickLevels = new List<CueStickLevel>();
            CueStickLevel belgiumCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel belgiumCueLevel2 = new CueStickLevel(2, 0, 0, 1, 0, 0, 0);
            CueStickLevel belgiumCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel belgiumCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel belgiumCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            belgiumStickLevels.Add(belgiumCueLevel1);
            belgiumStickLevels.Add(belgiumCueLevel2);
            belgiumStickLevels.Add(belgiumCueLevel3);
            belgiumStickLevels.Add(belgiumCueLevel4);
            belgiumStickLevels.Add(belgiumCueLevel5);
            CueStick belgiumCue = new CueStick(cueSprites[4], "Belgium Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, belgiumStickLevels, "Advanced", 0, 0, belgiumCueStickProperties);
            countryCueStickList.Add(belgiumCue);
            //Bosnia and Herzegovina Cue
            CueStickProperties bosniaAndHerzegovinaCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> bosniaAndHerzegovinaStickLevels = new List<CueStickLevel>();
            CueStickLevel bosniaAndHerzegovinaCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel bosniaAndHerzegovinaCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel bosniaAndHerzegovinaCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel bosniaAndHerzegovinaCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel bosniaAndHerzegovinaCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            bosniaAndHerzegovinaStickLevels.Add(bosniaAndHerzegovinaCueLevel1);
            bosniaAndHerzegovinaStickLevels.Add(bosniaAndHerzegovinaCueLevel2);
            bosniaAndHerzegovinaStickLevels.Add(bosniaAndHerzegovinaCueLevel3);
            bosniaAndHerzegovinaStickLevels.Add(bosniaAndHerzegovinaCueLevel4);
            bosniaAndHerzegovinaStickLevels.Add(bosniaAndHerzegovinaCueLevel5);
            CueStick bosniaAndHerzegovinaCue = new CueStick(cueSprites[4], "Bosnia and Herzegovina Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, bosniaAndHerzegovinaStickLevels, "Advanced", 0, 0, bosniaAndHerzegovinaCueStickProperties);
            countryCueStickList.Add(bosniaAndHerzegovinaCue);
            //Brazil ue
            CueStickProperties brazilCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> brazilStickLevels = new List<CueStickLevel>();
            CueStickLevel brazilCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel brazilCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel brazilCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel brazilCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel brazilCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            brazilStickLevels.Add(brazilCueLevel1);
            brazilStickLevels.Add(brazilCueLevel2);
            brazilStickLevels.Add(brazilCueLevel3);
            brazilStickLevels.Add(brazilCueLevel4);
            brazilStickLevels.Add(brazilCueLevel5);
            CueStick brazilCue = new CueStick(cueSprites[4], "Brazil Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, brazilStickLevels, "Advanced", 0, 0, brazilCueStickProperties);
            countryCueStickList.Add(brazilCue);
            // Bulgaria Cue
            CueStickProperties bulgariaCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> bulgariaStickLevels = new List<CueStickLevel>();
            CueStickLevel bulgariaCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel bulgariaCueLevel2 = new CueStickLevel(2, 0, 0, 1, 0, 0, 0);
            CueStickLevel bulgariaCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel bulgariaCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel bulgariaCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 1, 0);
            bulgariaStickLevels.Add(bulgariaCueLevel1);
            bulgariaStickLevels.Add(bulgariaCueLevel2);
            bulgariaStickLevels.Add(bulgariaCueLevel3);
            bulgariaStickLevels.Add(bulgariaCueLevel4);
            bulgariaStickLevels.Add(bulgariaCueLevel5);
            CueStick bulgariaCue = new CueStick(cueSprites[4], "Bulgaria Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, bulgariaStickLevels, "Advanced", 0, 0, bulgariaCueStickProperties);
            countryCueStickList.Add(bulgariaCue);
            //Canada
            CueStickProperties canadaCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> canadaStickLevels = new List<CueStickLevel>();
            CueStickLevel canadaCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel canadaCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel canadaCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel canadaCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel canadaCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            canadaStickLevels.Add(canadaCueLevel1);
            canadaStickLevels.Add(canadaCueLevel2);
            canadaStickLevels.Add(canadaCueLevel3);
            canadaStickLevels.Add(canadaCueLevel4);
            canadaStickLevels.Add(canadaCueLevel5);
            CueStick canadaCue = new CueStick(cueSprites[4], "Canada Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, canadaStickLevels, "Advanced", 0, 0, canadaCueStickProperties);
            countryCueStickList.Add(canadaCue);
            //Chile Cue
            CueStickProperties chileCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> chileStickLevels = new List<CueStickLevel>();
            CueStickLevel chileCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel chileCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel chileCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel chileCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel chileCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            chileStickLevels.Add(chileCueLevel1);
            chileStickLevels.Add(chileCueLevel2);
            chileStickLevels.Add(chileCueLevel3);
            chileStickLevels.Add(chileCueLevel4);
            chileStickLevels.Add(chileCueLevel5);
            CueStick chileCue = new CueStick(cueSprites[4], "Canada Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, chileStickLevels, "Advanced", 0, 0, chileCueStickProperties);
            countryCueStickList.Add(chileCue);
            //China Cue
            CueStickProperties chinaCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> chinaStickLevels = new List<CueStickLevel>();
            CueStickLevel chinaCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel chinaCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel chinaCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel chinaCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel chinaCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            chinaStickLevels.Add(chinaCueLevel1);
            chinaStickLevels.Add(chinaCueLevel2);
            chinaStickLevels.Add(chinaCueLevel3);
            chinaStickLevels.Add(chinaCueLevel4);
            chinaStickLevels.Add(chinaCueLevel5);
            CueStick chinaCue = new CueStick(cueSprites[4], "China Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, chinaStickLevels, "Advanced", 0, 0, chinaCueStickProperties);
            countryCueStickList.Add(chinaCue);
            //Colombia Cue
            CueStickProperties colombiaCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> colombiaStickLevels = new List<CueStickLevel>();
            CueStickLevel colombiaCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel colombiaCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel colombiaCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel colombiaCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel colombiaCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            colombiaStickLevels.Add(colombiaCueLevel1);
            colombiaStickLevels.Add(colombiaCueLevel2);
            colombiaStickLevels.Add(colombiaCueLevel3);
            colombiaStickLevels.Add(colombiaCueLevel4);
            colombiaStickLevels.Add(colombiaCueLevel5);
            CueStick colombiaCue = new CueStick(cueSprites[4], "Colombia Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, colombiaStickLevels, "Advanced", 0, 0, colombiaCueStickProperties);
            countryCueStickList.Add(colombiaCue);
            //Croatia Cue
            CueStickProperties croatiaCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> croatiaStickLevels = new List<CueStickLevel>();
            CueStickLevel croatiaCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel croatiaCueLevel2 = new CueStickLevel(2, 0, 0, 1, 0, 0, 0);
            CueStickLevel croatiaCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel croatiaCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel croatiaCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            croatiaStickLevels.Add(croatiaCueLevel1);
            croatiaStickLevels.Add(croatiaCueLevel2);
            croatiaStickLevels.Add(croatiaCueLevel3);
            croatiaStickLevels.Add(croatiaCueLevel4);
            croatiaStickLevels.Add(croatiaCueLevel5);
            CueStick croatiaCue = new CueStick(cueSprites[4], "Croatia Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, croatiaStickLevels, "Advanced", 0, 0, croatiaCueStickProperties);
            countryCueStickList.Add(croatiaCue);
            //Denmark Cue
            CueStickProperties denmarkCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> denmarkStickLevels = new List<CueStickLevel>();
            CueStickLevel denmarkCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel denmarkCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel denmarkCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 0);
            CueStickLevel denmarkCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel denmarkCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            denmarkStickLevels.Add(denmarkCueLevel1);
            denmarkStickLevels.Add(denmarkCueLevel2);
            denmarkStickLevels.Add(denmarkCueLevel3);
            denmarkStickLevels.Add(denmarkCueLevel4);
            denmarkStickLevels.Add(denmarkCueLevel5);
            CueStick denmarkCue = new CueStick(cueSprites[4], "Denmark Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, denmarkStickLevels, "Advanced", 0, 0, denmarkCueStickProperties);
            countryCueStickList.Add(denmarkCue);
            //Dominican Republic Cue
            CueStickProperties dominicanRepublicCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> dominicanRepublicStickLevels = new List<CueStickLevel>();
            CueStickLevel dominicanRepublicCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel dominicanRepublicCueLevel2 = new CueStickLevel(2, 0, 0, 1, 0, 0, 0);
            CueStickLevel dominicanRepublicCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel dominicanRepublicCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel dominicanRepublicCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 1, 0);
            dominicanRepublicStickLevels.Add(dominicanRepublicCueLevel1);
            dominicanRepublicStickLevels.Add(dominicanRepublicCueLevel2);
            dominicanRepublicStickLevels.Add(dominicanRepublicCueLevel3);
            dominicanRepublicStickLevels.Add(dominicanRepublicCueLevel4);
            dominicanRepublicStickLevels.Add(dominicanRepublicCueLevel5);
            CueStick dominicanRepublicCue = new CueStick(cueSprites[4], "Dominican Republic Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, dominicanRepublicStickLevels, "Advanced", 0, 0, dominicanRepublicCueStickProperties);
            countryCueStickList.Add(dominicanRepublicCue);
            //Ecuador Cue
            CueStickProperties ecuadorCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> ecuadorStickLevels = new List<CueStickLevel>();
            CueStickLevel ecuadorCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel ecuadorCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel ecuadorCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel ecuadorCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel ecuadorCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            ecuadorStickLevels.Add(ecuadorCueLevel1);
            ecuadorStickLevels.Add(ecuadorCueLevel2);
            ecuadorStickLevels.Add(ecuadorCueLevel3);
            ecuadorStickLevels.Add(ecuadorCueLevel4);
            ecuadorStickLevels.Add(ecuadorCueLevel5);
            CueStick ecuadorCue = new CueStick(cueSprites[4], "Ecuador Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, ecuadorStickLevels, "Advanced", 0, 0, ecuadorCueStickProperties);
            countryCueStickList.Add(ecuadorCue);
            //Egypt Cue
            CueStickProperties egyptCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> egyptStickLevels = new List<CueStickLevel>();
            CueStickLevel egyptCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel egyptCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel egyptCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel egyptCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel egyptCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            egyptStickLevels.Add(egyptCueLevel1);
            egyptStickLevels.Add(egyptCueLevel2);
            egyptStickLevels.Add(egyptCueLevel3);
            egyptStickLevels.Add(egyptCueLevel4);
            egyptStickLevels.Add(egyptCueLevel5);
            CueStick egyptCue = new CueStick(cueSprites[4], "Egypt Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, egyptStickLevels, "Advanced", 0, 0, egyptCueStickProperties);
            countryCueStickList.Add(egyptCue);
            //Finland Cue
            CueStickProperties finlandCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> finlandStickLevels = new List<CueStickLevel>();
            CueStickLevel finlandCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel finlandCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel finlandCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel finlandCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel finlandCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            finlandStickLevels.Add(finlandCueLevel1);
            finlandStickLevels.Add(finlandCueLevel2);
            finlandStickLevels.Add(finlandCueLevel3);
            finlandStickLevels.Add(finlandCueLevel4);
            finlandStickLevels.Add(finlandCueLevel5);
            CueStick finlandCue = new CueStick(cueSprites[4], "Finland Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, finlandStickLevels, "Advanced", 0, 0, finlandCueStickProperties);
            countryCueStickList.Add(finlandCue);
            //France Cue
            CueStickProperties franceCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> franceStickLevels = new List<CueStickLevel>();
            CueStickLevel franceCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel franceCueLevel2 = new CueStickLevel(2, 0, 0, 1, 0, 0, 0);
            CueStickLevel franceCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel franceCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel franceCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            franceStickLevels.Add(franceCueLevel1);
            franceStickLevels.Add(franceCueLevel2);
            franceStickLevels.Add(franceCueLevel3);
            franceStickLevels.Add(franceCueLevel4);
            franceStickLevels.Add(franceCueLevel5);
            CueStick franceCue = new CueStick(cueSprites[4], "France Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, franceStickLevels, "Advanced", 0, 0, franceCueStickProperties);
            countryCueStickList.Add(franceCue);
            //Germany Cue
            CueStickProperties germanyCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> germanyStickLevels = new List<CueStickLevel>();
            CueStickLevel germanyCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel germanyCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel germanyCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel germanyCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel germanyCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            germanyStickLevels.Add(germanyCueLevel1);
            germanyStickLevels.Add(germanyCueLevel2);
            germanyStickLevels.Add(germanyCueLevel3);
            germanyStickLevels.Add(germanyCueLevel4);
            germanyStickLevels.Add(germanyCueLevel5);
            CueStick germanyCue = new CueStick(cueSprites[4], "Germany Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, germanyStickLevels, "Advanced", 0, 0, germanyCueStickProperties);
            countryCueStickList.Add(germanyCue);
            //Greece
            CueStickProperties greeceCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> greeceStickLevels = new List<CueStickLevel>();
            CueStickLevel greeceCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0,1, 0);
            CueStickLevel greeceCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel greeceCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel greeceCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel greeceCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            greeceStickLevels.Add(greeceCueLevel1);
            greeceStickLevels.Add(greeceCueLevel2);
            greeceStickLevels.Add(greeceCueLevel3);
            greeceStickLevels.Add(greeceCueLevel4);
            greeceStickLevels.Add(greeceCueLevel5);
            CueStick greeceCue = new CueStick(cueSprites[4], "Greece Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, greeceStickLevels, "Advanced", 0, 0, greeceCueStickProperties);
            countryCueStickList.Add(greeceCue);
            //Hong Kong Cue
            CueStickProperties hongKongCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> hongKongStickLevels = new List<CueStickLevel>();
            CueStickLevel hongKongCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel hongKongCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel hongKongCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel hongKongCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel hongKongCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            hongKongStickLevels.Add(hongKongCueLevel1);
            hongKongStickLevels.Add(hongKongCueLevel2);
            hongKongStickLevels.Add(hongKongCueLevel3);
            hongKongStickLevels.Add(hongKongCueLevel4);
            hongKongStickLevels.Add(hongKongCueLevel5);
            CueStick hongKongCue = new CueStick(cueSprites[4], "Hong Kong Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, hongKongStickLevels, "Advanced", 0, 0, hongKongCueStickProperties);
            countryCueStickList.Add(hongKongCue);
            //Hungary Cue
            CueStickProperties hungaryCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> hungaryStickLevels = new List<CueStickLevel>();
            CueStickLevel hungaryCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel hungaryCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel hungaryCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 0);
            CueStickLevel hungaryCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel hungaryCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 1, 0);
            hungaryStickLevels.Add(hungaryCueLevel1);
            hungaryStickLevels.Add(hungaryCueLevel2);
            hungaryStickLevels.Add(hungaryCueLevel3);
            hungaryStickLevels.Add(hungaryCueLevel4);
            hungaryStickLevels.Add(hungaryCueLevel5);
            CueStick hungaryCue = new CueStick(cueSprites[4], "Hungary Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, hungaryStickLevels, "Advanced", 0, 0, hungaryCueStickProperties);
            countryCueStickList.Add(hungaryCue);
            //India Cue
            CueStickProperties indiaCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> indiaStickLevels = new List<CueStickLevel>();
            CueStickLevel indiaCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel indiaCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel indiaCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel indiaCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel indiaCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            indiaStickLevels.Add(indiaCueLevel1);
            indiaStickLevels.Add(indiaCueLevel2);
            indiaStickLevels.Add(indiaCueLevel3);
            indiaStickLevels.Add(indiaCueLevel4);
            indiaStickLevels.Add(indiaCueLevel5);
            CueStick indiaCue = new CueStick(cueSprites[4], "India Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, indiaStickLevels, "Advanced", 0, 0, indiaCueStickProperties);
            countryCueStickList.Add(indiaCue);
            //Indonesia Cue
            CueStickProperties indonesiaCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> indonesiaStickLevels = new List<CueStickLevel>();
            CueStickLevel indonesiaCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel indonesiaCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel indonesiaCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel indonesiaCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel indonesiaCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            indonesiaStickLevels.Add(indonesiaCueLevel1);
            indonesiaStickLevels.Add(indonesiaCueLevel2);
            indonesiaStickLevels.Add(indonesiaCueLevel3);
            indonesiaStickLevels.Add(indonesiaCueLevel4);
            indonesiaStickLevels.Add(indonesiaCueLevel5);
            CueStick indonesiaCue = new CueStick(cueSprites[4], "Indonesia Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, indonesiaStickLevels, "Advanced", 0, 0, indonesiaCueStickProperties);
            countryCueStickList.Add(indonesiaCue);
            //Iran Cue
            CueStickProperties iranCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> iranStickLevels = new List<CueStickLevel>();
            CueStickLevel iranCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel iranCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel iranCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel iranCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel iranCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            iranStickLevels.Add(iranCueLevel1);
            iranStickLevels.Add(iranCueLevel2);
            iranStickLevels.Add(iranCueLevel3);
            iranStickLevels.Add(iranCueLevel4);
            iranStickLevels.Add(iranCueLevel5);
            CueStick iranCue = new CueStick(cueSprites[4], "Iran Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, iranStickLevels, "Advanced", 0, 0, iranCueStickProperties);
            countryCueStickList.Add(iranCue);
            //Iraq Cue
            CueStickProperties iraqCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> iraqStickLevels = new List<CueStickLevel>();
            CueStickLevel iraqCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel iraqCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel iraqCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 0);
            CueStickLevel iraqCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel iraqCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            iraqStickLevels.Add(iraqCueLevel1);
            iraqStickLevels.Add(iraqCueLevel2);
            iraqStickLevels.Add(iraqCueLevel3);
            iraqStickLevels.Add(iraqCueLevel4);
            iraqStickLevels.Add(iraqCueLevel5);
            CueStick iraqCue = new CueStick(cueSprites[4], "Iraq Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, iraqStickLevels, "Advanced", 0, 0, iraqCueStickProperties);
            countryCueStickList.Add(iraqCue);
            // Ireland Cue
            CueStickProperties irelandCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> irelandStickLevels = new List<CueStickLevel>();
            CueStickLevel irelandCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel irelandCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel irelandCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel irelandCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel irelandCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            irelandStickLevels.Add(irelandCueLevel1);
            irelandStickLevels.Add(irelandCueLevel2);
            irelandStickLevels.Add(irelandCueLevel3);
            irelandStickLevels.Add(irelandCueLevel4);
            irelandStickLevels.Add(irelandCueLevel5);
            CueStick irelandCue = new CueStick(cueSprites[4], "Ireland Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, irelandStickLevels, "Advanced", 0, 0, irelandCueStickProperties);
            countryCueStickList.Add(irelandCue);
            //Israel Cue
            CueStickProperties israelCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> israelStickLevels = new List<CueStickLevel>();
            CueStickLevel israelCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel israelCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel israelCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 0);
            CueStickLevel israelCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel israelCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 1, 0);
            israelStickLevels.Add(israelCueLevel1);
            israelStickLevels.Add(israelCueLevel2);
            israelStickLevels.Add(israelCueLevel3);
            israelStickLevels.Add(israelCueLevel4);
            israelStickLevels.Add(israelCueLevel5);
            CueStick israelCue = new CueStick(cueSprites[4], "Israel Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, israelStickLevels, "Advanced", 0, 0, israelCueStickProperties);
            countryCueStickList.Add(israelCue);
            //Italy Cue
            CueStickProperties italyCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> italyStickLevels = new List<CueStickLevel>();
            CueStickLevel italyCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel italyCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel italyCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel italyCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel italyCueLevel5 = new CueStickLevel(5, 0, 0,1, 0, 0, 0);
            italyStickLevels.Add(italyCueLevel1);
            italyStickLevels.Add(italyCueLevel2);
            italyStickLevels.Add(italyCueLevel3);
            italyStickLevels.Add(italyCueLevel4);
            italyStickLevels.Add(italyCueLevel5);
            CueStick italyCue = new CueStick(cueSprites[4], "Israel Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, italyStickLevels, "Advanced", 0, 0, italyCueStickProperties);
            countryCueStickList.Add(italyCue);
            //Japan Cue
            CueStickProperties japanCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> japanStickLevels = new List<CueStickLevel>();
            CueStickLevel japanCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel japanCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel japanCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel japanCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel japanCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            japanStickLevels.Add(japanCueLevel1);
            japanStickLevels.Add(japanCueLevel2);
            japanStickLevels.Add(japanCueLevel3);
            japanStickLevels.Add(japanCueLevel4);
            japanStickLevels.Add(japanCueLevel5);
            CueStick japanCue = new CueStick(cueSprites[4], "Japan Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, japanStickLevels, "Advanced", 0, 0, japanCueStickProperties);
            countryCueStickList.Add(japanCue);
            //Jordan Cue
            CueStickProperties jordanCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> jordanStickLevels = new List<CueStickLevel>();
            CueStickLevel jordanCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel jordanCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel jordanCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 0);
            CueStickLevel jordanCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel jordanCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            jordanStickLevels.Add(jordanCueLevel1);
            jordanStickLevels.Add(jordanCueLevel2);
            jordanStickLevels.Add(jordanCueLevel3);
            jordanStickLevels.Add(jordanCueLevel4);
            jordanStickLevels.Add(jordanCueLevel5);
            CueStick jordanCue = new CueStick(cueSprites[4], "Jordan Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, jordanStickLevels, "Advanced", 0, 0, jordanCueStickProperties);
            countryCueStickList.Add(jordanCue);
            //Kuwait Cue
            CueStickProperties kuwaitCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> kuwaitStickLevels = new List<CueStickLevel>();
            CueStickLevel kuwaitCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel kuwaitCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel kuwaitCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel kuwaitCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel kuwaitCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            kuwaitStickLevels.Add(kuwaitCueLevel1);
            kuwaitStickLevels.Add(kuwaitCueLevel2);
            kuwaitStickLevels.Add(kuwaitCueLevel3);
            kuwaitStickLevels.Add(kuwaitCueLevel4);
            kuwaitStickLevels.Add(kuwaitCueLevel5);
            CueStick kuwaitCue = new CueStick(cueSprites[4], "Kuwait Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, kuwaitStickLevels, "Advanced", 0, 0, kuwaitCueStickProperties);
            countryCueStickList.Add(kuwaitCue);
            //Lebanon Cue
            CueStickProperties lebanonCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> lebanonStickLevels = new List<CueStickLevel>();
            CueStickLevel lebanonCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel lebanonCueLevel2 = new CueStickLevel(2, 0, 0, 1, 0, 0, 0);
            CueStickLevel lebanonCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel lebanonCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel lebanonCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            lebanonStickLevels.Add(lebanonCueLevel1);
            lebanonStickLevels.Add(lebanonCueLevel2);
            lebanonStickLevels.Add(lebanonCueLevel3);
            lebanonStickLevels.Add(lebanonCueLevel4);
            lebanonStickLevels.Add(lebanonCueLevel5);
            CueStick lebanonCue = new CueStick(cueSprites[4], "Lebanon Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, lebanonStickLevels, "Advanced", 0, 0, lebanonCueStickProperties);
            countryCueStickList.Add(lebanonCue);
            //Malaysia Cue
            CueStickProperties malaysiaCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> malaysiaStickLevels = new List<CueStickLevel>();
            CueStickLevel malaysiaCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel malaysiaCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel malaysiaCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel malaysiaCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel malaysiaCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            malaysiaStickLevels.Add(malaysiaCueLevel1);
            malaysiaStickLevels.Add(malaysiaCueLevel2);
            malaysiaStickLevels.Add(malaysiaCueLevel3);
            malaysiaStickLevels.Add(malaysiaCueLevel4);
            malaysiaStickLevels.Add(malaysiaCueLevel5);
            CueStick malaysiaCue = new CueStick(cueSprites[4], "Malaysia Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, malaysiaStickLevels, "Advanced", 0, 0, malaysiaCueStickProperties);
            countryCueStickList.Add(malaysiaCue);
            //Mexico Cue
            CueStickProperties mexicoCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> mexicoStickLevels = new List<CueStickLevel>();
            CueStickLevel mexicoCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel mexicoCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel mexicoCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 0);
            CueStickLevel mexicoCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel mexicoCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            mexicoStickLevels.Add(mexicoCueLevel1);
            mexicoStickLevels.Add(mexicoCueLevel2);
            mexicoStickLevels.Add(mexicoCueLevel3);
            mexicoStickLevels.Add(mexicoCueLevel4);
            mexicoStickLevels.Add(mexicoCueLevel5);
            CueStick mexicoCue = new CueStick(cueSprites[4], "Mexico Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, mexicoStickLevels, "Advanced", 0, 0, mexicoCueStickProperties);
            countryCueStickList.Add(mexicoCue);
            //Morocco Cue
            CueStickProperties moroccoCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> moroccoStickLevels = new List<CueStickLevel>();
            CueStickLevel moroccoCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel moroccoCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel moroccoCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel moroccoCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel moroccoCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 1, 0);
            moroccoStickLevels.Add(moroccoCueLevel1);
            moroccoStickLevels.Add(moroccoCueLevel2);
            moroccoStickLevels.Add(moroccoCueLevel3);
            moroccoStickLevels.Add(moroccoCueLevel4);
            moroccoStickLevels.Add(moroccoCueLevel5);
            CueStick moroccoCue = new CueStick(cueSprites[4], "Morocco Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, moroccoStickLevels, "Advanced", 0, 0, moroccoCueStickProperties);
            countryCueStickList.Add(moroccoCue);
            //Netherlands Cue
            CueStickProperties netherlandsCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> netherlandsStickLevels = new List<CueStickLevel>();
            CueStickLevel netherlandsCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel netherlandsCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel netherlandsCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel netherlandsCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel netherlandsCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            netherlandsStickLevels.Add(netherlandsCueLevel1);
            netherlandsStickLevels.Add(netherlandsCueLevel2);
            netherlandsStickLevels.Add(netherlandsCueLevel3);
            netherlandsStickLevels.Add(netherlandsCueLevel4);
            netherlandsStickLevels.Add(netherlandsCueLevel5);
            CueStick netherlandsCue = new CueStick(cueSprites[4], "Netherlands Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, netherlandsStickLevels, "Advanced", 0, 0, netherlandsCueStickProperties);
            countryCueStickList.Add(netherlandsCue);
            //New Zealand Cue
            CueStickProperties newZealandCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> newZealandStickLevels = new List<CueStickLevel>();
            CueStickLevel newZealandCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel newZealandCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel newZealandCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel newZealandCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel newZealandCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            newZealandStickLevels.Add(newZealandCueLevel1);
            newZealandStickLevels.Add(newZealandCueLevel2);
            newZealandStickLevels.Add(newZealandCueLevel3);
            newZealandStickLevels.Add(newZealandCueLevel4);
            newZealandStickLevels.Add(newZealandCueLevel5);
            CueStick newZealandCue = new CueStick(cueSprites[4], "New Zealand Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, newZealandStickLevels, "Advanced", 0, 0, newZealandCueStickProperties);
            countryCueStickList.Add(newZealandCue);
            //Nigeria Cue
            CueStickProperties nigeriaCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> nigeriaStickLevels = new List<CueStickLevel>();
            CueStickLevel nigeriaCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel nigeriaCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel nigeriaCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel nigeriaCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel nigeriaCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            nigeriaStickLevels.Add(nigeriaCueLevel1);
            nigeriaStickLevels.Add(nigeriaCueLevel2);
            nigeriaStickLevels.Add(nigeriaCueLevel3);
            nigeriaStickLevels.Add(nigeriaCueLevel4);
            nigeriaStickLevels.Add(nigeriaCueLevel5);
            CueStick nigeriaCue = new CueStick(cueSprites[4], "Nigeria Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, nigeriaStickLevels, "Advanced", 0, 0, nigeriaCueStickProperties);
            countryCueStickList.Add(nigeriaCue);
            //Norway Cue
            CueStickProperties norwayCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> norwayStickLevels = new List<CueStickLevel>();
            CueStickLevel norwayCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel norwayCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel norwayCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel norwayCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel norwayCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            norwayStickLevels.Add(norwayCueLevel1);
            norwayStickLevels.Add(norwayCueLevel2);
            norwayStickLevels.Add(norwayCueLevel3);
            norwayStickLevels.Add(norwayCueLevel4);
            norwayStickLevels.Add(norwayCueLevel5);
            CueStick norwayCue = new CueStick(cueSprites[4], "Norway Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, norwayStickLevels, "Advanced", 0, 0, norwayCueStickProperties);
            countryCueStickList.Add(norwayCue);
            //Oman Cue
            CueStickProperties omanCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> omanStickLevels = new List<CueStickLevel>();
            CueStickLevel omanCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel omanCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel omanCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel omanCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel omanCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 1, 0);
            omanStickLevels.Add(omanCueLevel1);
            omanStickLevels.Add(omanCueLevel2);
            omanStickLevels.Add(omanCueLevel3);
            omanStickLevels.Add(omanCueLevel4);
            omanStickLevels.Add(omanCueLevel5);
            CueStick omanCue = new CueStick(cueSprites[4], "Oman Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, omanStickLevels, "Advanced", 0, 0, omanCueStickProperties);
            countryCueStickList.Add(omanCue);
            //Pakistan  Cue
            CueStickProperties pakistanCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> pakistanStickLevels = new List<CueStickLevel>();
            CueStickLevel pakistanCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel pakistanCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel pakistanCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel pakistanCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel pakistanCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            pakistanStickLevels.Add(pakistanCueLevel1);
            pakistanStickLevels.Add(pakistanCueLevel2);
            pakistanStickLevels.Add(pakistanCueLevel3);
            pakistanStickLevels.Add(pakistanCueLevel4);
            pakistanStickLevels.Add(pakistanCueLevel5);
            CueStick pakistanCue = new CueStick(cueSprites[4], "Pakistan Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, pakistanStickLevels, "Advanced", 0, 0, pakistanCueStickProperties);
            countryCueStickList.Add(pakistanCue);
            //Panama Cue
            CueStickProperties panamaCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> panamaStickLevels = new List<CueStickLevel>();
            CueStickLevel panamaCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel panamaCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel panamaCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 0);
            CueStickLevel panamaCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel panamaCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            panamaStickLevels.Add(panamaCueLevel1);
            panamaStickLevels.Add(panamaCueLevel2);
            panamaStickLevels.Add(panamaCueLevel3);
            panamaStickLevels.Add(panamaCueLevel4);
            panamaStickLevels.Add(panamaCueLevel5);
            CueStick panamaCue = new CueStick(cueSprites[4], "Panama Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, panamaStickLevels, "Advanced", 0, 0, panamaCueStickProperties);
            countryCueStickList.Add(panamaCue);
            //Peru
            CueStickProperties peruCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> peruStickLevels = new List<CueStickLevel>();
            CueStickLevel peruCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel peruCueLevel2 = new CueStickLevel(2, 0, 0, 1, 0, 0, 0);
            CueStickLevel peruCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel peruCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel peruCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            peruStickLevels.Add(peruCueLevel1);
            peruStickLevels.Add(peruCueLevel2);
            peruStickLevels.Add(peruCueLevel3);
            peruStickLevels.Add(peruCueLevel4);
            peruStickLevels.Add(peruCueLevel5);
            CueStick peruCue = new CueStick(cueSprites[4], "Peru Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, peruStickLevels, "Advanced", 0, 0, peruCueStickProperties);
            countryCueStickList.Add(peruCue);
            //Philippines Cue
            CueStickProperties philippinesCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> philippinesStickLevels = new List<CueStickLevel>();
            CueStickLevel philippinesCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel philippinesCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel philippinesCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 0);
            CueStickLevel philippinesCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel philippinesCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 1, 0);
            philippinesStickLevels.Add(philippinesCueLevel1);
            philippinesStickLevels.Add(philippinesCueLevel2);
            philippinesStickLevels.Add(philippinesCueLevel3);
            philippinesStickLevels.Add(philippinesCueLevel4);
            philippinesStickLevels.Add(philippinesCueLevel5);
            CueStick philippinesCue = new CueStick(cueSprites[4], "Philippines Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, philippinesStickLevels, "Advanced", 0, 0, philippinesCueStickProperties);
            countryCueStickList.Add(philippinesCue);
            //Poland Cue
            CueStickProperties polandCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> polandStickLevels = new List<CueStickLevel>();
            CueStickLevel polandCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel polandCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel polandCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel polandCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel polandCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            polandStickLevels.Add(polandCueLevel1);
            polandStickLevels.Add(polandCueLevel2);
            polandStickLevels.Add(polandCueLevel3);
            polandStickLevels.Add(polandCueLevel4);
            polandStickLevels.Add(polandCueLevel5);
            CueStick polandCue = new CueStick(cueSprites[4], "Poland Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, polandStickLevels, "Advanced", 0, 0, polandCueStickProperties);
            countryCueStickList.Add(polandCue);
            //Portugal Cue
            CueStickProperties portugalCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> portugalStickLevels = new List<CueStickLevel>();
            CueStickLevel portugalCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel portugalCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel portugalCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel portugalCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel portugalCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            portugalStickLevels.Add(portugalCueLevel1);
            portugalStickLevels.Add(portugalCueLevel2);
            portugalStickLevels.Add(portugalCueLevel3);
            portugalStickLevels.Add(portugalCueLevel4);
            portugalStickLevels.Add(portugalCueLevel5);
            CueStick portugalCue = new CueStick(cueSprites[4], "Portugal Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, portugalStickLevels, "Advanced", 0, 0, portugalCueStickProperties);
            countryCueStickList.Add(portugalCue);
            //Puerto Rico Cue
            CueStickProperties puertoRicoCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> puertoRicoStickLevels = new List<CueStickLevel>();
            CueStickLevel puertoRicoCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel puertoRicoCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel puertoRicoCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel puertoRicoCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel puertoRicoCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            puertoRicoStickLevels.Add(puertoRicoCueLevel1);
            puertoRicoStickLevels.Add(puertoRicoCueLevel2);
            puertoRicoStickLevels.Add(puertoRicoCueLevel3);
            puertoRicoStickLevels.Add(puertoRicoCueLevel4);
            puertoRicoStickLevels.Add(puertoRicoCueLevel5);
            CueStick puertoRicoCue = new CueStick(cueSprites[4], "Puerto Rico Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, puertoRicoStickLevels, "Advanced", 0, 0, puertoRicoCueStickProperties);
            countryCueStickList.Add(puertoRicoCue);
            //Qatar Cue
            CueStickProperties qatarCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> qatarStickLevels = new List<CueStickLevel>();
            CueStickLevel qatarCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel qatarCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel qatarCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 0);
            CueStickLevel qatarCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel qatarCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 1, 0);
            qatarStickLevels.Add(qatarCueLevel1);
            qatarStickLevels.Add(qatarCueLevel2);
            qatarStickLevels.Add(qatarCueLevel3);
            qatarStickLevels.Add(qatarCueLevel4);
            qatarStickLevels.Add(qatarCueLevel5);
            CueStick qatarCue = new CueStick(cueSprites[4], "Qatar Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, qatarStickLevels, "Advanced", 0, 0, qatarCueStickProperties);
            countryCueStickList.Add(qatarCue);
            //Romania Cue
            CueStickProperties romaniaCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> romaniaStickLevels = new List<CueStickLevel>();
            CueStickLevel romaniaCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel romaniaCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel romaniaCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel romaniaCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel romaniaCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            romaniaStickLevels.Add(romaniaCueLevel1);
            romaniaStickLevels.Add(romaniaCueLevel2);
            romaniaStickLevels.Add(romaniaCueLevel3);
            romaniaStickLevels.Add(romaniaCueLevel4);
            romaniaStickLevels.Add(romaniaCueLevel5);
            CueStick romaniaCue = new CueStick(cueSprites[4], "Romania Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, romaniaStickLevels, "Advanced", 0, 0, romaniaCueStickProperties);
            countryCueStickList.Add(romaniaCue);
            //Russia Cue
            CueStickProperties russiaCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> russiaStickLevels = new List<CueStickLevel>();
            CueStickLevel russiaCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel russiaCueLevel2 = new CueStickLevel(2, 0, 0, 1, 0, 0, 0);
            CueStickLevel russiaCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel russiaCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel russiaCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            russiaStickLevels.Add(russiaCueLevel1);
            russiaStickLevels.Add(russiaCueLevel2);
            russiaStickLevels.Add(russiaCueLevel3);
            russiaStickLevels.Add(russiaCueLevel4);
            russiaStickLevels.Add(russiaCueLevel5);
            CueStick russiaCue = new CueStick(cueSprites[4], "Russia Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, russiaStickLevels, "Advanced", 0, 0, russiaCueStickProperties);
            countryCueStickList.Add(russiaCue);
            //Saudi Arabia Cue
            CueStickProperties saudiArabiaCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> saudiArabiaStickLevels = new List<CueStickLevel>();
            CueStickLevel saudiArabiaCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel saudiArabiaCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel saudiArabiaCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel saudiArabiaCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel saudiArabiaCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            saudiArabiaStickLevels.Add(saudiArabiaCueLevel1);
            saudiArabiaStickLevels.Add(saudiArabiaCueLevel2);
            saudiArabiaStickLevels.Add(saudiArabiaCueLevel3);
            saudiArabiaStickLevels.Add(saudiArabiaCueLevel4);
            saudiArabiaStickLevels.Add(saudiArabiaCueLevel5);
            CueStick saudiArabiaCue = new CueStick(null, "Saudi Arabia Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, saudiArabiaStickLevels, "Advanced", 0, 0, saudiArabiaCueStickProperties);
            countryCueStickList.Add(saudiArabiaCue);
            //Serbia Cue
            CueStickProperties serbiaCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> serbiaStickLevels = new List<CueStickLevel>();
            CueStickLevel serbiaCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel serbiaCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel serbiaCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel serbiaCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel serbiaCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            serbiaStickLevels.Add(serbiaCueLevel1);
            serbiaStickLevels.Add(serbiaCueLevel2);
            serbiaStickLevels.Add(serbiaCueLevel3);
            serbiaStickLevels.Add(serbiaCueLevel4);
            serbiaStickLevels.Add(serbiaCueLevel5);
            CueStick serbiaCue = new CueStick(cueSprites[4], "Serbia Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, serbiaStickLevels, "Advanced", 0, 0, serbiaCueStickProperties);
            countryCueStickList.Add(serbiaCue);
            //Singapore Cue
            CueStickProperties singaporeCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> singaporeStickLevels = new List<CueStickLevel>();
            CueStickLevel singaporeCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel singaporeCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel singaporeCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel singaporeCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel singaporeCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            singaporeStickLevels.Add(singaporeCueLevel1);
            singaporeStickLevels.Add(singaporeCueLevel2);
            singaporeStickLevels.Add(singaporeCueLevel3);
            singaporeStickLevels.Add(singaporeCueLevel4);
            singaporeStickLevels.Add(singaporeCueLevel5);
            CueStick singaporeCue = new CueStick(cueSprites[4], "Singapore Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, singaporeStickLevels, "Advanced", 0, 0, singaporeCueStickProperties);
            countryCueStickList.Add(singaporeCue);
            //  South Africa Cue
            CueStickProperties southAfricaCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> southAfricaStickLevels = new List<CueStickLevel>();
            CueStickLevel southAfricaCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel southAfricaCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel southAfricaCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 0);
            CueStickLevel southAfricaCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel southAfricaCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 1, 0);
            southAfricaStickLevels.Add(southAfricaCueLevel1);
            southAfricaStickLevels.Add(southAfricaCueLevel2);
            southAfricaStickLevels.Add(southAfricaCueLevel3);
            southAfricaStickLevels.Add(southAfricaCueLevel4);
            southAfricaStickLevels.Add(southAfricaCueLevel5);
            CueStick southAfricaCue = new CueStick(cueSprites[4], "South Africa Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, southAfricaStickLevels, "Advanced", 0, 0, southAfricaCueStickProperties);
            countryCueStickList.Add(southAfricaCue);
            //South Korea Cue
            CueStickProperties southKoreaCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> southKoreaStickLevels = new List<CueStickLevel>();
            CueStickLevel southKoreaCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel southKoreaCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel southKoreaCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel southKoreaCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel southKoreaCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            southKoreaStickLevels.Add(southKoreaCueLevel1);
            southKoreaStickLevels.Add(southKoreaCueLevel2);
            southKoreaStickLevels.Add(southKoreaCueLevel3);
            southKoreaStickLevels.Add(southKoreaCueLevel4);
            southKoreaStickLevels.Add(southKoreaCueLevel5);
            CueStick southKoreaCue = new CueStick(cueSprites[4], "South Korea Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, southKoreaStickLevels, "Advanced", 0, 0, southKoreaCueStickProperties);
            countryCueStickList.Add(southKoreaCue);
            //Spain
            CueStickProperties spainCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> spainStickLevels = new List<CueStickLevel>();
            CueStickLevel spainCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel spainCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel spainCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel spainCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel spainCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            spainStickLevels.Add(spainCueLevel1);
            spainStickLevels.Add(spainCueLevel2);
            spainStickLevels.Add(spainCueLevel3);
            spainStickLevels.Add(spainCueLevel4);
            spainStickLevels.Add(spainCueLevel5);
            CueStick spainCue = new CueStick(cueSprites[4], "Spain Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, spainStickLevels, "Advanced", 0, 0, spainCueStickProperties);
            countryCueStickList.Add(spainCue);
            //Srilanka Cue
            CueStickProperties srilankaCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> srilankaStickLevels = new List<CueStickLevel>();
            CueStickLevel srilankaCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel srilankaCueLevel2 = new CueStickLevel(2, 0, 0, 1, 0, 0, 0);
            CueStickLevel srilankaCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel srilankaCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel srilankaCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            srilankaStickLevels.Add(srilankaCueLevel1);
            srilankaStickLevels.Add(srilankaCueLevel2);
            srilankaStickLevels.Add(srilankaCueLevel3);
            srilankaStickLevels.Add(srilankaCueLevel4);
            srilankaStickLevels.Add(srilankaCueLevel5);
            CueStick srilankaCue = new CueStick(cueSprites[4], "Srilanka Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, srilankaStickLevels, "Advanced", 0, 0, srilankaCueStickProperties);
            countryCueStickList.Add(srilankaCue);
            //Sweden Cue
            CueStickProperties swedenCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> swedenStickLevels = new List<CueStickLevel>();
            CueStickLevel swedenCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel swedenCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel swedenCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel swedenCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel swedenCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            swedenStickLevels.Add(swedenCueLevel1);
            swedenStickLevels.Add(swedenCueLevel2);
            swedenStickLevels.Add(swedenCueLevel3);
            swedenStickLevels.Add(swedenCueLevel4);
            swedenStickLevels.Add(swedenCueLevel5);
            CueStick swedenCue = new CueStick(cueSprites[4], "Sweden Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, swedenStickLevels, "Advanced", 0, 0, swedenCueStickProperties);
            countryCueStickList.Add(swedenCue);
            //Switzerland Cue
            CueStickProperties switzerlandCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> switzerlandStickLevels = new List<CueStickLevel>();
            CueStickLevel switzerlandCueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel switzerlandCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel switzerlandCueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 0);
            CueStickLevel switzerlandCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel switzerlandCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 1, 0);
            switzerlandStickLevels.Add(switzerlandCueLevel1);
            switzerlandStickLevels.Add(switzerlandCueLevel2);
            switzerlandStickLevels.Add(switzerlandCueLevel3);
            switzerlandStickLevels.Add(switzerlandCueLevel4);
            switzerlandStickLevels.Add(switzerlandCueLevel5);
            CueStick switzerlandCue = new CueStick(cueSprites[4], "Switzerland Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, switzerlandStickLevels, "Advanced", 0, 0, switzerlandCueStickProperties);
            countryCueStickList.Add(switzerlandCue);
            //Taiwan Cue
            CueStickProperties taiwanCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> taiwanStickLevels = new List<CueStickLevel>();
            CueStickLevel taiwanCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel taiwanCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel taiwanCueLevel3 = new CueStickLevel(3, 0, 0, 0, 1, 0, 0);
            CueStickLevel taiwanCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel taiwanCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            taiwanStickLevels.Add(taiwanCueLevel1);
            taiwanStickLevels.Add(taiwanCueLevel2);
            taiwanStickLevels.Add(taiwanCueLevel3);
            taiwanStickLevels.Add(taiwanCueLevel4);
            taiwanStickLevels.Add(taiwanCueLevel5);
            CueStick taiwanCue = new CueStick(cueSprites[4], "Taiwan Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, taiwanStickLevels, "Advanced", 0, 0, taiwanCueStickProperties);
            countryCueStickList.Add(taiwanCue);
            //Thailand Cue
            CueStickProperties thailandCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> thailandStickLevels = new List<CueStickLevel>();
            CueStickLevel thailandCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 1, 0);
            CueStickLevel thailandCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel thailandCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel thailandCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel thailandCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            thailandStickLevels.Add(thailandCueLevel1);
            thailandStickLevels.Add(thailandCueLevel2);
            thailandStickLevels.Add(thailandCueLevel3);
            thailandStickLevels.Add(thailandCueLevel4);
            thailandStickLevels.Add(thailandCueLevel5);
            CueStick thailandCue = new CueStick(cueSprites[4], "Thailand Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, thailandStickLevels, "Advanced", 0, 0, thailandCueStickProperties);
            countryCueStickList.Add(thailandCue);
            //Tunisia Cue
            CueStickProperties tunisiaCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> tunisiaStickLevels = new List<CueStickLevel>();
            CueStickLevel tunisiaCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel tunisiaCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel tunisiaCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel tunisiaCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel tunisiaCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            tunisiaStickLevels.Add(tunisiaCueLevel1);
            tunisiaStickLevels.Add(tunisiaCueLevel2);
            tunisiaStickLevels.Add(tunisiaCueLevel3);
            tunisiaStickLevels.Add(tunisiaCueLevel4);
            tunisiaStickLevels.Add(tunisiaCueLevel5);
            CueStick tunisiaCue = new CueStick(cueSprites[4], "Tunisia Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, tunisiaStickLevels, "Advanced", 0, 0, tunisiaCueStickProperties);
            countryCueStickList.Add(tunisiaCue);
            //Turkey Cue
            CueStickProperties turkeyCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> turkeyStickLevels = new List<CueStickLevel>();
            CueStickLevel turkeyCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel turkeyCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel turkeyCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel turkeyCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel turkeyCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            turkeyStickLevels.Add(turkeyCueLevel1);
            turkeyStickLevels.Add(turkeyCueLevel2);
            turkeyStickLevels.Add(turkeyCueLevel3);
            turkeyStickLevels.Add(turkeyCueLevel4);
            turkeyStickLevels.Add(turkeyCueLevel5);
            CueStick turkeyCue = new CueStick(cueSprites[4], "Turkey Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, turkeyStickLevels, "Advanced", 0, 0, turkeyCueStickProperties);
            countryCueStickList.Add(turkeyCue);
            //UAE Cue
            CueStickProperties uAECueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> uAEStickLevels = new List<CueStickLevel>();
            CueStickLevel uAECueLevel1 = new CueStickLevel(1, 0, 0, 0, 1, 0, 0);
            CueStickLevel uAECueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel uAECueLevel3 = new CueStickLevel(3, 0, 0, 1, 0, 0, 0);
            CueStickLevel uAECueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel uAECueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 0, 1);
            uAEStickLevels.Add(uAECueLevel1);
            uAEStickLevels.Add(uAECueLevel2);
            uAEStickLevels.Add(uAECueLevel3);
            uAEStickLevels.Add(uAECueLevel4);
            uAEStickLevels.Add(uAECueLevel5);
            CueStick uAECue = new CueStick(cueSprites[4], "UAE Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, uAEStickLevels, "Advanced", 0, 0, uAECueStickProperties);
            countryCueStickList.Add(uAECue);
            //Ukraine Cue
            CueStickProperties ukraineCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> ukraineStickLevels = new List<CueStickLevel>();
            CueStickLevel ukraineCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel ukraineCueLevel2 = new CueStickLevel(2, 0, 0, 0, 1, 0, 0);
            CueStickLevel ukraineCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel ukraineCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 0, 1);
            CueStickLevel ukraineCueLevel5 = new CueStickLevel(5, 0, 0, 0, 0, 1, 0);
            ukraineStickLevels.Add(ukraineCueLevel1);
            ukraineStickLevels.Add(ukraineCueLevel2);
            ukraineStickLevels.Add(ukraineCueLevel3);
            ukraineStickLevels.Add(ukraineCueLevel4);
            ukraineStickLevels.Add(ukraineCueLevel5);
            CueStick ukraineCue = new CueStick(cueSprites[4], "Ukraine Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, ukraineStickLevels, "Advanced", 0, 0, ukraineCueStickProperties);
            countryCueStickList.Add(ukraineCue);
            //UK Cue
            CueStickProperties ukCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> ukStickLevels = new List<CueStickLevel>();
            CueStickLevel ukCueLevel1 = new CueStickLevel(1, 0, 0, 1, 0, 0, 0);
            CueStickLevel ukCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel ukCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel ukCueLevel4 = new CueStickLevel(4, 0, 0, 0, 0, 1, 0);
            CueStickLevel ukCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            ukStickLevels.Add(ukCueLevel1);
            ukStickLevels.Add(ukCueLevel2);
            ukStickLevels.Add(ukCueLevel3);
            ukStickLevels.Add(ukCueLevel4);
            ukStickLevels.Add(ukCueLevel5);
            CueStick ukCue = new CueStick(cueSprites[4], "Uk Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, ukStickLevels, "Advanced", 0, 0, ukCueStickProperties);
            countryCueStickList.Add(ukCue);
            //USA Cue
            CueStickProperties usaCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> usaStickLevels = new List<CueStickLevel>();
            CueStickLevel usaCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel usaCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 0, 1);
            CueStickLevel usaCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 1, 0);
            CueStickLevel usaCueLevel4 = new CueStickLevel(4, 0, 0, 0, 1, 0, 0);
            CueStickLevel usaCueLevel5 = new CueStickLevel(5, 0, 0, 1, 0, 0, 0);
            usaStickLevels.Add(usaCueLevel1);
            usaStickLevels.Add(usaCueLevel2);
            usaStickLevels.Add(usaCueLevel3);
            usaStickLevels.Add(usaCueLevel4);
            usaStickLevels.Add(usaCueLevel5);
            CueStick usaCue = new CueStick(cueSprites[4], "USA Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, usaStickLevels, "Advanced", 0, 0, usaCueStickProperties);
            countryCueStickList.Add(usaCue);
            //Vietnam Cue
            CueStickProperties vietnamCueStickProperties = new CueStickProperties(5, 5, 5, 4, 50);
            List<CueStickLevel> vietnamStickLevels = new List<CueStickLevel>();
            CueStickLevel vietnamCueLevel1 = new CueStickLevel(1, 0, 0, 0, 0, 0, 1);
            CueStickLevel vietnamCueLevel2 = new CueStickLevel(2, 0, 0, 0, 0, 1, 0);
            CueStickLevel vietnamCueLevel3 = new CueStickLevel(3, 0, 0, 0, 0, 0, 1);
            CueStickLevel vietnamCueLevel4 = new CueStickLevel(4, 0, 0, 1, 0, 0, 0);
            CueStickLevel vietnamCueLevel5 = new CueStickLevel(5, 0, 0, 0, 1, 0, 0);
            vietnamStickLevels.Add(vietnamCueLevel1);
            vietnamStickLevels.Add(vietnamCueLevel2);
            vietnamStickLevels.Add(vietnamCueLevel3);
            vietnamStickLevels.Add(vietnamCueLevel4);
            vietnamStickLevels.Add(vietnamCueLevel5);
            CueStick vietnamCue = new CueStick(null, "Vietnam Cue", "Country", "Las Vegas", "40", 40, "160", 160, 0, 4, 5, vietnamStickLevels, "Advanced", 0, 0, vietnamCueStickProperties);
            countryCueStickList.Add(vietnamCue);

            for (int i = 0; i < countryCueStickList.Count; i++)
            {
                if (GetCountryCueStickIsUnlockedFlag(i) == 1)
                {
                    countryUnlockedCueStickList.Add(i);
                    ownedCueStickList.Add(countryCueStickList[i]);
                }
            }
        }



    }
}
