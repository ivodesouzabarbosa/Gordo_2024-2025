using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SliderPLayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject _imgDescGame;
    public TextMeshProUGUI _textPlayerGame;

    public void SetNomePlayer(string nomePLayer)
    {

        _textPlayerGame.text = nomePLayer;

    }

}
