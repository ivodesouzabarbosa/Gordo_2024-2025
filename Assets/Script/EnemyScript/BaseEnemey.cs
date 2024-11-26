using System.Collections;
using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class BaseEnemey : MonoBehaviour
{
    public float rayDistance = 10f; // Dist�ncia m�xima do Raycast
    public LayerMask hitLayers; // Layers que o Raycast pode detectar
    public bool BaseOn;

    void Update()
    {
        // Origem do Raycast � a posi��o do objeto atual
        Vector3 origin = transform.position;

        // Dire��o do Raycast: Para baixo (eixo Y negativo)
        Vector3 direction = Vector3.down;

        // Vari�vel para armazenar informa��es do hit
        RaycastHit hit;

        // Executa o Raycast
        if (Physics.Raycast(origin, direction, out hit, rayDistance, hitLayers))
        {
            // Desenha uma linha at� o ponto de impacto
            Debug.DrawLine(origin, hit.point, Color.green);

            BaseOn = false;

          //  Debug.Log($"Acertou: {hit.collider.name} na posi��o {hit.point}");
        }
        else
        {
            // Desenha uma linha at� o final da dist�ncia m�xima, caso n�o acerte nada
            Debug.DrawLine(origin, origin + direction * rayDistance, Color.red);
            BaseOn = true;
          //  Debug.Log("Nenhum impacto detectado para baixo.");
        }
    }
}