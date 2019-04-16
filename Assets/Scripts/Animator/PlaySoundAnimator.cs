using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Play sound on state
/// </summary>
public class PlaySoundAnimator : StateMachineBehaviour
{
   
    [SerializeField] private SoundManager.sound soundToPlay;
    [SerializeField] private float timer;

    private bool canPlay = true;
    float time;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (canPlay) {
            canPlay = false;
            SoundManager.instance.PlaySound(soundToPlay);
            time = timer;
        }

    }


    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(time > 0) {
            time -= 0.1f;
        }
        else {
            canPlay = true;
        }
       
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
