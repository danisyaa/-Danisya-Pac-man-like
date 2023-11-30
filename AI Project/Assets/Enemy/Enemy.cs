using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public List<Transform> WayPoints = new List<Transform>();
    public float ChaseDistance;
    public Player Player;

    private BaseState _currentState;

    [HideInInspector] public PatrolState PatrolState = new PatrolState();
    [HideInInspector] public ChaseState ChaseState = new ChaseState();
    [HideInInspector] public RetreatState RetreatState = new RetreatState();

    public NavMeshAgent NavMeshAgent;

    public void SwitchState(BaseState state)
    {
        _currentState.ExitState(this);
        _currentState = state;
        _currentState.EnterState(this);
    }

    private void Awake()
    {
        _currentState = PatrolState;
        _currentState.EnterState(this);
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        if(Player != null)
        {
            Player.OnPowerUpStart += StartRetreating;
            Player.OnPowerUpStop += StopRetreating;
        }
    }

    private void Update()
    {
        if(_currentState != null)
        {
            _currentState.UpdateState(this);
        }
    }

    private void StartRetreating()
    {
        SwitchState(RetreatState);
    }

    private void StopRetreating()
    {
        SwitchState(PatrolState);
    }
}
