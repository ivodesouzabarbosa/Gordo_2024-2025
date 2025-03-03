using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyMove : MonoBehaviour
{
    public string targetTag = "Player"; // Tag usada para identificar os alvos
    public Animator _anim;
    public Transform targetIni; // Tag usada para identificar os alvos
    [SerializeField] float distance;
    [SerializeField] float distanceCheck;
    Vector3 direction;
    public float maxSpeed = 5f; // Velocidade m�xima
    public float acceleration = 2f; // Taxa de acelera��o
    public float rotationSpeed = 10f; // Velocidade de rota��o
    public float stoppingDistance = 1f; // Dist�ncia m�nima para parar
    public bool _stopMove;

    private Rigidbody rb;
    private Transform closestTarget;
    private float currentSpeed = 0f; // Velocidade atual
    GameControl _gameControl;

    public int timePos;

    public float countdownTime = 5f; // Tempo inicial do contador em segundos
    public float currentTime;
    public Transform _canvasLive;
    public HitSliderEnemy _hitSlider;
    public bool _deathOn;

   // public bool _invuneravel = false; // Variável que queremos controlar
    public float invincibilityTime = 1f; // Tempo de espera entre danos
    bool _moveBack;
    bool _moveZero;
    [SerializeField] private float reverseSpeed = 7f; // Velocidade fixa para trás
  //  bool _backMoveTemCheck;
    public int _atack=0;
    public bool checkAtack;
    private float timer;           // Armazena o tempo de início
    public float delay = .50f;       // Tempo em segundos para mudar para false
    public ParticleSystem[] _pe;
    public bool _hit;

    public float timeRemaining = 1f;
    float timer2 = 0;

    private void Awake()
    {
        _gameControl = GameObject.FindWithTag("GameController").GetComponent<GameControl>();
        _canvasLive.SetParent(_gameControl._canvasEnemy);
        for (int i = 0; i < _pe.Length; i++)
        {
          //  _pe[i].gameObject.SetActive(false);
            _pe[i].Stop();
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        timer = Time.time; // Registra o tempo inicial
        currentTime = countdownTime;
        closestTarget = targetIni;
        _moveBack = true;
        _moveZero = true;
      //  _backMoveTemCheck = false;
    }

    private void Update()
    {
        if (!_deathOn)
        {
            TimeRe();
            _canvasLive.transform.position = new Vector3(transform.position.x, _canvasLive.transform.position.y, transform.position.z);
        }
    }

    void FixedUpdate()
    {
        if (!_deathOn)
        {
            // Atualiza o alvo mais próximo
            FindClosestTarget();



            if (closestTarget == null)
            {
                Debug.LogWarning("Nenhum alvo disponível na lista.");
                rb.linearVelocity = Vector3.zero; // Para o movimento se não houver alvos
                return;
            }

            distance = Vector3.Distance(transform.position, closestTarget.position);
            // Calcula a direção padrão para o alvo
            if (distance > distanceCheck)
            {
                direction = (targetIni.position - transform.position).normalized;
              //  distance = Vector3.Distance(transform.position, targetIni.position);
            }
            else
            {
                direction = (closestTarget.position - transform.position).normalized;
              //  distance = Vector3.Distance(transform.position, closestTarget.position);
            }

            if (!_moveZero)
            {



                Invoke("MoveBackward", 0.2f);

                for (int i = 0; i < _pe.Length; i++)
                {
                    //  _pe[i].gameObject.SetActive(false);
                    _pe[i].Play();
                }
                _hit = true;
                _atack = 0;
                if (Time.time >= invincibilityTime + 0.5f)
                {
                    _moveBack = false;
                    _moveZero=true;
                    _hit = false;
                    rb.linearVelocity = Vector3.zero;
                    for (int i = 0; i < _pe.Length; i++)
                    {
                        //  _pe[i].gameObject.SetActive(false);
                        _pe[i].Stop();
                    }

                }
            }
            else if (!_moveBack)
            {
                if (Time.time >= invincibilityTime + 1f)
                {
                    _moveBack = true;
                  
                  //  _backMoveTemCheck = false;

                }
            }
            else if (_moveBack && distance > stoppingDistance && !_stopMove)
            {
                // Movimento normal em direção ao alvo
                MoveForward();
                _atack = 0;
            }
            else
            {
                // Para o movimento
                currentSpeed = 0f;
                rb.linearVelocity = Vector3.zero;
                if (!checkAtack)
                {
                    timer2 += Time.deltaTime;
                    if (timer2 >= 0.5f) // Chama a função a cada 2 segundos
                    {
                        checkAtack = true;
                        int r = UnityEngine.Random.Range(1, 3);
                        _atack = r;
                        
                    }

                   
                }
                // Verifica se o tempo decorrido é maior ou igual ao delay
                if (checkAtack && Time.time >= timer + delay)
                {
                    checkAtack = false; // Define como false após o delay
                    timer = Time.time; // Registra o tempo inicial
                    timeRemaining = 1.5f;
                    timer2 = 0f;
                    //  Debug.Log("Variável agora é false.");
                }

            }

            // Rotaciona para alinhar ao alvo
            RotateTowardsTarget();
        }
        else
        {

        }
        AnimEnemy();
    }
    public void RandAtck()
    {
        checkAtack = false;
    }

    void AnimEnemy()
    {
        float speedA = MathF.Abs(rb.linearVelocity.x + rb.linearVelocity.z);
       // float speedB = rb.linearVelocity.x + rb.linearVelocity.z;
        _anim.SetFloat("speed", speedA);
        _anim.SetBool("hit", _hit);
        _anim.SetInteger("atack", _atack);

    }

    private void MoveBackward()
    {
        // Define uma velocidade fixa para trás
        Vector3 backwardVelocity = -transform.forward * reverseSpeed; // Move para trás na direção fixa
        rb.linearVelocity = new Vector3(backwardVelocity.x, rb.linearVelocity.y, backwardVelocity.z);

        Debug.Log("Movendo para trás com invulnerabilidade!");
    }

    private void MoveForward()
    {
        // Aumenta a velocidade gradualmente até o máximo permitido
        currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.fixedDeltaTime);

        // Move o Rigidbody na direção do alvo
        Vector3 moveVelocity = direction * currentSpeed;
        rb.linearVelocity = new Vector3(moveVelocity.x, rb.linearVelocity.y, moveVelocity.z);
    }

    private void RotateTowardsTarget()
    {
        if (distance > 0.1f) // Apenas rotaciona se estiver longe o suficiente
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            Vector3 eulerRotation = targetRotation.eulerAngles;
            eulerRotation.x = transform.rotation.eulerAngles.x; // Preserva o eixo X
            rb.rotation = Quaternion.Euler(eulerRotation);
        }
    }

    void FindClosestTarget()
    {
        float closestDistance = Mathf.Infinity;
        Transform bestTarget = null;

        foreach (Transform target in _gameControl.targets)
        {
            if (target == null) continue; // Evita erros caso algum objeto tenha sido destru�do

            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget < closestDistance)
            {
                closestDistance = distanceToTarget;
                bestTarget = target;
            }
        }

        closestTarget = bestTarget;
      
    }

    void TimeRe()
    {
        if (timePos == 0)
        {
            // Reduz o tempo enquanto ele for maior que zero
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime; // Decremento com base no tempo real
                currentTime = Mathf.Max(currentTime, 0); // Evita valores negativos
            }
            else
            {
                // A��o quando o tempo chega a zero
                TimerEnded();
            }
        }
    }

    void TimerEnded()// se o inimigo estive seguindo  muito tempo o player e n�o alcan�a, muda de posi��o
    {
       
        currentTime = countdownTime;
        PosInver();//
    }

    public void PosInver()
    {
        if (_gameControl._levelOn <= 2)
        {
            int rand = UnityEngine.Random.Range(0, _gameControl._enemyBaseControl._posListBase2.Count);
            if (_gameControl._enemyBaseControl._posListBase2[rand].GetComponent<BaseEnemey>().BaseOn)
            {
                transform.position = _gameControl._enemyBaseControl._posListBase2[rand].position;
            }
            
           
          
        }
        else
        {
            int rand = UnityEngine.Random.Range(0, _gameControl._enemyBaseControl._posListBase1.Count);
            if (_gameControl._enemyBaseControl._posListBase1[rand].GetComponent<BaseEnemey>().BaseOn)
            {
                transform.position = _gameControl._enemyBaseControl._posListBase1[rand].position;
            }
           


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            timePos = 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            timePos = 0;
            currentTime = countdownTime;
        }
    }
    public void AtivarPorTempo(float duracao)
    {
     //   _invuneravel = true;
        _moveBack = false;
     //   _backMoveTemCheck = true;
        _moveZero = false;
        invincibilityTime = Time.time + duracao; // Define o tempo final
    
    }
}