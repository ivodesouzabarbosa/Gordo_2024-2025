using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SliderPLayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject _imgDescGame;
    public TextMeshProUGUI _textPlayerGame;
    public Transform _iniTransf;
    public Transform _circleMove;

    SpriteRenderer _spriteRenderer;
    public PlayerDados playerDadosSlider;
    public TextMeshProUGUI _text_numberEnemy;

    private void Start()
    {
     //  _circleMove.transform.localScale = Vector3.zero;
     ///   _spriteRenderer = _circleMove.GetComponent<SpriteRenderer>();

    }

    public void SetNomePlayer(string nomePLayer)
    {

        _textPlayerGame.text = nomePLayer;

    }

   

    public void HitMove(Transform _enemyHit,Material material)
    {
     //   StopCoroutine(HitMoveTime(_enemyHit, material));
        StartCoroutine(HitMoveTime(_enemyHit, material));
    }

    IEnumerator HitMoveTime(Transform _enemyHit, Material material)
    {
        //_circleMove.SetParent(_enemyHit);
        GameObject bullet = MovePoint._movePoint.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.SetParent(_iniTransf);
            bullet.SetActive(true);
            _spriteRenderer = bullet.GetComponent<SpriteRenderer>();
            playerDadosSlider.NumberEnemy++;
            bullet.transform.localScale= new Vector3(50,50,50);

            _spriteRenderer.enabled = true;
            _spriteRenderer.material = material;
            bullet.transform.position = _enemyHit.position;
            bullet.transform.DOLocalMove(Vector3.zero, 1f);
            yield return new WaitForSeconds(1.1f);
           _spriteRenderer.enabled = false;
           _text_numberEnemy.text = "" + playerDadosSlider.NumberEnemy;
            yield return new WaitForSeconds(.1f);
            bullet.SetActive(false);
        }
    }
  

}
