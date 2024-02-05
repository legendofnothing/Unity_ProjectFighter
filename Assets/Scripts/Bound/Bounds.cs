using System.Collections.Generic;
using UnityEngine;

//Bounds.cs
namespace Bound {
    public class Bounds : MonoBehaviour
    {
        [System.Serializable]
        public struct ColliderInfo
        {
            public ColliderPosition position;
            public BoxCollider2D collider;
        }
        public enum ColliderPosition
        {
            Top,
            Bottom,
            Right,
            Left
        }

        public List<ColliderInfo> colliders;
        public Camera boundCamera;

        [Header("Extra Settings:")]

        public float colliderThickness;
        public float offset;

        private void Awake() {
           GenerateBound();
        }
        
        private void GenerateBound() {
             var cameraPos = boundCamera.transform.position;
            var screenSize = boundCamera.ScreenToWorldPoint(new Vector3(Screen.width + offset, Screen.height + offset));

            foreach (var obj in colliders)
            {
                //the 1f is little offset so the thing overlaps to make sure no edge cases 
                obj.collider.transform.localScale = obj.position switch
                {
                    ColliderPosition.Top => new Vector3(screenSize.x * 2 + colliderThickness, colliderThickness, colliderThickness),
                    ColliderPosition.Bottom => new Vector3(screenSize.x * 2 + colliderThickness, colliderThickness, colliderThickness),
                    ColliderPosition.Right => new Vector3(colliderThickness, screenSize.y * 2 + colliderThickness, colliderThickness),
                    ColliderPosition.Left => new Vector3(colliderThickness, screenSize.y * 2 + colliderThickness, colliderThickness),
                    _ => throw new System.ArgumentOutOfRangeException()
                };

                obj.collider.transform.position = obj.position switch
                {
                    ColliderPosition.Top => new Vector3(
                        cameraPos.x
                        , screenSize.y + obj.collider.transform.localScale.y * 0.5f
                        , 0),

                    ColliderPosition.Bottom => new Vector3(
                        cameraPos.x
                        , -screenSize.y - obj.collider.transform.localScale.y * 0.5f
                        , 0),

                    ColliderPosition.Right => new Vector3(
                        screenSize.x + obj.collider.transform.localScale.x * 0.5f
                        , cameraPos.y
                        , 0),

                    ColliderPosition.Left => new Vector3(
                        -screenSize.x - obj.collider.transform.localScale.x * 0.5f
                        , cameraPos.y
                        , 0),

                    _ => throw new System.ArgumentOutOfRangeException()
                };
            }
        }
    }
}