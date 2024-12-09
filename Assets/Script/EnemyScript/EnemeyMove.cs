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
    }
    private void Update()
    {
        TimeRe();
        _canvasLive.transform.position = new Vector3(transform.position.x, _canvasLive.transform.position.y, transform.position.z);
    }

    void FixedUpdate()
    {
        // Atualiza o alvo mais pr�ximo
        FindClosestTarget();

        if (closestTarget == null)
        {
            Debug.LogWarning("Nenhum alvo dispon�vel na lista.");
            rb.linearVelocity = Vector3.zero; // Para o movimento se n�o houver alvos
            return;
        }
        if (distance>distanceCheck)
        {
           // closestTarget = targetIni;

            direction = (targetIni.position - transform.position).normalized;

            // Calcula a dist�ncia at� o alvo
            distance = Vector3.Distance(transform.position, targetIni.position);
        }
        else
        {
           
            // Calcula a dire��o para o alvo
            direction = (closestTarget.position - transform.position).normalized;

            // Calcula a dist�ncia at� o alvo
            distance = Vector3.Distance(transform.position, closestTarget.position);

        }
       
        distance = Vector3.Distance(transform.position, closestTarget.position);


        if (distance > stoppingDistance && !_stopMove)
        {
            // Aumenta a velocidade gradualmente at� o m�ximo permitido
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.fixedDeltaTime);

            // Move o Rigidbody na dire��o do alvo
            Vector3 moveVelocity = direction * currentSpeed;
            rb.linearVelocity = new Vector3(moveVelocity.x, rb.linearVelocity.y, moveVelocity.z); // Mant�m a velocidade vertical
        }
        else
        {
            // Reduz a velocidade para zero ao atingir a dist�ncia m�nima
            currentSpeed = 0f;
            rb.linearVelocity = Vector3.zero;
        }

        // Rotaciona para alinhar ao alvo sem afetar o eixo X
        if (distance > 0.1f) // Apenas rotaciona se estiver longe o suficiente
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction); // Calcula a rota��o desejada
            Vector3 eulerRotation = targetRotation.eulerAngles;

            // Preserva o valor atual do eixo X, mas aplica a rota��o no eixo Y e Z
            eulerRotation.x = transform.rotation.eulerAngles.x;

            // Aplica a rota��o sem modificar o eixo X
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
}