using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistenceManager : MonoBehaviour
{
    public enum ActiveCharacter
    {
        character1,
        character2
    }

#if (UNITY_EDITOR)
    public static bool is2ndCharacterUnlocked = true;
#else
    public static bool is2ndCharacterUnlocked = false;
#endif



    public static ActiveCharacter activeCharacter = ActiveCharacter.character1;

    public static int coins = 0;
}
