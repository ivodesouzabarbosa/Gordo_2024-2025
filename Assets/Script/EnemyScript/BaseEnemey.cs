using System.Collections;
using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class BaseEnemey : MonoBehaviour
{
    public float rayDistance = 10f; // Distância máxima do Raycast
    public LayerMask hitLayers; // Layers que o Raycast pode detectar
    public bool BaseOn;


   

    void Update()
    {
        // Origem do Raycast é a posição do objeto atual
        Vector3 origin = transform.position;

        // Direção do Raycast: Para baixo (eixo Y negativo)
        Vector3 direction = Vector3.down;

        // Variável para armazenar informações do hit
        RaycastHit hit;

        // Executa o Raycast
        if (Physics.Raycast(origin, direction, out hit, rayDistance, hitLayers))
        {
            // Desenha uma linha até o ponto de impacto
            Debug.DrawLine(origin, hit.point, Color.green);

            BaseOn = true;

          //  Debug.Log($"Acertou: {hit.collider.name} na posição {hit.point}");
        }
        else
        {
            // Desenha uma linha até o final da distância máxima, caso não acerte nada
            Debug.DrawLine(origin, origin + direction * rayDistance, Color.red);
            BaseOn = false;
          //  Debug.Log("Nenhum impacto detectado para baixo.");
        }
    }
}