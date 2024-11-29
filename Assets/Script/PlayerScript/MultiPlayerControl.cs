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


    private void Start()
    {
        _gameControl = GameObject.FindWithTag("GameController").GetComponent<GameControl>();
        for (int i = 0; i < _gameControl._playerMove.Count; i++)
        {
            _checkPersonSel.Add(false);
        }
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
        if (_numberPersonSel == _gameControl._numberPlayer)
        {
            Debug.Log("COMEÇAR GAME");
            PersonMovemntStart();
            _menuGame.SetActive(false);
            
        }
    }

    public void PersonMovemntStart()// liberar movimento de personagens
    {
        _gameControl._gameStart=true;
    }

   
   
}
