using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDados", menuName = "Scriptable Objects/PlayerDados")]
public class PlayerDados : ScriptableObject
{
    public int _numberEnemy;



    public int NumberEnemy
    {
        get
        {
            return _numberEnemy;

        }
        set
        {
            _numberEnemy = value;
        }
    }

}
