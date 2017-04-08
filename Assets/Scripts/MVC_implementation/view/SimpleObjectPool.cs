using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleObjectPool : MonoBehaviour {

    public GameObject prefab;
    private Stack <GameObject> inactiveInstances = new Stack<GameObject>();
    #region GetObject
    public GameObject GetObject()
    {
        GameObject spawnedGameObject;
        if (inactiveInstances.Count > 0)
        {
            spawnedGameObject = inactiveInstances.Pop();
        }
        else
        {
            spawnedGameObject = (GameObject)GameObject.Instantiate(prefab);

            PooledObject pooledObject = spawnedGameObject.AddComponent<PooledObject>();
            pooledObject.pool = this;
        }

        spawnedGameObject.transform.SetParent(null);
        spawnedGameObject.SetActive(true);
        return spawnedGameObject;
    }
    #endregion

    #region ReturnObject
    public void ReturnObject(GameObject toReturn)
    {
        PooledObject pooledObject = toReturn.GetComponent<PooledObject>();

        if (pooledObject != null && pooledObject.pool == this)
        {
            toReturn.transform.SetParent(transform);
            toReturn.SetActive(false);

            inactiveInstances.Push(toReturn);

        }
        else
        {
            Debug.LogWarning(toReturn.name + "Destroy");
            Destroy(toReturn);
        }
    }
}
#endregion
public class PooledObject : MonoBehaviour
{
    public SimpleObjectPool pool;
}
