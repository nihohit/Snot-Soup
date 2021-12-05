using System;
using CzernyStudio.Utilities;
using UnityEngine;

namespace SnotSoup.Gameplay.Ingredients {
    public class IngredientsSpawner : MonoBehaviour {
        [SerializeField] private SimpleObjectPool pool;

        public static Action<Vector3> OnRespawnIngredient;
        public static Action<GameObject> OnReturnIngredient;
        
        //TODO implement even bus
        protected void Awake() {
            OnRespawnIngredient += ReSpawnRandomIngredientAtPosition;
            OnReturnIngredient += ReturnIngredientToPool;
        }

        protected void OnDestroy() {
            OnRespawnIngredient -= ReSpawnRandomIngredientAtPosition;
            OnReturnIngredient -= ReturnIngredientToPool;
        }
  
        private void ReSpawnRandomIngredientAtPosition(Vector3 spawnPosition) {
            var ingredientObject = pool.GetObject();
            var ingredientView = ingredientObject.GetComponent<IngredientView>();
            if (ingredientView == null) return;
            ingredientView.transform.position = spawnPosition;
            ingredientView.SpawnPosition = spawnPosition;
        }

        private void ReturnIngredientToPool(GameObject ingredientObject) {
            pool.ReturnObject(ingredientObject);
        }
    }
}