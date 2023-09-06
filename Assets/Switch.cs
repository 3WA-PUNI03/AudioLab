using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour, IInteractable
{
    [SerializeField] UnityEvent _onUsed;


    public void Use()
    {
        _onUsed.Invoke();
    }


}
