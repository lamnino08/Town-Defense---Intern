using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlaneCheckGrid : MonoBehaviour
{
    [SerializeField] private LayerMask _planMask;
    private Collider _collider;
    static Color _normalColor = new Color(50f/255f, 65f/255f, 49f/255f); // Khi ma dat roi => 
    static Color _validColor = new Color(60f/255f, 70f/255f, 49f/255f); // Khi ma duoc dat
    // static Color _validColor = new Color(50f/255f, 70f/255f, 49f/255f);
    static Color _inValidColor = new Color(104f/255f, 43f/255f, 17f/255f); // Khi ma khong duoc dat
    private Renderer _rendere;

    private void Start() 
    {   
        _collider = GetComponent<BoxCollider>();
        _rendere = GetComponent<Renderer>();
    }
    public bool isCollisonWithOtherBuilding()
    {
        Bounds colliderBounds = _collider.bounds;
        
        Collider[] colliders = Physics.OverlapBox(transform.position, colliderBounds.extents, Quaternion.identity, _planMask);
        // Debug.Log(colliders.Length);
        bool rs = colliders.Length > 1;

        if (rs)
        {
            InvalidColor();
        } else
        {
            ValidColor();
        }
        
        return rs;
    }

    private void ValidColor()
    {
        _rendere.material.color = _validColor;
    }

    private void InvalidColor()
    {
        _rendere.material.color = _inValidColor;
    }

    public void NormalColor()
    {
        _rendere.material.color = _normalColor;
    }
}
