using UnityEngine;

namespace ScriptableObjects {
    [CreateAssetMenu(fileName = "IntVar", menuName = "ScriptableObjects/Int Vars", order = 2)]

    public class IntVar : ScriptableObject {
        public float Value;
    }
}