using UnityEngine;

public class FallSensor : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private SceneController sceneController;
    [SerializeField] private AudioSource audioSource;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
        audioSource = GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other)
    {
        audioSource.Play();
        player.Fall();
        sceneController.VerifyEndLevel(true);
    }
}
