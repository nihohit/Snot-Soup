using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SnotSoup.Gameplay {
    public class PlayerOnInputAnimationController : MonoBehaviour {
        [SerializeField] private Animator animator;
        [SerializeField] private string pickupAnimationTriggerName = "Pickup";
        [SerializeField] private string placeAnimationTriggerName = "Place";
        [SerializeField] private string cookAnimationTriggerName = "Cook";

        private int _pickupAnimationHash;
        private int _placeAnimationHash;
        private int _cookAnimationHash;
        
        protected void Awake() {
            _pickupAnimationHash = Animator.StringToHash(pickupAnimationTriggerName);
            _placeAnimationHash = Animator.StringToHash(placeAnimationTriggerName);
            _cookAnimationHash = Animator.StringToHash(cookAnimationTriggerName);
        }

        public void OnInteract(InputValue value) {

        }
        
        public void OnMove(InputValue value) {
        }

        public void OnLook(InputValue value) {
            
        }

        public void OnJump(InputValue value) {
        }

        public void OnSprint(InputValue value) {
        }
    }
}
