using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private int currentPlatformCount;
    [SerializeField] private int totalPlatformCount;
    [SerializeField] private int currentLevel;
    private readonly int maxLevel = 5;

    [SerializeField] private Map map;

    void Start()
    {
        StartCoroutine(InitLevelEnum());
    }

    private IEnumerator InitLevelEnum()
    {
        // PlayerPrefs.SetInt("CurrentLevel", 1);
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");

        //Instantiate FadeIn
        yield return new WaitForSeconds(1.0f);

        map = GameObject.Find("Map").GetComponent<Map>();

        currentPlatformCount = 0;
        totalPlatformCount = 0;

        map.Load(currentLevel);

    }

    private IEnumerator FinishLevelEnum()
    {
        //Instantiate FadeOut
        yield return new WaitForSeconds(1.0f);

        //Instantiate swip level effect
        yield return new WaitForSeconds(1.0f);

        ReloadCurrentScene();
    }

    public void AddPlatformCount()
    {
        totalPlatformCount++;
    }

    public void CountPlatformFall()
    {
        currentPlatformCount++;
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
