using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    // public static ObjectPool SharedInstance;
    protected GameControl _gameControl;
    [SerializeField] protected Transform ojbParent;
    protected List<GameObject> pooledObjects;
    [SerializeField] protected GameObject objectToPool;
    [SerializeField] protected int amountToPool;

    public virtual void Awake()
    {
        //  SharedInstance = this;
        _gameControl = GameObject.FindWithTag("GameController").GetComponent<GameControl>();
    }

    public virtual void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
            ObjectParentSet(tmp);

        }

    }

    void ObjectParentSet(GameObject inst)
    {
        inst.transform.SetParent(ojbParent);
    }



    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
