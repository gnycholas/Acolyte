using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPersonagem : MonoBehaviour {
    private CharacterController characterController;
    private Animator animator;
    [SerializeField] private float velocidade;

    private Vector3 gravidade = new Vector3(0f, -9.81f, 0f);

    private bool estaOrando = false;

    void Start() {
        velocidade = 0f;

        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        // Iniciar a ora��o quando a tecla C � pressionada
        if (Input.GetKeyDown(KeyCode.C) && !estaOrando) {
            estaOrando = true;
            animator.SetBool("Orando", true);
        }

        // Finalizar a ora��o quando a tecla C � solta
        if (Input.GetKeyUp(KeyCode.C) && estaOrando) {
            estaOrando = false;
            animator.SetBool("Orando", false);

        }

        // Aplica gravidade sempre
        characterController.Move(gravidade * Time.deltaTime);

        // Se n�o est� orando, permite o movimento
        if (!estaOrando){

            float movimentoHorizontal = Input.GetAxis("Horizontal");
            float movimentoVertical = Input.GetAxis("Vertical");

            Vector3 movimento = new Vector3(movimentoHorizontal, 0, movimentoVertical);

            if (movimento != Vector3.zero) {

                // Rotaciona o personagem na dire��o do movimento
                Quaternion rotacaoAlvo = Quaternion.LookRotation(movimento);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotacaoAlvo, Time.deltaTime * 10f);

                // Define a velocidade com base na tecla LeftShift
                if (Input.GetKey(KeyCode.LeftShift)) {
                    velocidade = 4f;
                } else {
                    velocidade = 2f;
                }

                // Move o personagem
                characterController.Move(movimento.normalized * velocidade * Time.deltaTime);

            } else {
                velocidade = 0f;
            }

            // Atualiza as anima��es de andar e correr
            animator.SetBool("Andando", velocidade > 0f && velocidade <= 2f);
            animator.SetBool("Correndo", velocidade > 2f);
        } else {
            // Se est� orando, impede qualquer movimento ou rota��o
            velocidade = 0f;

            // Mant�m as anima��es de movimento desativadas
            animator.SetBool("Andando", false);
            animator.SetBool("Correndo", false);
        }
    }
}
