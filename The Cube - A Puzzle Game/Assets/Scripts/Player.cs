using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float rollTime;
    [SerializeField] private float growTime;
    [SerializeField] private float decreaseTime;
    [SerializeField] private float fallTime;

    [Header("States")]
    [SerializeField] private bool rolling;
    [SerializeField] private bool growing;
    [SerializeField] private bool decreasing;
    [SerializeField] private bool fell;
    [SerializeField] private bool collapsed;

    [Header("Aux Rotate Attrib")]
    [SerializeField] private Vector3 targetRotation;
    [SerializeField] private Vector3 displacement;

    [Header("Structure Player")]
    [SerializeField] private GameObject player;
    [SerializeField] private Transform cubeTransform;
    [SerializeField] private List<Transform> pivot; //[0] grow | [1] forward | [2] back | [3] left | [4] right 

    void Start()
    {
        fell = false;
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.G)) Grow();
        // if (Input.GetKeyDown(KeyCode.V)) Decrease();

        if (fell) return;

        if (Input.GetKeyDown(KeyCode.R)) RollForward();
        if (Input.GetKeyDown(KeyCode.D)) RollBack();
        if (Input.GetKeyDown(KeyCode.E)) RollLeft();
        if (Input.GetKeyDown(KeyCode.F)) RollRight();
    }

    public void Fall()
    {
        StartCoroutine(FallEnum());
    }

    public void Grow()
    {
        if (InAnimation()) return;
        StartCoroutine(GrowAnimEnum());
    }

    public void Decrease()
    {
        if (InAnimation()) return;
        StartCoroutine(DecreaseAnimEnum());
    }

    public void RollForward()
    {
        if (InAnimation() || collapsed) return;
        targetRotation = new(90.0f, 0.0f, 0.0f);
        displacement = new(0.0f, 0.0f, 1.0f);
        StartCoroutine(RollAnimEnum(1));
    }

    public void RollBack()
    {
        if (InAnimation() || collapsed) return;
        targetRotation = new(-90.0f, 0.0f, 0.0f);
        displacement = new(0.0f, 0.0f, -1.0f);
        StartCoroutine(RollAnimEnum(2));
    }

    public void RollLeft()
    {
        if (InAnimation() || collapsed) return;
        targetRotation = new(0.0f, 0.0f, 90.0f);
        displacement = new(-1.0f, 0.0f, 0.0f);
        StartCoroutine(RollAnimEnum(3));
    }

    public void RollRight()
    {
        if (InAnimation() || collapsed) return;
        targetRotation = new(0.0f, 0.0f, -90.0f);
        displacement = new(1.0f, 0.0f, 0.0f);
        StartCoroutine(RollAnimEnum(4));
    }

    private bool InAnimation()
    {
        return growing || decreasing || rolling;
    }

    private IEnumerator RollAnimEnum(int pivotIndex)
    {
        rolling = true;

        Vector3 cubeInitialPosition = cubeTransform.position;
        cubeTransform.parent = pivot[pivotIndex];

        Quaternion initialRotation = pivot[pivotIndex].rotation;
        Quaternion finalRotation = Quaternion.Euler(targetRotation);

        float elapsed = 0.0f;

        while (elapsed < rollTime)
        {
            pivot[pivotIndex].rotation = Quaternion.Lerp(initialRotation, finalRotation, elapsed / rollTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        pivot[pivotIndex].rotation = finalRotation;

        cubeTransform.parent = pivot[0];
        cubeTransform.position = cubeInitialPosition + displacement;
        pivot[pivotIndex].rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

        for (int i = 1; i < 5; i++)
        {
            pivot[i].position += displacement;
        }

        rolling = false;
    }

    private IEnumerator GrowAnimEnum()
    {
        growing = true;

        Vector3 initialScale = new(1.0f, 0.0f, 1.0f);
        Vector3 finalScale = new(1.0f, 1.0f, 1.0f);

        pivot[0].localScale = initialScale;

        float elapsedTime = 0f;

        while (elapsedTime < growTime)
        {
            pivot[0].localScale = Vector3.Lerp(initialScale, finalScale, elapsedTime / growTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        pivot[0].localScale = finalScale;

        growing = false;
        collapsed = false;
    }

    private IEnumerator DecreaseAnimEnum()
    {
        decreasing = true;

        Vector3 initialScale = new(1.0f, 1.0f, 1.0f);
        Vector3 finalScale = new(1.0f, 0.0f, 1.0f);

        pivot[0].localScale = initialScale;

        float elapsedTime = 0f;

        while (elapsedTime < decreaseTime)
        {
            pivot[0].localScale = Vector3.Lerp(initialScale, finalScale, elapsedTime / decreaseTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        pivot[0].localScale = finalScale;

        decreasing = false;
        collapsed = true;
    }

    IEnumerator FallEnum()
    {
        fell = true;
        yield return new WaitForSeconds(0.2f);
        Vector3 fallOffset = new(0, -20, 0);
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
