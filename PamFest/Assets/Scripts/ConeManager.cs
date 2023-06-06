using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class ConeManager : MonoBehaviour
{
    public GameObject cone;

    [Header("Cone Spawning")]
    public int ammountOfCones = 0;
    public int coneIncreaseRate = 1;

    public Vector2 coneAreaMin;
    public Vector2 coneAreaMax;

    [Header("Cone Management")]
    public List<GameObject> spawnedCones = new List<GameObject>();
    public void spawnCones()
    {
        for (int i = 0; i < ammountOfCones; i++)
        {
            Vector2 position = new Vector2(Random.Range(coneAreaMin.x, coneAreaMax.x), Random.Range(coneAreaMin.y, coneAreaMax.y));
            var coneTemp = Instantiate(cone, position, Quaternion.identity);
            spawnedCones.Add(coneTemp);
        }
    }

    public void increaseConeAmmount()
    {
        ammountOfCones += coneIncreaseRate;
    }

    public void removeOldCones()
    {
        foreach (var cone in spawnedCones)
        {
            if (cone != null)
                Destroy(cone);
        }
        spawnedCones.Clear();
    }
}
