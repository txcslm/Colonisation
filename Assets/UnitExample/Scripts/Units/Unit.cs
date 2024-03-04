using CastleExample.Scripts;
using UnitExample.Scripts.Units.StateMachine;
using UnitExample.Scripts.Units.StateMachine.States;
using UnityEngine;

namespace UnitExample.Scripts.Units
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private Base _base;

        private UnitStateMachine _stateMachine;

        public bool IsFree { get; set; }
        public Vector3 Target { get; private set; }

        private void Awake() =>
            _stateMachine = new UnitStateMachine(this);

        private void Start()
        {
            _stateMachine.SwitchState<IdleState>();
        }

        private void Update() =>
            _stateMachine.Update(Time.deltaTime);

        public void SetTarget(Vector3 target)
        {
            Debug.Log($"Setting target for {gameObject.name} to {target}");
            Target = target;
        }
    }
}
