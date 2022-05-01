using System;
using UnityEngine;

namespace Agent
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;

        private Transform _transform;
        private void Start()
        {
            _transform = GetComponent<Transform>();
            _enemy.HealthUpdate += OnHealthUpdate;
        
        }

        private void OnHealthUpdate(float currHealth)
        {
            var maxHealth = _enemy.MaxHealth;
            var newHealth = currHealth / maxHealth;
            _transform.localScale = new Vector3(1f, newHealth, 1f);
        }
    }
}