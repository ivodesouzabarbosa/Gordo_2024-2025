using UnityEngine;

public class BoxRaycast : MonoBehaviour
{

    public float maxDistance = 5f;  // Distância máxima do BoxCast
    public Vector3 boxSize = new Vector3(1f, 1f, 1f);  // Tamanho da caixa
    public LayerMask layerMask;  // Camadas que o BoxCast irá detectar
    public Vector3 offset = new Vector3(0f, 1f, 0f);  // Offset para ajustar a posição

    void Update()
    {
        // Ajusta a origem do BoxCast com base na posição do objeto e no offset
        Vector3 origin = transform.position + offset;

        // Direção do BoxCast (frente do objeto)
        Vector3 direction = transform.forward;

        // Executa o BoxCast
        if (Physics.BoxCast(origin, boxSize / 2, direction, out RaycastHit hit, transform.rotation, maxDistance, layerMask))
        {
            Debug.Log("Colidiu com: " + hit.collider.name);
        }
    }

    // Desenha o BoxCast na cena (mesmo sem dar Play)
    void OnDrawGizmos()
    {
        Vector3 origin = transform.position + offset;
        Vector3 direction = transform.forward;

        // Desenha a caixa inicial
        Gizmos.color = Color.cyan;
        Gizmos.matrix = Matrix4x4.TRS(origin, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boxSize);

        // Desenha a caixa no final do BoxCast
        Gizmos.matrix = Matrix4x4.TRS(origin + direction * maxDistance, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boxSize);

        // Desenha uma linha entre as duas caixas para indicar o trajeto
        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, origin + direction * maxDistance);
    }
}