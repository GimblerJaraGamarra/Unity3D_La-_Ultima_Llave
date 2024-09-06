using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{

    public Transform player;
    public float launchForce = 10f;
    public float maxUpwardForce = 10f;
    public GameObject prefab;

    void Start()
    {
        GenerateAttack();
    }

    void GenerateAttack()
    {

        if (!Gamecontroller.instance.finishGame)
        {

            var prefabClone = Instantiate(prefab, transform.position, Quaternion.identity);
            var rb = prefabClone.GetComponent<Rigidbody>();


            Vector3 horizontalDirection = new Vector3(player.position.x - transform.position.x, 0, player.position.z - transform.position.z).normalized;
            float horizontalDistance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(player.position.x, 0, player.position.z));
            float heightDifference = player.position.y - transform.position.y;
            Vector3 force = horizontalDirection * launchForce;
            force.y = Mathf.Clamp(maxUpwardForce * (horizontalDistance / 10f), 0, maxUpwardForce) + heightDifference;
            rb.AddForce(force, ForceMode.Impulse);
            Destroy(prefabClone, 10);
        }

        Invoke("GenerateAttack", Random.Range(4, 8));
    }
}
