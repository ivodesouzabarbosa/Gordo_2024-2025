using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class InptPLayerControll : MonoBehaviour
{
    public Image _imgDesc;
    PlayerInput _playerInput;
    GameControl _gameControl;
    public SelectPerson selectPerson;

    public TextMeshProUGUI _nomePLayerMenu;
    bool checkPass;
    MultiplayerEventSystem _multiplayerEventSystem;
   

   

    void Start()
    {
   
        _gameControl = GameObject.FindWithTag("GameController").GetComponent<GameControl>();
        _playerInput = GetComponent<PlayerInput>();
        _multiplayerEventSystem = GetComponent<MultiplayerEventSystem>();

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
            selectPerson.BtSelectPlayer(0);




        }
        else if (!IsJoystickConnected() && !_imgDesc.gameObject.activeInHierarchy)
        {
            Debug.Log(selectPerson._nomePLayer + "Desconectado");
            _imgDesc.gameObject.SetActive(true);
            selectPerson._sliderPLayers.gameObject.SetActive(false);
            _gameControl._numberPlayer--;
            //  Debug.Log("ee "+selectPerson.playerInput.playerIndex);
            //  Debug.Log("yy "+selectPerson._indexPlayer);
            //  selectPerson.playerInput.playerIndex = selectPerson._indexPlayer;
            selectPerson.numbSelectPerson = -1;
            selectPerson.BtVoltarPerson(true);
            _multiplayerEventSystem.SetSelectedGameObject(selectPerson._btCima.gameObject);
         




        }
    }
    public void PlayerConectGame()
    {

        //Debug.Log(selectPerson._nomePLayer + " "+selectPerson._checkSelect);
        //Debug.Log(selectPerson._nomePLayer + " " + selectPerson._sliderPLayers._imgDescGame.activeInHierarchy);
        if (selectPerson != null)
        {
            if (IsJoystickConnected() && selectPerson._checkSelect && (!selectPerson._playerMove.gameObject.activeInHierarchy || !checkPass))
            {
                
                Debug.Log(selectPerson._nomePLayer + " conectado");
                _gameControl._numberPlayer++;
                selectPerson._sliderPLayers._imgDescGame.gameObject.SetActive(false);
                selectPerson._sliderPLayers.SetNomePlayer(selectPerson._nomePLayer);
                if (checkPass && selectPerson._playerMove._indexPerson != 0  && selectPerson._playerMove._selectPersonMove)
                {
                    selectPerson._playerMove.gameObject.SetActive(true);
                    selectPerson._playerMove._personMoveCam = false;
                    selectPerson._playerMove.transform.position = _gameControl._camPlayer.transform.position;
                }
                else
                {
                    Debug.Log("DesPausar game, pois o diguinho personagem está com jogador desconectado");
                }
                checkPass = true;
                //SetNomePlayer(selectPerson._nomePLayer);
                // selectPerson._sliderPLayers.gameObject.SetActive(true);

            }
            else if (!IsJoystickConnected() && selectPerson._checkSelect)
            {
                Debug.Log(selectPerson._nomePLayer + "Desconectado");

                _gameControl._numberPlayer--;
                selectPerson._sliderPLayers._imgDescGame.gameObject.SetActive(true);
                if (selectPerson._playerMove._indexPerson!= 0 && selectPerson._playerMove._selectPersonMove)
                {
                    selectPerson._playerMove.gameObject.SetActive(false);
                    selectPerson._playerMove._personMoveCam = true;
                }
                else
                {
                    Debug.Log("Pausar game, pois o diguinho personagem está com jogador desconectado");
                }


            }

        }
    }
}
