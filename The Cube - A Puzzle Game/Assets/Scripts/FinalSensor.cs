using UnityEngine;

public class FinalSensor : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private SceneController sceneController;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
    }
    void OnTriggerEnter(Collider other)
    {
        player.Decrease();
        sceneController.VerifyEndLevel();
    }
}
