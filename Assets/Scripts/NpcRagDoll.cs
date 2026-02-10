using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NpcRagDoll : MonoBehaviour
{
    private Rigidbody[] _ragdollRigidbodies;
    [SerializeField] float MaxForce = 100;

    
    void Awake()
    {
        _ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        DisableRagdoll();
    }

    void DisableRagdoll()
    {
        foreach (var rb in _ragdollRigidbodies)
            rb.isKinematic = true;
    }
    public void EnableRagdoll()
    {
        foreach (var rb in _ragdollRigidbodies)
            rb.isKinematic = false;
    }
    public void TriggerRagdoll(Vector3 force,Vector3 hitPoint)
    {
        EnableRagdoll();
        Rigidbody hitrb = _ragdollRigidbodies.OrderBy(rigidbody => Vector3.Distance(rigidbody.position, hitPoint)).First();
        hitrb.AddForceAtPosition(force*MaxForce, hitPoint,ForceMode.Impulse);
        //Debug.Log(hitrb.transform.name);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
