using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStateMachineBehaviour : StateMachineBehaviour
{
    public event Action Enter;
    public event Action Exit;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

      
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Exit?.Invoke();


    }
}
