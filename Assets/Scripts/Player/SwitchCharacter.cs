using System;
using UnityEngine;

public class SwitchCharacter : MonoBehaviour
{
    private Vector3 currentPosition;
    // Update is called once per frame

    void Start(){
        for (int i = 0; i < Enum.GetNames(typeof(PersistenceManager.ActiveCharacter)).Length; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        var characterIndex = (int)PersistenceManager.activeCharacter;

        transform.GetChild(characterIndex).gameObject.SetActive(true);
    }

    void Update()
    {
        
        if (InputController.IsCharacterChanging && PersistenceManager.is2ndCharacterUnlocked)
        {
            var characterIndex = (int)PersistenceManager.activeCharacter;

            currentPosition =  transform.GetChild(characterIndex).gameObject.transform.position;
            InputController.IsCharacterChanging = false;
            transform.GetChild(characterIndex).gameObject.SetActive(false);

            if(characterIndex < Enum.GetNames(typeof(PersistenceManager.ActiveCharacter)).Length-1)
                characterIndex++;
            else
                characterIndex = 0;

            PersistenceManager.activeCharacter = (PersistenceManager.ActiveCharacter) characterIndex;

            transform.GetChild(characterIndex).gameObject.transform.position = currentPosition;
            transform.GetChild(characterIndex).gameObject.SetActive(true);
        }
    }
}
