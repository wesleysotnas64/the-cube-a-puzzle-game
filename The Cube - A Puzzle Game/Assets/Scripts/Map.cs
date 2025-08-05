using System.Collections;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private Platform[,] grid;
    [SerializeField] private int[,] gridReference;
    [SerializeField] private Vector2Int initial;
    [SerializeField] private GameObject fallSensorPrefab;
    [SerializeField] private GameObject simplePlatformPrefab;
    [SerializeField] private GameObject doublePlatformPrefab;
    [SerializeField] private GameObject finalPlatformPrefab;

    [SerializeField] private SceneController sceneController;

    void Start()
    {
        sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
    }

    public void Load(int id)
    {
        switch (id)
        {
            case 1:
                Map_1();
                break;

            case 2:
                Map_2();
                break;

            case 3:
                Map_3();
                break;

            case 4:
                Map_4();
                break;

            case 5:
                Map_5();
                break;

            default:
                break;
        }
    }

    public IEnumerator Render()
    {
        int xOffset = height / 2;
        int zOffset = width / 2;

        for (int line = 0; line < height; line++)
        {
            for (int column = 0; column < width; column++)
            {

                int platformRef = gridReference[line, column];
                switch (platformRef)
                {
                    case 0:
                        GameObject fallSensorInstance = Instantiate(fallSensorPrefab);
                        fallSensorInstance.transform.position = new(line - xOffset, 0, column - zOffset);
                        fallSensorInstance.transform.parent = transform;
                        break;

                    case 1:
                        yield return new WaitForSeconds(0.1f);
                        GameObject simplePlatformInstance = Instantiate(simplePlatformPrefab);
                        simplePlatformInstance.transform.position = new(line - xOffset, -0.06f, column - zOffset);
                        simplePlatformInstance.transform.parent = transform;
                        grid[line, column] = simplePlatformInstance.GetComponent<Platform>();
                        sceneController.AddPlatformCount();
                        break;

                    case 2:
                        yield return new WaitForSeconds(0.1f);
                        GameObject doublePlatformInstance = Instantiate(doublePlatformPrefab);
                        doublePlatformInstance.transform.position = new(line - xOffset, -0.06f, column - zOffset);
                        doublePlatformInstance.transform.parent = transform;
                        grid[line, column] = doublePlatformInstance.GetComponent<Platform>();
                        sceneController.AddPlatformCount();
                        break;

                    case 3:
                        //teleport
                        break;

                    case 4:
                        yield return new WaitForSeconds(0.1f);
                        GameObject finalPlatformInstance = Instantiate(finalPlatformPrefab);
                        finalPlatformInstance.transform.position = new(line - xOffset, -0.06f, column - zOffset);
                        finalPlatformInstance.transform.parent = transform;
                        break;

                    default:
                        break;
                }
            }
        }

        GameObject.Find("Player").transform.position = new(initial.x - xOffset, 0, initial.y - zOffset);
        GameObject.Find("Player").GetComponent<Player>().Grow();
    }

    public void InitGrid(int w, int h)
    {
        width = w;
        height = h;
        grid = new Platform[height, width];
        gridReference = new int[height, width];

        for (int line = 0; line < height; line++)
        {
            for (int column = 0; column < width; column++)
            {
                grid[line, column] = null;
                gridReference[line, column] = 0;
            }
        }
    }

    public void Map_1()
    {
        InitGrid(10, 3);
        initial = new(1, 1);

        for (int j = 1; j < 8; j++)
            gridReference[1, j] = 1;

        gridReference[1, 8] = 4; // Final

        StartCoroutine(Render());
    }

    public void Map_2()
    {
        InitGrid(9, 8);
        initial = new(1, 1);

        gridReference[1, 1] = 1;

        for (int i = 1; i < 7; i++)
        {
            for (int j = 2; j < 8; j++)
                gridReference[i, j] = 1;
        }

        gridReference[4, 4] = 4; // Final

        StartCoroutine(Render());
    }

    public void Map_3()
    {
        InitGrid(7, 7);
        initial = new(2, 1);

        gridReference[2, 1] = 1;
        gridReference[3, 5] = 1;
        
        for (int i = 1; i < 3; i++)
        {
            for (int j = 2; j < 6; j++)
                gridReference[i, j] = 1;
        }

        for (int i = 4; i < 6; i++)
        {
            for (int j = 2; j < 6; j++)
                gridReference[i, j] = 1;
        }

        gridReference[4, 1] = 4; // Final

        StartCoroutine(Render());
    }

    public void Map_4()
    {
        InitGrid(12, 8);
        initial = new(4, 3);

        gridReference[1, 2] = 1;
        gridReference[1, 3] = 1;

        for (int i = 2; i < 6; i++)
        {
            for (int j = 1; j < 6; j++)
                gridReference[i, j] = 1;
        }

        for (int i = 4; i < 7; i++)
        {
            for (int j = 5; j < 10; j++)
                gridReference[i, j] = 1;
        }

        for (int i = 2; i < 6; i++)
        {
            for (int j = 8; j < 11; j++)
                gridReference[i, j] = 1;
        }

        gridReference[6, 7] = 4;

        StartCoroutine(Render());
    }

    public void Map_5()
    {
        InitGrid(6, 9);
        initial = new(1, 2);

        gridReference[1, 2] = 1;

        for (int i = 2; i < 8; i++)
        {
            for (int j = 1; j < 5; j++)
                gridReference[i, j] = 1;
        }

        gridReference[1, 3] = 4; // Final

        StartCoroutine(Render());
    }
    
    

    // public void Map_1()
    // {
    //     InitGrid(9, 9);
    //     initial = new(1, 1);

    //     gridReference[1, 1] = 1;
    //     gridReference[2, 1] = 1;
    //     gridReference[3, 1] = 1;

    //     gridReference[3, 2] = 2;
    //     gridReference[4, 2] = 1;

    //     gridReference[1, 3] = 2;
    //     gridReference[2, 3] = 2;
    //     gridReference[3, 3] = 1;

    //     gridReference[1, 4] = 1;
    //     gridReference[2, 4] = 1;
    //     gridReference[3, 4] = 1;
    //     gridReference[5, 4] = 1;
    //     gridReference[6, 4] = 4;

    //     gridReference[1, 5] = 1;
    //     gridReference[5, 5] = 1;
    //     gridReference[6, 5] = 1;

    //     gridReference[1, 6] = 1;
    //     gridReference[2, 6] = 2;
    //     gridReference[3, 6] = 2;
    //     gridReference[4, 6] = 2;
    //     gridReference[5, 6] = 1;
    //     gridReference[6, 6] = 1;

    //     gridReference[2, 7] = 1;
    //     gridReference[3, 7] = 1;
    //     gridReference[4, 7] = 1;

    //     StartCoroutine(Render());
    // }

}
