namespace Trung.Scene
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class BuildGrid : MonoBehaviour
    {
        private int _rows = 70;
        private int _columns = 55;
        private float _cellSize = 1f; public float cellSize { get { return _cellSize; } }

        public List<Building> buildings = new List<Building>();
        public GameObject cubeRoot;
        public Vector3 GetStartPosition(int x, int y)
        {
            Vector3 position = transform.position;
            position += (transform.right.normalized * x * _cellSize) + (transform.forward.normalized * y * _cellSize);
            //Debug.Log("start: " +  position);
            return position;
        }

        public Vector3 GetCenterPosition(int x, int y, int rows, int columns)
        {
            Vector3 position = GetStartPosition(x, y);
           
            position += (transform.right.normalized * columns * _cellSize / 2f) + (transform.forward.normalized * rows * _cellSize / 2f);
            //Debug.Log("center: " + position);
            return position;
        }

        public Vector3 GetEndPosition(int x, int y, int rows, int columns)
        {
            Vector3 position = GetStartPosition(x, y);
            position += (transform.right.normalized * columns * _cellSize) + (transform.forward.normalized * rows * _cellSize);
            return position;
        }

        public Vector3 GetEndPosition(Building building)
        {
            return GetEndPosition(building.currentX, building.currentY, building.columns, building.rows);
        }


        public bool IsWorldPositionIsOnPlane(Vector3 position, int x, int y, int rows, int columns)
        {
            position = transform.InverseTransformPoint(position);
            //Debug.Log("position: " + position);
            Rect rect = new Rect(x, y, columns, rows);
            //Debug.Log(rect);
            if (rect.Contains(new Vector2(position.x, position.z)))
            {
                return true;
            }
            return false;
        }

        public bool CanPlaceBuilding(Building building, int x, int y)
        {
            if (building.currentX < 0 || building.currentY < 0 || building.currentX + building.columns > _columns || building.currentY + building.rows > _rows)
            {
                return false;
            }
            for (int i = 0; i < buildings.Count; i++)
            {
                if (buildings[i] != building)
                {
                    Rect rect = new Rect(buildings[i].currentX, buildings[i].currentY, buildings[i].columns, buildings[i].rows);
                    Rect rect2 = new Rect(building.currentX, building.currentY, building.columns, building.rows);
                    if (rect2.Overlaps(rect))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            for (int i = 0; i <= _rows; i++)
            {
                Vector3 point = transform.position + transform.forward.normalized * _cellSize * (float)i;
                Gizmos.DrawLine(point, point + transform.right.normalized * _cellSize * (float)_columns);
            }
            for (int i = 0; i <= _columns; i++)
            {
                Vector3 point = transform.position + transform.right.normalized * _cellSize * (float)i;
                Gizmos.DrawLine(point, point + transform.forward.normalized * _cellSize * (float)_rows);
            }
        }
#endif
    }
}