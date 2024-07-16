using System.Collections;
using UnityEngine;
using LearnGame.Enemy.States;

namespace LearnGame.Enemy
{
    public class EnemyAiController : MonoBehaviour
    {
        [SerializeField]
        private float _viewRadius = 20f;

        private EnemyTarget _target;
        private EnemyStateMachine _stateMachine;
        protected void Awake()
        {
            var player = FindObjectOfType<PlayerCharacter>();

            var enemyDirectionController = GetComponent<EnemyDirectionController>();

            var navMasher = new NavMesher(transform);
            _target = new EnemyTarget(transform, player, _viewRadius);

            _stateMachine = new EnemyStateMachine(enemyDirectionController, navMasher, _target);
        }

       
        protected void Update()
        {
            _target.FindClosest();
            _stateMachine.UpDate();
        }
    }
}