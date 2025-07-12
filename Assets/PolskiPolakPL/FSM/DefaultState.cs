using UnityEngine;

public class DefaultState : BaseState
{
    public override void EnterState(BuilderStateManager stateManager)
    {
        Debug.Log("Current State: DEFAULT");
    }

    public override void UpdateState(BuilderStateManager stateManager)
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            stateManager.SwitchState(stateManager.BuildingState);
        }
    }

    public override void ExitState(BuilderStateManager stateManager)
    {
        Debug.Log("Exiting DEFAULT State");
    }
}
