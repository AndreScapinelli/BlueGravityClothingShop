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
            base.health = value;

            GamePlayUI.CallPlayerStatus(this);
        }
    }
    public override float stamina 
    { 
        get => base.stamina; 
        set
        {
            base.stamina = value;

            GamePlayUI.CallPlayerStatus(this);
        }
    }
    public override int goldCoin 
    { 
        get => base.goldCoin;
        set 
        {
            base.goldCoin = value;

            GamePlayUI.CallPlayerStatus(this);
        }
    }
    private void Awake()
    {
        GamePlayUI.CallPlayerStatus(this);
    }
}
