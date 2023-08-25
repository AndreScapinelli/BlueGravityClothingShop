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
            sellingObject.gameObject.SetActive(!IsSold);
            Debug.Log("is sold: " + IsSold);
        }
    }

    private void OnValidate()
    {
        if (avaiableAddon.sprite != null)
        {
            sellingObject.sprite = avaiableAddon.sprite;
        }

        gameObject.name = avaiableAddon.objectName;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isSold)
        {
            ShopUI.Instance.OpenUI();
            GamePlayUI.CallOpenShop(collision.gameObject.GetComponent<PlayerCharacter>(),this);
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
