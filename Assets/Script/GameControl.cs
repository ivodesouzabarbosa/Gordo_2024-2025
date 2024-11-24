using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public PlayerMove _playerMove;
    public GroundControl _groundControl;
    public CamPlayer _camPlayer;
    public EnemyBaseControl _enemyBaseControl;
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
    public bool[] _playerCamT;

    private void Start()
    {
        _BlockList[0].SetActive(false);
        _BlockList[1].SetActive(true);
        Physics.IgnoreLayerCollision(7, 6);
        Physics.IgnoreLayerCollision(3, 8);
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
        _player = targets[0];
        _checkCamOn= true;
    }
}
