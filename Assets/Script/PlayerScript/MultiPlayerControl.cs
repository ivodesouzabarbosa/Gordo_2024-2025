using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPlayerControl : MonoBehaviour
{
    public List<SelectPerson> _selectsPersonList;
    public List<int> _personSelecNumber = new List<int>();
    void Start()
    {
        
    }

    public void CheckSelecPersonList()
    {
        Debug.Log("CheckSelecPersonList");
        for (int j = 0; j < _selectsPersonList.Count; j++)
        {
            
            _selectsPersonList[j].imgBlockPerson.gameObject.SetActive(true);
        }

    }

   
}
