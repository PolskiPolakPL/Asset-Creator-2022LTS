using UnityEngine;

public class BuilderStateManager : MonoBehaviour
{

    //FSM Classes
    BaseState _CurrentState;
    public DefaultState DefaultState = new DefaultState();
    public BuildState BuildState = new BuildState();

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

    public void SwitchState(int stateID)
    {
        switch (stateID)
        {
            case 0:
                SwitchState(DefaultState); break;
                case 1:
                SwitchState(BuildState); break;
            default:
                SwitchState(DefaultState); break;
        }
    }
}