using UnityEngine;

/// <summary>
/// View respons�vel por controlar a parte visual (anima��es).
/// N�o cont�m l�gica de movimenta��o ou dados permanentes.
/// </summary>
public class NPCView : MonoBehaviour
{
    [SerializeField] private Animator animator;

    // Nome do par�metro que ativa anima��o de corrida no Animator
    private const string ANIM_IS_RUNNING = "isRunning";

    /// <summary>
    /// Atualiza o par�metro de anima��o
    /// </summary>
    public void SetRunning(bool running)
    {
        animator.SetBool(ANIM_IS_RUNNING, running);
    }
}
