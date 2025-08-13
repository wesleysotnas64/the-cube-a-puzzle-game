using System.Collections;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Teleport goTo;
    [SerializeField] private bool isActiveToTeleport;
    [SerializeField] private GameObject playerPrefab;

    void Start()
    {
        isActiveToTeleport = true;
    }

    public void SetIsActiveToTeleport(bool isActive)
    {
        isActiveToTeleport = isActive;
    }

    public void SetGoToPlatform(Teleport platformGoTo)
    {
        goTo = platformGoTo;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isActiveToTeleport) return;
        goTo.SetIsActiveToTeleport(false);

        //Antes tem que salvar o player atual em um ponteiro
        Player currentPlayer = GameObject.FindWithTag("Player").GetComponent<Player>();
        currentPlayer.Decrease();

        GameObject newPlayer = Instantiate(playerPrefab);
        newPlayer.GetComponent<Player>().Grow();
        newPlayer.transform.position = new Vector3(
            goTo.transform.position.x,
            0,
            goTo.transform.position.z
        );

        StartCoroutine(MoveOutMap(currentPlayer));

    }

    IEnumerator MoveOutMap(Player currentPlayer)
    {
        yield return new WaitForSeconds(0.5f);
        currentPlayer.transform.position = new Vector3(100, 0, 100);
        Vector2Int currentPosition = GetComponent<Platform>().GetMatrixPosition();
        GameObject.Find("Map").GetComponent<Map>().FallPlatform(currentPosition.x, currentPosition.y);
        Destroy(currentPlayer.gameObject);

    }
}
