﻿
using UnityEngine;
using UnityEngine.UIElements;

namespace LearnGame.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovmentController : MonoBehaviour
    {
        private static readonly float SqrEpsilon = Mathf.Epsilon * Mathf.Epsilon;
        
        [SerializeField]
        private float _speed = 1f;
        [SerializeField]
        private float _maxRadiansDelta = 10f;
        public Vector3 MovementDirection { get; set; }
        public Vector3 LookDirection { get; set; }

        private CharacterController _characterController;

        protected void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }


        protected void Update()
        {
            Translate();

            if (_maxRadiansDelta > 0f && LookDirection != Vector3.zero)
            {
                Rotate();
            }
        }


        private void Translate()
        {
            var delta = MovementDirection * _speed * Time.deltaTime;
            _characterController.Move(delta);
            
            if (Input.GetKey(KeyCode.Space))
            {
                var deltaB = MovementDirection * _speed * 2 * Time.deltaTime;
                _characterController.Move(deltaB);
            }
        }

        private void Rotate()
        {
            var currentLookDirection = transform.rotation * Vector3.forward;
            float sqrMagnirude = (currentLookDirection - LookDirection).sqrMagnitude;

            if (sqrMagnirude > SqrEpsilon)
            {
                var newRotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(LookDirection, Vector3.up),
                    _maxRadiansDelta * Time.deltaTime);

                transform.rotation = newRotation;
            }
        }
    }
}