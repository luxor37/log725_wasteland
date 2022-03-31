using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private bool _areTouching = false;

    public TextMesh instructions;

    public int HealthCost = 5;
    public int AtkCost = 8;

    public CanvasGroup shopUi;
    private CanvasGroup menu;

    void Start()
    {
        if (instructions != null)
        {
            instructions.characterSize = 0;
            menu = gameObject.GetComponentInChildren<CanvasGroup>();
        }
    }
    
    void Update()
    {
        if (!_areTouching || InputController.VerticalDirection != VerticalDirection.Up) return;

        if (shopUi != null)
        {
            shopUi.alpha = 1;
            menu.interactable = true;
            menu.blocksRaycasts = true;
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
