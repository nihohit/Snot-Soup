using UnityEngine;

namespace SnotSoup.Gameplay.Ingredients{
    public class IngredientView : MonoBehaviour {
        [SerializeField] private IngredientModel model;

        public IngredientModel IngredientModel {
            get {
                return model;
            }
        }
    }
}

