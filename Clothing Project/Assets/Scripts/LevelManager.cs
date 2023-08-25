using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Image UiTeleportAnimation;
    private void OnEnable()
    {
        GamePlayUI.onTeleportNewArea += GamePlayUI_onTeleportNewArea;
    }
    private void OnDisable()
    {
        GamePlayUI.onTeleportNewArea -= GamePlayUI_onTeleportNewArea;
    }
    private IEnumerator FadeCoroutine(Transform output, Transform obj)
    {
        float currentAlpha = 0f;

        while (currentAlpha < 0.9)
        {
            currentAlpha += Time.deltaTime;
            UiTeleportAnimation.fillAmount = currentAlpha;
            yield return null;
        }

        obj.transform.position = output.position;

        while (currentAlpha > 0f)
        {
            currentAlpha -= Time.deltaTime;
            UiTeleportAnimation.fillAmount = currentAlpha;
            yield return null;
        }
    }

    private void GamePlayUI_onTeleportNewArea(Transform output, Transform obj)
    {
        StartCoroutine(FadeCoroutine(output,obj));
    }
}
