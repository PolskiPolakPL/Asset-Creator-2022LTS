using UnityEngine;

public class BuilderStateManager : MonoBehaviour
{

    //FSM Classes
    BaseState _CurrentState;
    public DefaultState DefaultState = new DefaultState();
    public BuildingState BuildingState = new BuildingState();

    private void Start()
    {
        _CurrentState = DefaultState;
        _CurrentState.EnterState(this);
    }
    private void Update()
    {
        _CurrentState.UpdateState(this);
    }

    public void SwitchState(BaseState newState)
    {
        _CurrentState.ExitState(this);
        _CurrentState = newState;
        newState.EnterState(this);
    }
}