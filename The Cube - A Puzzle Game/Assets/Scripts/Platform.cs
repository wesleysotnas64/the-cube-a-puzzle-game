using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private int interaction;
    [SerializeField] private Vector3 fallOffset;
    [SerializeField] private float fallTime;

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
        interaction--;

        if (interaction <= 0)
        {
            StartCoroutine(FallEnum());
        }
        else if (interaction == 1)
        {
            // Set platform to simple
        }
        else if (interaction == 2)
        {
            // Set platfomr to double
        }
    }

    IEnumerator FallEnum()
    {
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
    }
}
