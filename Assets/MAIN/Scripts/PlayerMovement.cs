using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    [Header("MOVEMENT")]
    public CharacterController player;
    public float speedMovement = 12f;
    public float gravity = -9.8f;
    Vector3 velocity;

    [Header("JUMP")]
    public Transform groundCheck;
    public float sphereRadius = 0.3f;
    public LayerMask groundMask;

    bool isGrounded;
    public float jumpingHeight = 0f;

    [Header("SHOOT")]
    public GameObject bulletPrefab;
    public Transform shotPoint;
    public float shotForce = 1500f;
    public int amountBullet;

    [Header("LIFE")]
    public int lifePlayer;

    [Header("SOUNDS")]
    public AudioClip shootSound;
    public AudioSource audioSourcePlayer;

    private int amount;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UIController.instance.UpdateLifePlayer(lifePlayer);
        UIController.instance.UpdateBullet(amountBullet);
    }


    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        player.Move(speedMovement * Time.deltaTime * move);

        velocity.y += gravity * Time.deltaTime;

        player.Move(velocity * Time.deltaTime);

        isGrounded = Physics.CheckSphere(groundCheck.position, sphereRadius, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpingHeight * -2 * gravity);
        }

        if (Input.GetMouseButtonDown(0))
        {
            var bullet = Instantiate(bulletPrefab, shotPoint.position, shotPoint.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(shotPoint.forward * shotForce);
            audioSourcePlayer.PlayOneShot(shootSound);
            amountBullet--;
            UIController.instance.UpdateBullet(amountBullet);
            Destroy(bullet, 3f);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            amount++;
            if (amount > 10)
            {
                lifePlayer--;
                UIController.instance.UpdateLifePlayer(lifePlayer);

                if (lifePlayer <= 0)
                {
                    UIController.instance.ActivePanelFinishGame();
                    Gamecontroller.instance.finishGame = true;
                    gameObject.GetComponent<PlayerMovement>().enabled = false;
                }
                amount = 0;
            }
        }
    }

}
