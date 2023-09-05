using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class CharacterMove : MonoBehaviour
{

    [SerializeField] InputActionReference _move;
    [SerializeField] InputActionReference _look;
    [SerializeField] CharacterController _controller;
    [SerializeField] float _speed;

    [SerializeField] Transform _cameraTransform;

    float _vertical;
    float _horizontal;

    private void Update()
    {
        MoveCharacter();

        // Récup la direction Look
        Vector2 look = _look.action.ReadValue<Vector2>();
        Debug.Log(look);

        // On change notre rotation par rapport à Looks
        _horizontal += look.x;
        _vertical -= look.y;
        // On clamp l'axe vertical pour pas faire de looping
        _vertical = Mathf.Clamp(_vertical, -80, 80);

        transform.rotation = Quaternion.Euler(0, _horizontal, 0);
        _cameraTransform.localRotation = Quaternion.Euler(_vertical, 0, 0);

    }

    private void MoveCharacter()
    {
        // On récupère la direction du joystick
        Vector2 joystick = _move.action.ReadValue<Vector2>();

        // La direction Y du joystick on veut que ça bouge le Z de notre personnage, donc on prépare un vector3 
        // qui prend le Y comme direction Z.
        Vector3 direction = new Vector3(joystick.x, 0, joystick.y);

        // On lui applique une vitesse
        direction *= _speed;

        direction = _controller.transform.TransformDirection(direction);

        // On envoi au CharacterController
        _controller.Move(direction);
    }
}
