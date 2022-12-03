using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageSpawner : MonoBehaviour
{
    public List<GameObject> prefabs;
    public GameObject plane;

    private Vector3 planeSize;
    private bool dropGarbage = false;
    private float elapseTime = 0f;
    private float dropTime = 5f;
    private int garbageDropPerSpawn = 5;

    // Start is called before the first frame update
    void Start()
    {
        planeSize = plane.GetComponent<Renderer>().bounds.extents;
    }

    // Update is called once per frame
    void Update()
    {
        elapseTime += Time.deltaTime;

        if (elapseTime >= dropTime)
        {
            dropGarbage = true;
        }

        if (dropGarbage)
        {
            for (int i = 0; i < garbageDropPerSpawn; i++)
            {
                int randomIndex = Random.Range(0, prefabs.Count);
                Vector3 randomPosition = new Vector3(Random.Range(-planeSize.x, planeSize.x), 1, Random.Range(-planeSize.z, planeSize.z));
                Instantiate(prefabs[randomIndex], randomPosition, Quaternion.identity);
            }

            Reset();
            elapseTime = 0f;
        }
    }

    private void Reset()
    {
        dropGarbage = false;
    }
}
