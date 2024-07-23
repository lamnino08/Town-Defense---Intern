using UnityEngine.Tilemaps;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance { get => _instance; }

    private StateGame state = StateGame.Building;
    private Coroutine _countTime;
    [SerializeField] private ZombieAppearSystem _zombieAppearSystem;

    private void Awake()
    {
        if (_instance && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            LoadData();
            StartCoroutine(SwitchToBattleAfterDelay(1)); // 2 minutes
        }
    }

    private IEnumerator SwitchToBattleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        state = StateGame.Battle;
        _zombieAppearSystem.SpawnZombies();
    }

    private void Update()
    {
        // Add logic based on the current state if needed.
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

    public void LoadData()
    {
        ScriptableManager dataManager = Resources.Load<ScriptableManager>($"Map/ManagerData");
        if (dataManager == null)
        {
            Debug.LogError("Map data is not exist");
            return;
        }
        Penhouse.instance.SetData(dataManager.rock, dataManager.gold, dataManager.wooden);
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
