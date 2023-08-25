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
    private void GamePlayUI_onOpenShop(PlayerCharacter player, Shop shop)
    {
        currentShop = shop;
        character = player;

        switch (currentShop.avaiableAddon.partType)
        {
            case GameBase.OUTFITPART_TYPE.Hood:
                torsoSprite.sprite = null;
                hoodSprite.sprite = currentShop.avaiableAddon.sprite;
                legSprite.sprite = null;

                torsoSprite.gameObject.SetActive(false);
                hoodSprite.gameObject.SetActive(true);
                legSprite.gameObject.SetActive(false);
                break;
            case GameBase.OUTFITPART_TYPE.Torso:
                torsoSprite.sprite = currentShop.avaiableAddon.sprite;
                hoodSprite.sprite =null;
                legSprite.sprite = null;

                torsoSprite.gameObject.SetActive(true);
                hoodSprite.gameObject.SetActive(false);
                legSprite.gameObject.SetActive(false);
                break;
            case GameBase.OUTFITPART_TYPE.Pelvis:
                torsoSprite.sprite = null;
                hoodSprite.sprite = null;
                legSprite.sprite = currentShop.avaiableAddon.sprite;

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

        title.SetText(currentShop.avaiableAddon.objectName);

        description.SetText(currentShop.avaiableAddon.description);

        healthStatus.SetText("+"+ currentShop.avaiableAddon.healthBonus);
        staminaStatus.SetText("+" + currentShop.avaiableAddon.staminaBonus);

        price.SetText(currentShop.avaiableAddon.price.ToString());

        buyButton.interactable = character.goldCoin >= currentShop.avaiableAddon.price;
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
        if (character.goldCoin >= currentShop.avaiableAddon.price)
        {
            character.goldCoin -= currentShop.avaiableAddon.price;
            character.outfit.SetupOutfit(currentShop.avaiableAddon);
            currentShop.isSold = true;
            CloseUI();
        }
    }
}
