using Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Data.ScriptableObjects
{
    public abstract class EntityData : ScriptableObject
    {
        [SerializeField] private string entityName = "Entity";
        [SerializeField] private Color color;
        [SerializeField] private float health = Constants.EntityDefaultHealth;
        [SerializeField] private float attackPower = Constants.EntityDefaultAttackPower;
        
        public string EntityName => entityName;
        public Color Color => color;

        public float Health
        {
            get => health;
            protected set => health = value;
        }
        
        public float AttackPower
        {
            get => attackPower;
            protected set => attackPower = value;
        }

        private void OnEnable()
        {
            color = Random.ColorHSV();
            color.a = 1;
        }
    }
}