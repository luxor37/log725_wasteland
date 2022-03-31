using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBaker : MonoBehaviour
{
    public List<NavMeshSurface> surfaces;

    void Start()
    {
        foreach (var obj in GameObject.FindGameObjectsWithTag("Ground"))
        {
            surfaces.Add(obj.GetComponent<NavMeshSurface>());
            obj.GetComponent<NavMeshSurface>().BuildNavMesh();
        }
        
    }
}
