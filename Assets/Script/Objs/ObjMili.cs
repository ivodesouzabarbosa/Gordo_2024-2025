using System.Collections.Generic;
using UnityEngine;

public class ObjMili : MonoBehaviour
{
    public float launchSpeed = 10f;  // Velocidade do lan�amento
    public float rotationSpeed;
    public bool isLaunched = false;  // Controle de lan�amento
    public List<Transform> objects = new List<Transform>();


    Rigidbody _rb;
    void Start()
    {
      

    }


    void Update()
    {
        if (isLaunched)
        {
            // Move o objeto para frente na dire��o que ele est� olhando
            transform.Translate(new Vector3(2,0,0) * launchSpeed * Time.deltaTime);
            objects[0].Rotate(new Vector3(0, 0, -360), rotationSpeed * Time.deltaTime);
        }
    }
}
