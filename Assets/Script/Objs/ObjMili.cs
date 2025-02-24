using System.Collections.Generic;
using UnityEngine;

public class ObjMili : MonoBehaviour
{
    public float launchSpeed = 10f;  // Velocidade do lan�amento
    public float rotationSpeed;
    public bool isLaunched = false;  // Controle de lan�amento
    public List<Transform> objects = new List<Transform>();
    public MaoMiliControl maoMiliControl;
    public bool _naMao;


    Rigidbody _rb;
    void Start()
    {
      

    }


    void Update()
    {
        if (isLaunched)
        {
            if (transform.parent != null)
            {
                // Obt�m a refer�ncia ao controle do pai
                maoMiliControl = transform.parent.GetComponent<MaoMiliControl>();
                maoMiliControl._objMili = GetComponent<ObjMili>();

                // Remove o pai sem modificar a rota��o
                transform.SetParent(null);

                // Garante que o objeto mantenha a rota��o do pai antes de ser solto
                transform.rotation = Quaternion.Euler(0, maoMiliControl._m_transform.eulerAngles.y, 0);
            }

            // Move o objeto sempre para frente, sem inclinar para baixo
            transform.position += transform.forward * launchSpeed * Time.deltaTime;

            // Faz o objeto girar corretamente
            objects[0].Rotate(new Vector3(360 * rotationSpeed, 0, 0) * Time.deltaTime);
        }
    }
}
