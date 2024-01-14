﻿using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Enemies.Variants {
    public class EnemyChaseBehavior : EnemyBehaviour {
        [TitleGroup("Chasing Config")] 
        public float chaseSpeed;
        [Range(0f, 10f)]
        public float rotateSpeed;
        
        private float _currentSpeed;
        private float _currentRotateSpeed;

        protected override void OnEnter() {
            _currentSpeed = chaseSpeed;
            _currentRotateSpeed = rotateSpeed;
            AimAt(PlayerPos);
        }

        protected override void OnUpdate() {
            LookAt(PlayerPos, _currentRotateSpeed);
            Fly(_currentSpeed, transform.up);
        }

        protected override void OnExit() {
            transform.rotation = Quaternion.Euler(Vector3.zero);
            _currentRotateSpeed = 0;
            _currentSpeed = 0;
        }
    }
}