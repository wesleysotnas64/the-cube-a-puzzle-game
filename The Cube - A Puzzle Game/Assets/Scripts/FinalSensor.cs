using UnityEngine;

public class FinalSensor : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private SceneController sceneController;
    [SerializeField] private AudioSource audioSource;
    void Start()
    {
        InitSetUp();
    }

    public void InitSetUp()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        InitSetUp();
        audioSource.Play();
        player.Decrease();
        sceneController.VerifyEndLevel();
    }
}
