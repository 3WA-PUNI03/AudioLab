using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] InputActionReference _move;
    [SerializeField] InputActionReference _look;
    [SerializeField] InputActionReference _jump;
    [SerializeField] CharacterController _controller;
    [SerializeField] float _speed;
    [SerializeField] float _jumpPower;

    [SerializeField] Transform _cameraTransform;

    [SerializeField] Vector2 _cameraSensibility;

    [SerializeField] CinemachineVirtualCamera _vc;

    [SerializeField] UnityEvent _onWalkStart; 
    [SerializeField] UnityEvent _onWalkStop;

    float _vertical;
    float _horizontal;
    float _gravity;

    bool _isWalking;

    private void Update()
    {
        MoveCharacter();


        UpdateLook();

    }

    private void UpdateLook()
    {
        // Récup la direction Look
        Vector2 look = _look.action.ReadValue<Vector2>();

        // On change notre rotation par rapport à Looks
        _horizontal += look.x * _cameraSensibility.x;
        _vertical -= look.y * _cameraSensibility.y;

        // On clamp l'axe vertical pour pas faire de looping
        _vertical = Mathf.Clamp(_vertical, -80, 80);

        // On applique la rotation droite/gauche à notre objet pour tourner tout le monde
        transform.rotation = Quaternion.Euler(0, _horizontal, 0);
        // On applique la rotation haut/bas uniquement à notre camera pour qu'elle tourne seulesss
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

        // Jump
        if (_controller.isGrounded)
        {
            _gravity = 0;
            if(_jump.action.WasPressedThisFrame())
            {
                _gravity = _jumpPower;
            }
        }
        else
        {
            _gravity += Physics.gravity.y * Time.deltaTime;
        }


        // Is Walking events
        if (direction.magnitude > 0.001f)
        {
            if (_isWalking == false)
            {
                _onWalkStart.Invoke();
            }
            _isWalking = true;
        }
        else
        {
            if (_isWalking == true)
            {
                _onWalkStop.Invoke();
            }
            _isWalking = false;
        }


        // Jump
        if(_jump.action.WasPressedThisFrame())
        {
            _gravity = _jumpPower;
        }
        _gravity -= Physics.gravity.y * Time.deltaTime;

        if(_controller.isGrounded)
        {
            _gravity = 0;
        }  

        _controller.Move(new Vector3(direction.x, _gravity, direction.z));
        // On envoi au CharacterController
        //_controller.Move(new Vector3(direction.x, 0, direction.z));
    }


}
