using UnityEngine;

namespace SnotSoup.Gameplay.Ingredients{
    public class IngredientView : MonoBehaviour {
        [SerializeField] private IngredientModel model;

        public string Name { get { return model.Name; } }

        private Vector3 _initialPosition;
        
        public IngredientModel IngredientModel {
            get {
                return model;
            }
        }

        public Vector3 InitialPosition {
            get {
                return _initialPosition;
            }
        }

        protected void Awake() {
            _initialPosition = transform.localPosition;
        }

        protected void OnCollisionEnter(Collision collision)
        {
            if (collision.other.CompareTag("Cauldron"))
            {
                var c = collision.other.GetComponent<Cauldron>();
                c.Add(model);
                Destroy(gameObject);
            }
        }
    }
}

