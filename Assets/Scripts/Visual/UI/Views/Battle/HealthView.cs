using System;
using System.Globalization;
using Core;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Visual.UI.Views.Battle
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Image healthBar;
        [SerializeField] private TextMeshProUGUI healthText;
        private float maxHealth;

        internal void Initialize(float health)
        {
            maxHealth = health;
            healthBar.fillAmount = 1f;
            UpdateHealth(health);
        }

        internal void UpdateHealth(float currentHealth)
        {
            healthText.text = currentHealth.ToString(CultureInfo.InvariantCulture);
            
            var healthPercent = currentHealth / maxHealth;
            healthBar.DOFillAmount(healthPercent, Constants.NormalAnimationSpeed);
        }

        private void OnDestroy()
        {
            healthBar.DOKill(true);
        }
    }
}