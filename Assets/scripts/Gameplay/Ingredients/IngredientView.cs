using UnityEngine;

namespace SnotSoup.Gameplay.Ingredients{
    public class IngredientView : MonoBehaviour {
        [SerializeField] private IngredientModel model;

        public string Name { get { return model.Name; } }

        private Vector3 _spawnPosition;
        
        public IngredientModel IngredientModel {
            get {
                return model;
            }
        }

        public Vector3 SpawnPosition {
            get {
                return _spawnPosition;
            }
            set {
                _spawnPosition = value;
            }
        }

        protected void Awake() {
            _spawnPosition = transform.localPosition;
        }

        protected void OnCollisionEnter(Collision collision)
        {
            if (collision.other.CompareTag("Cauldron"))
            {
                var c = collision.other.GetComponent<Cauldron>();
                c.Add(model);
                IngredientsSpawner.OnRespawnIngredient(_spawnPosition);
                IngredientsSpawner.OnReturnIngredientToPool(gameObject);
            }
        }
    }
}

