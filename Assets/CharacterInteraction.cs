using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInteraction : MonoBehaviour
{

    [SerializeField] Camera _cam;
    [SerializeField] InputActionReference _interaction;


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // On calcul le point central de l'écran
        Vector2 centerScreen = new Vector2(Screen.width / 2, Screen.height / 2);

        // On demande à la camera de nous donner un rayon qui part de la cam dans sa direction
        Ray cameraRay = _cam.ScreenPointToRay(centerScreen);

        // On va pouvoir lancer un raycast
        Debug.DrawRay(cameraRay.origin, cameraRay.direction, Color.red);
        if (Physics.Raycast(cameraRay, out RaycastHit hit, 2f))
        {
            Debug.Log($"touché {hit.collider.name}!");
            hit.collider.GetComponent<MeshRenderer>()?.sharedMaterial.SetFloat("_OutlineEnabled", 1);

            // Si le joueur a apuyé sur le bouton d'interaction
            if (_interaction.action.WasPressedThisFrame())
            {
                if (hit.collider.TryGetComponent(out IInteractable usable))
                {
                    usable.Use();
                }
            }
        }
    }

}
