using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class ObjMili : MonoBehaviour
{
    public float launchSpeed = 10f;
    public float rotationSpeed;
    public bool isLaunched = false;
    public List<Transform> objects = new List<Transform>();
    public MaoMiliControl maoMiliControl;
    public bool _naMao;
    public float verticalSpeed = 0f;
    float gravity = -5.81f;
    public GameObject atackMiliBox;
    public Collider _miliCollider;

    public Vector3 _posIni;

    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer;
    public float disappearDelay = 2f;

    bool hasHitGround = false;
    float disappearTimer = 0f;

    public bool _objMove;

    Vector3 posIni;

    void Start()
    {

        posIni = transform.position;
    }


    void Update()
    {
        if (_objMove)
        {
            _miliCollider.enabled = false;
            if (isLaunched && !hasHitGround)
            {
               

                if (transform.parent != null)
                {
                    maoMiliControl = transform.parent.GetComponent<MaoMiliControl>();
                    maoMiliControl._objMili = GetComponent<ObjMili>();

                    transform.SetParent(null);
                    transform.rotation = Quaternion.Euler(0, maoMiliControl._m_transform.eulerAngles.y, 0);
                }
               

                verticalSpeed += gravity * Time.deltaTime;

                Vector3 moveDirection = transform.forward * launchSpeed;
                moveDirection.y += verticalSpeed;

                transform.position += moveDirection * Time.deltaTime;

                if (objects != null && objects.Count > 0)
                {
                    objects[0].Rotate(new Vector3(360 * rotationSpeed, 0, 0) * Time.deltaTime);
                }

                // BoxCast simples para detectar o ch�o
                if (Physics.BoxCast(transform.position, transform.localScale / 2f, Vector3.down, Quaternion.identity, groundCheckDistance, groundLayer))
                {
                    hasHitGround = true;
                    disappearTimer = disappearDelay;
                    atackMiliBox.SetActive(false);
                    _miliCollider.enabled = false;

                }
            }

            // Ap�s tocar o ch�o, inicia o timer pra desaparecer
            if (hasHitGround)
            {
                disappearTimer -= Time.deltaTime;
                if (disappearTimer <= 0f)
                {
                   // gameObject.SetActive(false); // ou Destroy(gameObject);
                    _miliCollider.GetComponent<MeshRenderer>().enabled = false;
                    _objMove =false;
                    ResetOBJ();
                    hasHitGround=false;

}
            }
        }
    }

    void ResetOBJ()
    {
        // Volta para a posi��o inicial
        transform.position = posIni;

        // Reseta a rota��o
        objects[0].eulerAngles = Vector3.zero;

        // Zera a velocidade vertical
        verticalSpeed = 0f;

        // Reseta flags de controle
        isLaunched = false;
        hasHitGround = false;
        _objMove = false;

        // Reativa componentes visuais e colisores
        if (_miliCollider != null)
        {
            _miliCollider.enabled = true;

            MeshRenderer renderer = _miliCollider.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.enabled = true;
            }
        }

        // desativa hitbox de ataque
        if (atackMiliBox != null)
        {
            atackMiliBox.SetActive(false);
        }

        // Reseta o tempo de desaparecimento
        disappearTimer = disappearDelay;

        // (Opcional) Reanexa ao pai original se quiser deixar ele na m�o de novo
        // transform.SetParent(maoMiliControl._m_transform); // se quiser isso
    }
}
