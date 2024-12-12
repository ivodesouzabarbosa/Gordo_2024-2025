using UnityEngine;

public class CapsHit : MonoBehaviour
{
    public LayerMask layerMask; // Define as camadas para verificar colis�es
    public Vector3 castDirection = Vector3.forward; // Dire��o do cast
    public float castDistance = 1f; // Dist�ncia do cast
    public Color gizmoColor = Color.red; // Cor do Gizmo para depura��o
    public float resetTime = 1.2f; // Tempo em segundos para reiniciar a detec��o

    public CapsuleCollider capsuleCollider; // Para refer�ncia ao colisor
    private Vector3 bottomPoint; // Ponto inferior da c�psula
    private Vector3 topPoint; // Ponto superior da c�psula
    private float radius; // Raio da c�psula
    private bool hasDetectedCollision = false; // Controle para detectar apenas uma vez
    private float collisionTime = 0f; // Armazena o tempo em que a colis�o foi detectada
   

    void Start()
    {
        // Obt�m o CapsuleCollider do objeto
    //    capsuleCollider = GetComponent<CapsuleCollider>();
        if (capsuleCollider == null)
        {
            Debug.LogError("Este objeto precisa de um CapsuleCollider para funcionar.");
        }
    }

    void Update()
    {
        // Se a colis�o foi detectada, verifica o tempo para resetar
        if (hasDetectedCollision)
        {
            // Se o tempo decorrido for maior que o tempo de reset, reinicia a detec��o
            if (Time.time - collisionTime >= resetTime)
            {
                hasDetectedCollision = false; // Reseta a detec��o
            }
            return; // Se a colis�o j� foi detectada, n�o faz mais nada
        }

        // Se ainda n�o foi detectada uma colis�o, executa o CapsuleCast
        if (capsuleCollider != null)
        {
            // Calcula as propriedades da c�psula considerando escala
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
                // Marca que a colis�o foi detectada e armazena o tempo da colis�o
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

    


    // M�todo para desenhar os Gizmos no Editor
    private void OnDrawGizmos()
    {
        if (capsuleCollider != null)
        {
            Gizmos.color = gizmoColor;

            // Desenha as esferas nas extremidades da c�psula
            Gizmos.DrawWireSphere(bottomPoint, radius);
            Gizmos.DrawWireSphere(topPoint, radius);

            // Desenha as esferas na posi��o final ap�s o cast
            Vector3 castOffset = transform.rotation * castDirection.normalized * castDistance;
            Gizmos.DrawWireSphere(bottomPoint + castOffset, radius);
            Gizmos.DrawWireSphere(topPoint + castOffset, radius);

            // Desenha a linha entre as esferas iniciais e finais
            Gizmos.DrawLine(bottomPoint, topPoint);
            Gizmos.DrawLine(bottomPoint + castOffset, topPoint + castOffset);
        }
    }
}