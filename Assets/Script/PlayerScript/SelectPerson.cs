using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SelectPerson : MonoBehaviour
{
    [SerializeField] int _indexPlayer;
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

    public bool _checkSelect;
    int _tempN;

    public List<GameObject> _personSelec = new List<GameObject>();
    public List<GameObject> _personSelecTemp = new List<GameObject>();

    public List<Button> _buttonsNav;

    private void Awake()
    {
        for (int i = 0; i < _personSelec.Count; i++)
        {
            _personSelecTemp.Add(_personSelec[i]);
        }
      

    }
    void Start()
    {
        _gameControl = GameObject.FindWithTag("GameController").GetComponent<GameControl>();
        _gameControl._multiPlayerControl._selectsPersonList.Add(this.GetComponent<SelectPerson>());
        numbPerson = _gameControl._playerMove.Count-1;
        _indexPlayer = _gameControl._numberPlayer;
        _gameControl._numberPlayer++;
        transform.SetParent(_gameControl._panelSelectPerson);

        transform.localScale = new Vector3(2,2,2);
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;

        PersonImgOn();

       // sourceObject = _gameControl._playerInputs[0].gameObject;
       // CopiarPlayer();
       // _gameControl._playerInputs[0].gameObject.SetActive(true);
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
       
        if (!_checkSelect)
        {

            if (value == 0)
            {
                numbSelectPerson++;
                _tempN = numbSelectPerson;
                if (numbSelectPerson >= numbPerson)
                {
                    numbSelectPerson = numbPerson;

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
                }
                PersonImgOn();
            }
            else if (!imgBlockPerson.gameObject.activeInHierarchy)//persosnagem selecioando
            {
                _checkSelect = true;
                _gameControl._playerMove[numbSelectPerson].gameObject.SetActive(true);
                _playerMove = _gameControl._playerMove[numbSelectPerson].GetComponent<PlayerMove>();
                imgSelecPerson.gameObject.SetActive(true);
                _gameControl._multiPlayerControl._personSelecNumber.Add(numbSelectPerson);

                for (int i = 0; i < _buttonsNav.Count; i++)
                {
                    _buttonsNav[i].interactable = false;
                }
                //_gameControl._multiPlayerControl.CheckBlockPersonMenu(playerInput.playerIndex);
                //ativar bot�o de voltar;
                PersonImgOn();
                _gameControl._multiPlayerControl.CheckSelecPersonList();

            }
            else
            {
                Debug.Log(" Aviso N�o pode escolher o personagem");
            }
        }
        else
        {
            for (int i = 0; i < _buttonsNav.Count; i++)
            {
                _buttonsNav[i].interactable = true;
            }
            imgSelecPerson.gameObject.SetActive(false);
            for (int i = 0; i < _gameControl._multiPlayerControl._personSelecNumber.Count; i++)
            {
                if (_gameControl._multiPlayerControl._personSelecNumber[i]== numbSelectPerson)
                {
                    _gameControl._multiPlayerControl._personSelecNumber.Remove(_gameControl._multiPlayerControl._personSelecNumber[i]);
                }
            }
            _gameControl._playerMove[numbSelectPerson].gameObject.SetActive(false);
            _playerMove = null;
            _checkSelect = false;
            PersonImgOn();
         // _gameControl._multiPlayerControl.CheckSelecPersonList();

        }

    }

    public void PersonImgOn()
    {
        ChechPersonBlock();
        if (_tempN < 0 || _tempN > _personSelecTemp.Count-1)
        {

        }
        else
        {
            for (int i = 0; i < _personSelecTemp.Count; i++)
            {
                _personSelecTemp[i].SetActive(false);
            }
            _personSelecTemp[numbSelectPerson].SetActive(true);
           
        }

    }
    public void SetMove(InputAction.CallbackContext value)
    {
        if(_checkSelect && _playerMove!=null)
        {
            _playerMove.SetMove(value);
        }
    }
    public void SetJump(InputAction.CallbackContext value)
    {
        //_checkJump = true;
    }

   

}
