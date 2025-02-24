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
  //  public PlayerDados playerDados;
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
    public PlayerMove _playerMoveTemp;
    public Button _buttonConf;

    public bool _checkSelect;
    int _tempN;

    public List<GameObject> _personSelec = new List<GameObject>();
    


    //  public List<GameObject> _personSelecTemp = new List<GameObject>();

    public List<Button> _buttonsNav;

    public Transform _btVoltar;
    public Transform _btVoltaSkin;
    public Button _btConfimSkin;
    public Button _btBaixo;
    public Button _btCima;
    public Button _btskin1;
    public Button _btskin2;
    public Transform _camImgPlayer;
    public RawImage _rawImagecam;
    public Transform _pPlayer;

    bool _timeD;

    public SliderPLayer _sliderPLayers;

    public string _nomePLayer;

   

    void Start()
    {

        _pPlayer.GetComponent<InptPLayerControll>().selectPerson = this.GetComponent<SelectPerson>();
        
      //  _btVoltar.localScale = Vector3.zero;
        _btVoltar.gameObject.SetActive(false);
        _gameControl = GameObject.FindWithTag("GameController").GetComponent<GameControl>();


        _gameControl._multiPlayerControl._selectsPersonList.Add(this.GetComponent<SelectPerson>());
      
        numbPerson = _gameControl._playerMove.Count-1;
       
        
            
        
        
      
        SetIndex();
        //Debug.Log("_gameControl._numberPlayer " + _indexPlayer);
        _sliderPLayers = _gameControl._multiPlayerControl._sliderPLayers[_indexPlayer];
       // _sliderPLayers.playerDadosSlider = _gameControl._multiPlayerControl._dadosListPlayer[_indexPlayer];
        _gameControl._multiPlayerControl._sliderPLayersOn.Add(_sliderPLayers);
        _sliderPLayers.gameObject.SetActive(true);
        _gameControl._numberPlayer++;
         transform.SetParent(_gameControl._panelSelectPerson);

        transform.localScale = new Vector3(2,2,2);
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
        PersonImgOn();

       // _btVoltar.localScale = Vector3.zero;
        _btVoltar.gameObject.SetActive(false);

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

        _btskin1.gameObject.SetActive(false);
        _btskin2.gameObject.SetActive(false);
        _btConfimSkin.gameObject.SetActive(false);
        _btVoltaSkin.gameObject.SetActive(false);

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


    public void BtSelectSkink( int Value)
    {
        _playerMoveTemp = _gameControl._playerMove[numbSelectPerson].GetComponent<PlayerMove>();
        if (Value == 0)
        {
            _playerMoveTemp.SkinU();
        }
        else if (Value == 1)
        {
            _playerMoveTemp.SkinD();
        }
    }

    public void BtSelectPlayer(int value)
    {
      
       
        if (value == 0)
        {
            numbSelectPerson++;
        //    _playerMoveTemp._selectTemp = false;
            _tempN = numbSelectPerson;
            if (numbSelectPerson >= numbPerson)
            {
                numbSelectPerson = numbPerson;
                _btBaixo.gameObject.SetActive(false);
                _btCima.Select();
            }
            else
            {
                _btCima.gameObject.SetActive(true);
            }
            PersonImgOn();


        }
        else if (value == 1)
        {
           // _playerMoveTemp._selectTemp = false;
            numbSelectPerson--;
            _tempN = numbSelectPerson;
            if (numbSelectPerson <= 0)
            {
                numbSelectPerson = 0;

                _btCima.gameObject.SetActive(false);
                _btBaixo.Select();
            }
            else
            {
                _btBaixo.gameObject.SetActive(true);
            }
            PersonImgOn();
        }
        else
        {
            if (numbSelectPerson < 0)
            {
                numbSelectPerson = 0;
            }
            _playerMoveTemp = _gameControl._playerMove[numbSelectPerson].GetComponent<PlayerMove>();
           
            if (!_gameControl._multiPlayerControl._checkPersonSel[numbSelectPerson])
            {
                _gameControl._multiPlayerControl._personSelecNumber.Add(numbSelectPerson);
                if (_playerMoveTemp._checkSkin && !_playerMoveTemp._personSeleck)
                {
                    _btskin1.gameObject.SetActive(true);
                    _btskin2.gameObject.SetActive(true);
                    _btConfimSkin.gameObject.SetActive(true);
                    _buttonConf.gameObject.SetActive(false);
                    _btskin1.Select();
                    _sliderPLayers._playerMove = _playerMoveTemp;
                    _playerMoveTemp._personSeleck = true;
                    _btVoltar.gameObject.SetActive(true);
                   // _btVoltar.localScale = Vector3.one; Debug.Log("Animação de aumentar botão de voltar");
                    _gameControl._multiPlayerControl.SetCheckBlock(numbSelectPerson, true);
                    _btBaixo.gameObject.SetActive(false);
                    _btCima.gameObject.SetActive(false);


                    for (int i = 0; i < _gameControl._multiPlayerControl._selectsPersonList.Count; i++)
                    {
                       
                        if (!_gameControl._multiPlayerControl._selectsPersonList[i]._checkSelect)
                        {
                            _gameControl._multiPlayerControl._selectsPersonList[i].imgBlockPerson.gameObject.SetActive(true);
                        }

                        _gameControl._multiPlayerControl._selectsPersonList[i].CamConfirm();
                    }

                    imgBlockPerson.gameObject.SetActive(false);
                    imgSelecPerson.gameObject.SetActive(true);

                    PersonImgOn();


                }
                else
                {
                   
                    ConfirmPerson();

                }
              
            }
            else
            {
                Debug.Log(" Aviso Não pode escolher o personagem");
            }


        }
        if(!imgSelecPerson.gameObject.activeInHierarchy)
        imgBlockPerson.gameObject.SetActive(_gameControl._multiPlayerControl._checkPersonSel[numbSelectPerson]);
        _gameControl._multiPlayerControl.MoveCamMenu(_camImgPlayer, _gameControl, numbSelectPerson);
        _playerMoveTemp = _gameControl._playerMove[numbSelectPerson].GetComponent<PlayerMove>();
       
        
    }

    void TimeBlock()
    {

    }

    public void ConfirmSkin()
    {
        _btskin1.gameObject.SetActive(false);
        _btskin2.gameObject.SetActive(false);
        _btConfimSkin.gameObject.SetActive(false);
        _btVoltaSkin.gameObject.SetActive(true);
        _buttonConf.interactable = false;
        _buttonConf.transform.localScale = Vector3.zero; Debug.Log("Animação de diminuir botão de confirmar");
        _buttonConf.gameObject.SetActive(false);
        _gameControl._multiPlayerControl.SetCheckBlock(numbSelectPerson, true);
        _btVoltaSkin.GetComponent<Button>().Select();
        _btVoltar.gameObject.SetActive(false);
        _btVoltaSkin.gameObject.SetActive(true);
        _btVoltar.gameObject.SetActive(false);
         ConfirmSelec();
        _sliderPLayers._playerMove = _playerMoveTemp;
        _gameControl._multiPlayerControl._numberPersonSel++;

    }
    public void ConfirmPerson()
    {
        if (numbSelectPerson >= 0)
        {
            _gameControl._multiPlayerControl.SetCheckBlock(numbSelectPerson, true);
        }
        ConfirmSelec();
        _btVoltar.gameObject.SetActive(true);
        //_btVoltar.GetComponent<Button>().interactable = true;
        _btVoltar.GetComponent<Button>().Select();
        _buttonConf.gameObject.SetActive(false);
        _btVoltaSkin.gameObject.SetActive(false);
        _sliderPLayers._playerMove = _playerMoveTemp;
        // _btVoltar.localScale = Vector3.one; Debug.Log("Animação de aumentar botão de voltar");
        // _buttonConf.transform.localScale = Vector3.zero; Debug.Log("Animação de diminuir botão de confirmar");


    }

    public void BtVoltarPerson(bool menu)
    {
        if (!_timeD)
        {
            if (numbSelectPerson >= 0)
            {
                _playerMove = _gameControl._playerMove[numbSelectPerson].GetComponent<PlayerMove>();
            }
           
            _timeD = true;
            Debug.Log("Animação de dinimuir botão  de voltar");
            // _sliderPLayers.gameObject.SetActive(false);
            //_pPlayer.SetParent(transform);

            //_gameControl._multiPlayerControl._numberPersonSel--;
            if (_playerMove != null)
            {
                _playerMove.transform.DOMove(_playerMove._posIniMenu, 0.25f);
                _playerMove._selectPersonMove = false;
            }
          
            //  _playerMove.transform.DOMove(_playerMove._posIniMenu, 0.25f);
            _checkSelect = false;
            if (_playerMove != null)
            {
                _playerMove._personSeleck = false;
            }
           
            _buttonConf.interactable = true;
            _buttonConf.transform.localScale = Vector3.one; Debug.Log("Animação de aumentar botão de confirmar");

            _btBaixo.gameObject.SetActive(true);
            _btCima.gameObject.SetActive(true);
            _btConfimSkin.gameObject.SetActive(false);
           // _btCima.gameObject.SetActive(true);
            _buttonConf.gameObject.SetActive(true);
            _btskin1.gameObject.SetActive(false);
            _btskin2.gameObject.SetActive(false);
            _btConfimSkin.gameObject.SetActive(false);
            _btVoltaSkin.gameObject.SetActive(false);
            _sliderPLayers._playerMove = null;
          //  _btBaixo.Select();
            Invoke("TimeVoltar", 0.3f);
        }

      
    }

    public void BtVoltarSkin()
    {
        _playerMove = _gameControl._playerMove[numbSelectPerson].GetComponent<PlayerMove>();
        Debug.Log("Animação de dinimuir botão  de voltar");

        _btskin1.gameObject.SetActive(true);
        _btskin2.gameObject.SetActive(true);
        _btBaixo.gameObject.SetActive(false);
        _btCima.gameObject.SetActive(false);
        _btConfimSkin.gameObject.SetActive(true);
        _btVoltaSkin.gameObject.SetActive(false);
        _btVoltar.gameObject.SetActive(true);
        _btskin1.Select();
        _checkSelect = false;

       // _gameControl._multiPlayerControl._numberPersonSel--;
        //_pPlayer.SetParent(transform);
        //  _gameControl._multiPlayerControl._numberPersonSel--;
        //   _playerMove.transform.DOMove(_playerMove._posIniMenu, 0.25f);
        //  _playerMove._selectPersonMove = false;
        //  _playerMove.transform.DOMove(_playerMove._posIniMenu, 0.25f);
        //   _checkSelect = false;
        //  _playerMove._personSeleck = false;
        //  Invoke("TimeVoltar", 0.3f);
    }


    public void VoltarCam()
    {
        _gameControl._multiPlayerControl.MoveCamMenu(_camImgPlayer, _gameControl, numbSelectPerson);
       

    }
    void TimeVoltar()
    {
        if (numbSelectPerson >= 0)
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

            for (int i = 0; i < _gameControl._multiPlayerControl._personSelecNumber.Count; i++)
            {
                if (_gameControl._multiPlayerControl._personSelecNumber[i] == numbSelectPerson)
                {
                    // Debug.Log("r " + _gameControl._multiPlayerControl._personSelecNumber[i]);
                    _gameControl._multiPlayerControl._personSelecNumber.Remove(_gameControl._multiPlayerControl._personSelecNumber[i]);

                    _gameControl._multiPlayerControl._numberPersonSel--;
                    numbSelectPerson = -1;
                }
            }



;
            // _gameControl._multiPlayerControl.CheckListFree(imgBlockPerson.gameObject, numbSelectPerson);
            //_playerMove = null;
            _timeD = false;
            PersonImgOn();



            imgBlockPerson.gameObject.SetActive(false);
            imgSelecPerson.gameObject.SetActive(false);
            _btVoltar.gameObject.SetActive(false);
            //_btVoltar.localScale = Vector3.zero; Debug.Log("Animação de diminuir botão de voltar");
            _buttonConf.interactable = true;
            _buttonConf.transform.localScale = Vector3.one; Debug.Log("Animação de aumentar botão de confirmar");
            _buttonConf.gameObject.SetActive(true);
        }
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

        if (!_playerMove._checkSkin)
        {
            _gameControl._multiPlayerControl._numberPersonSel++;
           
        }
       // _gameControl._multiPlayerControl._personSelecNumber.Add(numbSelectPerson);

       

        _sliderPLayers._playerMove = _playerMove;

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
      //  _gameControl._multiPlayerControl._numberPersonSel++;
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
            if(numbSelectPerson>=0)
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
    public void SetAtack(InputAction.CallbackContext value)
    {
        if(_checkSelect && _playerMove != null && _gameControl._gameStart)
        {
            _playerMove.SetAtack(value);
            Debug.Log("ataque");
         
        }
            
    }
    public void SetLuva(InputAction.CallbackContext value)
    {
        if (_checkSelect && _playerMove != null && _gameControl._gameStart)
        {
            _playerMove.SetLuva(value);

        }

    }
    public void SetJogarObj(InputAction.CallbackContext value)
    {
        if (_checkSelect && _playerMove != null && _gameControl._gameStart)
        {
            _playerMove.SetJogarObj(value);

        }

    }




}
