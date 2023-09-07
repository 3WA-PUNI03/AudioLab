using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{

    [SerializeField] int _startHealth;

    [SerializeField] UnityEvent _onShot;

    int _currentHealth;

    private void Start()
    {
        _currentHealth = _startHealth;
    }

    internal void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        if( _currentHealth < 0 )
        {
            _onShot.Invoke();
            //Destroy(gameObject);
        }
    }

    
}
