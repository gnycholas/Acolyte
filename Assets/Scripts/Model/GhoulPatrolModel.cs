using UnityEngine;

[CreateAssetMenu(menuName = "Ghoul/GhoulPatrolModel")]
public class GhoulPatrolModel : ScriptableObject
{
    [Header("Configura��es de movimento - Patrulha")]
    public float walkSpeed = 2f;
    public float idleTime = 2f;
    public float minRandomDistance = 3f;
    public float maxRandomDistance = 8f;
    public Transform patrolCenter;

    [Header("Detec��o do jogador")]
    [Tooltip("Raio de vis�o do monstro (em metros).")]
    public float detectionRadius = 10f;

    [Tooltip("Se quiser simular campo de vis�o, defina �ngulo (em graus). 0 = desativado.")]
    public float fieldOfViewAngle = 120f;

    [Header("Anima��o de scream")]
    [Tooltip("Dura��o (em segundos) da anima��o de scream antes de iniciar corrida.")]
    public float screamDuration = 2f;

    [Header("Configura��es de corrida (chase)")]
    public float runSpeed = 4f;
    [Tooltip("Ap�s perder a vis�o do jogador, quanto tempo at� retomar patrulha.")]
    public float chaseTimeout = 5f;
}
