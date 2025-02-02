using UnityEngine;

[CreateAssetMenu(menuName = "Model/PlayerModel", fileName = "PlayerModel")]
public class PlayerModel : ScriptableObject
{
    [Header("Configura��es de Velocidade")]
    public float walkSpeed = 2f;
    public float runSpeed = 4f;

    [Header("Estados de Movimento")]
    public bool isWalking;
    public bool isRunning;

    [Header("Estados do Personagem")]
    public int maxHealth = 5;
    public int currentHealth = 5;
    public bool isInjured;
    public bool isDead;
    public bool isKnifeEquiped;

    [Header("Estado de Hit")]
    public bool isHit;
    // Valor padr�o (caso o clip n�o seja atribu�do); ser� substitu�do pelo comprimento do clip de hit.
    public float hitDuration = 1f;
}
