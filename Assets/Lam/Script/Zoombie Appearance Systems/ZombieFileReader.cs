using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ZombieData
{
    public int type;
    public int qty;

    public ZombieData(int type, int qty)
    {
        this.type = type;
        this.qty = qty;
    }
}

public class ZombieFileReader
{
    private string _filePath;

    public ZombieFileReader(string filePath)
    {
        _filePath = Path.Combine(Application.streamingAssetsPath, filePath);
    }

    public List<ZombieData> GetZombieDataForLevel(int level)
    {
        List<ZombieData> zombieDataList = new List<ZombieData>();

        if (File.Exists(_filePath))
        {
            string[] lines = File.ReadAllLines(_filePath);
            foreach (var line in lines)
            {
                if (line.StartsWith(level.ToString() + ":"))
                {
                    string data = line.Substring(line.IndexOf(':') + 1);
                    string[] entries = data.Split(';');

                    foreach (var entry in entries)
                    {
                        if (!string.IsNullOrEmpty(entry))
                        {
                            string[] parts = entry.Split(',');
                            if (parts.Length == 2)
                            {
                                int type = int.Parse(parts[0]);
                                int qty = int.Parse(parts[1]);
                                zombieDataList.Add(new ZombieData(type, qty));
                            }
                        }
                    }
                    break;
                }
            }
        }
        else
        {
            Debug.LogError("File not found: " + _filePath);
        }

        return zombieDataList;
    }
}
