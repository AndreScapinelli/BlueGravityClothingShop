using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public SpriteRenderer sellingObject;
    public OutfitAddonObject avaiableAddon;
    private bool IsSold;
    public bool isSold
    {
        get { return IsSold; }
        set 
        {
            IsSold = value;

            if(!value) sellingObject.gameObject.SetActive(false);
            else sellingObject.gameObject.SetActive(true);
        }
    }

    private void OnValidate()
    {
        if (avaiableAddon.sprite != null)
        {
            sellingObject.sprite = avaiableAddon.sprite;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isSold)
        {
            ShopUI.Instance.OpenUI();
            GamePlayUI.CallOpenShop(collision.gameObject.GetComponent<PlayerCharacter>(), avaiableAddon,this);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isSold)
        {
            ShopUI.Instance.CloseUI();
        }
    }
}
