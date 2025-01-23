using UnityEngine;

public class GhoulPatrolView : MonoBehaviour
{
    [SerializeField] private Animator animator;

    // Hash dos par�metros (evita string repetida e melhora performance)
    private static readonly int IsWalkingHash = Animator.StringToHash("isWalking");

    /// <summary>
    /// Ativa a anima��o de caminhada.
    /// </summary>
    public void PlayWalkAnimation()
    {
        if (animator == null) return;
        animator.SetBool(IsWalkingHash, true);
    }

    /// <summary>
    /// Ativa a anima��o de idle (parado).
    /// </summary>
    public void PlayIdleAnimation()
    {
        if (animator == null) return;
        animator.SetBool(IsWalkingHash, false);
    }
}
