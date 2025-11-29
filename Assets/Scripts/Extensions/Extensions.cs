using UnityEngine;

public static class AnimatorExtensions
{
    public static void PlaySmooth(this Animator animator, int stateHash, float fadeDuration = 0.1f, int layer = 0)
    {
        if (animator == null) return;

        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(layer);

        if (info.fullPathHash == stateHash || info.shortNameHash == stateHash) return;

        AnimatorTransitionInfo transitionInfo = animator.GetAnimatorTransitionInfo(layer);
        if (transitionInfo.fullPathHash == stateHash || transitionInfo.userNameHash == stateHash)
            return;

        animator.CrossFade(stateHash, fadeDuration, layer, 0f);
    }
}
