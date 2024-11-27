using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiPlayerControl : MonoBehaviour
{
  //  public List<Transform> _personMenu=new List<Transform>();
    public List<SelectPerson> _selectsPersonList=new List<SelectPerson>();
    public List<bool> _checkPersonSel = new List<bool>();
    public List<int> _personSelecNumber = new List<int>();
    GameControl _gameControl;



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

   
}
