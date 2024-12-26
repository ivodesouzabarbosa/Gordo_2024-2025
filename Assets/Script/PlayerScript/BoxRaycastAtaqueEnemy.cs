using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoxRaycastAtaqueEnemy : MonoBehaviour
{
    public Vector3 boxOrigin = Vector3.zero;      // Origem da caixa relativa ao objeto
    public Vector3 boxSize = new Vector3(1, 1, 1); // Tamanho da caixa
    public Vector3 direction = Vector3.forward;   // Direção local do raycast
    public float distance = 5f;                   // Distância máxima do raycast
    public LayerMask layerMask;                   // Camadas para detectar colisões

    public float delayInSeconds; // Tempo de atraso
    private float timer = 0f;
    private bool functionCalled;
    public AtackAnimEnemy _atackanimEnemy;
    RaycastHit _hit;
    bool _passCheck;
   // SliderPLayer _sliderplayer;

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
        if (_atackanimEnemy._checkAtack && Physics.BoxCast(origin, boxSize / 2, rotatedDirection, out RaycastHit hit, transform.rotation, distance, layerMask))
        {
            //  Debug.Log("Hit: true");
            _atackanimEnemy._checkAtack = false;
            Debug.Log("Hit: " + gameObject.name + " "+hit.collider.name);
            _hit=hit;
            functionCalled = false;

        }


        if (!functionCalled)
        {
            timer += Time.deltaTime;
            if (timer >= delayInSeconds)
            {
                HitTime(_hit);
                functionCalled = true; // Para evitar múltiplas chamadas
                timer = 0f;
            }
        }
    }


    public void HitTime(RaycastHit hit)
    {
        Debug.Log("Dano");
        SelectPerson selectPersonTemp = hit.collider.transform.parent.GetComponent<PlayerMove>()._selectPerson;
        selectPersonTemp._sliderPLayers._hitSliderPlayer.TakeDamage(15);
  
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
