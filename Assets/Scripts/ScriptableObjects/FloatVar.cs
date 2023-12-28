using UnityEngine;

namespace ScriptableObjects {
    [CreateAssetMenu(fileName = "FloatVar", menuName = "ScriptableObjects/Float Vars", order = 1)]

    public class FloatVar : ScriptableObject {
        public float Value;
    }
}
