using UnityEngine;

/// <summary>
/// View responsável por controlar a parte visual (animações).
/// Não contém lógica de movimentação ou dados permanentes.
/// </summary>
public class NPCView : MonoBehaviour
{
    [SerializeField] private Animator animator;

    // Nome do parâmetro que ativa animação de corrida no Animator
    private const string ANIM_IS_RUNNING = "isRunning";

    /// <summary>
    /// Atualiza o parâmetro de animação
    /// </summary>
    public void SetRunning(bool running)
    {
        animator.SetBool(ANIM_IS_RUNNING, running);
    }
}
