using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public int _indexPlayer; 
    public GameControl _gameControl;
    
    void Start()
    {
        _gameControl = GameObject.FindWithTag("GameController").GetComponent<GameControl>();
        _indexPlayer = _gameControl._numbPlayer;
        _gameControl._playerCamT[_indexPlayer] = false;
        _gameControl._numbPlayer++;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CamTriguer"))
        {
            _gameControl._playerCamT[_indexPlayer] = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CamTriguer"))
        {
            _gameControl._playerCamT[_indexPlayer] = false;
        }
    }
}
