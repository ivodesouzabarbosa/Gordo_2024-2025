using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class BoxRaycastAtaquePLayer : MonoBehaviour
{
    public Vector3 boxOrigin = Vector3.zero;      // Origem da caixa relativa ao objeto
    public Vector3 boxSize = new Vector3(1, 1, 1); // Tamanho da caixa
    public Vector3 direction = Vector3.forward;   // Direção local do raycast
    public float distance = 5f;                   // Distância máxima do raycast
    public LayerMask layerMask;                   // Camadas para detectar colisões
    public PlayerMove _playerMove;

    public float delayInSeconds; // Tempo de atraso
    private float timer = 0f;
    private bool functionCalled;
    RaycastHit _hit;

    private void Start()
    {
        functionCalled = true;
    }

    private void Update()
    {
        // Calcula a posição de origem global
        Vector3 origin = transform.position + transform.TransformDirection(boxOrigin);

        // Rotaciona a direção baseada na rotação do objeto
        Vector3 rotatedDirection = transform.TransformDirection(direction.normalized);

        // Faz o BoxCast
        if (_playerMove._checkAt && Physics.BoxCast(origin, boxSize / 2, rotatedDirection, out RaycastHit hit, transform.rotation, distance, layerMask))
        {
            //  Debug.Log("Hit: true");
            Debug.Log("Hit: " + gameObject.name + hit.collider.name);
            _hit=hit;
            functionCalled = false;

        }


        if (!functionCalled)
        {
            //timer += Time.deltaTime;
            HitTime(_hit);
            functionCalled = true; // Para evitar múltiplas chamadas
           // timer = 0f;
           /* if (timer >= delayInSeconds)
            {
                HitTime(_hit);
                functionCalled = true; // Para evitar múltiplas chamadas
                timer = 0f;
            }*/
        }
    }


    void HitTime(RaycastHit hit)
    {
        hit.collider.transform.parent.GetComponent<HitSliderEnemy>()._hitSliderPlayer = _playerMove._selectPerson._sliderPLayers;
        hit.collider.transform.parent.GetComponent<HitSliderEnemy>().TakeDamage(25);
        hit.collider.transform.parent.GetComponent<EnemeyMove>()._stopMove = true;
        Debug.Log("ataqueplaterMili");
    }


    private void OnDrawGizmos()
    {
        // Calcula a posição de origem global
        Vector3 origin = transform.position + transform.TransformDirection(boxOrigin);

        // Rotaciona a direção baseada na rotação do objeto
        Vector3 rotatedDirection = transform.TransformDirection(direction.normalized);

        // Calcula o ponto final do BoxCast
        Vector3 endPoint = origin + rotatedDirection * distance;

        // Desenha a caixa inicial
        Gizmos.color = Color.green;
        Gizmos.matrix = Matrix4x4.TRS(origin, transform.rotation, Vector3.one); // Aplica a rotação para a visualização
        Gizmos.DrawWireCube(Vector3.zero, boxSize);

        // Desenha a caixa no ponto final
        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS(endPoint, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boxSize);

        // Reseta o Gizmos.matrix para evitar interferência
        Gizmos.matrix = Matrix4x4.identity;

        // Desenha a linha entre as caixas
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(origin, endPoint);
    }
}
