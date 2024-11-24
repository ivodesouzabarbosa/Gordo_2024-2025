using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GroundControl : MonoBehaviour
{
    public Transform _groundBase;
    Transform _groundTemp;
    [SerializeField] Vector3 _distance;
    public List<GameObject> _groundList;
    public int _indexG;
    public int _inter1n;
    public int _inter2n;
    void Start()
    {
        StartCoroutine(TimeGround());
       
    }

   

    IEnumerator TimeGround()
    {
        _groundTemp=_groundBase.transform;
        yield return new WaitForSeconds(0.5f);
        _inter1n = _groundList.Count / 3;
        _inter2n = _inter1n + _inter1n;
       // Debug.Log(_groundList.Count);
        for (int i = 0; i < _groundList.Count; i++)
        {
          
            if (i < _groundList.Count)
            {
                yield return new WaitForSeconds(0.5f);
                GameObject bullet = GroundPool._groundPool.GetPooledObject();
                if (bullet != null)
                {
                    bullet.transform.position = _groundTemp.position + _distance;
                    bullet.SetActive(true);
                    _groundTemp = bullet.transform;
                    _groundTemp.GetComponent<GroundPref>().CheckGroundOn(false);
                }
            }
           
        }
    }
}
