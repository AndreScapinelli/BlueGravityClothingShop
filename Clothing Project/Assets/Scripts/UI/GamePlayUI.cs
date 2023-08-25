using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GamePlayUI : MonoBehaviour
{
    public Image lifeBar;
    public Image staminaBar;
    public TMP_Text coinCounter;

    private void OnEnable()
    {
        onPlayerStatusChanged += SetupPlayerUI;
    }

    private void OnDisable()
    {
        onPlayerStatusChanged -= SetupPlayerUI;
    }

    private void SetupPlayerUI(PlayerCharacter player)
    {
        lifeBar.fillAmount = player.health / player.GetMaxHealth();
        staminaBar.fillAmount = player.stamina / player.GetMaxStamina();
        coinCounter.SetText(player.goldCoin.ToString());
    }

    // GAME PLAY UI EVENTS
    public delegate void UIPlayerStatusHandler (PlayerCharacter player);
    public static event UIPlayerStatusHandler onPlayerStatusChanged;
    public static void CallPlayerStatus(PlayerCharacter player)
    {
        if (onPlayerStatusChanged != null)
        {
            onPlayerStatusChanged(player);
        }
    }

    public delegate void UIPlayerOpenShop(PlayerCharacter player, Shop shop);
    public static event UIPlayerOpenShop onOpenShop;
    public static void CallOpenShop(PlayerCharacter player,  Shop shop)
    {
        if (onOpenShop != null) 
        {
            onOpenShop(player, shop);
        }
    }
}
