using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explse : MonoBehaviour
{
    public float explosionForce = 100f; // Lực nổ để đẩy các mảnh nhỏ
    public float explosionRadius = 5f; // Bán kính của lực nổ
    public float upwardsModifier = 0.5f; // Thay đổi mức độ bay lên

    private void Start()
    {
        DestroyAndExplode();
    }

    private void DestroyAndExplode()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            Rigidbody rb = child.gameObject.AddComponent<Rigidbody>();
            rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsModifier);
        }
    }
}
