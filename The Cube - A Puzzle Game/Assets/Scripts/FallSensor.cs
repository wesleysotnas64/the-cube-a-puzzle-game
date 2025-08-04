using UnityEngine;

public class FallSensor : MonoBehaviour
{
    [SerializeField] private Player player;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    void OnTriggerEnter(Collider other)
    {
        player.Fall();
    }
}
