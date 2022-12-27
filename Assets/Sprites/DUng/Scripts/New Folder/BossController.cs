using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private enum State
    {
        Walking,
        Dead
    }

    private State currentState;
    [SerializeField]
    
        

    private void Update()
    {
        switch (currentState)
        {
            case State.Walking:
                UpdateWalkingState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
    }

    

    //=walking state
    private void EnterWalkingState()
    {


    }

    private void UpdateWalkingState()
    {

    }

    private void ExitWalkingState()
    {

    }

    //=Dead state
    private void EnterDeadState()
    {


    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }

    //=other functionn
    private void SwitchState(State state)
    {
        switch (currentState)
        {
            case State.Walking:
                ExitWalkingState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        switch (state)
        {
            case State.Walking:
                EnterWalkingState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        currentState = state;
    }
}
