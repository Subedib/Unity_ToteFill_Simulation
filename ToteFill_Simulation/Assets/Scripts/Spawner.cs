using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject totePrefab;

    public void SpawnTote()
    {
        Instantiate(totePrefab, transform.position, Quaternion.identity);
    }
}
