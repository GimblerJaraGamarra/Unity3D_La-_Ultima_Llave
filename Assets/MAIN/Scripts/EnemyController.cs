using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private GameObject player;

    public NavMeshAgent agent;

    public Animator animatorEnemy;
    private int lifeEnemy;

    public AudioSource effectWalking;

    [Header("SOUND")]
    // public AudioClip audioClipEnemyEating;

    public AudioSource audioSourceEnemy;


    private void Start()
    {
        lifeEnemy = Random.Range(5, 8);
        player = PlayerMovement.Instance.gameObject;
    }

    void Update()
    {
        MovementEnemy();
    }

    void MovementEnemy()
    {
        if (!Gamecontroller.instance.finishGame)
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

            transform.LookAt(player.transform);

            if (distanceToPlayer < 0.1f)
            {
                animatorEnemy.SetBool("shoot", true);
                agent.isStopped = true;
            }
            else
            {
                animatorEnemy.SetBool("shoot", false);
                agent.destination = player.transform.position;
                agent.isStopped = false;

            }
        }
        else
        {
            agent.isStopped = true;
            animatorEnemy.SetBool("IsDead", true);
            effectWalking.Stop();
            gameObject.GetComponent<EnemyController>().enabled = false;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            lifeEnemy -= 3;

            if (lifeEnemy <= 0)
            {
                animatorEnemy.SetBool("IsDead", true);
                agent.isStopped = true;
                effectWalking.Stop();
                gameObject.GetComponent<EnemyController>().enabled = false;
                gameObject.GetComponent<CapsuleCollider>().enabled = false;
                Destroy(gameObject.GetComponent<Rigidbody>());
                Destroy(gameObject, 5);
            }
        }

        if (other.CompareTag("Player"))
        {
            audioSourceEnemy.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSourceEnemy.Stop();
        }

    }

}
