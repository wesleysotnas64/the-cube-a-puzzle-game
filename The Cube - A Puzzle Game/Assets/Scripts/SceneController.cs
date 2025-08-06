using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private int currentPlatformCount;
    [SerializeField] private int totalPlatformCount;
    [SerializeField] private int currentLevel;
    private readonly int maxLevel = 5;

    [SerializeField] private TMP_Text textLevel;
    [SerializeField] private TMP_Text textPlatform;

    [SerializeField] private Map map;
    [SerializeField] private FadeFX fadeFX;
    [SerializeField] private SwipLevelFX swipLevelFX;

    void Start()
    {
        fadeFX = GameObject.Find("FadeFX_Image").GetComponent<FadeFX>();
        swipLevelFX = GameObject.Find("TextLevelArea").GetComponent<SwipLevelFX>();
        StartCoroutine(InitLevelEnum());
    }

    private void UpdatePlatformCountUI()
    {
        textPlatform.text = $"{currentPlatformCount}/{totalPlatformCount}";
    }

    private void UpdateLevelCountUI()
    {
        textLevel.text = $"Level {currentLevel}/{maxLevel}";
    }

    private IEnumerator InitLevelEnum()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        UpdateLevelCountUI();
        
        //Instantiate FadeIn
        fadeFX.TriggerFadeIn();
        yield return new WaitForSeconds(1.0f);

        map = GameObject.Find("Map").GetComponent<Map>();

        currentPlatformCount = 0;
        totalPlatformCount = 0;

        map.Load(currentLevel);

    }

    private IEnumerator FinishLevelEnum()
    {
        //Instantiate FadeOut
        fadeFX.TriggerFadeOut();
        yield return new WaitForSeconds(1.0f);

        //Instantiate swip level effect
        swipLevelFX.TriggerSwipFX();
        yield return new WaitForSeconds(1.5f);

        ReloadCurrentScene();
    }

    public void AddPlatformCount()
    {
        totalPlatformCount++;
        UpdatePlatformCountUI();
    }

    public void CountPlatformFall()
    {
        currentPlatformCount++;
        UpdatePlatformCountUI();
    }

    public void VerifyEndLevel(bool isFallEvent = false)
    {
        if (isFallEvent) // Avoid completing the level by falling and knocking down all the platforms.
        {
            StartCoroutine(FinishLevelEnum());
            return;
        }
        else if (currentPlatformCount == totalPlatformCount)
        {
            currentLevel++;
            if (currentLevel > maxLevel) currentLevel = 1;
            PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        }

        StartCoroutine(FinishLevelEnum());
    }

    private void ReloadCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

}
