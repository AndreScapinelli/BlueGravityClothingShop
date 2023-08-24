using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "outfit", menuName = "outfit/ new outfit addon")]
public class OutfitAddonObject : ScriptableObject
{
    public string objectName;
    public GameBase.OUTFITPART_TYPE partType;
    public string description;
    public int price;
    public Sprite sprite;
    public int healthBonus;
    public int staminaBonus;
}
