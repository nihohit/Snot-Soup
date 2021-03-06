using UnityEngine;
using System.Collections.Generic;

namespace CzernyStudio.Utilities {
  public class RandomObjectPool : MonoBehaviour {
    [SerializeField] GameObject[] pooledPrefabs;

    public GameObject[] PooledPrefabs {
      get {
        return pooledPrefabs;
      }
    }

    private Stack<GameObject> _inactiveInstances = new Stack<GameObject>();

    public GameObject GetObject(int index) {
      GameObject spawnedGameObject;

      // if there is an inactive instance of the prefab ready to return, return that
      if (_inactiveInstances.Count > 0) {
        // remove the instance from teh collection of inactive instances
        spawnedGameObject = _inactiveInstances.Pop();
      }
      // otherwise, create a new instance
      else {
        spawnedGameObject = Instantiate(pooledPrefabs[index]);

        var pooledObject = spawnedGameObject.GetComponent<PooledObject>();
        pooledObject.pool = this;
      }

      // put the instance in the root of this script
      spawnedGameObject.transform.SetParent(transform, false);
      spawnedGameObject.SetActive(true);

      // return a reference to the instance
      return spawnedGameObject;
    }

    public GameObject GetRandomObject() {
      GameObject spawnedGameObject;

      var randomIndex = Random.Range(0, pooledPrefabs.Length);
      spawnedGameObject = Instantiate(pooledPrefabs[randomIndex]);
      var view = spawnedGameObject.GetComponent<SnotSoup.Gameplay.Ingredients.IngredientView>();
      view.LoadModel();

      var pooledObject = spawnedGameObject.GetComponent<PooledObject>();
      pooledObject.pool = this;

      // put the instance in the root of this script
      spawnedGameObject.transform.SetParent(transform, false);
      spawnedGameObject.SetActive(true);

      // return a reference to the instance
      return spawnedGameObject;
    }

    // Return an instance of the prefab to the pool
    public void ReturnObject(GameObject toReturn) {
      var pooledObject = toReturn.GetComponent<PooledObject>();

      // if the instance came from this pool, return it to the pool
      if (pooledObject != null && pooledObject.pool == this) {
        // make the instance a child of this and disable it
        toReturn.SetActive(false);

        Destroy(toReturn);
      }
      // otherwise, just destroy it
      else {
        Debug.LogWarning(toReturn.name + " was returned to a pool it wasn't spawned from! Destroying.");
        Destroy(toReturn);
      }
    }
  }
}