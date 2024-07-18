using UnityEngine;

[System.Serializable]
public abstract class Resource : ScriptableObject
{
    public int amount;
    public abstract void Manufacture();
    
}







