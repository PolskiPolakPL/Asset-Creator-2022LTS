// Blueprint for other states
using UnityEngine;

public abstract class BaseState
{
    public abstract void EnterState(BuilderStateManager stateManager);
    public abstract void UpdateState(BuilderStateManager stateManager);
    public abstract void ExitState(BuilderStateManager stateManager);
}
