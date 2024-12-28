using System.Linq;
using Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data.ScriptableObjects
{
    public abstract class EntitySO : ScriptableObject
    {
        [SerializeField] private string id;
        [SerializeField] protected int index = -1;
        [SerializeField] private string entityName;
        [SerializeField] private Color color;
        [FormerlySerializedAs("health")] [SerializeField] private float baseHealth = Constants.EntityDefaultHealth;
        [FormerlySerializedAs("attackPower")] [SerializeField] private float baseAttackPower = Constants.EntityDefaultAttackPower;
        public string Id => id;
        public int Index => index;
        public string EntityName => entityName;
        public Color Color => color;

        public float BaseHealth
        {
            get => baseHealth;
            protected set => baseHealth = value;
        }
        
        public float BaseAttackPower
        {
            get => baseAttackPower;
            protected set => baseAttackPower = value;
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