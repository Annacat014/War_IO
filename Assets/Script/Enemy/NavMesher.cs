using UnityEngine;
using UnityEngine.AI;

namespace LearnGame.Enemy
{
    public class NavMesher
    {

        private const float DistanceEps = 1.5f;
        public bool IsPathCalculated { get; private set; }

        private readonly NavMeshQueryFilter _filter;
        private readonly Transform _agentTransform;

        private NavMeshPath _navMashPath;
        private NavMeshHit _targetHit;
        private int _currentPathPoinIndex;
        public NavMesher(Transform agentTransform)
        {
            _filter = new NavMeshQueryFilter { areaMask = NavMesh.AllAreas };
            IsPathCalculated = false;

            _agentTransform = agentTransform;
        }

        public void CalculatePath(Vector3 targetPosirion)
        {
            NavMesh.SamplePosition(_agentTransform.position, out var agentHit, 10f, _filter);
            NavMesh.SamplePosition(targetPosirion, out _targetHit, 10f, _filter);

            IsPathCalculated = NavMesh.CalculatePath(agentHit.position, _targetHit.position, _filter, _navMashPath);
            _currentPathPoinIndex = 0;
        }

        public Vector3 GetCurrentPoint()
        {
            var currentPoint = _navMashPath.corners[_currentPathPoinIndex];
            var distance = (_agentTransform.position - currentPoint).magnitude;

            if (distance < DistanceEps) 
                _currentPathPoinIndex++;

            if(_currentPathPoinIndex >= _navMashPath.corners.Length)
                IsPathCalculated = false ;
            else
                currentPoint = _navMashPath.corners[_currentPathPoinIndex];

            return currentPoint;

        }
        public float DistanseToTargetPointFrom(Vector3 position) => (_targetHit.position - position).magnitude;
    }
}

