using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NPCView), typeof(NavMeshAgent))]
public class NPCController : MonoBehaviour
{
    [SerializeField] private NPCModel npcModel;
    [SerializeField] private Transform target;

    private NPCView npcView;
    private NavMeshAgent agent;

    private bool canMove = false; // Flag para indicar se o NPC j� pode andar

    private void Awake()
    {
        npcView = GetComponent<NPCView>();
        agent = GetComponent<NavMeshAgent>();
    } 

    private void Start()
    {
        // Configura o NavMeshAgent de acordo com os dados do Model,
        // mas N�O chamamos SetDestination aqui
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
            // Se ainda n�o "liberamos" o movimento ou n�o h� target, anima��o fica inativa
            npcModel.isRunning = false;
            npcView.SetRunning(false);
            return;
        }

        if (!agent.pathPending)
        {
            float distanceToDestination = agent.remainingDistance;

            // Se o NPC est� longe do destino, isRunning = true
            if (distanceToDestination > agent.stoppingDistance)
            {
                npcModel.isRunning = true;
            }
            else
            {
                // Dentro da stoppingDistance -> para
                npcModel.isRunning = false;
            }

            // Atualiza a anima��o
            npcView.SetRunning(npcModel.isRunning);
        }
    }

    /// <summary>
    /// M�todo p�blico para iniciar a movimenta��o at� o alvo.
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
            Debug.LogWarning("Target n�o definido no NPCController.", this);
        }
    }
}
