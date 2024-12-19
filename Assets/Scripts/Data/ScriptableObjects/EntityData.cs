using System.Linq;
using Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Data.ScriptableObjects
{
    public abstract class EntityData : ScriptableObject
    {
        [SerializeField] private string id;
        [SerializeField] protected int index = -1;
        [SerializeField] private string entityName;
        [SerializeField] private Color color;
        [SerializeField] private float health = Constants.EntityDefaultHealth;
        [SerializeField] private float attackPower = Constants.EntityDefaultAttackPower;
        public string Id => id;
        public int Index => index;
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

#if UNITY_EDITOR
        
        protected virtual void OnEnable()
        {
            AssignDefaults(); 
        }
        
        protected virtual void AssignDefaults()
        {
            if (string.IsNullOrEmpty(id))
                GenerateUniqueId();

            if (color == default)
            {
                color = Random.ColorHSV();
                color.a = 1;
            }
        }
        
        protected void SetIndexAndName(int index, string baseName)
        {
            this.index = index;
            entityName = $"{baseName} {index + 1}";
        }
        
        /// <summary>
        /// Generates unique id (10 chars)
        /// </summary>
        private void GenerateUniqueId()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new System.Random();
            id = new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        
#endif
    }
}