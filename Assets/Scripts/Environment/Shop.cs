using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private bool _areTouching = false;
    private bool isShopOpen = false;

    private TextMesh instructions;

    public int HealthCost = 5;
    public int AtkCost = 8;

    public CanvasGroup shopUi;
    private CanvasGroup menu;

    void Start()
    {
        instructions = GameObject.Find("interactPrompt").GetComponent<TextMesh>();
        if (instructions != null)
        {
            instructions.characterSize = 0;
            menu = gameObject.GetComponentInChildren<CanvasGroup>();
        }
    }
    
    void Update()
    {
        if (_areTouching && InputController.IsInteracting && !isShopOpen)
        {
            if (shopUi != null)
            {
                isShopOpen = true;
                
            }
        }
        else if (isShopOpen && (InputController.IsInteracting || InputController.IsPausing))
            isShopOpen = false;

        DisplayShop();
    }

    private void DisplayShop()
    {
        if (isShopOpen)
        {
            shopUi.alpha = 1;
            menu.interactable = true;
            menu.blocksRaycasts = true;
        }
        else
        {
            shopUi.alpha = 0;
            menu.interactable = false;
            menu.blocksRaycasts = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Character") return;

        _areTouching = true;
        if (instructions != null)
        {
            instructions.characterSize = 1;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Character") return;

        _areTouching = false;
        if (instructions != null)
        {
            instructions.characterSize = 0;
        }

        if (shopUi != null)
        {
            isShopOpen = false;
            shopUi.alpha = 0;
            menu.interactable = false;
            menu.blocksRaycasts = false;
        }
    }

    public void BuyHealth()
    {
        if (PersistenceManager.coins < HealthCost) return;
        Debug.Log("buying");
        PersistenceManager.coins -= HealthCost;
        PersistenceManager.HealthPotionAmount += 1;
    }

    public void BuyAtkBoost()
    {
        if (PersistenceManager.coins < AtkCost) return;
        PersistenceManager.coins -= AtkCost;
        PersistenceManager.AtkBoostAmount += 1;
    }
}
