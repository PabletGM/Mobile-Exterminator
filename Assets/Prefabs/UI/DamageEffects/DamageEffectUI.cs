using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DamageEffectUI : DamageVisualizer
{


    [SerializeField] private Image bloodyImage;

    [Header("Parameters")]
    [SerializeField] private float durationOfEffectFadeIn = 0.5f; // Duración del FadeIn
    [SerializeField] private float durationOfEffectFadeOut = 0.5f; // Duración del FadeOut
    [SerializeField] private float opacity = 0.5f; // Opacidad máxima

    private Coroutine fadeRoutine; // Referencia a la corrutina activa

    public void SetDamageUIImageEffect()
    {
        // Si ya hay una corrutina ejecutándose, la detenemos para evitar conflictos
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
        }

        // Iniciamos una nueva corrutina para el efecto
        fadeRoutine = StartCoroutine(FadeInOutCoroutine());
    }

    private IEnumerator FadeInOutCoroutine()
    {
        // *** FADE IN ***
        yield return FadeTo(opacity, durationOfEffectFadeIn);

        // *** FADE OUT ***
        yield return FadeTo(0f, durationOfEffectFadeOut);

        // Limpia la referencia a la corrutina al finalizar
        fadeRoutine = null;
    }

    private IEnumerator FadeTo(float targetAlpha, float duration)
    {
        // Obtenemos el color inicial de la imagen
        Color startColor = bloodyImage.color;
        float startAlpha = startColor.a;

        // Tiempo transcurrido
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // Interpolamos la opacidad (alpha)
            Color currentColor = bloodyImage.color;
            currentColor.a = Mathf.Lerp(startAlpha, targetAlpha, t);
            bloodyImage.color = currentColor;

            yield return null; // Esperamos al siguiente frame
        }

        // Aseguramos el valor final exacto
        Color finalColor = bloodyImage.color;
        finalColor.a = targetAlpha;
        bloodyImage.color = finalColor;
    }

    protected override void TookDamage(float health, float amount, float maxHealth, GameObject instigator)
    {
        // Activamos el efecto al recibir daño
        SetDamageUIImageEffect();
    }
}