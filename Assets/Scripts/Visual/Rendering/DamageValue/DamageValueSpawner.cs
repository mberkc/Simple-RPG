using System.Collections.Generic;
using UnityEngine;

namespace Visual.Rendering.DamageValue
{
    public class DamageValueSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject damageValuePrefab;
        [SerializeField] private int initialPoolSize = 10;

        private readonly Queue<DamageValue> _pool = new();

        private void Awake()
        {
            InitializePool();
        }

        private void InitializePool()
        {
            for (var i = 0; i < initialPoolSize; i++)
            {
                var damageValue = InstantiateDamageValue();
                _pool.Enqueue(damageValue);
            }
        }

        /// <summary>
        /// Recycle is handled on DamageValue itself, don't try to recycle it.
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="scenePosition"></param>
        public void Spawn(float damage, Vector3 scenePosition)
        {
            var damageValue = Get();
            damageValue.Show(damage, scenePosition, Recycle);
        }

        private void Recycle(DamageValue damageValue)
        {
            damageValue.gameObject.SetActive(false);
            _pool.Enqueue(damageValue);
        }

        private DamageValue Get()
        {
            if (_pool.Count > 0)
            {
                var damageValue = _pool.Dequeue();
                damageValue.gameObject.SetActive(true);
                return damageValue;
            }

            // If pool is empty, create a new instance
            return InstantiateDamageValue(true);
        }

        private DamageValue InstantiateDamageValue(bool instant = false)
        {
            var go = Instantiate(damageValuePrefab, transform);
            var damageValue = go.GetComponent<DamageValue>();
            if (damageValue == null)
            {
                Debug.LogError("DamageValuePrefab must have a DamageValue component!");
                Destroy(go);
            }
            go.SetActive(instant);
            return damageValue;
        }
    }
}