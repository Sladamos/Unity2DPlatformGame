using UnityEngine;

public class EagleStateDead : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SendMessage("SetInactive");
    }
}
