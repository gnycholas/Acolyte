using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom Editor para desenhar gizmos de debug (raio de detec��o, FOV etc.)
/// </summary>
[CustomEditor(typeof(GhoulPatrolController))]
public class GhoulPatrolControllerEditor : Editor
{
    private void OnSceneGUI()
    {
        // "target" � o objeto que estamos inspecionando
        GhoulPatrolController controller = (GhoulPatrolController)target;
        if (controller == null) return;
        if (controller.Model == null) return; // caso seu script use: public GhoulPatrolModel Model => model;

        // Vamos desenhar o raio de detec��o
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(controller.transform.position, Vector3.up, controller.Model.detectionRadius);

        // Se tiver FOV, desenha um arco
        if (controller.Model.fieldOfViewAngle > 0)
        {
            // Ponto central e dire��o
            Vector3 centerPos = controller.transform.position;
            Vector3 forward = controller.transform.forward;
            float halfFov = controller.Model.fieldOfViewAngle * 0.5f;

            // Cor do arco
            Handles.color = Color.cyan;
            // Desenha um arco no plano Y
            Handles.DrawWireArc(
                centerPos,
                Vector3.up,
                Quaternion.Euler(0, -halfFov, 0) * forward,
                controller.Model.fieldOfViewAngle,
                controller.Model.detectionRadius
            );

            // Desenha linhas at� as extremidades do arco
            Vector3 leftDir = Quaternion.Euler(0, -halfFov, 0) * forward * controller.Model.detectionRadius;
            Vector3 rightDir = Quaternion.Euler(0, halfFov, 0) * forward * controller.Model.detectionRadius;

            Handles.DrawLine(centerPos, centerPos + leftDir);
            Handles.DrawLine(centerPos, centerPos + rightDir);
        }
    }
}
