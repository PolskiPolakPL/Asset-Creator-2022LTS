using UnityEngine;

public class BuildState : BaseState
{
    public override void EnterState(BuilderStateManager stateManager)
    {
        Debug.Log("Current State: BUILDING");
    }

    public override void UpdateState(BuilderStateManager stateManager)
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            stateManager.SwitchState(stateManager.DefaultState);
        }
    }

    public override void ExitState(BuilderStateManager stateManager)
    {
        Debug.Log("Exiting BUILDING State");
    }
}
