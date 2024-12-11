using UnityEngine;

public class BoxCastAtack : MonoBehaviour
{
    public LayerMask layerMask; // Define as camadas para verificar colisões
    public Vector3 castDirection = Vector3.forward; // Direção do cast
    public float castDistance = 1f; // Distância do cast
    public Color gizmoColor = Color.green; // Cor do Gizmo para depuração
    public float cooldownTime = 1f; // Tempo de cooldown para permitir nova colisão

    private BoxCollider boxCollider; // Para referência ao tamanho do cubo
    private Vector3 boxCenter; // Centro ajustado do colisor
    private Vector3 boxSize; // Tamanho ajustado do colisor
    private Quaternion boxRotation; // Rotação atual do objeto

    private bool canCollide = true; // Controla se é possível colidir novamente
    private float lastCollisionTime; // Armazena o tempo da última colisão detectada
   

    void Start()
    {
        // Obtém o BoxCollider do objeto
        boxCollider = GetComponent<BoxCollider>();
        if (boxCollider == null)
        {
            Debug.LogError("Este objeto precisa de um BoxCollider para sincronizar o BoxCast.");
        }
      
    }

    void Update()
    {
        if (boxCollider != null)
        {
            // Checa se o cooldown acabou
            if (Time.time - lastCollisionTime >= 1.2f)//cooldownTime+1
            {
                canCollide = true; // Permite nova colisão
            }

            // Ajusta o centro e o tamanho do BoxCollider com a escala do objeto
            boxSize = Vector3.Scale(boxCollider.size, transform.localScale);
            boxCenter = transform.position + transform.rotation * boxCollider.center; // Aplica rotação ao centro
            boxRotation = transform.rotation;

            // Executa o BoxCast com rotação e direção ajustadas
            RaycastHit hit;
            if (canCollide && Physics.BoxCast(boxCenter, boxSize / 2, transform.rotation * castDirection.normalized, out hit, boxRotation, castDistance, layerMask))
            {
                Debug.Log($"Colidiu com: {hit.collider.name}");
                lastCollisionTime = Time.time; // Armazena o tempo da colisão
                canCollide = false; // Desabilita colisões até o cooldown passar
                int value = Random.Range(10,25);

                hit.collider.transform.parent.GetComponent<HitSlider>().TakeDamage(value);//hit do inimigo
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (boxCollider != null)
        {
            Gizmos.color = gizmoColor;

            // Desenha a caixa no editor com rotação e escala ajustadas
            Matrix4x4 matrix = Matrix4x4.TRS(boxCenter, boxRotation, Vector3.one); // Aplica transformação ao Gizmo
            Gizmos.matrix = matrix;

            // Desenha o cubo no local da colisão
            Gizmos.DrawWireCube(Vector3.zero, boxSize); // Centro ajustado

            // Desenha a posição final após o BoxCast
            Gizmos.matrix = Matrix4x4.identity; // Reseta a transformação do Gizmo
            Gizmos.DrawWireCube(boxCenter + transform.rotation * castDirection.normalized * castDistance, boxSize);

            // Desenha a linha do cast no Gizmo
            Gizmos.DrawLine(boxCenter, boxCenter + transform.rotation * castDirection.normalized * castDistance);
        }
    }
}