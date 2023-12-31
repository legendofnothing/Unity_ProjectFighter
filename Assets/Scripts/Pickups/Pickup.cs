using Core;
using Core.Events;
using UnityEngine;
using EventType = Core.Events.EventType;

namespace Pickups {
    public class Pickup : MonoBehaviour {
        public PickupType type;
        public LayerMask interactLayer;
        public LayerMask playerLayer;
        
        private void OnTriggerEnter2D(Collider2D collision) {
            if (CheckLayerMask.IsInLayerMask(collision.gameObject, interactLayer)) {
                Destroy(gameObject);
                if (CheckLayerMask.IsInLayerMask(collision.gameObject, playerLayer)) this.FireEvent(EventType.OnPickupAdded, type);
            }
        }
    }
}