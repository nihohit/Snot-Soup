using UnityEngine;

namespace SnotSoup.Gameplay.Ingredients {
  public class IngredientView : MonoBehaviour {
    [SerializeField] private IngredientModel model;
    [SerializeField] private GameObject miniIngredientView;
  
    public string Name { get { return model.Name; } }
    public string Description { get { return model.Description; } }

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
      if (!string.IsNullOrWhiteSpace(model.ResourceName)) {
        transform.Find("Graphic").gameObject.SetActive(false);
        var resource = Resources.Load<GameObject>(model.ResourceName);
        var instance = Instantiate(resource);
        instance.transform.position = transform.position;
        instance.transform.parent = transform;
      }

    }

    protected void OnCollisionEnter(Collision collision) {
      if (collision.gameObject.CompareTag("Cauldron")) {
        var c = collision.gameObject.GetComponent<Cauldron>();
        c.Add(model);
        var miniIngredient = Instantiate(miniIngredientView);
        miniIngredient.transform.position = transform.position;
        IngredientsSpawner.OnRespawnIngredient(_spawnPosition);
        IngredientsSpawner.OnReturnIngredientToPool(gameObject);
      }
    }
  }
}
