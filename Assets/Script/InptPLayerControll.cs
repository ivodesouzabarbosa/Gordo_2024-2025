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
        if (!_gameControl._gameStart)
        {
            PlayerConectMenu();
        }
        else
        {
            PlayerConectGame();
        }
       
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

    public void PlayerConectMenu()
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
            if (selectPerson._checkSelect)// se ainda estiver no menu de seleção de personagem
            {
                selectPerson.BtVoltar();
            }
           
        }
    }
    public void PlayerConectGame()
    {
        if (IsJoystickConnected() && selectPerson._sliderPLayers._imgDescGame.activeInHierarchy)
        {
            Debug.Log("Joystick conectado ao jogador.");
            _gameControl._numberPlayer++;
            selectPerson._sliderPLayers._imgDescGame.gameObject.SetActive(false);
        }
        else if (!IsJoystickConnected() && !selectPerson._sliderPLayers._imgDescGame.activeInHierarchy)
        {
            Debug.Log("Nenhum joystick conectado ao jogador.");

            _gameControl._numberPlayer--;
            selectPerson._sliderPLayers._imgDescGame.gameObject.SetActive(true);
        }
    }
}
