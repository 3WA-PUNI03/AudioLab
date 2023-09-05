using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMove : MonoBehaviour
{

    [SerializeField] InputActionReference _move;
    [SerializeField] CharacterController _controller;
    [SerializeField] float _speed;


    private void Update()
    {
        // On récupère la direction du joystick
        Vector2 joystick = _move.action.ReadValue<Vector2>();

        // La direction Y du joystick on veut que ça bouge le Z de notre personnage, donc on prépare un vector3 
        // qui prend le Y comme direction Z.
        var direction = new Vector3(joystick.x, 0, joystick.y);

        // On lui applique une vitesse
        direction *= _speed;

        // On envoi au CharacterController
        _controller.Move(direction);

    }

}
