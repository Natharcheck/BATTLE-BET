using UnityEngine;

namespace Characters
{
    public enum MainAttribute
    {
        Strong,
        Agility,
        Intelligence
    }
    
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CharacterData", order = 1)]
    public class CharacterData : ScriptableObject
    {
        public MainAttribute mainAttribute;
        [Space]
        public int strong;
        public int agility;
        public int intelligence;
        [Space] 
        public float magicResist;
        public float physicResist;
        [Space] 
        public float moveSpeed;
        public float rotationSpeed;
    }
}