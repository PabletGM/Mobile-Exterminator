using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DamageEffectUI : DamageVisualizer
{

    [SerializeField] private Image bloodyImage;
    [SerializeField] private Sprite bloodyImageLittle;
    [SerializeField] private Sprite bloodyImageMedium;
    [SerializeField] private Sprite bloodyImageLarge;
    [SerializeField] private PlayerHealthBar playerHealthBar;

    [Header("Parameters")]
    [SerializeField] private float durationOfEffectFadeIn = 0.5f; // Duración del FadeIn
    [SerializeField] private float durationOfEffectFadeOut = 0.5f; // Duración del FadeOut
    [SerializeField] private float opacity = 0.5f; // Opacidad máxima

    private Coroutine fadeRoutine; // Referencia a la corrutina activa

    public void SetDamageUIImageEffect()
    {
        //If there is any coroutine, we stop it
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
        }

        //Start a new coroutine for the effect
        fadeRoutine = StartCoroutine(FadeInOutCoroutine());
    }

    private Sprite GetBloodySprite(float damage)
    {
        //switch with conditions
        return damage switch
        {
            < 0.3f => bloodyImageMedium,
            < 0.7f => bloodyImageLarge,
            <= 1f => bloodyImageLittle,
            _ => null, 
        };
    }

    private void SetBloodyImageSprite(Sprite sprite)
    {
        bloodyImage.sprite = sprite;
    }

    private IEnumerator FadeInOutCoroutine()
    {
        //choose the image depending of the damage
        Sprite bloodyImageSprite = GetBloodySprite(playerHealthBar.ActualLife);

        if(bloodyImage)
        {
            //change the sprite
            SetBloodyImageSprite(bloodyImageSprite);
            // *** FADE IN ***
            yield return FadeTo(opacity, durationOfEffectFadeIn);

            // *** FADE OUT ***
            yield return FadeTo(0f, durationOfEffectFadeOut);

            // Limpia la referencia a la corrutina al finalizar
            fadeRoutine = null;
        } 
    }

    private IEnumerator FadeTo(float targetAlpha, float duration)
    {
        //obtain the color of the initial image
        Color startColor = bloodyImage.color;
        float startAlpha = startColor.a;

        //time passed
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            //interpolate the opacity
            Color currentColor = bloodyImage.color;
            currentColor.a = Mathf.Lerp(startAlpha, targetAlpha, t);
            bloodyImage.color = currentColor;

            yield return null; //wait for next frame
        }

        //we make sure the final value
        Color finalColor = bloodyImage.color;
        finalColor.a = targetAlpha;
        bloodyImage.color = finalColor;
    }

    protected override void TookDamage(float health, float amount, float maxHealth, GameObject instigator)
    {
        //activate the damage effect
        SetDamageUIImageEffect();
    }
}