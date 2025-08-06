using UnityEngine;

public class LoopMusicBackground : MonoBehaviour
{
    public static LoopMusicBackground instance;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
