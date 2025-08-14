using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] private int currentPage;
    [SerializeField] private int maxPage;
    [SerializeField] private int currentLockLevel;
    [SerializeField] private GameObject fadeFXGameObject;
    [SerializeField] private FadeFX fadeFX;
    [SerializeField] private GameObject buttonPreviousPage;
    [SerializeField] private GameObject buttonNextPage;
    [SerializeField] private List<ButtonLevel> buttons;

    private readonly int levelsPerPage = 12;
    private readonly int totalLevels = Map.maxLevel;

    void Start()
    {
        fadeFXGameObject.SetActive(true);
        fadeFX = fadeFXGameObject.GetComponent<FadeFX>();
        currentPage = 1;
        currentLockLevel = PlayerPrefs.GetInt("CurrentLockLevel", 1);

        maxPage = Mathf.CeilToInt(totalLevels / (float)levelsPerPage);

        StartCoroutine(InitEnum());
    }

    IEnumerator InitEnum()
    {
        SetAllButtonsLevels();
        fadeFX.TriggerFadeIn();
        yield return new WaitForSeconds(2.0f);

        fadeFXGameObject.SetActive(false);
    }

    private void SetAllButtonsLevels()
    {
        int startLevelIndex = (currentPage - 1) * levelsPerPage + 1;

        for (int i = 0; i < buttons.Count; i++)
        {
            int level = startLevelIndex + i;

            if (level <= totalLevels)
            {
                // buttons[i].SetButton(
                //     level,
                //     false
                // );
                buttons[i].SetButton(
                    level,
                    !(level <= currentLockLevel)
                );
                buttons[i].Enable();
            }
            else
            {
                buttons[i].Disable();
            }
        }

        if (currentPage > 1) buttonPreviousPage.SetActive(true);
        else buttonPreviousPage.SetActive(false);

        if (currentPage < maxPage) buttonNextPage.SetActive(true);
        else buttonNextPage.SetActive(false);

    }

    public void NextPage()
    {
        if (currentPage < maxPage)
        {
            currentPage++;
            SetAllButtonsLevels();
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 1)
        {
            currentPage--;
            SetAllButtonsLevels();
        }
    }

    public void GoToLevel(int levelId)
    {
        StartCoroutine(GoToLevelEnum(levelId));
    }

    private IEnumerator GoToLevelEnum(int levelId)
    {
        fadeFXGameObject.SetActive(true);
        fadeFX.TriggerFadeOut();
        yield return new WaitForSeconds(2.0f);

        PlayerPrefs.SetInt("CurrentLevel", levelId);
        SceneManager.LoadScene("Gameplay");
    }
}
