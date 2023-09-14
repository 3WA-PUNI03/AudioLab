using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CharacterFire : MonoBehaviour
{
    [SerializeField] Camera _cam;
    [SerializeField] InputActionReference _fireInput;

    [SerializeField] float _ammoMax;
    [SerializeField] UnityEvent _onFire;


    [Header("Debug")]
    [SerializeField] float _currentAmmo;

    private void Start()
    {
        _currentAmmo = _ammoMax;
    }

    private void Update()
    {
        // On calcul le point central de l'écran
        Vector2 centerScreen = new Vector2(Screen.width / 2, Screen.height / 2);

        // On demande à la camera de nous donner un rayon qui part de la cam dans sa direction
        Ray cameraRay = _cam.ScreenPointToRay(centerScreen);

        // On va pouvoir lancer un raycast
        Debug.DrawRay(cameraRay.origin, cameraRay.direction, Color.red);
        if (_fireInput.action.WasPressedThisFrame())
        {
            if (_currentAmmo <= 0) return;
            _currentAmmo--;
            _onFire.Invoke();

            if (Physics.Raycast(cameraRay, out RaycastHit hit, 100f))
            {
                // Si le joueur a apuyé sur le bouton d'interaction
                Debug.Log($"touché {hit.collider.name}!");
                if (hit.collider.TryGetComponent(out Health usable))
                {
                    usable.TakeDamage(10);
                }
            }
        }
    }

    public void Refill()
    {
        _currentAmmo = _ammoMax;
    }
}
