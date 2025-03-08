using UnityEngine;

public class MaoMiliControl : MonoBehaviour
{
    public Transform _m_transform;
    public ObjMili _objMili;
    public PlayerMove _playerMove;

    private void Start()
    {
        _playerMove = _m_transform.parent.GetComponent<PlayerMove>();
    }
}
