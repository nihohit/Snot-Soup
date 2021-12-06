using CzernyStudio.Utilities;
using System;
using UnityEngine;

namespace SnotSoup.Gameplay.Ingredients {
    public class IngredientsSpawner : MonoBehaviour {
        [SerializeField] private RandomObjectPool pool;
        [SerializeField] private Transform[] spawnPoints;

        public static Action<Vector3> OnRespawnIngredient;
        public static Action<GameObject> OnReturnIngredientToPool;
        
        protected void Awake() {
            OnRespawnIngredient += ReSpawnRandomIngredientAtPosition;
            OnReturnIngredientToPool += ReturnIngredientToPool;
            
            SpawnIngredientsOnGameStart();
        }

        protected void OnDestroy() {
            OnRespawnIngredient -= ReSpawnRandomIngredientAtPosition;
            OnReturnIngredientToPool -= ReturnIngredientToPool;
        }

        private void SpawnIngredientsOnGameStart() {
            for (int i = 0; i < spawnPoints.Length; i++) {
                var ingredient = pool.GetObject(i);
                ingredient.transform.position = spawnPoints[i].position;
                ingredient.GetComponent<IngredientView>().SpawnPosition = spawnPoints[i].position;
            }
        }
  
        private void ReSpawnRandomIngredientAtPosition(Vector3 spawnPosition) {
            var ingredientObject = pool.GetRandomObject();
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