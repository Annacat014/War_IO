using LearnGame.Movement;
using UnityEngine;

namespace LearnGame.Enemy
{
    public class EnemyDirectionController : MonoBehaviour, IMovementDirectionSource
    {
        public Vector3 MovementDirection {  get; private set; }

        public void UpDateMovementDirection(Vector3 targrtPosition)
        {
            var realdirection = targrtPosition - transform.position;
            MovementDirection = new Vector3(realdirection.x, 0, realdirection.z).normalized;
        }
    }
}