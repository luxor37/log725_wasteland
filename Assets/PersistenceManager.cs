using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistenceManager : MonoBehaviour
{
    public enum ActiveCharacter{
        character1,
        character2
    }
    public static bool is2ndCharacterUnlocked = false;

    public static ActiveCharacter activeCharacter = ActiveCharacter.character1;
    
    public static int coins = 0;
}
