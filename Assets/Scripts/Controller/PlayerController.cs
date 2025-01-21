using UnityEngine;

/// <summary>
/// Respons�vel por receber inputs, atualizar o modelo (Model)
/// e orquestrar o que deve ser exibido (View).
/// </summary>
[RequireComponent(typeof(PlayerView))]
public class PlayerController : MonoBehaviour
{
    [Header("Refer�ncia ao ScriptableObject Model")]
    [SerializeField] private PlayerModel playerModel;

    [Header("Depend�ncias (View)")]
    private PlayerView playerView;

    [Header("Configura��es de Movimento")]
    [SerializeField] private CharacterController characterController;
    // Ou use um Rigidbody, de acordo com a sua prefer�ncia.

    private void Awake()
    {
        playerView = GetComponent<PlayerView>();
    }

    private void Update()
    {
        // 1. Capturar Input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool isShiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        // 2. Determinar se est� andando ou correndo
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude > 0.1f)
        {
            // Se estiver movendo (magnitude > 0.1 para evitar "tremida")
            playerModel.isWalking = true;
            playerModel.isRunning = isShiftPressed;
        }
        else
        {
            // Se n�o estiver movendo
            playerModel.isWalking = false;
            playerModel.isRunning = false;
        }

        // 3. Calcular velocidade com base no Model
        float currentSpeed = playerModel.isRunning ? playerModel.runSpeed : playerModel.walkSpeed;
        Vector3 velocity = direction * currentSpeed;

        // 4. Aplicar movimento
        // Se estiver usando CharacterController:
        characterController.SimpleMove(velocity);

        // Caso use transform.Translate em vez de CharacterController:
        // transform.Translate(velocity * Time.deltaTime, Space.World);

        // 5. Atualizar rota��o do personagem (opcional)
        // Se quiser que o personagem rotacione para a dire��o do movimento:
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(direction),
                0.2f
            );
        }

        // 6. Pedir para a View atualizar as anima��es
        playerView.UpdateAnimations(playerModel.isWalking, playerModel.isRunning);
    }
}
