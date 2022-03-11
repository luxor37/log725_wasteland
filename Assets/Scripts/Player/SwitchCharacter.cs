using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SwitchCharacter : MonoBehaviour
{
    public int characterIndex = 0;

    private Vector3 currentPosition;
    // Update is called once per frame
    void Update()
    {
        
        if (InputController.characterChange)
        {
            currentPosition =  transform.GetChild(characterIndex).gameObject.transform.position;
            InputController.characterChange = false;
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
