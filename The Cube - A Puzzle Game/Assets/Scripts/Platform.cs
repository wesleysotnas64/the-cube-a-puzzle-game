using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] public Vector2Int matrixPosition;
    [SerializeField] private int interaction;
    [SerializeField] private Vector3 fallOffset;
    [SerializeField] private float fallTime;
    [SerializeField] private float enterTime;
    [SerializeField] private GameObject fallSensorPrefab;
    [SerializeField] private Material simplePlatformMaterial;
    [SerializeField] private SceneController sceneController;
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(EnterAnimEnum());
    }

    public void SetMatrixPosition(int line, int column)
    {
        matrixPosition = new(line, column);
    }

    public Vector2Int GetMatrixPosition()
    {
        return matrixPosition;
    }

    void OnTriggerExit(Collider other)
    {
        Fall();
    }

    public void Fall()
    {
        interaction--;

        if (interaction <= 0)
        {
            StartCoroutine(FallEnum());
        }
        else if (interaction == 1)
        {
            SetToSimple();
        }
    }

    private void SetToSimple()
    {
        GetComponent<MeshRenderer>().material = simplePlatformMaterial;
    }

    IEnumerator FallEnum()
    {
        sceneController.CountPlatformFall();

        Vector3 initial = transform.position;
        Vector3 final = transform.position + fallOffset;

        float elapsed = 0.0f;
        while (elapsed < fallTime)
        {
            yield return null;
            elapsed += Time.deltaTime;
            transform.position = Vector3.Lerp(initial, final, elapsed / fallTime);

        }

        transform.position = final;

        GameObject fallSensorInstance = Instantiate(fallSensorPrefab);
        fallSensorInstance.transform.position = initial;
    }

    IEnumerator EnterAnimEnum()
    {
        Vector3 initial = Vector3.zero;
        Vector3 final = transform.localScale;

        float elapsed = 0.0f;
        while (elapsed < enterTime)
        {
            yield return null;
            elapsed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(initial, final, elapsed / enterTime);

        }
        transform.localScale = final;

        
        audioSource.Play();
    }
}
