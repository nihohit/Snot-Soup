using UnityEngine;

namespace SnotSoup.Gameplay.Ingredients{
    public class IngredientView : MonoBehaviour {
        [SerializeField] private IngredientModel model;

        public IngredientModel IngredientModel {
            get {
                return model;
            }
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

