using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Events;
using Core.Logging;
using Enemies.Variants;
using Icon;
using Sirenix.OdinInspector;
using UnityEngine;
using EventType = Core.Events.EventType;

namespace SpawnSystem {
    [Serializable]
    public struct SpawnData {
        [TitleGroup("Config")]
        public Vector2 position;
        public GameObject toSpawn;
        [TitleGroup("Spawning Config")] 
        public float spawningDelay;
        public bool spawnInInterval;
        [ShowIf(nameof(spawnInInterval))] public float spawningInterval;
    }

    [Serializable]
    public struct SpawnDataList {
        public float duration;
        [Space]
        public List<SpawnData> data;
    }
    
    public class SpawnManager : MonoBehaviour {
        public List<SpawnDataList> spawns = new();
        [TitleGroup("Editor Preview Only")]
        [PropertyRange(0, nameof(_spawnCounts))]
        public int wave;

        private static int _spawnCounts;
        private List<GameObject> _currentSpawnObjects = new();

        private void Start() {
            StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine() {
            var list = spawns;
#if UNITY_EDITOR
            list = spawns.Skip(wave).ToList();
            NCLogger.Log($"Debugging wave {wave}");
#endif
            foreach (var spawnList in list) {
                foreach (var spawn in spawnList.data) {
                    _currentSpawnObjects.Add(SpawnObject(spawn.position, spawn.spawnInInterval, spawn.toSpawn, spawn.spawningDelay ,spawn.spawningInterval));
                }

                yield return new WaitForSeconds(spawnList.duration);

                foreach (var inst in _currentSpawnObjects) {
                    Destroy(inst);
                }
            }
            this.FireEvent(EventType.OnSpawnSystemFinished);
            yield return null;
        }

        private GameObject SpawnObject(Vector3 position, bool inInterval, GameObject toSpawn, float delay, float interval) {
            var inst = new GameObject("SpawningInstance", typeof(DrawIcon), typeof(SpawnInstance)) {
                transform = {
                    position = position,
                    parent = gameObject.transform
                }
            };
            inst.GetComponent<SpawnInstance>().Init(new SpawnInstanceSetting {
                spawningObject = toSpawn,
                spawningDelay = delay,
                spawningInInterval = inInterval,
                spawningInterval = interval
            });
            return inst;
        }

        private void OnDrawGizmosSelected() {
            if (spawns.Count > 0) {
                _spawnCounts = spawns.Count - 1;
                if (spawns.Contains(spawns.ElementAtOrDefault(wave))) {
                    foreach (var data in spawns[wave].data) {
                        Gizmos.DrawWireCube(data.position, new Vector3(1f, 1f));

                        if (data.toSpawn != null) {
                            DebugExtension.DrawString(data.toSpawn.name,
                                (Vector3)data.position - new Vector3(0, 0.8f, 0f), Color.white, Vector2.zero, 10f);
                        }
                    }
                }
            }
            else _spawnCounts = 0;
        }
    }
}
