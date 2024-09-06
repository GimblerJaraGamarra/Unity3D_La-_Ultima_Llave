using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEnemy : MonoBehaviour
{
    public static AudioEnemy Instance;

    public AudioSource audioSource;
    public bool isDead = false;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            audioSource.Stop();
        }
    }
}
