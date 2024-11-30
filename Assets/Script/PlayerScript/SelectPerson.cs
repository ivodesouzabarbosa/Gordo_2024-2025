using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SelectPerson : MonoBehaviour
{
    public int _indexPlayer;
    GameControl _gameControl;
    [SerializeField] Button _buttonSelect;
    public PlayerInput playerInput;
    public Transform _ordT;

    public int numbPerson;
    public int numbSelectPerson;
    public Image imgBlockPerson;
    public Image imgSelecPerson;
    public Image imgFreePerson;

    public PlayerMove _playerMove;
    public Button _buttonConf;

    public bool _checkSelect;
    int _tempN;

    public List<GameObject> _personSelec = new List<GameObject>();
    


    //  public List<GameObject> _personSelecTemp = new List<GameObject>();

    public List<Button> _buttonsNav;

    public Transform _btVoltar;
    public Button _btBaixo;
    public Button _btCima;
    public Transform _camImgPlayer;
    public RawImage _rawImagecam;
    public Transform _pPlayer;

    bool _timeD;

    public SliderPLayer _sliderPLayers;

    public string _nomePLayer;

   

    void Start()
    {

        _pPlayer.GetComponent<InptPLayerControll>().selectPerson = this.GetComponent<SelectPerson>();

        _btVoltar.localScale = Vector3.zero; 
        _gameControl = GameObject.FindWithTag("GameController").GetComponent<GameControl>();


        _gameControl._multiPlayerControl._selectsPersonList.Add(this.GetComponent<SelectPerson>());
      
        numbPerson = _gameControl._playerMove.Count-1;
       
        
            
        
        
      
        SetIndex();
        Debug.Log("_gameControl._numberPlayer " + _indexPlayer);
        _sliderPLayers = _gameControl._multiPlayerControl._sliderPLayers[_indexPlayer];
        _gameControl._multiPlayerControl._sliderPLayersOn.Add(this.GetComponent<SliderPLayer>());
        _sliderPLayers.gameObject.SetActive(true);
        _gameControl._numberPlayer++;
         transform.SetParent(_gameControl._panelSelectPerson);

        transform.localScale = new Vector3(2,2,2);
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
        PersonImgOn();

        _btVoltar.localScale = Vector3.zero;

        //  _gameControl._multiPlayerControl.CheckListBloc(imgBlockPerson.gameObject, numbSelectPerson, true);

        imgBlockPerson.gameObject.SetActive(_gameControl._multiPlayerControl._checkPersonSel[numbSelectPerson]);
        _gameControl._multiPlayerControl.MoveCamMenu(_camImgPlayer, _gameControl, numbSelectPerson);

        _pPlayer.SetParent(_gameControl._multiPlayerControl._PlayerGame);

        // sourceObject = _gameControl._playerInputs[0].gameObject;
        // CopiarPlayer();
        // _gameControl._playerInputs[0].gameObject.SetActive(true);
        _nomePLayer = "Player " + (playerInput.playerIndex+1);// nomear o player
        _pPlayer.GetComponent<InptPLayerControll>()._nomePLayerMenu.text = _nomePLayer;//(_nomePLayer);
        _sliderPLayers.SetNomePlayer(_nomePLayer);



    }

    public void SetIndex()
    {
        _indexPlayer = _pPlayer.GetComponent<PlayerInput>().playerIndex;//_gameControl._numberPlayer;
        _camImgPlayer = _gameControl._multiPlayerControl._camImg[_indexPlayer];
        _rawImagecam.texture = _gameControl._multiPlayerControl._TextImg[_indexPlayer];
       
        

    }

    public void ChechPersonBlock()
    {
       // imgBlockPerson.gameObject.SetActive(true);
        for (int i = 0; i < _gameControl._multiPlayerControl._selectsPersonList.Count; i++)
        {
            _gameControl._multiPlayerControl._selectsPersonList[i].imgBlockPerson.gameObject.SetActive(false);
        }

        for (int i = 0; i < _gameControl._multiPlayerControl._personSelecNumber.Count; i++)
        {
            if (_gameControl._multiPlayerControl._personSelecNumber[i] == numbSelectPerson)
            {
                imgBlockPerson.gameObject.SetActive(true);
            }
        }
    }

    public void BtSelectPlayer(int value)
    {

        if (value == 0)
        {
            numbSelectPerson++;
            _tempN = numbSelectPerson;
            if (numbSelectPerson >= numbPerson)
            {
                numbSelectPerson = numbPerson;
                _btBaixo.interactable = false;
                _btCima.Select();
            }
            else
            {
                _btCima.interactable = true;
            }
            PersonImgOn();


        }
        else if (value == 1)
        {
            numbSelectPerson--;
            _tempN = numbSelectPerson;
            if (numbSelectPerson <= 0)
            {
                numbSelectPerson = 0;
                _btCima.interactable = false;
                _btBaixo.Select();
            }
            else
            {
                _btBaixo.interactable = true;
            }
            PersonImgOn();
        }
        else
        {
            if (!_gameControl._multiPlayerControl._checkPersonSel[numbSelectPerson])
            {
                _gameControl._multiPlayerControl.SetCheckBlock(numbSelectPerson, true);
                ConfirmSelec();
               
                _btVoltar.GetComponent<Button>().Select();
                
                _btVoltar.localScale = Vector3.one; Debug.Log("Animação de aumentar botão de voltar");
                _buttonConf.transform.localScale = Vector3.zero; Debug.Log("Animação de diminuir botão de confirmar");
            }
            else
            {
                Debug.Log(" Aviso Não pode escolher o personagem");
            }


        }
        if(!imgSelecPerson.gameObject.activeInHierarchy)
        imgBlockPerson.gameObject.SetActive(_gameControl._multiPlayerControl._checkPersonSel[numbSelectPerson]);
        _gameControl._multiPlayerControl.MoveCamMenu(_camImgPlayer, _gameControl, numbSelectPerson);

    }

    public void BtVoltar()
    {
        if (!_timeD)
        {
            _timeD = true;
            Debug.Log("Animação de dinimuir botão  de voltar");
            _sliderPLayers.gameObject.SetActive(false);
            //_pPlayer.SetParent(transform);
            _gameControl._multiPlayerControl._numberPersonSel--;
            _playerMove.transform.DOMove(_playerMove._posIniMenu, 0.25f);
            _playerMove._selectPersonMove = false;
            //  _playerMove.transform.DOMove(_playerMove._posIniMenu, 0.25f);
            _checkSelect = false;

            _buttonConf.Select();
            Invoke("TimeVoltar", 0.3f);
        }

      
    }


    public void VoltarCam()
    {
        _gameControl._multiPlayerControl.MoveCamMenu(_camImgPlayer, _gameControl, numbSelectPerson);
       

    }
    void TimeVoltar()
    {
        _gameControl._multiPlayerControl.SetCheckBlock(numbSelectPerson, false);
        for (int i = 0; i < _gameControl._multiPlayerControl._selectsPersonList.Count; i++)
        {
            bool chck = _gameControl._multiPlayerControl._checkPersonSel[i];
            if (!_gameControl._multiPlayerControl._selectsPersonList[i]._checkSelect && !chck)
            {
                _gameControl._multiPlayerControl._selectsPersonList[i].imgBlockPerson.gameObject.SetActive(false);
            }
            _gameControl._multiPlayerControl._selectsPersonList[i].VoltarCam();
        }

        for (int i = 0; i < _buttonsNav.Count; i++)
        {
            _buttonsNav[i].interactable = true;
        }
        imgSelecPerson.gameObject.SetActive(false);


        // _gameControl._playerMove[numbSelectPerson].gameObject.SetActive(false);

        _gameControl._multiPlayerControl._personSelecNumber.Remove(numbSelectPerson);
;
        // _gameControl._multiPlayerControl.CheckListFree(imgBlockPerson.gameObject, numbSelectPerson);
        //_playerMove = null;
        _timeD = false;
        PersonImgOn();

       

        imgBlockPerson.gameObject.SetActive(false);
        imgSelecPerson.gameObject.SetActive(false);
        _btVoltar.localScale = Vector3.zero; Debug.Log("Animação de diminuir botão de voltar");
        _buttonConf.transform.localScale = Vector3.one; Debug.Log("Animação de aumentar botão de confirmar");
    }

    public void CamConfirm()
    {
        _gameControl._multiPlayerControl.MoveCamMenu(_camImgPlayer, _gameControl, numbSelectPerson);
    }

    void ConfirmSelec()
    {
        _checkSelect = true;

        //_gameControl._playerMove[numbSelectPerson].gameObject.SetActive(true); ----             // ativar personagem
        _playerMove = _gameControl._playerMove[numbSelectPerson].GetComponent<PlayerMove>();
        _playerMove._selectPerson=this.GetComponent<SelectPerson>();
        _playerMove._selectPersonMove = true;
        _playerMove.transform.position = _gameControl._basePlayer[_indexPlayer].position;
        _playerMove.transform.DOMove(_gameControl._basePlayer[_indexPlayer].position, 0.25f);
        _gameControl._multiPlayerControl.MoveCamMenu(_camImgPlayer, _gameControl, numbSelectPerson);
        _gameControl._multiPlayerControl._personSelecNumber.Add(numbSelectPerson);

        for (int i = 0; i < _gameControl._multiPlayerControl._selectsPersonList.Count; i++)
        {
            bool chck = _gameControl._multiPlayerControl._checkPersonSel[i];
            if (!_gameControl._multiPlayerControl._selectsPersonList[i]._checkSelect && chck)
            {
                _gameControl._multiPlayerControl._selectsPersonList[i].imgBlockPerson.gameObject.SetActive(true);
            }
           
            _gameControl._multiPlayerControl._selectsPersonList[i].CamConfirm();
        }
  
        imgBlockPerson.gameObject.SetActive(false);
        imgSelecPerson.gameObject.SetActive(true);
        // _gameControl._multiPlayerControl.CheckListBloc(imgBlockPerson.gameObject, numbSelectPerson);

        for (int j = 0; j < _buttonsNav.Count; j++)
        {
            _buttonsNav[j].interactable = false;
        }
        //_gameControl._multiPlayerControl.CheckBlockPersonMenu(playerInput.playerIndex);
        //ativar botão de voltar;
        PersonImgOn();

        //  _gameControl._multiPlayerControl.CheckSelecPersonList();
        _gameControl._multiPlayerControl._numberPersonSel++;
        _gameControl._multiPlayerControl.CheckIniGame(_indexPlayer);

    }

    public void PersonImgOn()
    {
       // ChechPersonBlock();
        if (_tempN < 0 || _tempN > _personSelec.Count-1)
        {

        }
        else
        {
            for (int i = 0; i < _personSelec.Count; i++)
            {
                _personSelec[i].SetActive(false);
              

            }
            _personSelec[numbSelectPerson].SetActive(true);
          


        }

    }
    public void SetMove(InputAction.CallbackContext value)
    {
        if(_checkSelect && _playerMove!=null && _gameControl._gameStart)
        {
            _playerMove.SetMove(value);
        }
    }
    public void SetJump(InputAction.CallbackContext value)
    {
        //_checkJump = true;
    }

    

    
}
