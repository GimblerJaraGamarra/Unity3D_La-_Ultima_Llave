using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public GameObject effectshoot;


    [Header("LIFE")]
    public float lifePlayer;

    [Header("SOUNDS")]
    public AudioClip shootSound;
    public AudioClip reloadWeaponSound;
    public AudioClip getKeySound;
    public AudioSource audioSourcePlayer;

    [Header("EXIT DOOR")]
    public BoxCollider exitDoor;

    private int timerDamage;
    private float totalLife;
    public PlayerInput controllers;
    public InputAction moveAction;
    public InputAction shootAction;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UIController.instance.UpdateLifePlayer(lifePlayer);
        UIController.instance.UpdateAmountBullet(amountBullet);
        totalLife = lifePlayer;
        moveAction = controllers.actions["move"];
        shootAction = controllers.actions["shoot"];

    }


    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //var m = moveAction.ReadValue<Vector2>();

        //float x = m.x;
        //float z = m.y;

        Vector3 move = transform.right * x + transform.forward * z;

        player.Move(speedMovement * Time.deltaTime * move);

        velocity.y += gravity * Time.deltaTime;

        player.Move(velocity * Time.deltaTime);

        isGrounded = Physics.CheckSphere(groundCheck.position, sphereRadius, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        //{
        //    velocity.y = Mathf.Sqrt(jumpingHeight * -2 * gravity);
        //}
        // if (Input.GetMouseButtonDown(0) && amountBullet > 0)
        var shootPressButton = shootAction.WasPressedThisFrame();

        if (shootPressButton && amountBullet > 0)
        {
            var bullet = Instantiate(bulletPrefab, shotPoint.position, shotPoint.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(shotPoint.forward * shotForce);
            Destroy(bullet, 3f);

            var effectShoot = Instantiate(effectshoot, shotPoint);
            effectShoot.transform.localPosition = Vector3.zero;
            Destroy(effectShoot, 1f);

            amountBullet--;
            UIController.instance.UpdateAmountBullet(amountBullet);

            audioSourcePlayer.volume = 0.4f;
            audioSourcePlayer.PlayOneShot(shootSound);
        }

    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Bullets"))
        {
            amountBullet += 20;
            UIController.instance.UpdateAmountBullet(amountBullet);

            audioSourcePlayer.volume = 0.4f;
            audioSourcePlayer.PlayOneShot(reloadWeaponSound);

            Destroy(other.gameObject);
        }

        if (other.CompareTag("key"))
        {
            Gamecontroller.instance.amountsKey++;
            UIController.instance.UpdateAmountKey(Gamecontroller.instance.amountsKey);
            if (Gamecontroller.instance.amountsKey >= Gamecontroller.instance.totalAmountKeys)
            {
                Gamecontroller.instance.fullKey = true;
                Gamecontroller.instance.OpenDoor();
            }

            audioSourcePlayer.volume = 0.7f;
            audioSourcePlayer.PlayOneShot(getKeySound);

            Destroy(other.gameObject);
        }

        if (other.CompareTag("exitdoor") && Gamecontroller.instance.amountsKey >= Gamecontroller.instance.totalAmountKeys)
        {
            Gamecontroller.instance.finishGame = true;
            Gamecontroller.instance.audioSourceGameController.volume = 0f;
        }

        if (other.CompareTag("Bomb"))
        {
            UIController.instance.ActiveBloodPanel(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            timerDamage++;
            if (timerDamage > 10)
            {
                lifePlayer--;
                UpdateLifePlayerToUI();
                UIController.instance.ActiveBloodPanel(true);

                if (lifePlayer <= 0)
                {
                    UIController.instance.ActivePanelFinishGame();
                    Gamecontroller.instance.finishGame = true;
                    gameObject.GetComponent<PlayerMovement>().enabled = false;
                }
                timerDamage = 0;
            }
        }

        if (other.CompareTag("Bomb"))
        {
            timerDamage++;
            if (timerDamage > 10)
            {
                lifePlayer -= 5;
                Debug.Log("menos 5");
                UpdateLifePlayerToUI();

                if (lifePlayer <= 0)
                {
                    UIController.instance.ActivePanelFinishGame();
                    Gamecontroller.instance.finishGame = true;

                }
                timerDamage = 0;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            UIController.instance.ActiveBloodPanel(false);
        }

        if (other.CompareTag("Bomb"))
        {
            UIController.instance.ActiveBloodPanel(false);
        }
    }

    void UpdateLifePlayerToUI()
    {
        float x = (1 * lifePlayer) / totalLife;
        UIController.instance.UpdateLifePlayer(x);
    }

}
