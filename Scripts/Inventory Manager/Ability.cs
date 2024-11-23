using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Optional { get; set; }
    
    [SerializeField] private GameObject prefab;

    public void SpawnPrefab(Vector3 position, Quaternion rotation)
    {
        Instantiate(prefab, position, rotation);
    }
}