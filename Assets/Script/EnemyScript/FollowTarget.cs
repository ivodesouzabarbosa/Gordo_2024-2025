using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public string targetTag = "Player"; // Tag usada para identificar os alvos
    public float maxSpeed = 5f; // Velocidade m�xima
    public float acceleration = 2f; // Taxa de acelera��o
    public float rotationSpeed = 10f; // Velocidade de rota��o
    public float stoppingDistance = 1f; // Dist�ncia m�nima para parar

    private Rigidbody rb;
    private Transform closestTarget;
    private float currentSpeed = 0f; // Velocidade atual
    private List<Transform> targets = new List<Transform>(); // Lista de alvos

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Encontra todos os objetos com a tag especificada e os adiciona � lista
        GameObject[] targetObjects = GameObject.FindGameObjectsWithTag(targetTag);
        foreach (GameObject obj in targetObjects)
        {
            targets.Add(obj.transform);
        }
    }

    void FixedUpdate()
    {
        // Atualiza o alvo mais pr�ximo
        FindClosestTarget();

        if (closestTarget == null)
        {
            Debug.LogWarning("Nenhum alvo dispon�vel na lista.");
            rb.velocity = Vector3.zero; // Para o movimento se n�o houver alvos
            return;
        }

        // Calcula a dire��o para o alvo
        Vector3 direction = (closestTarget.position - transform.position).normalized;

        // Calcula a dist�ncia at� o alvo
        float distance = Vector3.Distance(transform.position, closestTarget.position);

        if (distance > stoppingDistance)
        {
            // Aumenta a velocidade gradualmente at� o m�ximo permitido
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.fixedDeltaTime);

            // Move o Rigidbody na dire��o do alvo
            Vector3 moveVelocity = direction * currentSpeed;
            rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z); // Mant�m a velocidade vertical
        }
        else
        {
            // Reduz a velocidade para zero ao atingir a dist�ncia m�nima
            currentSpeed = 0f;
            rb.velocity = Vector3.zero;
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

        foreach (Transform target in targets)
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
}