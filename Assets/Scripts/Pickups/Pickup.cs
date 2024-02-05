using Core;
using Core.Events;
using UnityEngine;
using EventType = Core.Events.EventType;

namespace Pickups {
    public class Pickup : MonoBehaviour {
        public float flyDownSpeed;
        public PickupType type;
        public LayerMask interactLayer;
        public LayerMask playerLayer;

        private Rigidbody2D _rigidbody;
        
        private void Start() {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update() {
            _rigidbody.velocity = -transform.up * flyDownSpeed;
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (CheckLayerMask.IsInLayerMask(collision.gameObject, interactLayer)) {
                Destroy(gameObject);
                if (CheckLayerMask.IsInLayerMask(collision.gameObject, playerLayer)) this.FireEvent(EventType.OnPickupAdded, type);
            }
        }
    }
}