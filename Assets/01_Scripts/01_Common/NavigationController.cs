using UnityEngine;
using UnityEngine.AI;

public class NavigationController
{
    private NavMeshPath _path = new NavMeshPath();
    private Vector3 _curPosition = Vector3.zero;

    public Vector3 GetDirectionTo(Vector3 targetPos)
    {
        NavMesh.CalculatePath(
            _curPosition,
            targetPos,
            NavMesh.AllAreas,
            _path);

        if (_path.corners.Length >= 2)
        {
            Vector3 next = _path.corners[1];
            return (next - _curPosition).normalized;
        }

        return Vector3.zero;
    }

    public void UpdatePosition(Vector3 position)
    {
        _curPosition = position;
    }
}
