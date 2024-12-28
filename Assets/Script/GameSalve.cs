using TMPro;
using UnityEngine;

public class GameSalve : MonoBehaviour
{
    public int _numbPlayers;
    public int _numbPlayerVivos;//chegaram no final Vivos

    public string _namePlayer1;
    public string _namePlayer2;
    public string _namePlayer3;
    public string _namePlayer4;

    public string _apelidoPlayer1;
    public string _apelidoPlayer2;
    public string _apelidoPlayer3;
    public string _apelidoPlayer4;

    int _playerEnemey1=0;
    int _playerEnemey2=0;
    int _playerEnemey3=0;
    int _playerEnemey4=0;


    public int NumbPlayers
    {
        get => _numbPlayers;
        set
        {
            PlayerPrefs.SetInt("_numbPlayer", value);
        }
    }
    public string NamePlayer1
    {
        get => _namePlayer1;
        set
        {
            PlayerPrefs.SetString("_namePlayer1", value);
        }
    }
    public string NamePlayer2
    {
        get => _namePlayer2;
        set
        {
            PlayerPrefs.SetString("_namePlayer2", value);
        }
    }
    public string NamePlayer3
    {
        get => _namePlayer3;
        set
        {
            PlayerPrefs.SetString("_namePlayer3", value);
        }
    }
    public string NamePlayer4
    {
        get => _namePlayer4;
        set
        {
            PlayerPrefs.SetString("_namePlayer4", value);
        }
    }

    //----------------------------------------------------------------

    public string ApelidoPlayer1
    {
        get => _apelidoPlayer1;
        set
        {
            PlayerPrefs.SetString("_apelidoPlayer1", value);
        }
    }
    public string ApelidoPlayer2
    {
        get => _apelidoPlayer2;
        set
        {
            PlayerPrefs.SetString("_apelidoPlayer2", value);
        }
    }
    public string ApelidoPlayer3
    {
        get => _apelidoPlayer3;
        set
        {
            PlayerPrefs.SetString("_apelidoPlayer3", value);
        }
    }
    public string ApelidoPlayer4
    {
        get => _apelidoPlayer1;
        set
        {
            PlayerPrefs.SetString("_apelidoPlayer4", value);
        }
    }

    //----------------------------------------------------------------


    public int PlayerEnemey1
    {
        get => _playerEnemey1;
        set
        {
            PlayerPrefs.SetInt("_playerEnemey1", value);
        }
    }
    public int PlayerEnemey2
    {
        get => _playerEnemey2;
        set
        {
            PlayerPrefs.SetInt("_playerEnemey2", value);
        }
    }
    public int PlayerEnemey3
    {
        get => _playerEnemey3;
        set
        {
            PlayerPrefs.SetInt("_playerEnemey3", value);
        }
    }
    public int PlayerEnemey4
    {
        get => _playerEnemey4;
        set
        {
            PlayerPrefs.SetInt("_playerEnemey4", value);
        }
    }
    
    //-----------------------------------------------------------------------------


    public void PlayerNome(int value, TextMeshProUGUI _nomePLayer, TextMeshProUGUI _apelidoPLayer)
    {
       
        switch (value)
        {
            case 0:
                NamePlayer1 = _nomePLayer.text;
                ApelidoPlayer1 = _apelidoPLayer.text;
                break;
            case 1:
                NamePlayer2 = _nomePLayer.text;
                ApelidoPlayer2 = _apelidoPLayer.text;
                break;
            case 2:
                NamePlayer3 = _nomePLayer.text;
                ApelidoPlayer3 = _apelidoPLayer.text;
                break;
            case 3:
                NamePlayer4 = _nomePLayer.text;
                ApelidoPlayer4 = _apelidoPLayer.text;
                break;
        }
    }

    //-------------------------------------------------------------------------------

    public void PlayerEnemeys(int value, TextMeshProUGUI textName)
    {
        Debug.Log("SOM inimigo morto ponto");
        switch (value)
        {
            case 0:
                _playerEnemey1++;
                textName.text = "" + _playerEnemey1;
                PlayerEnemey1 = _playerEnemey1;
                break;
            case 1:
                _playerEnemey2++;
                textName.text = "" + PlayerEnemey2;
                PlayerEnemey2 = _playerEnemey2;
                break;
            case 2:
                _playerEnemey3++;
                textName.text = "" + PlayerEnemey3;
                PlayerEnemey3 = _playerEnemey3;
                break;
            case 3:
                _playerEnemey4++;
                textName.text = "" + PlayerEnemey4;
                PlayerEnemey4 = _playerEnemey4;
                break;
        }
    }

}
