namespace Trung.Scene
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;


    [Serializable]
    public class BuildingData
    {
        [field: SerializeField]
        public int id { get; private set; }
        [field: SerializeField]
        public int level {get; private set; }
        [field: SerializeField]
        public int height { get; private set; }
        [field: SerializeField]
        public int width { get; private set; }
        [field: SerializeField]
        public int direction {get; private set; }
        [field: SerializeField]
        public float[] health { get; private set; }
        [field: SerializeField]
        public GameObject[] prefab { get; private set; }
    }

    public class ConstructData : MonoBehaviour
    {
        [SerializeField] List<BuildingData> _listConstruction = new List<BuildingData>();

        public BuildingData GetObjectDataById(int id)
        {
            return _listConstruction.Find(d => d.id == id);
        }
    }

}