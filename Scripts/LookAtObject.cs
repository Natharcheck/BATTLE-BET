using System;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    public Transform target;

    private void Awake()
    {
        target = Camera.main.transform;
    }

    private void FixedUpdate()
    {
        LookAt();
    }

    private void LookAt()
    {
        transform.rotation = Quaternion.LookRotation(target.forward);
    }
}
