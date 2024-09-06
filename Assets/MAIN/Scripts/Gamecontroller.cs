using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamecontroller : MonoBehaviour
{
    public static Gamecontroller instance;

    [Header("ENEMYS")]
    public GameObject[] enemyPrefab;

    [Header("CREATE ENEMYS")]
    public List<Transform> spawns;

    [Header("VALIDATORS")]
    [HideInInspector] public bool finishGame;
    [HideInInspector] public bool fullKey;

    [Header("CREATE BULLETS")]
    public GameObject bulletsPrefab;
    public List<Transform> spawnsBulletsCreate;

    [Header("KEY")]
    public int totalAmountKeys;
    [HideInInspector] public int amountsKey = 0;
    public GameObject keyPrefab;
    public List<Transform> spawnsKeyCreate;

    [Header("SOUNDS")]
    public AudioSource audioSourceGameController;
    public AudioClip audioEnvironment;
    public AudioClip openDoorAudioAudio;

    [Header("ANIMATOR")]
    public Animator exitDoorAnimator;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateEnemies();
        InstanciateBullets();
        InstanciateKeys();
        audioSourceGameController.PlayOneShot(audioEnvironment);
        UIController.instance.UpdateAmountKey(amountsKey);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenDoor()
    {
        audioSourceGameController.PlayOneShot(openDoorAudioAudio);
        exitDoorAnimator.SetBool("openDoor", true);
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

    void InstanciateBullets()
    {
        if (!finishGame)
        {
            var coodenada = spawnsBulletsCreate[Random.Range(0, spawnsBulletsCreate.Count)];
            var bullets = Instantiate(bulletsPrefab, coodenada.position, Quaternion.identity);
            Destroy(bullets, 30f);
        }

        Invoke("InstanciateBullets", 10);
    }

    void InstanciateKeys()
    {
        if (!fullKey)
        {
            var coodenada = spawnsKeyCreate[Random.Range(0, spawnsKeyCreate.Count)];
            var key = Instantiate(keyPrefab, coodenada.position, Quaternion.identity);
            Destroy(key, 25f);
        }

        Invoke("InstanciateKeys", 35);
    }
}

