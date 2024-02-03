using UnityEngine;

namespace Icon {
    public class DrawIcon : MonoBehaviour {
        private void OnDrawGizmos() {
            Gizmos.DrawIcon(transform.position, "konata.jpg", true);
        }
    }
}