using UnityEngine;

/// <summary>
/// Respons�vel pela parte visual do personagem (Animator, GameObject 3D, etc.).
/// </summary>
public class PlayerView : MonoBehaviour
{
    [Header("Refer�ncia ao Animator")]
    [SerializeField] private Animator animator;

    // Par�metros do Animator
    private const string ANIM_IS_WALKING = "isWalking";
    private const string ANIM_IS_RUNNING = "isRunning";

    /// <summary>
    /// Atualiza par�metros de anima��o
    /// com base nos valores passados.
    /// </summary>
    public void UpdateAnimations(bool isWalking, bool isRunning)
    {
        animator.SetBool(ANIM_IS_WALKING, isWalking);
        animator.SetBool(ANIM_IS_RUNNING, isRunning);
    }

    // Se quiser controlar rota��o do personagem, 
    // ou eventos de anima��o (Animation Events), 
    // voc� pode colocar aqui tamb�m.
}
