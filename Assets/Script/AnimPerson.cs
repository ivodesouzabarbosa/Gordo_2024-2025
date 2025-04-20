using UnityEngine;

public class AnimPerson : MonoBehaviour
{
    [SerializeField] PlayerMove _playerMove;
    void Start()
    {
        _playerMove = transform.parent.parent.GetComponent<PlayerMove>();
    }
    public void AnimPegarItem()
    {
       // _playerMove._nunbAtaque = 0;
        _playerMove._playerControl._boxRaycast.ObjMove();
        if (!_playerMove._maoMiliControl._objMili._objMove)
        {
            _playerMove._maoMiliControl._objMili.atackMiliBox.SetActive(false);
        }
        _playerMove._maoMiliControl._objMili._miliCollider.enabled =false;
    }

    public void AnimPegarfalse()
    {
        _playerMove._pegarMili = false;
        _playerMove._stopPerson = false;
        _playerMove.IniciarTransicao();
        _playerMove._nunbAtaque = 0;
    }
    public void Atack_0()
    {
        _playerMove._nunbAtaque = 0;
       

    }

    public void IniciarTransicao()
    {
        _playerMove.IniciarTransicao();
    }

    public void JogarOBJ()
    {
        _playerMove._maoMiliControl._objMili.isLaunched = true;
        _playerMove._maoMiliControl._objMili.atackMiliBox.SetActive(true);
        _playerMove._maoMiliControl._objMili = null;
        _playerMove._pegarMili = false;
        _playerMove._stopPerson = false;
        _playerMove.IniciarTransicao();
        _playerMove._nunbAtaque = 0;
        _playerMove._maoOcupada = false;
      
    }

   public void AtackMiliON()
    {
        if (!_playerMove._maoMiliControl._objMili._objMove)
        {
            _playerMove._maoMiliControl._objMili.atackMiliBox.SetActive(true);
        }
    }
    public void AtackMiliOff()
    {
        if (!_playerMove._maoMiliControl._objMili._objMove)
        {
            _playerMove._maoMiliControl._objMili.atackMiliBox.SetActive(false);
        }
    }
}
