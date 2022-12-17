using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    public State currentState { get; private set; }
     
    public void Initialize(State startState)
    {
        currentState = startState;
        currentState.Enter();
    }

    public void ChangeStatus(State newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
