using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerVolume : MonoBehaviour
{
    // Start is called before the first frame update

    BoxCollider boxCollider;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetPositionInBounds()
    {
        Bounds boxBounds = boxCollider.bounds;
        return new Vector3(Random.Range(boxBounds.min.x, boxBounds.max.x), transform.position.y, Random.Range(boxBounds.min.z, boxBounds.max.z));
    }
}
