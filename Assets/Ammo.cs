using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.TryGetComponent<CharacterFire>(out var cf))
        {
            cf.Refill();
            Destroy(gameObject);
        }

    }
}
