using System.Collections;
using TMPro;
using UnityEngine;

public class SwipLevelFX : MonoBehaviour
{
    [SerializeField] private int currentLevel;
    [SerializeField] private float fadeTime;
    [SerializeField] private float presentationTime;
    [SerializeField] private TMP_Text textCurrentLevel;
    [SerializeField] private TMP_Text textLastLevel;
    [SerializeField] private TMP_Text textNextLevel;
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public void TriggerSwipFX()
    {
        StopAllCoroutines();
        StartCoroutine(SwipLevelFXEnum());
    }

    private IEnumerator SwipLevelFXEnum()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");

        int lvLast = currentLevel - 2;
        int lvCurrent = currentLevel - 1;
        int lvNext = currentLevel;

        textLastLevel.text = lvLast > 0 ? lvLast.ToString() : "";
        textCurrentLevel.text = lvCurrent > 0 ? lvCurrent.ToString() : "";
        textNextLevel.text = lvNext.ToString();

        yield return StartCoroutine(FadeTexts(0f, 1f)); // Fade In

        yield return new WaitForSeconds(presentationTime/2);

        // Simula avanço
        lvLast++;
        lvCurrent++;
        lvNext++;

        textLastLevel.text = lvLast > 0 ? lvLast.ToString() : "";
        textCurrentLevel.text = lvCurrent > 0 ? lvCurrent.ToString() : "";
        textNextLevel.text = lvNext.ToString();

        if (audioSource != null) audioSource.Play();

        yield return new WaitForSeconds(presentationTime);

        yield return StartCoroutine(FadeTexts(1f, 0f)); // Fade Out
    }

    private IEnumerator FadeTexts(float fromAlpha, float toAlpha)
    {
        float elapsed = 0f;

        Color colorLast = textLastLevel.color;
        Color colorCurrent = textCurrentLevel.color;
        Color colorNext = textNextLevel.color;

        while (elapsed < fadeTime)
        {
            float t = elapsed / fadeTime;
            float alpha = Mathf.Lerp(fromAlpha, toAlpha, t);

            colorLast.a = alpha;
            colorCurrent.a = alpha;
            colorNext.a = alpha;

            textLastLevel.color = colorLast;
            textCurrentLevel.color = colorCurrent;
            textNextLevel.color = colorNext;

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Garantir valor final
        colorLast.a = toAlpha;
        colorCurrent.a = toAlpha;
        colorNext.a = toAlpha;

        textLastLevel.color = colorLast;
        textCurrentLevel.color = colorCurrent;
        textNextLevel.color = colorNext;
    }

}
