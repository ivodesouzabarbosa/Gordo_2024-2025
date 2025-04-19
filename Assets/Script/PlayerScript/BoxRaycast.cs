using DG.Tweening;
using UnityEngine;

public class BoxRaycast : MonoBehaviour
{

    public float maxDistance = 5f;  // Distância máxima do BoxCast
    public Vector3 boxSize = new Vector3(1f, 1f, 1f);  // Tamanho da caixa
    public LayerMask layerMask;  // Camadas que o BoxCast irá detectar
    public Vector3 offset = new Vector3(0f, 1f, 0f);  // Offset para ajustar a posição
    public Transform _transformOBj;
    public Transform _transformM;
    public MaoMiliControl _maoMiliControl;

    private void Start()
    {
        transform.parent.GetComponent<PlayerControl>()._boxRaycast = this;
    }

    void Update()
    {
        // Ajusta a origem do BoxCast com base na posição do objeto e no offset
        Vector3 origin = transform.position + transform.rotation * offset;

        // Direção do BoxCast (para onde o objeto está olhando)
        Vector3 direction = transform.forward;

        // Executa o BoxCast
        if (!_maoMiliControl._playerMove.luva &&
            !_maoMiliControl._playerMove._maoOcupada &&
            Physics.BoxCast(origin, boxSize / 2, direction, out RaycastHit hit, transform.rotation, maxDistance, layerMask))
        {
            Debug.Log("Colidiu com: " + hit.collider.name);
            _transformOBj = hit.collider.gameObject.transform.parent;
            _maoMiliControl._objMili = _transformOBj.GetComponent<ObjMili>();
        }
        else if (!_maoMiliControl._playerMove.luva &&
                 !_maoMiliControl._playerMove._maoOcupada &&
                 _transformOBj != null)
        {
            _transformOBj = null;
        }
    }

    // Desenha o BoxCast na cena para depuração
    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Vector3 origin = transform.position + transform.rotation * offset;
        Vector3 direction = transform.forward;

        // Define a matriz de transformação para desenhar corretamente a rotação do BoxCast
        Gizmos.color = Color.cyan;
        Gizmos.matrix = Matrix4x4.TRS(origin, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boxSize);

        // Desenha a caixa no final do BoxCast
        Gizmos.matrix = Matrix4x4.TRS(origin + direction * maxDistance, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boxSize);

        // Desenha uma linha indicando o trajeto
        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, origin + direction * maxDistance);
    }

    public void ObjMove()
    {
        if (_transformOBj != null)
        {
            _transformOBj.SetParent(_transformM);
            _transformOBj.DOLocalMove(Vector3.zero, .5f);
            _transformOBj.DOLocalRotate(Vector3.zero, .7f);
        }
    }

}