using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InptPLayerControll : MonoBehaviour
{
    public Image _imgDesc;
    PlayerInput _playerInput;
    GameControl _gameControl;
    public SelectPerson selectPerson;


    void Start()
    {
   
        _gameControl = GameObject.FindWithTag("GameController").GetComponent<GameControl>();
        _playerInput = GetComponent<PlayerInput>();
        
    }

    private void Update()
    {
        if (IsJoystickConnected() && _imgDesc.gameObject.activeInHierarchy)
        {
            Debug.Log("Joystick conectado ao jogador.");
            _imgDesc.gameObject.SetActive(false);
            _gameControl._numberPlayer++;
           
           
        }
        else if (!IsJoystickConnected() && !_imgDesc.gameObject.activeInHierarchy)
        { 
            Debug.Log("Nenhum joystick conectado ao jogador.");
            _imgDesc.gameObject.SetActive(true);
            _gameControl._numberPlayer--;
            if (!_gameControl._gameStart && selectPerson._checkSelect)// se ainda estiver no menu de seleção de personagem
            {
                selectPerson.BtVoltar();
            }
        }
        //_imgDesc = IsJoystickConnected();
    }
    bool IsJoystickConnected()
    {
        // Verifica se o dispositivo do PlayerInput é um Gamepad
        foreach (var device in _playerInput.devices)
        {
            return true;
        }
        return false;
    }
}
