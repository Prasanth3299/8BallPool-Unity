using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RevolutionGames.Data
{
    class PlayerDetails
    {
        public string playerId = "";
        public string playerUuid = "";
        public string playerName = "";
        public string playerPassword = "";
        public string playerEmailId = "";
        public string playerSocialId = "";
        public string playerLoginMode = "";
        public long playerBalance; //coins
        public int playerLevel; //stars
        public int playerExpertiseId;
        public int totalGamesPlayed;
        public int totalGamesWon;
        public int totalTournamentsPlayed;
        public int totalTournamentsWon;
        public int totalSpecialEventsPlayed;
        public int totalSpecialEventsWon;
        public int playerAvatarId = 1;
        public int playerCountryId = 1;
        public int playerDiceId = 1;
        public int playerBoardId = 1;
        public int playerTokenId = 1;
        public string playerCreationDate = "";
        public string playerAvatarUrl = "";
        public string playerCountryUrl = "";
        public string playerCountryName = "";
        public string playerSecurityKey = "";
    }

    class GameDetails
    {
        public string gameUuid = "guuid-2e55910a-7159-4ed7-95bb-c3976187991c-2182020155410615";
        public string deviceName = "iPhone";
        //SystemInfo.deviceName;
        public string deviceId = "127dst94e736tee52e7wge";
        //SystemInfo.deviceModel;
        public string hardwareInfo = "ver 2.3";
        public string softwareInfo = "qwqw";
        //SystemInfo.operatingSystem;
        public string deviceToken = "qweewq";
        //SystemInfo.deviceUniqueIdentifier;
    }

    class OpponentDetails
    {
        public string opponentId = "";
        public string opponentUuid = "";
        public string opponentName = "";
        public int opponentAvatarId = 1;
        public int opponentLevel; //stars
        public Sprite opponentAvatar;
    }

    public class GameData : MonoBehaviour
    {
        private static GameData instance = null;
        private PlayerDetails playerDetails;
        private GameDetails gameDetails;
        private OpponentDetails opponentDetails;
        private string toScreenName;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(this);
        }
        void Start()
        {
            playerDetails = new PlayerDetails();
            gameDetails = new GameDetails();
            opponentDetails = new OpponentDetails();
            playerDetails.playerBalance = 2220;
        }

        public static GameData Instance()
        {
            return instance;
        }


        #region Player Data Functions
        public void UpdatePlayerDetails(string playerId, string playerUuid, string playerName, string playerEmailId, string playerSocialId,
           string playerLoginMode, long playerBalance, int playerLevel, int playerExpertiseId, int totalGamesPlayed, int totalGamesWon,
           int totalTournamentsPlayed, int totalTournamentsWon, int totalSpecialEventsPlayed, int totalSpecialEventsWon,
           int playerAvatarId, int playerCountryId, int playerDiceId, int playerBoardId, int playerTokenId, string playerCreationDate,
           string playerAvatarUrl, string playerCountryUrl, string playerCountryName, string playerSecurityKey)
        {
            playerDetails.playerId = playerId;
            playerDetails.playerUuid = playerUuid;
            playerDetails.playerName = playerName;
            playerDetails.playerEmailId = playerEmailId;
            playerDetails.playerSocialId = playerSocialId;
            playerDetails.playerLoginMode = playerLoginMode;
            playerDetails.playerBalance = playerBalance;
            playerDetails.playerLevel = playerLevel;
            playerDetails.playerExpertiseId = playerExpertiseId;
            playerDetails.totalGamesPlayed = totalGamesPlayed;
            playerDetails.totalGamesWon = totalGamesWon;
            playerDetails.totalTournamentsPlayed = totalTournamentsPlayed;
            playerDetails.totalTournamentsWon = totalTournamentsWon;
            playerDetails.totalSpecialEventsPlayed = totalSpecialEventsPlayed;
            playerDetails.totalSpecialEventsWon = totalSpecialEventsWon;
            playerDetails.playerAvatarId = playerAvatarId;
            playerDetails.playerCountryId = playerCountryId;
            playerDetails.playerDiceId = playerDiceId;
            playerDetails.playerBoardId = playerBoardId;
            playerDetails.playerTokenId = playerTokenId;
            playerDetails.playerCreationDate = playerCreationDate;
            playerDetails.playerAvatarUrl = playerAvatarUrl;
            playerDetails.playerCountryUrl = playerCountryUrl;
            playerDetails.playerCountryName = playerCountryName;
            playerDetails.playerSecurityKey = playerSecurityKey;
        }
        public string PlayerId
        {
            get
            {
                return playerDetails.playerId;
            }
            set
            {
                playerDetails.playerId = value;
            }
        }
        public string PlayerUuid
        {
            get
            {
                return playerDetails.playerUuid;
            }
            set
            {
                playerDetails.playerUuid = value;
            }
        }
        public string PlayerName
        {
            get
            {
                return playerDetails.playerName;
            }
            set
            {
                playerDetails.playerName = value;
            }
        }
        public string PlayerPassword
        {
            get
            {
                return playerDetails.playerPassword;
            }
            set
            {
                playerDetails.playerPassword = value;
            }
        }
        public string PlayerEmailId
        {
            get
            {
                return playerDetails.playerEmailId;
            }
            set
            {
                playerDetails.playerEmailId = value;
            }
        }
        public string PlayerSocialId
        {
            get
            {
                return playerDetails.playerSocialId;
            }
            set
            {
                playerDetails.playerSocialId = value;
            }
        }
        public string PlayerLoginMode
        {
            get
            {
                return playerDetails.playerLoginMode;
            }
            set
            {
                playerDetails.playerLoginMode = value;
            }
        }
        public long PlayerBalance
        {
            get
            {
                return playerDetails.playerBalance;
            }
            set
            {
                playerDetails.playerBalance = value;
            }
        }
        public int PlayerLevel
        {
            get
            {
                return playerDetails.playerLevel;
            }
            set
            {
                playerDetails.playerLevel = value;
            }
        }
        public int PlayerExpertiseId
        {
            get
            {
                return playerDetails.playerExpertiseId;
            }
            set
            {
                playerDetails.playerExpertiseId = value;
            }
        }
        public int TotalGamesPlayed
        {
            get
            {
                return playerDetails.totalGamesPlayed;
            }
            set
            {
                playerDetails.totalGamesPlayed = value;
            }
        }
        public int TotalGamesWon
        {
            get
            {
                return playerDetails.totalGamesWon;
            }
            set
            {
                playerDetails.totalGamesWon = value;
            }
        }
        public int TotalTournamentsPlayed
        {
            get
            {
                return playerDetails.totalTournamentsPlayed;
            }
            set
            {
                playerDetails.totalTournamentsPlayed = value;
            }
        }
        public int TotalTournamentsWon
        {
            get
            {
                return playerDetails.totalTournamentsWon;
            }
            set
            {
                playerDetails.totalTournamentsWon = value;
            }
        }
        public int TotalSpecialEventsPlayed
        {
            get
            {
                return playerDetails.totalSpecialEventsPlayed;
            }
            set
            {
                playerDetails.totalSpecialEventsPlayed = value;
            }
        }
        public int TotalSpecialEventsWon
        {
            get
            {
                return playerDetails.totalSpecialEventsWon;
            }
            set
            {
                playerDetails.totalSpecialEventsWon = value;
            }
        }
        public int PlayerAvatarId
        {
            get
            {
                return playerDetails.playerAvatarId;
            }
            set
            {
                playerDetails.playerAvatarId = value;
            }
        }
        public int PlayerCountryId
        {
            get
            {
                return playerDetails.playerCountryId;
            }
            set
            {
                playerDetails.playerCountryId = value;
            }
        }
        public int PlayerDiceId
        {
            get
            {
                return playerDetails.playerDiceId;
            }
            set
            {
                playerDetails.playerDiceId = value;
            }
        }
        public int PlayerBoardId
        {
            get
            {
                return playerDetails.playerBoardId;
            }
            set
            {
                playerDetails.playerBoardId = value;
            }
        }
        public int PlayerTokenId
        {
            get
            {
                return playerDetails.playerTokenId;
            }
            set
            {
                playerDetails.playerTokenId = value;
            }
        }
        public string PlayerCreationDate
        {
            get
            {
                return playerDetails.playerCreationDate;
            }
            set
            {
                playerDetails.playerCreationDate = value;
            }
        }
        public string PlayerAvatarUrl
        {
            get
            {
                return playerDetails.playerAvatarUrl;
            }
            set
            {
                playerDetails.playerAvatarUrl = value;
            }
        }
        public string PlayerCountryUrl
        {
            get
            {
                return playerDetails.playerCountryUrl;
            }
            set
            {
                playerDetails.playerCountryUrl = value;
            }
        }
        public string PlayerCountryName
        {
            get
            {
                return playerDetails.playerCountryName;
            }
            set
            {
                playerDetails.playerCountryName = value;
            }
        }
        public string PlayerSecurityKey
        {
            get
            {
                return playerDetails.playerSecurityKey;
            }
            set
            {
                playerDetails.playerSecurityKey = value;
            }
        }
        #endregion

        #region Game Data Functions
        public string GameUuid
        {
            get
            {
                return gameDetails.gameUuid;
            }
            set
            {
                gameDetails.gameUuid = value;
            }
        }
        public string DeviceName
        {
            get
            {
                return gameDetails.deviceName;
            }
            set
            {
                gameDetails.deviceName = value;
            }
        }
        public string DeviceId
        {
            get
            {
                return gameDetails.deviceId;
            }
            set
            {
                gameDetails.deviceId = value;
            }
        }
        public string HardwareInfo
        {
            get
            {
                return gameDetails.hardwareInfo;
            }
            set
            {
                gameDetails.hardwareInfo = value;
            }
        }
        public string SoftwareInfo
        {
            get
            {
                return gameDetails.softwareInfo;
            }
            set
            {
                gameDetails.softwareInfo = value;
            }
        }
        public string DeviceToken
        {
            get
            {
                return gameDetails.deviceToken;
            }
            set
            {
                gameDetails.deviceToken = value;
            }
        }
        #endregion

        #region Opponent Data functions
        public void UpdateOpponentDetails(string opponentId, string opponentUuid, string opponentName, int opponentLevel, int opponentAvatarId)
        {
            opponentDetails.opponentId = opponentId;
            opponentDetails.opponentUuid = opponentUuid;
            opponentDetails.opponentName = opponentName;
            opponentDetails.opponentLevel = opponentLevel;
            opponentDetails.opponentAvatarId = opponentAvatarId;
        }

        public string OpponentId
        {
            get
            {
                return opponentDetails.opponentId;
            }
            set
            {
                opponentDetails.opponentId = value;
            }
        }
        public string OpponentUuid
        {
            get
            {
                return opponentDetails.opponentUuid;
            }
            set
            {
                opponentDetails.opponentUuid = value;
            }
        }
        public string OpponentName
        {
            get
            {
                return opponentDetails.opponentName;
            }
            set
            {
                opponentDetails.opponentName = value;
            }
        }

        public int OpponentLevel
        {
            get
            {
                return opponentDetails.opponentLevel;
            }
            set
            {
                opponentDetails.opponentLevel = value;
            }
        }

        public int OpponentAvatarId
        {
            get
            {
                return opponentDetails.opponentAvatarId;
            }
            set
            {
                opponentDetails.opponentAvatarId = value;
            }
        }

        public Sprite OpponentAvatar
        {
            get
            {
                return opponentDetails.opponentAvatar;
            }
            set
            {
                opponentDetails.opponentAvatar = value;
            }
        }


        #endregion

        public string ToScreenName { get => toScreenName; set => toScreenName = value; }
    }
}

