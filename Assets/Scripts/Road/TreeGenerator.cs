using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour
{
    public GameObject treePrefab;
    public GameObject treeParent;

    public Bounds SpawnBounds;
    public Vector2Int GridSize;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateForest()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(SpawnBounds.center, SpawnBounds.size);    
    }


}
