using UnityEngine;

public class CapsHit : MonoBehaviour
{
    public LayerMask layerMask; // Define as camadas para verificar colisões
    public Vector3 castDirection = Vector3.forward; // Direção do cast
    public float castDistance = 1f; // Distância do cast
    public Color gizmoColor = Color.red; // Cor do Gizmo para depuração
    public float resetTime = 1.2f; // Tempo em segundos para reiniciar a detecção

    public CapsuleCollider capsuleCollider; // Para referência ao colisor
    private Vector3 bottomPoint; // Ponto inferior da cápsula
    private Vector3 topPoint; // Ponto superior da cápsula
    private float radius; // Raio da cápsula
    private bool hasDetectedCollision = false; // Controle para detectar apenas uma vez
    private float collisionTime = 0f; // Armazena o tempo em que a colisão foi detectada
   

    void Start()
    {
        // Obtém o CapsuleCollider do objeto
    //    capsuleCollider = GetComponent<CapsuleCollider>();
        if (capsuleCollider == null)
        {
            Debug.LogError("Este objeto precisa de um CapsuleCollider para funcionar.");
        }
    }

    void Update()
    {
        // Se a colisão foi detectada, verifica o tempo para resetar
        if (hasDetectedCollision)
        {
            // Se o tempo decorrido for maior que o tempo de reset, reinicia a detecção
            if (Time.time - collisionTime >= resetTime)
            {
                hasDetectedCollision = false; // Reseta a detecção
            }
            return; // Se a colisão já foi detectada, não faz mais nada
        }

        // Se ainda não foi detectada uma colisão, executa o CapsuleCast
        if (capsuleCollider != null)
        {
            // Calcula as propriedades da cápsula considerando escala
            radius = capsuleCollider.radius * Mathf.Max(transform.localScale.x, transform.localScale.z);
            float height = capsuleCollider.height * transform.localScale.y;
            Vector3 center = transform.position + transform.rotation * capsuleCollider.center;

            float halfHeight = Mathf.Max(0, (height / 2) - radius);
            topPoint = center + transform.up * halfHeight;
            bottomPoint = center - transform.up * halfHeight;

            // Executa o CapsuleCast
            RaycastHit hit;
            if (Physics.CapsuleCast(bottomPoint, topPoint, radius, transform.rotation * castDirection.normalized, out hit, castDistance, layerMask))
            {
                // Marca que a colisão foi detectada e armazena o tempo da colisão
                hasDetectedCollision = true;
                collisionTime = Time.time; // Registra o tempo atual
                Debug.Log($"Colidiu com: {hit.collider.name}");
            }
        }
    }

    public void ColliderON()
    {
        capsuleCollider.enabled = true;
    }

    


    // Método para desenhar os Gizmos no Editor
    private void OnDrawGizmos()
    {
        if (capsuleCollider != null)
        {
            Gizmos.color = gizmoColor;

            // Desenha as esferas nas extremidades da cápsula
            Gizmos.DrawWireSphere(bottomPoint, radius);
            Gizmos.DrawWireSphere(topPoint, radius);

            // Desenha as esferas na posição final após o cast
            Vector3 castOffset = transform.rotation * castDirection.normalized * castDistance;
            Gizmos.DrawWireSphere(bottomPoint + castOffset, radius);
            Gizmos.DrawWireSphere(topPoint + castOffset, radius);

            // Desenha a linha entre as esferas iniciais e finais
            Gizmos.DrawLine(bottomPoint, topPoint);
            Gizmos.DrawLine(bottomPoint + castOffset, topPoint + castOffset);
        }
    }
}