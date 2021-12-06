using UnityEngine;

namespace SnotSoup.Gameplay.Ingredients {
    public class BounceGameObject : MonoBehaviour {
        [SerializeField] private float speed = 4f;

        private bool _isBouncing;

        protected void OnEnable() {
            _isBouncing = true;
        }

        protected void OnDestroy() {
            Destroy(gameObject);
        }
        
        protected void Update() {
            if (!_isBouncing) return;
            var bounce = 0.2f * Mathf.Sin(Time.time * speed) * 0.5f + 1.2f;
            transform.position = new Vector3(transform.position.x, bounce, transform.position.z);
        }
    }
}