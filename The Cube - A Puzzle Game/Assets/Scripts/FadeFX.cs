using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeFX : MonoBehaviour
{

    [SerializeField] private float fadeTime;
    [SerializeField] private Image image;

    void Awake()
    {
        if (image == null)
            image = GetComponent<Image>();
    }

    private IEnumerator FadeInFX()
    {
        float elapsed = 0f;

        Color color = image.color;
        color.a = 1.0f;
        image.color = color;

        while (elapsed < fadeTime)
        {
            color.a = Mathf.Lerp(1f, 0f, elapsed / fadeTime);
            image.color = color;

            elapsed += Time.deltaTime;
            yield return null;
        }

        color.a = 0f;
        image.color = color;
    }

    private IEnumerator FadeOutFX()
    {
        float elapsed = 0f;

        Color color = image.color;
        color.a = 0f;
        image.color = color;

        while (elapsed < fadeTime)
        {
            color.a = Mathf.Lerp(0f, 1f, elapsed / fadeTime);
            image.color = color;

            elapsed += Time.deltaTime;
            yield return null;
        }

        color.a = 1f;
        image.color = color;
    }

    public void TriggerFadeIn()
    {
        StopAllCoroutines();
        StartCoroutine(FadeInFX());
    }

    public void TriggerFadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutFX());
    }
}
