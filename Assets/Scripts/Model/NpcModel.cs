using UnityEngine;

/// <summary>
/// Model responsável por guardar dados de movimento e estado do NPC.
/// Como ScriptableObject, podemos editar no Inspector e reaproveitar valores.
/// </summary>
[CreateAssetMenu(menuName = "Model/NpcModel", fileName = "NpcModel")]
public class NPCModel : ScriptableObject
{
    [Header("Configurações de Movimento")]
    public float runSpeed = 4.0f;          // Velocidade de corrida
    public float stoppingDistance = 1.0f;  // Distância de parada

    [Header("Estados")]
    public bool isRunning; // Se estiver em movimento (correndo), definimos como true
}
