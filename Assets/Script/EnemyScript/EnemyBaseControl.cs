using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBaseControl : MonoBehaviour
{
    public List<Transform> _posListBase1 = new List<Transform>();

    int _numbList1;
    public List<Transform> _posListBase2 = new List<Transform>();
    int _numbList2;

    public List<GameObject> _listEnemy1;
    public List<GameObject> _listEnemy2;
    public List<GameObject> _listEnemy3;
    public List<GameObject> _listEnemy4;
    public List<GameObject> _listEnemy5;
    public List<GameObject> _listEnemy6;

    public float countdownTime = 10f; // Tempo inicial do contador em segundos
    private float currentTime;
    GameControl _gameControl;
    bool _checkPosini;


    private void Awake()
    {
        _gameControl = GameObject.FindWithTag("GameController").GetComponent<GameControl>();
    }

    void Start()
    {
        currentTime = countdownTime;
        Shuffle(_posListBase1);

    }
    void Update()
    {
        // Reduz o tempo enquanto ele for maior que zero
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime; // Decremento com base no tempo real
            currentTime = Mathf.Max(currentTime, 0); // Evita valores negativos
        }
        else
        {
            // Ação quando o tempo chega a zero
            TimerEnded(_gameControl._levelOn);
        }
    }


    void TimerEnded(int level)
    {
        currentTime = countdownTime;
            if (level == 0)
            {
                InvokeEnemey(EnemeyPool1._enemeyPool1.GetPooledObject());
                InvokeEnemey(EnemeyPool2._enemeyPool2.GetPooledObject());
                InvokeEnemey(EnemeyPool3._enemeyPool3.GetPooledObject());
            }
            else if (level == 1)
            {
                InvokeEnemey(EnemeyPool1._enemeyPool1.GetPooledObject());
                InvokeEnemey(EnemeyPool2._enemeyPool2.GetPooledObject());
                InvokeEnemey(EnemeyPool3._enemeyPool3.GetPooledObject());
                InvokeEnemey(EnemeyPool4._enemeyPool4.GetPooledObject());

            }
            else if (level == 2)
            {
                InvokeEnemey(EnemeyPool1._enemeyPool1.GetPooledObject());
                InvokeEnemey(EnemeyPool2._enemeyPool2.GetPooledObject());
                InvokeEnemey(EnemeyPool3._enemeyPool3.GetPooledObject());
                InvokeEnemey(EnemeyPool4._enemeyPool4.GetPooledObject());
                InvokeEnemey(EnemeyPool5._enemeyPool5.GetPooledObject());
            }
            else if (level == 3)
            {
         
                InvokeEnemey(EnemeyPool1._enemeyPool1.GetPooledObject());
                InvokeEnemey(EnemeyPool2._enemeyPool2.GetPooledObject());
                InvokeEnemey(EnemeyPool3._enemeyPool3.GetPooledObject());
                InvokeEnemey(EnemeyPool4._enemeyPool4.GetPooledObject());
                InvokeEnemey(EnemeyPool5._enemeyPool5.GetPooledObject());
                InvokeEnemey(EnemeyPool6._enemeyPool6.GetPooledObject());
        }
       

    }
    void InvokeEnemey(GameObject bullet)
    {
        if (bullet != null)
        {
            if (!_checkPosini && _posListBase1[_numbList1].GetComponent<BaseEnemey>().BaseOn)
            {
                bullet.transform.position = _posListBase2[_numbList1].position;
                bullet.GetComponent<EnemeyMove>().targetIni = _posListBase1[_numbList1];
              //  bullet.GetComponent<EnemeyMove>()._hitSlider._capsHit.ColliderON();// = true;
                Pos1list();
            }
            else if (_checkPosini && _posListBase2[_numbList2].GetComponent<BaseEnemey>().BaseOn)
            {
                bullet.transform.position = _posListBase1[_numbList2].position;
                bullet.GetComponent<EnemeyMove>().targetIni = _posListBase2[_numbList2];
              //  bullet.GetComponent<EnemeyMove>()._hitSlider._capsHit.ColliderON();
                Pos2list();
            }
            else
            {
                bullet.GetComponent<EnemeyMove>().PosInver();
              //  bullet.GetComponent<EnemeyMove>()._hitSlider._capsHit.ColliderON();
                return;
            }
            bullet.SetActive(true);
            bullet.GetComponent<HitSlider>().ResetLife();
            bullet.GetComponent<HitSlider>()._capsHit.ColliderON();
            _checkPosini = !_checkPosini;
        }
    }

    void Pos1list()
    {
        _numbList1++;
        if (_numbList1 >= _posListBase1.Count)
        {
            _numbList1 = 0;
            Shuffle(_posListBase1);
        }
    }
    void Pos2list()
    {
        _numbList2++;
        if (_numbList2 >= _posListBase2.Count)
        {
            _numbList2 = 0;
            Shuffle(_posListBase1);
        }
    }

    public void Shuffle(List<Transform> lists)
    {
        for (int j = lists.Count - 1; j > 0; j--)
        {
            int rnd = UnityEngine.Random.Range(0, j + 1);
            Transform temp = lists[j];
            lists[j] = lists[rnd];
            lists[rnd] = temp;
        }
    }
}
