using System.Collections.Generic;
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
        var surfaces = new List<NavMeshSurface>();
        foreach (var obj in GameObject.FindGameObjectsWithTag("Ground"))
        {
            surfaces.Add(obj.GetComponent<NavMeshSurface>());
            //obj.GetComponent<NavMeshSurface>().collectObjects == CollectObjects.Children;
            obj.GetComponent<NavMeshSurface>().BuildNavMesh();

            obj.GetComponent<NavMeshSurface>().UpdateNavMesh(obj.GetComponent<NavMeshSurface>().navMeshData);
        }
    }
}
