using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamecontroller : MonoBehaviour
{
    public static Gamecontroller instance;
    public GameObject[] enemyPrefab;

    public List<Transform> spawns;

    public bool finishGame;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateEnemies();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateEnemies()
    {
        if (!finishGame)
        {
            var index = Random.Range(0, enemyPrefab.Length - 1);
            var coodenada = spawns[Random.Range(0, spawns.Count)];
            var enemyclone = Instantiate(enemyPrefab[index], coodenada.position, Quaternion.identity);
        }

        Invoke("CreateEnemies", Random.Range(3, 6));

    }
}

