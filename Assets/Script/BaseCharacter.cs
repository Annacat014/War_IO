using LearnGame.Movement;
using LearnGame.Shooting;
using LearnGame.PickUp;
using UnityEngine;
using LearnGame.Enemy;

namespace LearnGame
{
    [RequireComponent(typeof(CharacterMovmentController), typeof(ShootingController))]
    public abstract class BaseCharacter : MonoBehaviour
    {
        [SerializeField]
        private Weapon _baseWeaponPrefab;

        [SerializeField]
        private Transform _hand;

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private float _health = 2f;

        private IMovementDirectionSource _movementDirectionSource;
        private CharacterMovmentController _characterMovmentController;
        private ShootingController _shootingController;
        //private EnemyTarget _enemyTarget;
        
        protected void Awake()
        {
            _movementDirectionSource = GetComponent<IMovementDirectionSource>();
            _characterMovmentController = GetComponent<CharacterMovmentController>();
            _shootingController = GetComponent<ShootingController>();
            //_enemyTarget = GetComponent<EnemyTarget>();
        }

        protected void Start()
        {
            SetWeapon(_baseWeaponPrefab);
        }

        protected void Update()
        {
            var direction = _movementDirectionSource.MovementDirection;
            var lookDirection = direction;
            if (_shootingController.HastTarget)
            {
                lookDirection = (_shootingController.TargetPosition - transform.position).normalized;
            }

            _characterMovmentController.MovementDirection = direction;
            _characterMovmentController.LookDirection = lookDirection;
            //int SIZE = _enemyTarget.FindAAllTargets(LayerUtils.CharactersMask);
            

            _animator.SetBool("IsMoving", direction != Vector3.zero);
            _animator.SetBool("IsShooting", _shootingController.HastTarget);
            //_animator.SetBool("Win", SIZE == 1);

            if (_health <= 0f)
                Destroy(gameObject);
        }

        protected float Get_health()
        {
            return _health;
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (LayerUtils.IsBullet(other.gameObject))
            {
                var bullet = other.gameObject.GetComponent<Bullet>();

                _health -= bullet.Damage;

                Destroy(other.gameObject);
            }
            else if (LayerUtils.IsPickUp(other.gameObject))
            {
                var pickUp = other.gameObject.GetComponent<PickUpWeapon>();
                pickUp.PickUp(this);
                //_shootingController.SetWeapon(pickUp.WeaponPrefab, _hand);

                Destroy(other.gameObject);
            }
        }

        public void SetWeapon(Weapon weapon)
        {
            _shootingController.SetWeapon(weapon, _hand);
        }
    }
}

