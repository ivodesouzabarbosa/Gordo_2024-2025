using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPlayerControl : MonoBehaviour
{
  //  public List<Transform> _personMenu=new List<Transform>();
    public List<SelectPerson> _selectsPersonList=new List<SelectPerson>();
    public List<SelectPerson> _selectsPersonListFree = new List<SelectPerson>();
    public List<SelectPerson> _selectsPersonListBlock = new List<SelectPerson>();
    public List<int> _personSelecNumber = new List<int>();
   

    public void BlockOnBig()
    {
        for (int i = 0; i < _selectsPersonList.Count; i++)
        {
            _selectsPersonList[i].BlockOnList();
        }
    }

    public void BlockOffBig()
    {
        for (int i = 0; i < _selectsPersonList.Count; i++)
        {
            _selectsPersonList[i].BlockOffList();
        }

    }
    public void CheckListFree(GameObject value)
    {
        for (int i = 0; i < _selectsPersonListBlock.Count; i++)
        {
            _selectsPersonListBlock[i].imgBlockPerson.gameObject.SetActive(false);
        }
        value.gameObject.SetActive(false);
   
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
