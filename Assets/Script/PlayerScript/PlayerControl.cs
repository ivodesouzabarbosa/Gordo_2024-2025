using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public int _indexPlayer; 
    GameControl _gameControl;
    PlayerMove _playerMove;
    


    private void Awake()
    {
        _playerMove = GetComponent<PlayerMove>();
        _gameControl = GameObject.FindWithTag("GameController").GetComponent<GameControl>();
        _gameControl.ContPLayer();
    }

    void Start()
    {
        
        transform.SetParent(_gameControl._playerBase);
        _indexPlayer = _gameControl._numbPlayer;
        _playerMove._personMoveCam = true;
        _gameControl._numbPlayer++;

    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CamTriguer"))
        {
            _playerMove._personMoveCam = true; 
        }
        if (_indexPlayer==0 && other.gameObject.name == "Inter1")
        {
            _gameControl._levelOn = 1;
        }
        if (_indexPlayer == 0 && other.gameObject.name == "Inter2")
        {
            _gameControl._levelOn = 2;
        }
        
        if (other.gameObject.name== "TTFim")
        {
            _gameControl._isDir=true;
            _gameControl._camPlayer.RotateToY(-_gameControl._camPlayer.targetYRotation);
            _gameControl.BlockFim();
            _gameControl._coolFimFase.SetActive(true);
            _gameControl._levelOn = 3;
        }
        if (other.gameObject.name == "TTFimaFase")
        {
            Debug.Log("Fim da fase 1");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CamTriguer"))
        {
            _playerMove._personMoveCam = false;
        }
    }
}
