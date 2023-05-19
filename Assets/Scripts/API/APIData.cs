using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RevolutionGames.APIData
{
    public class Player
    {
        public string player_id;
        public string player_uuid;
        public string player_name;
        public string player_email;
        public string social_id;
        public string player_login_mode;
        public int player_balance;
        public int player_level;
        public int expertise_id;
        public int total_games_played;
        public int total_games_won;
        public int total_tournaments_played;
        public int total_tournaments_won;
        public int total_specialevents_played;
        public int total_specialevents_won;
        public int avatar_id;
        public int country_id;
        public int dice_id;
        public int board_id;
        public int token_id;
        public string created_date;
        public string avatar_url;
        public string country_flag_url;
        public string country_name;
        public string security_key;
    }
    public class AvatarList
    {
        public int avatar_id;
        public string avatar_name;
        public string avatar_url;
        public int is_premium;
        //public int is_locked;
        // public int cost;
        public int is_hidden;
        public string created_date;

    }

    #region Spin data class
    public class SpinData
    {
        public int spin_item_id;
        public int game_id;
        public string spin_item_title;
        public string spin_item_subtitle;
        public string spin_item_url;
        public int coins_count;
        public int is_premium;
        public int is_locked;
        public int is_hidden;
        public string created_date;
    }
    #endregion

    #region Shop Classes
    public class ShopCoin
    {
        public int coins_id;
        public int game_id;
        public string coins_title;
        public string coins_description;
        public int coins_count;
        public float coins_cost;
        public int is_hidden;
        public string created_date;
    }
    public class ShopAvatar
    {
        public int avatar_id;
        public int game_id;
        public string avatar_name;
        public string avatar_url;
        public float cost;
        public int premium_flag;
        public int locked_flag;
        public int hidden_flag;
        public string created_date;
        public string status;
    }
    public class ShopBoard
    {
        public int board_id;
        public int game_id;
        public string board_name;
        public string board_description;
        public string board_url;
        public int is_premium;
        public int is_locked;
        public float coin_count;
        public int is_hidden;
        public string created_date;
        public string status;
    }
    public class ShopToken
    {
        public int token_id;
        public int game_id;
        public string token_name;
        public string token_url_folder;
        public string red_color_token_url;
        public string green_color_token_url;
        public string blue_color_token_url;
        public string yellow_color_token_url;
        public int is_premium;
        public int is_locked;
        public float cost;
        public int is_hidden;
        public string created_date;
        public string status;
    }
    public class ShopDice
    {
        public int dice_id;
        public int game_id;
        public string dice_name;
        public string dice_description;
        public string dice_url;
        public int is_premium;
        public int is_locked;
        public float cost;
        public int is_hidden;
        public string created_date;
        public string status;
    }
    #endregion

    #region Leaderboard classes
    public class Leaderboard
    {
        public int player_id;
        public string player_uuid;
        public string player_name;
        public string player_email;
        public string player_password;
        public string social_id;
        public string player_login_mode;
        public int player_balance;
        public int player_level;
        public int expertise_id;
        public int total_games_played;
        public int total_games_won;
        public int total_tournaments_played;
        public int total_tournaments_won;
        public int total_specialevents_won;
        public int avatar_id;
        public int country_id;
        public int dice_id;
        public int board_id;
        public int token_id;
        public string created_date;
        public string total_point;
    }
    #endregion

    #region Friends classes
    public class Friend
    {
        public string player_name;
        public int id;
        public int player_id;
        public int friend_id; // all 3 needed?
        //public int player_level;
        //public int wins;
        //public int loss;
        //public string last_online;
        //public int total_winnings;
        //public bool is_FBFriend;
        public int is_hidden; //needed?
        public int is_deleted;
        public string created_date;
    }
    public class FbFriend
    {
        public string player_id;
        public string player_uuid;
        public string player_name;
        public string player_email;
        public string player_password;
        public string social_id;
        public string player_login_mode;
        public int player_balance;
        public int player_level;
        public int expertise_id;
        public int total_tournaments_played;
        public int total_tournaments_won;
        public int total_specialevents_played;
        public int total_specialevents_won;
        public int avatar_id;
        public int country_id;
        public int dice_id;
        public int board_id;
        public int token_id;
        public string created_date;
    }
    #endregion

    #region Game mode class
    public class GameMode
    {
        public string game_type_name;
        public int arena_id;
        public int game_id;
        public int game_type_id;
        public string arena_title;
        public string arena_description;
        public string arena_url;
        public int maximum_no_of_players;
        public int entry_fee_coins;
        public int winning_coins;
        public int is_premium;
        public int is_locked;
        public int cost;
        public int is_hidden;
        public string created_date;
    }
    #endregion

    #region Opponent Data class
    public class Opponent //verify with praju sir and remove
    {
        public string player_id;
        public string player_uuid;
        public string player_name;
        public int avatar_id;
    }
    #endregion

    #region Tournament Progress class
    public class TournamentProgress
    {
        public bool newGame;
        public TournamentPlayerData quarter_final1_player1;
        public TournamentPlayerData quarter_final1_player2;
        public TournamentPlayerData quarter_final2_player1;
        public TournamentPlayerData quarter_final2_player2;
        public TournamentPlayerData quarter_final3_player1;
        public TournamentPlayerData quarter_final3_player2;
        public TournamentPlayerData quarter_final4_player1;
        public TournamentPlayerData quarter_final4_player2;

        public TournamentPlayerData semi_final1_player1;
        public TournamentPlayerData semi_final1_player2;
        public TournamentPlayerData semi_final2_player1;
        public TournamentPlayerData semi_final2_player2;

        public TournamentPlayerData final_player1;
        public TournamentPlayerData final_player2;


    }
    #endregion

    public class TournamentPlayerData
    {
        public string player_id;
        public string player_name;
        //public int avatar_id;
        public int player_balance;
        public int player_level;
        public bool is_game_over;
        public bool is_winner;
    }

    public class Video //Check with Praju Sir
    {
        public int video_ads_id;
        public int game_id;
        public string video_ads_title;
        public string video_ads_provider;
        public int coins_count;
        public string video_ads_key1;
        public string video_ads_key2;
        public int is_hidden;
        public string created_date;
    }

    public class ProgressMilestone
    {
        public int milestoneText;
        public Sprite prizeImage;
    }

    public class City
    {
        public string city_id;
        public int winning_trophies;
        public int losing_trophies;
        public int prize;
        public int entry_fee;
        public string game_type;
        public Sprite city_image;
    }

    class CueStickProperties //APIData
    {
        private int force = 0;
        private int spin = 0;
        private int aim = 0;
        private int time = 0;
        private int charge = 0;

        public CueStickProperties(int force, int spin, int aim, int time, int charge)
        {
            this.force = force;
            this.spin = spin;
            this.aim = aim;
            this.time = time;
            this.charge = charge;
        }

        public int Force { get => force; set => force = value; }
        public int Spin { get => spin; set => spin = value; }
        public int Aim { get => aim; set => aim = value; }
        public int Time { get => time; set => time = value; }
        public int Charge { get => charge; set => charge = value; }
    }

    class CueStick //APIData
    {
        private Sprite image;
        private string name; //primary key
        private string category; // Standard, premium, country, victory, owned
        private string price;
        private long priceInNumbers;
        private int rechargePrice;
        private int rechargePriceInNumbers;
        private int unlockedPieces;
        private int totalPieces;
        private int level;
        private int subLevel;
        private string cueType; // Basic, advanced, legendary
        private CueStickProperties cueStickProperties;

        public CueStick(Sprite image, string name, CueStickProperties cueStickProperties)
        {
            this.image = image;
            this.name = name;
            this.cueStickProperties = cueStickProperties;
        }

        public CueStick(Sprite image, string name, string category, string price, long priceInNumbers, int rechargePrice,
            int unlockedPieces, int totalPieces, int level, int subLevel, string cueType, CueStickProperties cueStickProperties)
        {
            this.image = image;
            this.name = name;
            this.category = category;
            this.price = price;
            this.priceInNumbers = priceInNumbers;
            this.rechargePrice = rechargePrice;
            this.unlockedPieces = unlockedPieces;
            this.totalPieces = totalPieces;
            this.level = level;
            this.subLevel = subLevel;
            this.cueType = cueType;
            this.cueStickProperties = cueStickProperties;
        }

        public Sprite Image { get => image; set => image = value; }
        public string Name { get => name; set => name = value; }
        public string Category { get => category; set => category = value; }
        public string Price { get => price; set => price = value; }
        public long PriceInNumbers { get => priceInNumbers; set => priceInNumbers = value; }
        public int RechargePrice { get => rechargePrice; set => rechargePrice = value; }
        public int RechargePriceInNumbers { get => rechargePriceInNumbers; set => rechargePriceInNumbers = value; }
        public int UnlockedPieces { get => unlockedPieces; set => unlockedPieces = value; }
        public int TotalPieces { get => totalPieces; set => totalPieces = value; }
        public int Level { get => level; set => level = value; }
        public int SubLevel { get => subLevel; set => subLevel = value; }
        public string CueType { get => cueType; set => cueType = value; }
        internal CueStickProperties CueStickProperties { get => cueStickProperties; set => cueStickProperties = value; }
    }
}
