using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAppearSystem : MonoBehaviour
{
    [SerializeField] private List<GameObject> _prefabZombies = new List<GameObject>();
    [SerializeField] private string filePath = "Assets/Lam/zombieSpawnData.txt";
    [SerializeField] private int level = 1; 
    List<ZombieData> zombieDataList;

    private void Start()
    {

        // SpawnZombies(zombieDataList);
    }

     public void SpawnZombies()
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
                Vector3 randomPosition = new Vector3(Random.Range(15f, 23f), 0, Random.Range(11f, 20f));
                Instantiate(_prefabZombies[zombieData.type], randomPosition, Quaternion.identity);
                yield return new WaitForSeconds(0.5f); // Tạm dừng 0.5 giây trước khi xuất hiện zombie tiếp theo
            }
        }
    }
}
