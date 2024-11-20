using UnityEngine;
using System.Collections.Generic;

public class MovimentoPersonagem : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;

    private float velocidade;
    private Vector3 gravidade = new Vector3(0f, -9.81f, 0f);

    private bool estaOrando;

    // Invent�rio do personagem
    public List<string> inventario = new List<string>(); // Lista simples armazenando nomes de itens

    // Start � chamado antes da primeira atualiza��o
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        velocidade = 0f;
        estaOrando = false;
    }

    // M�todo para adicionar itens ao invent�rio
    public void AdicionarAoInventario(string nomeItem)
    {
        inventario.Add(nomeItem);
        Debug.Log("Item adicionado ao invent�rio: " + nomeItem);
    }

    // Update � chamado uma vez por frame
    void Update()
    {
        float movimentoHorizontal = Input.GetAxis("Horizontal");
        float movimentoVertical = Input.GetAxis("Vertical");

        Vector3 movimento = new Vector3(movimentoHorizontal, 0, movimentoVertical);

        if (Input.GetKeyDown(KeyCode.C))
        {
            estaOrando = true;
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            estaOrando = false;
        }

        if (!estaOrando)
        {
            if (movimento != Vector3.zero) // Se est� movimentando...
            {
                if (Input.GetKey(KeyCode.LeftShift)) // Se est� pressionando Shift...
                {
                    velocidade = 4f; // Velocidade de corrida
                }
                else
                {
                    velocidade = 2f; // Velocidade de caminhada
                }

                characterController.Move(movimento.normalized * velocidade * Time.deltaTime); // Move o personagem

                Quaternion rotacaoAlvo = Quaternion.LookRotation(movimento);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotacaoAlvo, Time.deltaTime * 10f); // Rotaciona o personagem
            }
            else
            {
                velocidade = 0f; // Sem movimento
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                Debug.Log("Invent�rio:");
                foreach (string item in inventario)
                {
                    Debug.Log("- " + item);
                }
            }

            characterController.Move(gravidade * Time.deltaTime); // Aplica gravidade

            // Atualiza anima��es
            animator.SetBool("Parado", velocidade <= 0f);
            animator.SetBool("Andando", velocidade > 0f && velocidade <= 2f);
            animator.SetBool("Correndo", velocidade > 2f);
        }
       // Debug.Log("Est� orando: " + estaOrando);
    }
}
