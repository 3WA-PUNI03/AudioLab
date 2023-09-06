using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Key : MonoBehaviour, IInteractable
{
    [SerializeField] UnityEvent _onUse;
    [SerializeField] float _waitInSeconds;

    public void Use()
    {
        // Dans ce cas on lance la routine
        StartCoroutine(UseCoroutine());
    }

    IEnumerator UseCoroutine()
    {
        _onUse.Invoke();

        yield return new WaitForSeconds(_waitInSeconds);

        gameObject.SetActive(false);
    }

}
