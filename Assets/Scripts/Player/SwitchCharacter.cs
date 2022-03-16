using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SwitchCharacter : MonoBehaviour
{
    private int characterIndex = 0;

    private Vector3 currentPosition;
    // Update is called once per frame
    void Update()
    {
        
        if (InputController.IsCharacterChanging && InputController.canCharacterChange)
        {
            currentPosition =  transform.GetChild(characterIndex).gameObject.transform.position;
            InputController.IsCharacterChanging = false;
            transform.GetChild(characterIndex).gameObject.SetActive(false);
            if (characterIndex == 0)
            {
                characterIndex++;
            }
            else
            {
                characterIndex = 0;
            }
            transform.GetChild(characterIndex).gameObject.transform.position = currentPosition;
            transform.GetChild(characterIndex).gameObject.SetActive(true);
        }
    }
}
