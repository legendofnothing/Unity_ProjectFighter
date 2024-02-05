using UnityEngine;

namespace Bound {
    public class DestroyViewport : MonoBehaviour {
        private void OnTriggerEnter2D(Collider2D other) {
            Destroy(other.gameObject);
        }
    }
}