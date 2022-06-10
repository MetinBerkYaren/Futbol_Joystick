using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{
    public static bool Vibration
    {
        get => PlayerPrefs.GetInt(PlayerPrefKeys.Vibration, 1) == 1;
        set => PlayerPrefs.SetInt(PlayerPrefKeys.Vibration, value ? 1 : 0);
    }

    public static string Version
    {
        get => PlayerPrefs.GetString(PlayerPrefKeys.Version, "");
        set => PlayerPrefs.SetString(PlayerPrefKeys.Version, value);
    }

    public static int DefaultCurrencyAmount = 10;
    public static UnityAction OnCurrencyUpdate;

    public static int Currency
    {
        get => PlayerPrefs.GetInt(PlayerPrefKeys.Currency, DefaultCurrencyAmount);
        set
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.Currency, value);

            OnCurrencyUpdate?.Invoke();
        }
    }

    public static int DefaultUnlockCost = 100;
    public static float UnlockCostFactor = 1.75f;

    public static int UnlockCost
    {
        get => PlayerPrefs.GetInt(PlayerPrefKeys.UnlockCost, DefaultUnlockCost);
        set
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.UnlockCost, value);
        }
    }

    // public int CurrentLevel;
    public static UnityAction OnLevelUpdate;

    public static int CurrentLevel
    {
        get => PlayerPrefs.GetInt(PlayerPrefKeys.PlayerLevel, 0);

        set
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.PlayerLevel, value);
            OnLevelUpdate?.Invoke();
        }
    }

    public static int PlayerPlayCount
    {
        get => PlayerPrefs.GetInt(PlayerPrefKeys.PlayerPlayCount, 0);

        set
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.PlayerPlayCount, value);
        }
    }

    //private static string ZoneKey = "ZoneKey";
    //public static int CurrentZone
    //{
    //    get => PlayerPrefs.GetInt(ZoneKey, 0);
    //    set
    //    {
    //        PlayerPrefs.SetInt(ZoneKey, value);

    //    }
    //}

    //   public UnityAction OnCharacterChange;
    //   private string CharacterKey = "CharacterKey";
    //   private bool isCharacterLoaded;
    //   private int characterId;
    //   public int CharacterId
    //   {
    //       get
    //       {
    //           if (!isCharacterLoaded)
    //           {
    //               CharacterId = PlayerPrefs.GetInt(CharacterKey, 0);
    //               isCharacterLoaded = true;
    //           }
    //           return characterId;
    //       }
    //
    //       set
    //       {
    //           characterId = value;
    //           OnCharacterChange?.Invoke();
    //           PlayerPrefs.SetInt(CharacterKey, characterId);
    //
    //       }
    //   }
    //
    //
    //   public GameObject GetSkin()
    //   {
    //
    //       return Characters[CharacterId];
    //   }

    //private static readonly string KeyCountKey = "KeyCount";
    //private static bool isKeyLoaded;
    //private static int keyCount;
    //public static int KeyCount
    //{
    //    get
    //    {
    //        if (!isKeyLoaded)
    //        {
    //            KeyCount = PlayerPrefs.GetInt(KeyCountKey, 0);
    //            isKeyLoaded = true;
    //        }
    //        return keyCount;
    //    }

    //    set
    //    {
    //        keyCount = value;
    //        if (keyCount > 3)
    //            keyCount = 3;
    //        PlayerPrefs.SetInt(KeyCountKey, keyCount);
    //    }
    //}
}

public struct PlayerPrefKeys
{
    public const string Version = "Version";
    public const string Vibration = "Vibration";
    public const string PlayerName = "PlayerName";
    public const string Currency = "Currency";
    public const string UnlockCost = "UnlockCost";
    public const string SelectedGameMode = "SelectedGameMode";

    public const string NoAdsKey = "NoAds";
    public const string Tutorial = "Tutorial";
    public const string PlayerLevel = "PlayerLevel";
    public const string PlayerZone = "PlayerZone";
    public const string PlayerPlayCount = "PlayerPlayCount";

    public const string PlayerLeague = "PlayerLeague";
    public const string PlayerDivision = "PlayerDivision";
    public const string PlayerLeaguePoints = "PlayerLeaguePoints";
    public const string FollowerNumber = "FollowerNumber";
    public const string PlayerId = "PlayerId";
    public const string HashtagTutorial = "HashtagTutorial";
    public const string GiftBoxPopup = "GiftBoxPopup";
    public const string NewEnviPopup = "NewEnviPopup";
    public const string DayNo = "DayNo";
    public const string Nexttime = "Nexttime";
    public const string PlayerBadge = "PlayerBadge";

    //  public const string PhotoSaveIndex = "PhotoSaveIndex";

    //public const string TotalGoldCount = "TotalGoldCount";
    //public const string TotalKillCount = "TotalKillCount";
    //public const string SoloBestScore = "SoloBestScore";
    //public const string PlayerFlagIndex = "PlayerFlagIndex";
    //public static string PlanetUnlocked(Planets planet) { return "PlanetUnlocked" + planet; }
    //public static string PlanetAdWatched(Planets planet) { return "PlanetAdWatched" + planet; }
    //public static string SkinUnlocked(int skinId) { return "SkinUnlocked" + skinId; }
    //public static string SkinAdWatched(int skinId) { return "SkinAdWatched" + skinId; }
    //public const string TotalExp = "TotalExp";
    //public const string TotalElo = "TotalElo";
    //public const string SelectedPlanet = "SelectedPlanet";
    //public const string SelectedSkin = "SelectedSkin";
    //public const string LastPlayerRank = "LastPlayerRank"; //To show placement window on main menu
    //public const string WindowPlaying = "WindowPlaying";

    ////Task Keys
    //public const string DailyTaskDate = "DailyTaskDate";
    //public const string DailyTaskLeague = "DailyTaskLeague";
    //public const string DailyTaskFirstClaim = "DailyTaskFirstClaim";
    //public static string DailyTaskID(int taskNo) { return "DailyTaskID" + taskNo; }
    //public static string DailyTaskCount(int taskNo) { return "DailyTaskCount" + taskNo; }
    //public static string DailyTaskCompleted(int taskNo) { return "DailyTaskCompleted" + taskNo; }
    //public static string DailyTaskClaimed(int taskNo) { return "DailyTaskClaimed" + taskNo; }
    //public static string DailyTaskCurrentCount(int taskNo) { return "DailyTaskCurrentCount" + taskNo; }
}