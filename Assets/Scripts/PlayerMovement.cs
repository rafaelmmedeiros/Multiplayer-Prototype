using Mirror;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent = null;

    private Camera mainCamera;

    #region Server

    [Command]
    private void CmdMove(Vector3 target)
    {
        if (!NavMesh.SamplePosition(target, out NavMeshHit hit, 1f, NavMesh.AllAreas)) return;

        navMeshAgent.SetDestination(hit.position);
    }

    #endregion

    #region Client

    public override void OnStartAuthority()
    {
        mainCamera = Camera.main;
    }

    [ClientCallback]
    private void Update()
    {
        if (!hasAuthority) return;

        if (!Input.GetMouseButton(1)) return;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) return;

        CmdMove(hit.point);
    }

    #endregion
}
