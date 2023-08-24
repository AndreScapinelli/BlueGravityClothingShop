using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ShopUI : MonoBehaviour
{
    private static ShopUI _instance;
    public static ShopUI Instance
    {
        get { return _instance; }
    }

    public CanvasGroup canvas;
    public PlayerCharacter character;
    public Shop currentShop;
    [Header("Current Addon")]
    public OutfitAddonObject addon;
    [Header("UI GameObjects")]
    public TMP_Text title;
    public Image torsoSprite, hoodSprite, legSprite;
    public TMP_Text description;
    public TMP_Text healthStatus;
    public TMP_Text staminaStatus;
    public TMP_Text price;
    public Button buyButton;



    private void OnEnable()
    {
        GamePlayUI.onOpenShop += GamePlayUI_onOpenShop;
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void GamePlayUI_onOpenShop(PlayerCharacter player, OutfitAddonObject addon, Shop shop)
    {
        currentShop = shop;
        character = player;

        switch (addon.partType)
        {
            case GameBase.OUTFITPART_TYPE.Hood:
                torsoSprite.sprite = null;
                hoodSprite.sprite = addon.sprite;
                legSprite.sprite = null;

                torsoSprite.gameObject.SetActive(false);
                hoodSprite.gameObject.SetActive(true);
                legSprite.gameObject.SetActive(false);
                break;
            case GameBase.OUTFITPART_TYPE.Torso:
                torsoSprite.sprite = addon.sprite;
                hoodSprite.sprite =null;
                legSprite.sprite = null;

                torsoSprite.gameObject.SetActive(true);
                hoodSprite.gameObject.SetActive(false);
                legSprite.gameObject.SetActive(false);
                break;
            case GameBase.OUTFITPART_TYPE.Pelvis:
                torsoSprite.sprite = null;
                hoodSprite.sprite = null;
                legSprite.sprite = addon.sprite;

                torsoSprite.gameObject.SetActive(false);
                hoodSprite.gameObject.SetActive(false);
                legSprite.gameObject.SetActive(true);
                break;
            case GameBase.OUTFITPART_TYPE.None:
                torsoSprite.sprite = null;
                hoodSprite.sprite = null;
                legSprite.sprite = null;

                torsoSprite.gameObject.SetActive(false);
                hoodSprite.gameObject.SetActive(false);
                legSprite.gameObject.SetActive(false);
                break;
        }

        title.SetText(addon.objectName);

        description.SetText(addon.description);

        healthStatus.SetText("+"+ addon.healthBonus);
        staminaStatus.SetText("+" + addon.staminaBonus);

        price.SetText(addon.price.ToString());

        buyButton.interactable = character.goldCoin >= addon.price;
    }

    public void OpenUI()
    {
        canvas.alpha = 1;
        canvas.interactable = true;
        canvas.blocksRaycasts = true;
    }

    public void CloseUI()
    {
        canvas.alpha = 0;
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
    }
    public void UI_Click_Buy()
    {
        if (character.goldCoin >= addon.price)
        {
            character.goldCoin -= addon.price;
            character.outfit.SetupOutfit(addon);
            currentShop.isSold = true;
            CloseUI();
        }
    }
}
