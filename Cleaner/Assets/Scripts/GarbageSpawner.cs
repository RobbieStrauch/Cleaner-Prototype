using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GarbageSpawner : MonoBehaviour
{
    public List<GameObject> prefabs;
    public GameObject garbageParent;
    public GameObject plane;
    public GameObject gameOverPanel;
    public float spawnThickness = 0f;
    public int garbageLimit = 50;

    private Vector3 planeSize;
    private bool dropGarbage = false;
    private float elapseTime = 0f;
    private float dropTime = 5f;
    private int garbageDropPerSpawn = 5;

    void Start()
    {
        Time.timeScale = 1f;
        planeSize = plane.GetComponent<Renderer>().bounds.extents;
    }

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
                Vector3 randomPosition = new Vector3(Random.Range(-planeSize.x + spawnThickness, planeSize.x - spawnThickness), 1, Random.Range(-planeSize.z + spawnThickness, planeSize.z - spawnThickness));
                Instantiate(prefabs[randomIndex], randomPosition, Quaternion.identity).transform.SetParent(garbageParent.transform);
            }

            Reset();
            elapseTime = 0f;
        }

        if (garbageParent.transform.childCount >= garbageLimit)
        {
            Time.timeScale = 0;
            gameOverPanel.GetComponentInChildren<TMP_Text>().text = "Game Over Too Much Garbage!\nScore: " + Player.instance.GetScore().ToString();
            gameOverPanel.SetActive(true);
        }
    }

    private void Reset()
    {
        dropGarbage = false;
    }
}
