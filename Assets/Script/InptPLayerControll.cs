using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InptPLayerControll : MonoBehaviour
{
    public Image _imgDesc;
    PlayerInput _playerInput;
    GameControl _gameControl;
    public SelectPerson selectPerson;

    public TextMeshProUGUI _nomePLayerMenu;
   

   

    void Start()
    {
   
        _gameControl = GameObject.FindWithTag("GameController").GetComponent<GameControl>();
        _playerInput = GetComponent<PlayerInput>();
     
       // Invoke("SetTimeNome",0.3f);


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
            Debug.Log(selectPerson._nomePLayer + " conectado");
            _nomePLayerMenu.text = selectPerson._nomePLayer;
            selectPerson._sliderPLayers.SetNomePlayer(selectPerson._nomePLayer);
            _imgDesc.gameObject.SetActive(false);
           // SetNomePlayer(selectPerson._nomePLayer);
            _gameControl._numberPlayer++;
            selectPerson._sliderPLayers.gameObject.SetActive(true);


        }
        else if (!IsJoystickConnected() && !_imgDesc.gameObject.activeInHierarchy)
        {
            Debug.Log(selectPerson._nomePLayer + "Desconectado");
            _imgDesc.gameObject.SetActive(true);
            selectPerson._sliderPLayers.gameObject.SetActive(false);
            _gameControl._numberPlayer--;
            if (selectPerson._checkSelect)// se ainda estiver no menu de seleção de personagem
            {
                selectPerson.BtVoltar();
                
            }
           
        }
    }
    public void PlayerConectGame()
    {

        //Debug.Log(selectPerson._nomePLayer + " "+selectPerson._checkSelect);
        //Debug.Log(selectPerson._nomePLayer + " " + selectPerson._sliderPLayers._imgDescGame.activeInHierarchy);
        if (selectPerson != null)
        {
            if (IsJoystickConnected() && selectPerson._checkSelect)
            {
                Debug.Log(selectPerson._nomePLayer + " conectado");
                _gameControl._numberPlayer++;
                selectPerson._sliderPLayers._imgDescGame.gameObject.SetActive(false);
                selectPerson._sliderPLayers.SetNomePlayer(selectPerson._nomePLayer);
                //SetNomePlayer(selectPerson._nomePLayer);
                // selectPerson._sliderPLayers.gameObject.SetActive(true);

            }
            else if (!IsJoystickConnected() && selectPerson._checkSelect)
            {
                Debug.Log(selectPerson._nomePLayer + "Desconectado");

                _gameControl._numberPlayer--;
                selectPerson._sliderPLayers._imgDescGame.gameObject.SetActive(true);
              
            }

        }
    }
}
