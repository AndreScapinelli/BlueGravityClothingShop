using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameBase
{
    public enum OUTFITPART_TYPE
    {
        None = 0,
        Hood = 1,
        Torso = 2,
        Pelvis = 3,
        Boots = 4
    }

    [System.Serializable]
    public class CharacterOutfit
    {
        [Header(" Art Ref: Head, Hood, Eyes ")]
        public SpriteRenderer hoodSprite;
        [SerializeField]
        private OutfitAddonObject Hood;
        public OutfitAddonObject hood
        {
            get { return Hood; }
            set
            {
                if (value.partType == GameBase.OUTFITPART_TYPE.Hood)
                {
                    Hood = value;
                    hoodSprite.sprite = Hood.sprite;
                }
                else
                {
                    Debug.Log($"ALERT: {value.partType.ToString()} ON HOOD");
                }
            }
        }
        [Header(" Art Ref: Torso, Arms ")]
        public SpriteRenderer torsoSprite;
        public SpriteRenderer leftShoulder;
        public SpriteRenderer rightShoulder;

        public SpriteRenderer leftElbow;
        public SpriteRenderer rightElbow;

        public SpriteRenderer leftHand;
        public SpriteRenderer rightHand;
        [SerializeField]
        private OutfitAddonArmor Armour;
        public OutfitAddonArmor armour
        {
            get { return Armour; }
            set
            {
                if (value.partType == GameBase.OUTFITPART_TYPE.Torso)
                {
                    Armour = value;

                    torsoSprite.sprite = Armour.sprite;
                    leftShoulder.sprite = Armour.leftShoulder;
                    rightShoulder.sprite= Armour.rightShoulder;
                    leftElbow.sprite = Armour.leftElbow;
                    rightElbow.sprite= Armour.rightElbow;
                    leftHand.sprite = Armour.leftHand;
                    rightHand.sprite = Armour.rightHand;

                }
                else
                {
                    Debug.Log($"ALERT: {value.partType.ToString()} ON ARMOUR");
                }
            }
        }
        [Header(" Art Ref: Legs, Shoes ")]
        public SpriteRenderer pelvisSprite;
        [SerializeField]
        private OutfitAddonObject Pelvis;
        public OutfitAddonObject pelvis
        {
            get { return Pelvis; }
            set
            {
                if (value.partType == GameBase.OUTFITPART_TYPE.Pelvis)
                {
                    Pelvis = value;
                    pelvisSprite.sprite = Pelvis.sprite;
                }
                else
                {
                    Debug.Log($"ALERT: {value.partType.ToString()} ON PELVIS");
                }
            }
        }
        public void SetupOutfit(OutfitAddonObject newOutfit)
        {
            switch (newOutfit.partType)
            {
                case GameBase.OUTFITPART_TYPE.None:
                    break;
                case GameBase.OUTFITPART_TYPE.Hood:
                    hood = newOutfit;
                    break;
                case GameBase.OUTFITPART_TYPE.Torso:
                    armour = newOutfit as OutfitAddonArmor;
                    break;
                case GameBase.OUTFITPART_TYPE.Pelvis:
                    pelvis = newOutfit;
                    break;
                case GameBase.OUTFITPART_TYPE.Boots:
                    // still no implemented
                    break;
                default:
                    break;
            }
        }
    }
}
