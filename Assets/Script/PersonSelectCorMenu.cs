using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonSelectCorMenu : MonoBehaviour
{
    public GameObject _imgBlock;  
    public void BlockOn(bool value)
    {
        _imgBlock.SetActive(value);
    }
}
