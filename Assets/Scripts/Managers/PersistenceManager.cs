using UnityEngine;

public class PersistenceManager : MonoBehaviour
{
    public enum ActiveCharacterEnum
    {
        Character1,
        Character2
    }
    
    public static bool Is2NdCharacterUnlocked;

    public static int HealthPotionAmount = 1;
    public static int AtkBoostAmount = 1;

    public static ActiveCharacterEnum ActiveCharacter = ActiveCharacterEnum.Character1;

    public static int Coins = 15;

    public static void Reset()
    {
        HealthPotionAmount = 0;
        AtkBoostAmount = 0;
        Coins = 0;
        ActiveCharacter = ActiveCharacterEnum.Character1;
        Is2NdCharacterUnlocked = false;
    }

}
