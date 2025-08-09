using TMPro;
using UnityEngine;

public class ButtonLevel : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int level;
    [SerializeField] private bool isLock;


    [Header("Game Object")]
    [SerializeField] private GameObject thisButtonGameObject;
    [SerializeField] private GameObject lockLevelGameObject;
    [SerializeField] private TMP_Text textLevel;

    public void SetButton(int lv, bool locked)
    {
        level = lv;
        isLock = locked;

        textLevel.text = $"{level}";
        lockLevelGameObject.SetActive(isLock);
    }

    public void Disable()
    {
        thisButtonGameObject.SetActive(false);
    }
    public void Enable()
    {
        thisButtonGameObject.SetActive(true);
    }

    public void OnClickLevelButton()
    {
        if (!isLock)
        {
            GameObject.Find("LevelController").GetComponent<LevelController>().GoToLevel(level);
            // Call LevelController to redirect to a new scene and block anothers buttons
        }
    }

}
