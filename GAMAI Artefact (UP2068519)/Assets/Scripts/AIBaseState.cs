using UnityEngine;

public abstract class AIBaseState
{
    public abstract void EnterState(StateManager AI);
    public abstract void UpdateState(StateManager AI);
}

