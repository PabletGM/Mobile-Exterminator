using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] CanvasGroup GameplayControl;
    [SerializeField] CanvasGroup GameplayMenu;

    [Header("DamageEffectUI")]
    [SerializeField] DamageEffectUI damageEffect;

    public void SetGameplayControlEnabled(bool enabled)
    {
        SetCanvasGroupEnabled(GameplayControl, enabled);
    }

    public void SetGameplayMenuEnabled(bool enabled)
    {
        SetCanvasGroupEnabled(GameplayMenu, enabled);
    }



    private void SetCanvasGroupEnabled(CanvasGroup grp,bool enabled)
    {
        grp.interactable = enabled;
        grp.blocksRaycasts = enabled;
    }


    //===================================================================================================================

    public void SetDamageEffect()
    {
        damageEffect.SetDamageUIImageEffect();
    }
}
