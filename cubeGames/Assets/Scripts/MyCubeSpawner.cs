using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCubeSpawner : MonoBehaviour
{
    // Singleton class
    public static MyCubeSpawner Instance;
    Queue<myCube> cubesQueue = new Queue<myCube>();
    [SerializeField] private int cubesQueueCapacity = 20;
    [SerializeField] private bool autoQueueGrow = true;

    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Color[] cubeColors;

    [HideInInspector] public int maxCubeNumber; // in our case it's 4096 2^12

    private int maxPower = 12;

    private Vector3 defaultSpawnPosition;
    private void Awake()
    {
        Instance = this; 
        defaultSpawnPosition =  transform.position;
        maxCubeNumber = (int)Mathf.Pow(2, maxPower);
        InitializeCubesQueue();


    }
    private void InitializeCubesQueue()
    {
        for (int i = 0; i < cubesQueueCapacity; i++)
        {
            AddCubeToQueue();
        }
    }

    private void AddCubeToQueue()
    {
      myCube cube = Instantiate(cubePrefab, defaultSpawnPosition, Quaternion.identity, transform).GetComponent<myCube>();
        cube.gameObject.SetActive(false);
        cube.IsMainCube = false;
        cubesQueue.Enqueue(cube);
    }
    public myCube Spawn(int number, Vector3 position)
    {
        if (cubesQueue.Count == 0)
        {
            if (autoQueueGrow)
            {
                cubesQueueCapacity++;
                AddCubeToQueue();
            } else
            {
                Debug.LogError("[Cubes Queue] : no more cubes available in th pool");
                return null;
            }
        }
        myCube cube = cubesQueue.Dequeue();
        cube.transform.position = position;
        cube.SetNumber(number);
        cube.SetColor(GetColor(number));
        cube.gameObject.SetActive(true);

        return cube;
    }
    public myCube SpawnRandom()
    {
        return Spawn(GenerateRandomNumber(), defaultSpawnPosition);
    }
    public void DestroyCube(myCube cube)
    {
        cube.CubeRgidbody.velocity = Vector3.zero;
        cube.CubeRgidbody.angularVelocity = Vector3.zero;
        cube.transform.rotation = Quaternion.identity;
        cube.IsMainCube = false;
        cube.gameObject.SetActive(false);
        cubesQueue.Enqueue(cube);
    }
    public int GenerateRandomNumber()
    {
        return (int)Mathf.Pow(2, Random.Range(1, 6));
    }
    private Color GetColor(int number)
    {
        return cubeColors[(int)(Mathf.Log (number) / Mathf.Log(2)) - 1];
    }
}
