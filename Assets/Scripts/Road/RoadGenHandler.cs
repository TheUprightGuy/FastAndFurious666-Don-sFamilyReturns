using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenHandler : MonoBehaviour
{
    public List<RoadGenerator> GeneratorLoadOrder = new List<RoadGenerator>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (RoadGenerator item in GeneratorLoadOrder)
        {
            item.LoadRoad();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
