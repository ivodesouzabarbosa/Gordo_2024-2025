using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPref : MonoBehaviour
{
    public GameControl _gameControl;
    public List<GameObject> _groundListLevel;
    public List<GameObject> _interList;
    public int _index;
    public int _level;
    public GameObject _groundBaseCool;

  

    public void CheckGroundOn(bool CheckFimGround)
    {
        _gameControl = GameObject.FindWithTag("GameController").GetComponent<GameControl>();
        _index = _gameControl._groundControl._indexG;
        _gameControl._groundControl._indexG++;
        SetLevelGround();

        if (_gameControl._groundControl._inter1n == _index)//fim do level 1
        {
            _interList[0].SetActive(true);
            _groundBaseCool.SetActive(false);
        }
        else if (_gameControl._groundControl._inter2n == _index)// fim do level 2
        {
            _interList[1].SetActive(true);
            _groundBaseCool.SetActive(false);
        }
        else if (_gameControl._groundControl._groundList.Count - 1 == _index)// inicio do level 3 e volta sem pausar o game
        {
            _interList[2].SetActive(true);
            _groundBaseCool.SetActive(false);
        }
        else
        {
          
            GroundLevel(_level);
        }
    
    }
    void GroundLevel(int level)
    {
        if (level == 2)
        {
            _groundListLevel[0].SetActive(true);
        }
        else if (level == 3)
        {
            _groundListLevel[1].SetActive(true);
        }
        else
        {
            _groundListLevel[2].SetActive(true);
        }
    }

    void SetLevelGround()
    {

        if (_index <= _gameControl._groundControl._inter1n)
        {
            _level = 2;
        }
        else if (_index <= _gameControl._groundControl._inter2n)
        {
            _level = 3;
        }
        else 
        {
            _level = 4;
        }
    }



}
