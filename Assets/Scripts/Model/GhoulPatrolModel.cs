using UnityEngine;

[CreateAssetMenu(menuName = "Ghoul/GhoulPatrolModel")]
public class GhoulPatrolModel : ScriptableObject
{
    [Header("Configura��es de movimento")]
    [Tooltip("Velocidade de caminhada do Ghoul.")]
    public float walkSpeed = 2f;

    [Header("Configura��es de patrulha")]
    [Tooltip("Tempo de espera (em segundos) ao terminar de andar antes de escolher uma nova dire��o.")]
    public float idleTime = 2f;

    [Tooltip("Dist�ncia m�nima do ponto aleat�rio (raio interno).")]
    public float minRandomDistance = 3f;

    [Tooltip("Dist�ncia m�xima do ponto aleat�rio (raio externo).")]
    public float maxRandomDistance = 8f;

    [Header("�rea de patrulha")]
    [Tooltip("Se quiser limitar a patrulha a uma �rea, defina um ponto central.")]
    public Transform patrolCenter;
}
