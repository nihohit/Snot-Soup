using UnityEngine;

namespace SnotSoup.Gameplay.Ingredients {
    [CreateAssetMenu(fileName = "Ingredient Model", menuName = "Snot Soup /Ingredients/IngredientModel")]
    public class IngredientModel: ScriptableObject, IIngredientModel {
        public string Name { get; set; }
        public float Vorticity { get; set; }
        public float Viscosity { get; set; }
        public int SoupImpactScore { get; set; }
    }
}
