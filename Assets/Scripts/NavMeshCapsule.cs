using System;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NavMeshCapsule : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    [SerializeField] private NavMeshSurface _navMeshSurface;

    private Vector2 changeDirectionAfter = new Vector2(1f, 3f);
    private float changeDirectionTime;
    private float timer;
    private Vector3 destination;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        timer = 0;
        changeDirectionTime = Random.Range(changeDirectionAfter.x, changeDirectionAfter.y);
    }

    //Update destination every 5 seconds to test
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1)
        {
            if (_navMeshSurface.navMeshData == null)
            {
                _navMeshSurface.BuildNavMesh();
                _navMeshAgent.destination = SetRandomDest(_navMeshSurface.navMeshData.sourceBounds);
            }
        }

        if (timer > changeDirectionTime)
        {
            _navMeshAgent.destination = SetRandomDest(_navMeshSurface.navMeshData.sourceBounds);
            Debug.Log(_navMeshSurface.navMeshData.sourceBounds);
            timer = 0;
            changeDirectionTime = Random.Range(changeDirectionAfter.x, changeDirectionAfter.y);
        }
    }

    Vector3 SetRandomDest(Bounds bounds)
    {
        var x = Random.Range(bounds.min.x, bounds.max.x);
        var z = Random.Range(bounds.min.z, bounds.max.z);

        destination = new Vector3(x, 1, z);
        return destination;
    }
}