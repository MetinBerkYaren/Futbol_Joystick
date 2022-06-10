using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MarketItemType
{
    Skin,
    Hover,
    Trail
}

[CreateAssetMenu(fileName = "New MarketItem Type", menuName = "MarketItem")]
public class ScriptableExample :ScriptableObject
{
    public string Name;
    public Sprite Image;
    public int Id;
    public MarketItemType Type;
    public bool IsDefault;
    //public MarketItemButton ItemButton;


    private string unlockKey = "unlockKey";
    public  bool IsUnlock
    {
        get => IsDefault? true : PlayerPrefs.GetInt(unlockKey + Name, 0) == 1;
        set => PlayerPrefs.SetInt(unlockKey + Name, value ? 1 : 0);

    }

    

}
