using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameControl : MonoBehaviour
{
    public GroundControl _groundControl;
    public bool _gameStart;
    public CamPlayer _camPlayer;
    public EnemyBaseControl _enemyBaseControl;
    public MultiPlayerControl _multiPlayerControl;
    public Transform _playerBase;
    public GameObject _coolFimFase;

    public List<Transform> targets = new List<Transform>(); // Lista de
    public Transform _player;
    public bool _checkCamOn;
    public List<GameObject> _BlockList;

    public int _levelOn;

    public int _numbPlayer;
    public bool _isDirIvert;
    public bool _isDir;
    //public bool[] _playerCamT;

    public Transform _panelSelectPerson;
    public int _numberPlayer;

    public List<PlayerMove> _playerMove = new List<PlayerMove>();
    public List<PlayerMove> _playerMoveSkinDig = new List<PlayerMove>();
    public List<Transform> _basePlayer = new List<Transform>();
    public Transform _canvasEnemy;

    private void Awake()
    {
        _BlockList[0].SetActive(false);
        _BlockList[1].SetActive(true);
        _multiPlayerControl = GetComponent<MultiPlayerControl>();
        Physics.IgnoreLayerCollision(7, 6);
        Physics.IgnoreLayerCollision(3, 8);
        Physics.IgnoreLayerCollision(3, 9);
    }
   
     public void BlockFim()
    {
        _BlockList[0].SetActive(true);
        _BlockList[1].SetActive(false);
    }

    public void ContPLayer()
    {
        GameObject[] targetObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject obj in targetObjects)
        {
            targets.Add(obj.transform);
        }
        //_player = targets[0];
        _checkCamOn= true;
    }

    
}
