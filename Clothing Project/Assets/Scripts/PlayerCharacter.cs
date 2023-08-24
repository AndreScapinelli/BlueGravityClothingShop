using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    public override int health 
    { 
        get => base.health; 
        set
        {
            GamePlayUI.CallPlayerStatus(this);

            base.health = value;
        }
    }
    public override float stamina 
    { 
        get => base.stamina; 
        set
        {
            GamePlayUI.CallPlayerStatus(this);

            base.stamina = value;
        }
    }
    public override int goldCoin 
    { 
        get => base.goldCoin;
        set 
        {
            GamePlayUI.CallPlayerStatus(this);

            base.goldCoin = value;
        }
    }
}
