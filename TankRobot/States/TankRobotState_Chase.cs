using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class TankRobotState_Chase : IState
    {
        private RobotVision _robotVision;
        private NavMeshAgent _navMeshAgent;
        private Player _player;
        private float _speed;
        private float _minDistance;
        private float _maxDistance;

        public TankRobotState_Chase(RobotVision robotVision, NavMeshAgent navMeshAgent, float speed, float minDistance, float maxDistance)
        {
            _robotVision = robotVision;
            _navMeshAgent = navMeshAgent;
            _speed = speed;
            _minDistance = minDistance;
            _maxDistance = maxDistance;
        }

        public void OnEnter()
        {
            _navMeshAgent.speed = _speed;
            _navMeshAgent.stoppingDistance = _minDistance;
        }

        public void OnExit()
        {
            _player = null;
        }

        public void Tick()
        {
            if (_robotVision.IsVisible(out _player))
            {
                _navMeshAgent.destination = _player.transform.position;
            }

        }

        public bool IsLostPlayer()
        {
            return _player == null || Vector3.Distance(_player.transform.position, _navMeshAgent.transform.position) > _maxDistance;
        }

        public bool IsDone()
        {
            return _navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete && _player != null;
        }
    }
}