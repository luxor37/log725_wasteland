using UnityEngine;
using UnityEngine.AI;

public class NavigationBaker : MonoBehaviour
{

    void Start()
    {
        BakeSurfaces();
    }

    public static void BakeSurfaces()
    {
        foreach (var obj in GameObject.FindGameObjectsWithTag("Ground"))
        {
            obj.GetComponent<NavMeshSurface>().BuildNavMesh();
            obj.GetComponent<NavMeshSurface>().UpdateNavMesh(obj.GetComponent<NavMeshSurface>().navMeshData);
        }
    }
}
