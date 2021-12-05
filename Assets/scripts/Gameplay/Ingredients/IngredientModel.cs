using UnityEngine;

namespace SnotSoup.Gameplay.Ingredients {
    [CreateAssetMenu(fileName = "Ingredient Model", menuName = "Snot Soup /Ingredients/IngredientModel")]
    public class IngredientModel: ScriptableObject, IIngredientModel {
        [SerializeField] private string name;
        [Range(0f,1f)]
        [SerializeField] private float vorticity;
        [Range(0f,1f)]
        [SerializeField] private float viscosity;
        [Range(-10, 10)]
        [SerializeField] private int soupImpactScore;

        public string Name {
            get {
                return name;
            } 
            set {
                name = value;
            }
        }

        public float Vorticity {
            get {
                return vorticity;
            }
            set {
                vorticity = value;
            }
        }

        public float Viscosity {
            get {
                return viscosity;
            }
            set {
                viscosity = value;
            }
        }

        public int SoupImpactScore {
            get { return soupImpactScore; }
            set { soupImpactScore = value; }
        }
    }
}
