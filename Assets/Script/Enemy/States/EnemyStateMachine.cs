using LearnGame.FSM;
using System.Collections.Generic;


namespace LearnGame.Enemy.States
{
    public class EnemyStateMachine : BaseStateMachine
    {
        private const float NavMashTurnOffDistance = 5;

        public EnemyStateMachine(EnemyDirectionController enemyDirectionController,
            NavMesher navMesher, EnemyTarget target) 
        {
            var idleState = new IdleState();
            var findWayState = new FindWayState(target, navMesher, enemyDirectionController);
            var moveForwaardState = new MoveForwardState(target, enemyDirectionController);

            SetInitialState(idleState);

            //переходы состояний
            AddState(state: idleState, transitions: new List<Transition>
            {
                new Transition(
                    findWayState,
                    () => target.DistanceToClosesFromAgent() > NavMashTurnOffDistance),
                new Transition(
                    moveForwaardState,
                    () => target.DistanceToClosesFromAgent() <= NavMashTurnOffDistance),
            }
            );

            AddState(state: findWayState, transitions: new List<Transition>
            {
                new Transition(
                    idleState,
                    () => target.Closest == null),
                new Transition(
                    moveForwaardState,
                    () => target.DistanceToClosesFromAgent() <= NavMashTurnOffDistance),
            }
           );

            AddState(state: moveForwaardState, transitions: new List<Transition>
            {
                new Transition(
                    idleState,
                    () => target.Closest == null),
                new Transition(
                    findWayState,
                    () => target.DistanceToClosesFromAgent() > NavMashTurnOffDistance),
            }
           );
        }
    }
}
