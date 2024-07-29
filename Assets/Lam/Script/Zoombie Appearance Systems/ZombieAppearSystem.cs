using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAppearSystem : MonoBehaviour
{
    [SerializeField] private List<GameObject> _prefabZombies = new List<GameObject>();
    [SerializeField] private string filePath = "Assets/Lam/zombieSpawnDataSouth.txt";
    // [SerializeField] private int level = 1; 
    [SerializeField] private float xMin = 1; 
    [SerializeField] private float xMax = 1; 
    [SerializeField] private float zMin = 1; 
    [SerializeField] private float zMax = 1; 
    List<ZombieData> zombieDataList;

    private void Start()
    {

        // SpawnZombies(zombieDataList);
    }

     public void SpawnZombies(int level)
    {
        ZombieFileReader fileReader = new ZombieFileReader(filePath);
        zombieDataList = fileReader.GetZombieDataForLevel(level);
        StartCoroutine(SpawnZombiesCoroutine());
    }

    private IEnumerator SpawnZombiesCoroutine()
    {
        foreach (var zombieData in zombieDataList)
        {
            for (int i = 0; i < zombieData.qty; i++)
            {
                Vector3 randomPosition = new Vector3(Random.Range(xMin, xMax), 0, Random.Range(zMin, zMax));
                GameObject g = Instantiate(_prefabZombies[zombieData.type], randomPosition, Quaternion.identity);
                GameManager.instance.AddEnemy(g);
                yield return new WaitForSeconds(0.5f); 
            }
        }
    }
}
