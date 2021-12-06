using CzernyStudio.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SnotSoup.Gameplay.Ingredients {
    public class IngredientsSpawner : MonoBehaviour {
        [SerializeField] private RandomObjectPool pool;
        [SerializeField] private AudioSource dropIngredientSource;
        [SerializeField] private Transform[] spawnPoints;

        public static Action<Vector3> OnRespawnIngredient;
        public static Action<GameObject> OnReturnIngredientToPool;
        public static Action<GameObject> OnSpawnMiniIngredient;
        public static Action ClearMiniIngredientList;

        private List<GameObject> _miniIngredientsPerCooking = new List<GameObject>();
        
        protected void Awake() {
            OnRespawnIngredient += ReSpawnRandomIngredientAtPosition;
            OnReturnIngredientToPool += ReturnIngredientToPool;
            OnSpawnMiniIngredient += AddMiniIngredientsToList;
            ClearMiniIngredientList += ClearMiniIngredients;
            
            SpawnIngredientsOnGameStart();
        }

        protected void OnDestroy() {
            OnRespawnIngredient -= ReSpawnRandomIngredientAtPosition;
            OnReturnIngredientToPool -= ReturnIngredientToPool;
            OnSpawnMiniIngredient -= AddMiniIngredientsToList;
            ClearMiniIngredientList -= ClearMiniIngredients;
        }

        private void SpawnIngredientsOnGameStart() {
            for (int i = 0; i < spawnPoints.Length; i++) {
                var ingredient = pool.GetObject(i);
                ingredient.transform.position = spawnPoints[i].position;
                ingredient.GetComponent<IngredientView>().SpawnPosition = spawnPoints[i].position;
            }
        }
  
        private void ReSpawnRandomIngredientAtPosition(Vector3 spawnPosition) {
            dropIngredientSource?.Play();
            var ingredientObject = pool.GetRandomObject();
            var ingredientView = ingredientObject.GetComponent<IngredientView>();
            if (ingredientView == null) return;
            ingredientView.transform.position = spawnPosition;
            ingredientView.SpawnPosition = spawnPosition;
        }

        private void ReturnIngredientToPool(GameObject ingredientObject) {
            pool.ReturnObject(ingredientObject);
        }

        private void AddMiniIngredientsToList(GameObject ingredient) {
            _miniIngredientsPerCooking.Add(ingredient);
        }

        private void ClearMiniIngredients() {
            for (int i = 0; i < _miniIngredientsPerCooking.Count; i++) {
                Destroy(_miniIngredientsPerCooking[i]);
            }
        }
    }
}