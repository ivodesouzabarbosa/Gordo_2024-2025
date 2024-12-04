using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRaycastAtaquePLayer : MonoBehaviour
{
    public Vector3 boxOrigin = Vector3.zero;      // Origem da caixa relativa ao objeto
    public Vector3 boxSize = new Vector3(1, 1, 1); // Tamanho da caixa
    public Vector3 direction = Vector3.forward;   // Dire��o local do raycast
    public float distance = 5f;                   // Dist�ncia m�xima do raycast
    public LayerMask layerMask;                   // Camadas para detectar colis�es
    public PlayerMove _playerMove;

    private void Start()
    {
        
    }

    private void Update()
    {
        // Calcula a posi��o de origem global
        Vector3 origin = transform.position + transform.TransformDirection(boxOrigin);

        // Rotaciona a dire��o baseada na rota��o do objeto
        Vector3 rotatedDirection = transform.TransformDirection(direction.normalized);

        // Faz o BoxCast
        if (Physics.BoxCast(origin, boxSize / 2, rotatedDirection, out RaycastHit hit, transform.rotation, distance, layerMask))
        {
            //  Debug.Log("Hit: true");
           // Debug.Log("Hit: " + hit.collider.name);
            hit.collider.GetComponent<HitSlider>().TakeDamage(25);
           // _enemeyMove._stopMove = true;

        }
        else
        {
           // Debug.Log("Hit: false");
          //  _enemeyMove._stopMove = false;
        }
    }

    private void OnDrawGizmos()
    {
        // Calcula a posi��o de origem global
        Vector3 origin = transform.position + transform.TransformDirection(boxOrigin);

        // Rotaciona a dire��o baseada na rota��o do objeto
        Vector3 rotatedDirection = transform.TransformDirection(direction.normalized);

        // Calcula o ponto final do BoxCast
        Vector3 endPoint = origin + rotatedDirection * distance;

        // Desenha a caixa inicial
        Gizmos.color = Color.green;
        Gizmos.matrix = Matrix4x4.TRS(origin, transform.rotation, Vector3.one); // Aplica a rota��o para a visualiza��o
        Gizmos.DrawWireCube(Vector3.zero, boxSize);

        // Desenha a caixa no ponto final
        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS(endPoint, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boxSize);

        // Reseta o Gizmos.matrix para evitar interfer�ncia
        Gizmos.matrix = Matrix4x4.identity;

        // Desenha a linha entre as caixas
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(origin, endPoint);
    }
}
