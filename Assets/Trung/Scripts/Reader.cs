namespace Trung.Scene
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class BuildingInfo
    {
        public int id;
        public int level;
        public float x;
        public float y;
        public float direction;

        public BuildingInfo(int id, int level, float x, float y, float direction)
        {
            this.id = id;
            this.level = level;
            this.x = x;
            this.y = y;
            this.direction = direction;
        }
    }
    public class ListBuilding
    {
        public BuildingInfo[] buildings;
    }

    public class Reader : MonoBehaviour
    {
        public TextAsset jsonData;
        public ListBuilding buildings = new ListBuilding();

        private void Start()
        {
            buildings = JsonUtility.FromJson<ListBuilding>(jsonData.text);
        }
    }
}
