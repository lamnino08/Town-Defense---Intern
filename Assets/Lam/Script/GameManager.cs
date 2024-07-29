using UnityEngine.Tilemaps;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance { get => _instance; }

    private StateGame state = StateGame.Building;
    private Coroutine _countTime;

    [SerializeField] private TMP_Text countUI;
    [SerializeField] private CameraSystem _camaraSystem;

    [SerializeField] private ConstructionSave _constructionSave;
    [SerializeField] Transform player;


    [SerializeField] private List<ZombieAppearSystem> _zombieAppearSystems = new List<ZombieAppearSystem>();
    public List<GameObject> destroyEffect = new List<GameObject>();
    public List<GameObject> listBuilding = new List<GameObject>();
    public List<GameObject> Residents = new List<GameObject>();
    public List<GameObject> buildings = new List<GameObject>();
    public List<GameObject> soldier = new List<GameObject>();
    public List<GameObject> enemy = new List<GameObject>();
    public List<GameObject> gold = new List<GameObject>();
    public int level = 1;
    

    private void Awake()
    {
        if (_instance && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            StartCoroutine(SwitchToBattleAfterDelay(120)); 
        }
    }

    private IEnumerator SwitchToBattleAfterDelay(float delay)
    {
        countUI.gameObject.SetActive(true);
        while (delay >= 0)
        {
             int minutes = Mathf.FloorToInt(delay / 60);
            int seconds = Mathf.FloorToInt(delay % 60);
            countUI.text = $"{minutes:00}:{seconds:00}";
            delay--;
            yield return new WaitForSeconds(1);
        }
        _camaraSystem.BattleCamera();
        state = StateGame.Battle;
        AudioAssitance.Instance.AttackSoud();


        DestroyAllResident();

        foreach(var _zombieAppearSystem in _zombieAppearSystems)
        {
            _zombieAppearSystem.SpawnZombies(level);
        }
    }

    public void EndBattle()
    {
        Time.timeScale = 1f; 
        DestroyAllBuildings();
        DestroyAllDestroyEffect();
        DestroyAllSoldier();
        DestroyAllEnemy();
            _constructionSave.LoadMap();
            HavestGold();
        AudioAssitance.Instance.Playmusic("Theme");
        player.GetComponent<PlayerHealth>().SetHealth();
        Penhouse.instance.ResetHealth();
        StartCoroutine(SwitchToBattleAfterDelay(120)); 

    }

    public void DestroyAllBuildings()
    {
        foreach (GameObject building in buildings)
        {
            if (building != null)
            {
                Destroy(building);
            }
        }

        buildings.Clear();
    }

    public void DestroyAllDestroyEffect()
    {
        foreach (GameObject building in destroyEffect)
        {
            if (building != null)
            {
                Destroy(building);
            }
        }

        destroyEffect.Clear();
    }

    public void DestroyAllSoldier()
    {
        foreach (GameObject building in soldier)
        {
            if (building != null)
            {
                Destroy(building);
            }
        }

        soldier.Clear();
    }

    public void DestroyAllEnemy()
    {
        foreach (GameObject building in enemy)
        {
            Debug.Log("hererr");
            if (building != null)
            {
                Destroy(building);
            }
        }

        enemy.Clear();
    }

    public void DestroyAllResident()
    {
        foreach (GameObject building in Residents)
        {
            if (building != null)
            {
                Destroy(building);
            }
        }

        Residents.Clear();
    }


    public void RemoveBuilding(GameObject b)
    {
        if (buildings.Contains(b))
        {
            buildings.Remove(b);
        }
    }
    public void Lose()
    {
        UIManager.instance.LoseUI();
        Time.timeScale = 0f; 
    }

    public void Win()
    {
        UIManager.instance.WinUI();
        AudioAssitance.Instance.Playmusic("Theme");
        level++;
        Time.timeScale = 0f; 

    }

    public void AddBuilding(GameObject nb)
    {
        if (!buildings.Contains(nb))
        {
            buildings.Add(nb);
        }
    }

    public void RemoveEnemay(GameObject b)
    {
        if (enemy.Contains(b))
        {
            enemy.Remove(b);
        }

        if (enemy.Count == 0)
        {
            Win();
        }
    }

    private void HavestGold()
    {
        foreach(GameObject e in gold)
        {
            e.GetComponent<CoinController>().MoveCoinToPlayer(player.position);
        }
    }

    public void AddEnemy(GameObject nb)
    {
        if (!enemy.Contains(nb))
        {
            enemy.Add(nb);
        }
    }



    public void SaveData()
    {
        ScriptableManager dataManager = Resources.Load<ScriptableManager>($"Map/ManagerData");
        if (dataManager == null)
        {
            Debug.LogError("Map data is not exist");
            return;
        }
        dataManager.Save(Penhouse.instance.rock, Penhouse.instance.gold, Penhouse.instance.wooden);
        EditorUtility.SetDirty(dataManager);
        AssetDatabase.SaveAssets();
    }

    public void LoadData(GameObject penhosue)
    {
        ScriptableManager dataManager = Resources.Load<ScriptableManager>($"Map/ManagerData");
        if (dataManager == null)
        {
            Debug.LogError("Map data is not exist");
            return;
        }
        penhosue.GetComponent<Penhouse>().SetData(dataManager.rock, dataManager.gold, dataManager.wooden);
    }
}

public static class ScriptableObjecManagertUtility
{
    public static void SaveLevelFile(ScriptableManager level)
    {
        AssetDatabase.CreateAsset(level, $"Assets/Resources/Map/ManagerData.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}

enum StateGame
{
    Battle = 1,
    Building = 2,
}
