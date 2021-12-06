using UnityEngine;

namespace SnotSoup.Gameplay.Ingredients {
    public class BounceGameObject : MonoBehaviour {
        [SerializeField] private float speed = 0.5f;

        private bool isBouncing;

        protected void OnEnable() {
            isBouncing = true;
        }

        protected void OnDestroy() {
            Destroy(gameObject);
        }
        
        protected void Update() {
            if (!isBouncing) return;
            var bounce = 0.2f * Mathf.Sin(Time.time) * 0.5f + 1.4f;
            transform.position = new Vector3(transform.position.x, bounce, transform.position.z);
        }
    }
}