using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public SpriteRenderer sellingObject;
    public OutfitAddonObject avaiableAddon;
    public bool isSold = false;

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
            Debug.Log("player collided with the box");

            //collision.gameObject.GetOrAddComponent<Character>().outfit.SetupOutfit(avaiableAddon);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isSold)
        {
            // Hide the selling UI or perform any other action here
        }
    }
}
