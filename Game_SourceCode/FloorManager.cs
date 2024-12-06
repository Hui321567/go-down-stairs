using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
   public GameObject[] FloorPrefabs;
    public void SpawnFloor()
    {
       int r = Random.Range(0, FloorPrefabs.Length);
       GameObject Floor = Instantiate(FloorPrefabs[r], transform);
        Floor.transform.position = new Vector3(Random.Range(-3.8f, 3.8f), -6f, 0f);
    }
}
