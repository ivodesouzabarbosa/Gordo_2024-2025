using UnityEngine;

public class BoxCastAtack : MonoBehaviour
{
    public LayerMask layerMask; // Define as camadas para verificar colis�es
    public Vector3 castDirection = Vector3.forward; // Dire��o do cast
    public float castDistance = 1f; // Dist�ncia do cast
    public Color gizmoColor = Color.green; // Cor do Gizmo para depura��o
    public float cooldownTime = 1f; // Tempo de cooldown para permitir nova colis�o

    private BoxCollider boxCollider; // Para refer�ncia ao tamanho do cubo
    private Vector3 boxCenter; // Centro ajustado do colisor
    private Vector3 boxSize; // Tamanho ajustado do colisor
    private Quaternion boxRotation; // Rota��o atual do objeto

    private bool canCollide = true; // Controla se � poss�vel colidir novamente
    private float lastCollisionTime; // Armazena o tempo da �ltima colis�o detectada
   

    void Start()
    {
        // Obt�m o BoxCollider do objeto
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
            if (Time.time - lastCollisionTime >= cooldownTime)//cooldownTime+1
            {
                canCollide = true; // Permite nova colis�o
            }

            // Ajusta o centro e o tamanho do BoxCollider com a escala do objeto
            boxSize = Vector3.Scale(boxCollider.size, transform.localScale);
            boxCenter = transform.position + transform.rotation * boxCollider.center; // Aplica rota��o ao centro
            boxRotation = transform.rotation;

            // Executa o BoxCast com rota��o e dire��o ajustadas
            RaycastHit hit;
            if (Physics.BoxCast(boxCenter, boxSize / 2, transform.rotation * castDirection.normalized, out hit, boxRotation, castDistance, layerMask))
            {
                Debug.Log($"Colidiu com: {hit.collider.name}");
                lastCollisionTime = Time.time; // Armazena o tempo da colis�o
                canCollide = false; // Desabilita colis�es at� o cooldown passar
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

            // Desenha a caixa no editor com rota��o e escala ajustadas
            Matrix4x4 matrix = Matrix4x4.TRS(boxCenter, boxRotation, Vector3.one); // Aplica transforma��o ao Gizmo
            Gizmos.matrix = matrix;

            // Desenha o cubo no local da colis�o
            Gizmos.DrawWireCube(Vector3.zero, boxSize); // Centro ajustado

            // Desenha a posi��o final ap�s o BoxCast
            Gizmos.matrix = Matrix4x4.identity; // Reseta a transforma��o do Gizmo
            Gizmos.DrawWireCube(boxCenter + transform.rotation * castDirection.normalized * castDistance, boxSize);

            // Desenha a linha do cast no Gizmo
            Gizmos.DrawLine(boxCenter, boxCenter + transform.rotation * castDirection.normalized * castDistance);
        }
    }
}