using CzernyStudio.Utilities;
using UnityEngine;

namespace SnotSoup.Gameplay.Ingredients {
    public class IngredientsSpawner : MonoBehaviour {
        [SerializeField] private SimpleObjectPool pool;

        protected void Awake() {
            
        }

        protected void OnDestroy() {
            
        }

        private void ReSpawnRandomIngredientAtPosition(Vector3 position) {
            var ingredientObject = pool.GetObject();
            //ingredientObject.transform.position = 
        }
    }
}
