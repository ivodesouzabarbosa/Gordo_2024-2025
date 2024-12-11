using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyMove : MonoBehaviour
{
    public string targetTag = "Player"; // Tag usada para identificar os alvos
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
    public HitSlider _hitSlider;
    public bool _deathOn;

    public bool _invuneravel = false; // Variável que queremos controlar
    public float invincibilityTime = 1f; // Tempo de espera entre danos
    bool _moveBack;
    [SerializeField] private float reverseSpeed = 10f; // Velocidade fixa para trás

    private void Awake()
    {
        _gameControl = GameObject.FindWithTag("GameController").GetComponent<GameControl>();
        _canvasLive.SetParent(_gameControl._canvasEnemy);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentTime = countdownTime;
        closestTarget = targetIni;
        _moveBack = true;
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

            if (_invuneravel)
            {
                // Movimento para trás independente de direção
                MoveBackward();
                if (_invuneravel && Time.time >= invincibilityTime)
                {
                    _invuneravel = false;
                    rb.linearVelocity = Vector3.zero; 

                }
               
            }
            else if (!_moveBack)
            {
                if (Time.time >= invincibilityTime + 1f)
                {
                    _moveBack = true;

                }
            }
            else if (_moveBack && !_invuneravel && distance > stoppingDistance && !_stopMove)
            {
                // Movimento normal em direção ao alvo
                MoveForward();
            }
            else
            {
                // Para o movimento
                currentSpeed = 0f;
                rb.linearVelocity = Vector3.zero;
            }

            // Rotaciona para alinhar ao alvo
            RotateTowardsTarget();
        }
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
            int rand = Random.Range(0, _gameControl._enemyBaseControl._posListBase2.Count);
            if (_gameControl._enemyBaseControl._posListBase2[rand].GetComponent<BaseEnemey>().BaseOn)
            {
                transform.position = _gameControl._enemyBaseControl._posListBase2[rand].position;
            }
            
           
          
        }
        else
        {
            int rand = Random.Range(0, _gameControl._enemyBaseControl._posListBase1.Count);
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
        _invuneravel = true;
        _moveBack = false;
        invincibilityTime = Time.time + duracao; // Define o tempo final
    
    }
}