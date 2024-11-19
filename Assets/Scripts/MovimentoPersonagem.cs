using UnityEngine;

public class MovimentoPersonagem : MonoBehaviour
{

    private CharacterController characterController;
    private Animator animator;

    private float velocidade;
    private Vector3 gravidade = new Vector3 (0f, -9.81f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        velocidade = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        characterController.Move (gravidade * Time.deltaTime);

        float movimentoHorizontal = Input.GetAxis("Horizontal");
        float movimentoVertical = Input.GetAxis("Vertical");

        Vector3 movimento = new Vector3 (movimentoHorizontal, 0, movimentoVertical);

        if (movimento != Vector3.zero) // Caso esteja pressionando algum bot�o de movimenta��o...
        {
            if (Input.GetKey(KeyCode.LeftShift)) // Se estiver pressionando a tecla shift...
            {
                velocidade = 4f; // Aumenta a velocidade para a corrida...
            }
            else
            {
                velocidade = 2f; // Do contr�rio, mant�m a velocidade de caminhada...
            }

            characterController.Move(movimento.normalized * velocidade * Time.deltaTime); // Invoca o componente Character controller para mover o personagem, conforme a dire��o e velocidade atual...

            Quaternion rotacaoAlvo = Quaternion.LookRotation(movimento);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacaoAlvo, Time.deltaTime * 10f); // Altera a dire��o que o personagem est� olhando...
        } else
        {
            velocidade = 0f; // Caso n�o esteja pressionando nenhum bot�o de movimenta��o, ent�o sua velocidade � zero...
        }

        animator.SetBool("Parado", velocidade <= 0f);
        animator.SetBool("Andando", velocidade > 0f && velocidade <= 2f);
        animator.SetBool("Correndo", velocidade > 2f);

        Debug.Log(velocidade);
    }
}
