using System;
using UnityEngine;

public class SwitchCharacter : MonoBehaviour
{
    private Vector3 currentPosition;

    private static int characterIndex = 0;

    public static GameObject currentCharacter;

    void Start()
    {

        for (int i = 0; i < Enum.GetNames(typeof(PersistenceManager.ActiveCharacterEnum)).Length; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        var characterIndex = (int)PersistenceManager.ActiveCharacter;

        currentCharacter = transform.GetChild(characterIndex).gameObject;

        transform.GetChild(characterIndex).gameObject.SetActive(true);
    }

    void Update()
    {
        if (InputController.IsCharacterChanging && PersistenceManager.Is2NdCharacterUnlocked)
        {
            characterIndex = (int)PersistenceManager.ActiveCharacter;

            currentPosition = transform.GetChild(characterIndex).gameObject.transform.position;
            InputController.IsCharacterChanging = false;
            transform.GetChild(characterIndex).gameObject.SetActive(false);

            if (characterIndex < Enum.GetNames(typeof(PersistenceManager.ActiveCharacterEnum)).Length - 1)
                characterIndex++;
            else
                characterIndex = 0;

            PersistenceManager.ActiveCharacter = (PersistenceManager.ActiveCharacterEnum)characterIndex;

            transform.GetChild(characterIndex).gameObject.transform.position = currentPosition;
            transform.GetChild(characterIndex).gameObject.SetActive(true);
        }

        try
        {
            currentCharacter = transform.GetChild(characterIndex).gameObject;
            gameObject.transform.position = transform.GetChild(characterIndex).gameObject.transform.position;
        }
        catch (Exception ex)
        {
            Debug.Log("Cannot find character at index "+characterIndex+". Is it dead?");
        }
    }
}
