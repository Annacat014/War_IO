﻿
using UnityEngine;

namespace LearnGame.Shooting
{
    public class ShootingController : MonoBehaviour
    {
        public bool HastTarget => _target != null; 
        public Vector3 TargetPosition => _target.transform.position;

        private Weapon _weapon;

        private Collider[] _collider = new Collider[2];

        private float _nexShotTimerSec;

        private GameObject _target;

        
        protected void Update()
        {

            _target = GetTarget();

            _nexShotTimerSec -= Time.deltaTime;

            if ( _nexShotTimerSec < 0 )
            {
                if (HastTarget)
                    _weapon.Shoot(TargetPosition);
                
                //var target = transform.forward * 100f;
                //_weapon.Shoot(target);

                _nexShotTimerSec = _weapon.ShootFrequencySec;
            }
        }

        public void SetWeapon( Weapon weaponPrefab, Transform hand )
        {
            _weapon = Instantiate(weaponPrefab, hand);
            _weapon.transform.localPosition = Vector3.zero;
            _weapon.transform.localRotation = Quaternion.identity;
        }

        private GameObject GetTarget()
        {
            GameObject target = null; 
            
            var position = _weapon.transform.position;
            var radius = _weapon.ShootRadius;
            var mask = LayerUtils.EnemyMask;

            var size = Physics.OverlapSphereNonAlloc(position, radius, _collider, mask);

            if (size > 0)
            {
                for (int i = 0; i < size; i++)
                {
                    if (_collider[i].gameObject != gameObject)
                    {
                        target = _collider[i].gameObject;
                        break;
                    }
                }
            }
            return target; 
        }

    }
}