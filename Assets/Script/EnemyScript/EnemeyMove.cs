using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyMove : MonoBehaviour
{
    public string targetTag = "Player"; // Tag usada para identificar os alvos
    public float maxSpeed = 5f; // Velocidade máxima
    public float acceleration = 2f; // Taxa de aceleração
    public float rotationSpeed = 10f; // Velocidade de rotação
    public float stoppingDistance = 1f; // Distância mínima para parar

    private Rigidbody rb;
    private Transform closestTarget;
    private float currentSpeed = 0f; // Velocidade atual
    GameControl _gameControl;

    public int timePos;

    public float countdownTime = 5f; // Tempo inicial do contador em segundos
    public float currentTime;

    private void Awake()
    {
        _gameControl = GameObject.FindWithTag("GameController").GetComponent<GameControl>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentTime = countdownTime;
    }
    private void Update()
    {
        TimeRe();
    }

    void FixedUpdate()
    {
        // Atualiza o alvo mais próximo
        FindClosestTarget();

        if (closestTarget == null)
        {
            Debug.LogWarning("Nenhum alvo disponível na lista.");
            rb.velocity = Vector3.zero; // Para o movimento se não houver alvos
            return;
        }

        // Calcula a direção para o alvo
        Vector3 direction = (closestTarget.position - transform.position).normalized;

        // Calcula a distância até o alvo
        float distance = Vector3.Distance(transform.position, closestTarget.position);

        if (distance > stoppingDistance)
        {
            // Aumenta a velocidade gradualmente até o máximo permitido
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.fixedDeltaTime);

            // Move o Rigidbody na direção do alvo
            Vector3 moveVelocity = direction * currentSpeed;
            rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z); // Mantém a velocidade vertical
        }
        else
        {
            // Reduz a velocidade para zero ao atingir a distância mínima
            currentSpeed = 0f;
            rb.velocity = Vector3.zero;
        }

        // Rotaciona para alinhar ao alvo sem afetar o eixo X
        if (distance > 0.1f) // Apenas rotaciona se estiver longe o suficiente
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction); // Calcula a rotação desejada
            Vector3 eulerRotation = targetRotation.eulerAngles;

            // Preserva o valor atual do eixo X, mas aplica a rotação no eixo Y e Z
            eulerRotation.x = transform.rotation.eulerAngles.x;

            // Aplica a rotação sem modificar o eixo X
            rb.rotation = Quaternion.Euler(eulerRotation);
        }
    }

    void FindClosestTarget()
    {
        float closestDistance = Mathf.Infinity;
        Transform bestTarget = null;

        foreach (Transform target in _gameControl.targets)
        {
            if (target == null) continue; // Evita erros caso algum objeto tenha sido destruído

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
                // Ação quando o tempo chega a zero
                TimerEnded();
            }
        }
    }

    void TimerEnded()// se o inimigo estive seguindo  muito tempo o player e não alcança, muda de posição
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