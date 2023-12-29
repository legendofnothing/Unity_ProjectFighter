using Player;
using UnityEngine;

namespace ScriptableObjects {
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Player/Data")]
    public class PlayerData : ScriptableObject {
        public PlayerStat stats; 
    }
}