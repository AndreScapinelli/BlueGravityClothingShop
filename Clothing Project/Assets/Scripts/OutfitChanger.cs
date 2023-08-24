using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OutfitChanger : MonoBehaviour
{
    [Header("Sprite To Be Changed")]
    public Character character;
    [Header("Avaiable Outfit Options")]
    public List<OutfitAddonObject> addons;
    [Header("EDITOR")]
    public bool updateOutfit = false;
    public int addonId = 0;

    private void OnValidate()
    {
        if (updateOutfit)
        {
            updateOutfit = false;

            character.outfit.SetupOutfit(addons[addonId]);
        }
    }
}
