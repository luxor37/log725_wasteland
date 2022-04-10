using System;
using UnityEngine;

public class SwitchCharacter : MonoBehaviour
{
    private Vector3 currentPosition;

    private static int _characterIndex;

    public static GameObject currentCharacter;

    void Start()
    {

        for (var i = 0; i < Enum.GetNames(typeof(PersistenceManager.ActiveCharacterEnum)).Length; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        var activeCharacter = (int)PersistenceManager.ActiveCharacter;

        currentCharacter = transform.GetChild(activeCharacter).gameObject;

        transform.GetChild(activeCharacter).gameObject.SetActive(true);
    }

    void Update()
    {
        if (InputController.IsCharacterChanging && PersistenceManager.Is2NdCharacterUnlocked)
        {
            _characterIndex = (int)PersistenceManager.ActiveCharacter;

            currentPosition = transform.GetChild(_characterIndex).gameObject.transform.position;
            InputController.IsCharacterChanging = false;
            transform.GetChild(_characterIndex).gameObject.SetActive(false);

            if (_characterIndex < Enum.GetNames(typeof(PersistenceManager.ActiveCharacterEnum)).Length - 1)
                _characterIndex++;
            else
                _characterIndex = 0;

            PersistenceManager.ActiveCharacter = (PersistenceManager.ActiveCharacterEnum)_characterIndex;

            transform.GetChild(_characterIndex).gameObject.transform.position = currentPosition;
            transform.GetChild(_characterIndex).gameObject.SetActive(true);
        }

        try
        {
            currentCharacter = transform.GetChild(_characterIndex).gameObject;
            gameObject.transform.position = transform.GetChild(_characterIndex).gameObject.transform.position;
        }
        catch (Exception)
        {
            Debug.Log("Cannot find character at index "+_characterIndex+". Is it dead?");
        }
    }
}
