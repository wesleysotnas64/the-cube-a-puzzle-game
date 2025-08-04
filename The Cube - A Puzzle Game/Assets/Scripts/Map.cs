using System.Collections;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private Platform[,] grid;
    [SerializeField] private Vector2Int initial;
    [SerializeField] private Vector2Int final;

    [SerializeField] private GameObject fallSensorPrefab;
    [SerializeField] private GameObject simplePlatformPrefab;
    [SerializeField] private GameObject doublePlatformPrefab;

    void Start()
    {
        Render(0);
    }

    public void Render(int mapId)
    {
        switch (mapId)
        {
            case 0:
                StartCoroutine(Map_0());
                break;

            default:
                break;
        }
    }

    public void InitGrid(int w, int h)
    {
        width = w;
        height = h;
        grid = new Platform[height, width];

        for (int line = 0; line < height; line++)
        {
            for (int column = 0; column < width; column++)
            {
                grid[line, column] = null;
            }
        }
    }

    IEnumerator Map_0()
    {
        InitGrid(6, 6);
        initial = new(2, 2);
        final = new(4, 4);

        for (int line = 1; line < height - 1; line++)
        {
            for (int column = 1; column < width - 1; column++)
            {
                yield return new WaitForSeconds(0.5f);
                GameObject platformInstance;
                if (((line + column) % 2) == 0) platformInstance = Instantiate(simplePlatformPrefab);
                else platformInstance = Instantiate(doublePlatformPrefab);

                platformInstance.transform.position = new(line, -0.06f, column);
                platformInstance.transform.parent = transform;
                grid[line, column] = platformInstance.GetComponent<Platform>();

            }
        }

        GameObject.Find("Player").transform.position = new(initial.x, 0, initial.y);

        SetFallSensor();
    }

    private void SetFallSensor()
    {
        for (int line = 0; line < height; line++)
        {
            for (int column = 0; column < width; column++)
            {
                if (grid[line, column] == null)
                {
                    GameObject fallSensorInstance = Instantiate(fallSensorPrefab);
                    fallSensorInstance.transform.position = new(line, 0, column);
                    fallSensorInstance.transform.parent = transform;
                    // grid[line, column] = fallSensorInstance.GetComponent<Platform>();
                }

            }
        }
    }
}
