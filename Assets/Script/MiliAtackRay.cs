using UnityEngine;

public class MiliAtackRay : MonoBehaviour
{
    public float castDistance = 5f;
    public LayerMask hitLayers;
    public HitSliderEnemy _hitSliderEnemy;

    public ObjMili objMili;

    void Update()
    {
        Vector3 center = transform.position;
        Vector3 halfExtents = transform.localScale / 2f;
        Quaternion orientation = transform.rotation;

        Collider[] hits = Physics.OverlapBox(center, halfExtents, orientation, hitLayers);

        foreach (var hit in hits)
        {
            Debug.Log("Tocando: " + hit.name);
            _hitSliderEnemy = hit.transform.parent.GetComponent<HitSliderEnemy>();
            _hitSliderEnemy.HitMili(objMili._selectPersonMili);
            if (objMili._objMove)
            {
                objMili._hitEnemy = true;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}

