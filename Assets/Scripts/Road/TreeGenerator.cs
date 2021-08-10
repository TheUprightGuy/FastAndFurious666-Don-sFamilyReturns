using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TreeGenerator : MonoBehaviour
{
    public GameObject treePrefab;
    public Transform treeParent;
    
    public Bounds SpawnBounds;
    public Vector2Int GridSize;

    [SerializeField]
    float DistanceFromRoad = 2.0f;


    public bool GenForest = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnValidate()
    {
        if (GenForest)
        {
            GenForest = false;
            CreateForest();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void CommitDeforestation()
    {
        if (treeParent == null)
        {
            return;
        }
        List<Transform> destroyList = new List<Transform>();
        foreach (Transform tree in treeParent)
        {

            float distanceFromRoad = RoadUtilities.instance.GetDistanceToLine(tree.position);
            if (distanceFromRoad < RoadUtilities.instance.LineWidth + DistanceFromRoad
                /*RoadUtilities.instance.IsOnLine(tree.position)*/)
            {
                destroyList.Add(tree);
            }
        }

        foreach (Transform item in destroyList)
        {
            Destroy(item.gameObject);
        }
    }

    void CreateForest()
    {

        if (treePrefab == null)
        {
            return;
        }

        Transform parent = treeParent;
        if (parent == null)
        {
            parent = this.transform;
        }

        Vector3 botLeft = SpawnBounds.min;

        //Gizmos.DrawSphere(botLeft, 10.0f);
        Vector3 spacing = new Vector3(SpawnBounds.size.x / GridSize.x, 0.0f, SpawnBounds.size.z / GridSize.y);

        for (int i = 0; i <= GridSize.x; i++)
        {
            for (int k = 0; k <= GridSize.y; k++)
            {
                Vector3 posInSquare = new Vector3(i * spacing.x, 0.0f, k * spacing.z);

                Vector3 randInSquare = new Vector3(Random.Range(0.0f, spacing.x), 0.0f, Random.Range(0.0f, spacing.z));
                
                posInSquare += randInSquare;
                posInSquare += botLeft;
                posInSquare += transform.position;

                Instantiate(treePrefab, posInSquare, Quaternion.identity, parent);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(SpawnBounds.center + transform.position, SpawnBounds.size);

        //Vector3 botLeft = SpawnBounds.min;

        ////Gizmos.DrawSphere(botLeft, 10.0f);
        //Vector3 spacing = new Vector3(SpawnBounds.size.x / GridSize.x, 0.0f, SpawnBounds.size.z / GridSize.y);

        //for (int i = 0; i <= GridSize.x; i++)
        //{
        //    for (int k = 0; k <= GridSize.y; k++)
        //    {
        //        Vector3 pos = new Vector3(i * spacing.x, 0.0f, k * spacing.z);
        //        pos += botLeft;
        //        pos += transform.position;

        //        //Gizmos.DrawSphere(pos, 0.25f);
        //    }
        //}
    }


}
