using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistenceManager : MonoBehaviour
{
    public enum ActiveCharacterEnum
    {
        character1,
        character2
    }
    
    public static bool Is2NdCharacterUnlocked = false;

    public static int HealthPotionAmount = 1;
    public static int AtkBoostAmount = 1;

    public static ActiveCharacterEnum ActiveCharacter = ActiveCharacterEnum.character1;

    public static int coins = 10;

    public static void Reset()
    {
        HealthPotionAmount = 0;
        AtkBoostAmount = 0;
        coins = 0;
        ActiveCharacter = ActiveCharacterEnum.character1;
        Is2NdCharacterUnlocked = false;
    }

}
