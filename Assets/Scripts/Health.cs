using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Health : MonoBehaviour
{
    [SerializeField] private GameObject HPSpherePrefab;
    [SerializeField] private TMPro.TextMeshPro hitsText;
    private int hits;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Bullet>())
        {
            TakeDamage();
            Destroy(other.gameObject);
        }
    }

    private void TakeDamage()
    {
        hitsText.SetText($"HITS\n{++hits}");
        Instantiate(HPSpherePrefab, transform);
        meshRenderer.material.color = Random.ColorHSV();
    }
}