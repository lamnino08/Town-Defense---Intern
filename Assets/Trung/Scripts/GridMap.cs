namespace Trung.Scene{
    using UnityEngine;
    using System.Collections.Generic;
    using Unity.VisualScripting;

    public class GridMap : MonoBehaviour
    {
        private static GridMap _instance;
        public static GridMap instance { get => _instance;}

        [SerializeField] private ConstructData _constructData;

        private List<BuildingController_T> _buildings = new List<BuildingController_T>();
        public List<BuildingController_T> Buildings { get {  return _buildings; } }

        private void Start()
        {
            
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
            InitializeGrid();
        }

        public void InitializeGrid()
        {
            /*foreach (Node node in _nodes)
            {
                int id = node.id;
                if (id != 0)
                {
                    int level = node.level;
                    float x = node.x;
                    float y = node.y;
                    int direction = node.direction;

                    BuildingData objectNode = _constructData.GetObjectDataById(id);
                    GameObject prefabToInstantiate = objectNode.prefab[level - 1];

                    x = (objectNode.width & 1) == 1 ? x : x - 0.5f;
                    y = (objectNode.height & 1) == 1 ? y : y - 0.5f;

                    if (prefabToInstantiate != null)
                    {
                        Vector3 spawnPosition = new Vector3(x, y, 0f);

                        GameObject instantiatedObject = Instantiate(prefabToInstantiate, spawnPosition, Quaternion.identity);
                    }
                    else
                    {
                        Debug.LogWarning("Prefab for id " + id + " and level " + level + " not assigned or missing.");
                    }
                }
            }*/

        }

        /*public bool IsPositionIsPlace(Vector3 position)
        {
            float x = position.x;
            float y = position.z;

            Node dataCell = _nodes.Find(e => e.x == x && e.y == y);
            if (dataCell == null) return false;
            return dataCell.id == 0 ? true : false;
        }*/

        public void AddBuilding(BuildingController_T building)
        {
            _buildings.Add(building);
        }

        public void RemoveBuilding(BuildingController_T building)
        {
            for (int i = 0; i < _buildings.Count; i++)
            {
                if (_buildings[i].transform.position.x == _buildings[i].transform.position.x && _buildings[i].transform.position.y == _buildings[i].transform.position.y)
                {
                    _buildings.RemoveAt(i);
                    return;
                }
            }
        }

    }

}
