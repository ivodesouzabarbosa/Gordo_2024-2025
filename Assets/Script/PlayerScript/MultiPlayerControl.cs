using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MultiPlayerControl : MonoBehaviour
{
  //  public List<Transform> _personMenu=new List<Transform>();
    public List<SelectPerson> _selectsPersonList=new List<SelectPerson>();
    public List<bool> _checkPersonSel = new List<bool>();
    public int _numberPersonSel;
    public List<int> _personSelecNumber = new List<int>();
    public List<Transform> _camImg = new List<Transform>();
    public List<Texture> _TextImg = new List<Texture>();
    GameControl _gameControl;
    public GameObject _menuGame;
    public Transform _PlayerGame;
    public PlayerInputManager _inputManager;
    public List<SliderPLayer> _sliderPLayers = new List<SliderPLayer>();
    public List<PlayerDados> _dadosListPlayer = new List<PlayerDados>();
    public List<SliderPLayer> _sliderPLayersOn = new List<SliderPLayer>();
    public Transform _sliderPLayersGame;


    private void Start()
    {
        _gameControl = GameObject.FindWithTag("GameController").GetComponent<GameControl>();
        for (int i = 0; i < _gameControl._playerMove.Count; i++)
        {
            _checkPersonSel.Add(false);
        }
        _sliderPLayersGame.transform.localScale = Vector3.zero;
    }
    public void SetCheckBlock(int value,bool check)
    {
        _checkPersonSel[value] = check;
    }

    public void MoveCamMenu(Transform _transform, GameControl gameControl, int selecPerson )
    {
        Vector3 vectorCam = gameControl._playerMove[selecPerson].transform.position;
        _transform.DOMove(new Vector3(vectorCam.x+3, -3,vectorCam.z), .5f);
    }
    
    public void CheckIniGame(int value)
    {
        PlayerMove playerMove = _gameControl._playerMove[0];
        if (_numberPersonSel == _gameControl._numberPlayer)
        {
            if (playerMove._selectPersonMove)
            {
                Debug.Log("COMEÇAR GAME");
                PersonMovemntStart();
                _menuGame.SetActive(false);
                SliderPLayer sliderPLayer1= _sliderPLayers[0];
                _sliderPLayersGame.transform.localScale = Vector3.one;
                for (int i = 0; i < _camImg.Count; i++)
                {
                    _camImg[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < _gameControl._playerMove.Count; i++)
                {
                    _gameControl._playerMove[i]._personMoveCam=true;
                }
            }
            else
            {
                Debug.Log("Personagem 1(Diguinho) precisa ser escolhido");
            }
           
          
            
        }
    }

    public void PersonMovemntStart()// liberar movimento de personagens
    {
        _gameControl._gameStart=true;
    }

   
   
}
