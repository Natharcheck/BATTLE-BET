using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    
    public Transform target;
    [Space] 
    [SerializeField] private float moveOffset;
    [Space]
    [SerializeField] private float speed;
    [SerializeField] private float height;

    private void Awake()
    {
        Instance = this;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(
            new Vector3(transform.position.x, height, transform.position.z), 
            new Vector3(target.position.x, height, target.position.z + moveOffset), 
            speed * Time.deltaTime);
    }
}
