using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GamePlayUI : MonoBehaviour
{
    public Image lifeBar;
    public TMP_Text lifeValue;
    public Image staminaBar;
    public TMP_Text staminaValue;
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
        lifeValue.SetText("{0}/{1}", Mathf.FloorToInt(player.health), Mathf.FloorToInt(player.GetMaxHealth()));
        lifeBar.fillAmount = player.health / player.GetMaxHealth();

        staminaValue.SetText("{0}/{1}", Mathf.FloorToInt(player.stamina), Mathf.FloorToInt(player.GetMaxStamina()));
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

    public delegate void UITeleportNewArea(Transform output, Transform obj);
    public static event UITeleportNewArea onTeleportNewArea;
    public static void CallTeleportToNewArea(Transform output, Transform obj)
    {
        if (onTeleportNewArea != null)
        {
            onTeleportNewArea(output, obj);
        }
    }
}
