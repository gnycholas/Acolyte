using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class GhoulPatrolController : MonoBehaviour
{
    [Header("Refer�ncias MVC")]
    [SerializeField] private GhoulPatrolModel model; // ScriptableObject com configs
    [SerializeField] private GhoulPatrolView view;   // Controla as anima��es
    [SerializeField] private NavMeshAgent agent;     // Refer�ncia ao NavMeshAgent

    private bool _isIdle;                            // Flag para indicar se est� parado

    // Se quiser limitar a patrulha a uma �rea, voc� pode armazenar aqui a pos. inicial
    private Vector3 _patrolCenterPosition;

    private void Start()
    {
        // Garante que temos um agent associado
        if (agent == null) agent = GetComponent<NavMeshAgent>();

        // Ajusta a velocidade do agent com base no Model
        agent.speed = model.walkSpeed;

        // Define o centro da patrulha
        if (model.patrolCenter != null)
        {
            _patrolCenterPosition = model.patrolCenter.position;
        }
        else
        {
            // Se n�o tiver um "patrolCenter" definido, usamos a posi��o inicial do Ghoul
            _patrolCenterPosition = transform.position;
        }

        // Escolhe o primeiro destino de patrulha
        ChooseNewDestination();
    }

    private void Update()
    {
        if (_isIdle) return;

        // Se o agente n�o estiver processando o caminho e tiver chegado ao destino
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            // Inicia a l�gica de ficar parado
            StartCoroutine(IdleRoutine());
        }
    }

    /// <summary>
    /// Escolhe um ponto aleat�rio no NavMesh dentro de um raio e manda o agente para l�.
    /// </summary>
    private void ChooseNewDestination()
    {
        // Gera um ponto aleat�rio na esfera (ou c�rculo no plano) entre min e max
        var randomRadius = Random.Range(model.minRandomDistance, model.maxRandomDistance);
        var randomDirection = Random.insideUnitSphere * randomRadius;

        // Ajusta a altura e desloca em rela��o ao centro
        randomDirection += _patrolCenterPosition;
        randomDirection.y = _patrolCenterPosition.y; // mant�m o y consistente (se desejar)

        NavMeshHit hit;
        // Tenta encontrar uma posi��o v�lida no NavMesh
        if (NavMesh.SamplePosition(randomDirection, out hit, model.maxRandomDistance, NavMesh.AllAreas))
        {
            // Se encontrou uma posi��o v�lida, manda o agente para l�
            _isIdle = false;
            agent.SetDestination(hit.position);

            // Toca a anima��o de caminhada
            view.PlayWalkAnimation();
        }
        else
        {
            // Se n�o encontrou, tenta novamente (cuidado para n�o cair em loop infinito)
            ChooseNewDestination();
        }
    }

    /// <summary>
    /// Rotina de Idle (ficar parado) por alguns segundos antes de definir nova rota.
    /// </summary>
    private IEnumerator IdleRoutine()
    {
        _isIdle = true;
        // Toca anima��o de Idle
        view.PlayIdleAnimation();

        // Espera pelo tempo configurado no Model
        yield return new WaitForSeconds(model.idleTime);

        // Ap�s o tempo, escolhe outro ponto
        ChooseNewDestination();
    }
}
