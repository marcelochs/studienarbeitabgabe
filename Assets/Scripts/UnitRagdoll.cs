using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRagdoll : MonoBehaviour
{
    [SerializeField] private Transform ragdollRootBone;
    [SerializeField] private float randomForce= 0;

    private float timer = 2f;

    private void Start()
    {
        Rigidbody ragdollRigidbody = ragdollRootBone.GetComponent<Rigidbody>();
        Vector3 randomDirection = new Vector3(Random.Range(-1f * randomForce, randomForce), 0, Random.Range(-1f * randomForce, randomForce));
        ragdollRigidbody.AddForce(randomDirection);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Destroy(gameObject);
        }

    }

    public void Setup(Transform originalRootBone)
    {
        MatchAllChildTransform(originalRootBone, ragdollRootBone);

        Vector3 randomDirection = new Vector3(Random.Range(-1f, +1f), 0, Random.Range(-1f, +1f));
        ApplyExlposionToRagdoll(ragdollRootBone, 300f, transform.position + randomDirection, 10f);
    }

    private void MatchAllChildTransform(Transform root, Transform clone)
    {
        foreach(Transform child in root)
        {
            Transform cloneChild = clone.Find(child.name);
            if (cloneChild != null)
            {
                cloneChild.position = child.position;
                cloneChild.rotation = child.rotation;

                MatchAllChildTransform(child, cloneChild);
            }
        }
    }

    private void ApplyExlposionToRagdoll(Transform root, float explosionForce,Vector3 explosionPosition, float explosionRange)
    {
        foreach (Transform child in root)
        {
            if(child.TryGetComponent<Rigidbody>(out Rigidbody childRigidbody))
            {
                childRigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRange);
            }

            ApplyExlposionToRagdoll(child, explosionForce, explosionPosition, explosionRange);
        }
    }
}
