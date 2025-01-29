using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NPCView), typeof(NavMeshAgent))]
public class NPCController : MonoBehaviour
{
    [SerializeField] private NPCModel npcModel;
    [SerializeField] private Transform target;

    private NPCView npcView;
    private NavMeshAgent agent;

    private bool canMove = false; // Flag para indicar se o NPC já pode andar

    private void Awake()
    {
        npcView = GetComponent<NPCView>();
        agent = GetComponent<NavMeshAgent>();
    } 

    private void Start()
    {
        // Configura o NavMeshAgent de acordo com os dados do Model,
        // mas NÃO chamamos SetDestination aqui
        if (npcModel != null)
        {
            agent.speed = npcModel.runSpeed;
            agent.stoppingDistance = npcModel.stoppingDistance;
        }
    }

    private void Update()
    {
        if (!canMove || target == null)
        {
            // Se ainda não "liberamos" o movimento ou não há target, animação fica inativa
            npcModel.isRunning = false;
            npcView.SetRunning(false);
            return;
        }

        if (!agent.pathPending)
        {
            float distanceToDestination = agent.remainingDistance;

            // Se o NPC está longe do destino, isRunning = true
            if (distanceToDestination > agent.stoppingDistance)
            {
                npcModel.isRunning = true;
            }
            else
            {
                // Dentro da stoppingDistance -> para
                npcModel.isRunning = false;
            }

            // Atualiza a animação
            npcView.SetRunning(npcModel.isRunning);
        }
    }

    /// <summary>
    /// Método público para iniciar a movimentação até o alvo.
    /// </summary>
    public void StartMovement()
    {
        if (target != null)
        {
            canMove = true;               // Libera o movimento
            agent.SetDestination(target.position); // Define destino no NavMesh
        }
        else
        {
            Debug.LogWarning("Target não definido no NPCController.", this);
        }
    }
}
