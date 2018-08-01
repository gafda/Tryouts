using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A very simples object pooling class
/// </summary>
public class SimpleObjectPool : MonoBehaviour
{
    /// <summary>
    /// The prefab that this object pool returns instances of
    /// </summary>
    public GameObject prefab;

    /// <summary>
    /// Collection of currently inactive instances of the prefab
    /// </summary>
    private Stack<GameObject> inactiveInstances;

    public SimpleObjectPool()
    {
        this.inactiveInstances = new Stack<GameObject>(0);
    }

    /// <summary>
    /// Returns an instance of the prefab
    /// </summary>
    /// <returns></returns>
    public GameObject GetObject()
    {
        GameObject spwanedGameObject;

        // if there is an inactive instance of the prefab ready to return, return that
        if (this.inactiveInstances.Count > 0)
        {
            // remove the instance from the collection of inactive instances
            spwanedGameObject = inactiveInstances.Pop();
        }
        else
        {
            spwanedGameObject = GameObject.Instantiate(prefab);

            // add the PooledObejct component to the prefab so we know it came from this pool
            var pooledObject = spwanedGameObject.AddComponent<PooledObject>();
            pooledObject.pool = this;
        }

        // put the instance in the root of the scene and enable it
        spwanedGameObject.transform.SetParent(null);
        spwanedGameObject.SetActive(true);

        // return a reference to the instace
        return spwanedGameObject;
    }

    /// <summary>
    /// Returns an instance of the prefab to the pool.
    /// </summary>
    /// <param name="toReturn">To return.</param>
    public void ReturnObject(GameObject toReturn)
    {
        var pooledObject = toReturn.GetComponent<PooledObject>();

        // if instance came from this pool, return it to the pool
        if (pooledObject != null && pooledObject.pool == this)
        {
            // make the instance a child of this and disable it
            toReturn.transform.SetParent(transform);
            toReturn.SetActive(false);

            // add the instance to the collection of inactive instances
            this.inactiveInstances.Push(toReturn);
        }
        else
        {
            // otherwise, just destroy it
            Debug.LogWarning($"{toReturn.name} was returned to a pool it wasn't from! Destroying...");
            Destroy(toReturn);
        }
    }
}