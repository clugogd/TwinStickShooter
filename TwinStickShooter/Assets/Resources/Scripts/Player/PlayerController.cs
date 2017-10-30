using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(TrailRenderer))]
public class PlayerController : MonoBehaviour
{
    //  Movement
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    private bool bPlayerCanControl = true;
    public bool PLAYERCONTROL { get { return bPlayerCanControl; } set { bPlayerCanControl = value; } }

    [SerializeField]
    private bool bCanJump = false;
    [SerializeField]
    private bool bMovementSkillAvailable = true;

    [SerializeField]
    private float movementSkillDuration = 2.0f;
    [SerializeField]
    private float movementSkillSpeed = 1.5f;
    [SerializeField]
    private float movementSkillCooldown = 2.0f;
    [SerializeField]
    private bool bSkillHasCooldown = true;
    [SerializeField]
    private TrailRenderer dashTrail;

    private float currentSkillTimer = 0.0f;
    private bool bPlayerCanMove = true;

    [SerializeField]
    private float rsRotationSpeed = 1.0f;

    private Animator animator;
    private CharacterController controller;
    private AudioSource audioSource;

    private string joystick = "";
    private string[] joysticks;

    private Health healthScript;
    private Experience experienceScript;

    [SerializeField]
    private CharacterHUD myHUD;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        audioSource = GetComponent<AudioSource>();
        dashTrail = GetComponent<TrailRenderer>();

        healthScript = GetComponentInChildren<Health>();
        experienceScript = GetComponentInChildren<Experience>();

        joysticks = Input.GetJoystickNames();

        bPlayerCanControl = true;
    }

    void UpdateHealth()
    {
        myHUD.SetHP((int)healthScript.HP, (int)healthScript.MAXHP);
    }
    void UpdateXP()
    {
        myHUD.SetXP((int)experienceScript.CURRENTXP, (int)experienceScript.NEXTLEVELXP);
    }
    void UpdateLevel()
    {
        myHUD.SetLevel((int)experienceScript.CURRENTLEVEL);
    }
    void UpdateSkills()
    {
        if (bMovementSkillAvailable)
            myHUD.SetSkillCooldown(0, 1.0f);
        else
            myHUD.SetSkillCooldown(0, currentSkillTimer / movementSkillDuration);
    }

    void UpdateHUD()
    {
        if (myHUD == null)
        {
            GameObject hud = GameObject.Find("CharacterHUD");
            if (hud)
                myHUD = hud.GetComponent<CharacterHUD>();
        }
        if (myHUD)
        {
            UpdateHealth();
            UpdateXP();
            UpdateLevel();
            UpdateSkills();
        }
    }

    void Update()
    {
        if (!bPlayerCanControl)
            return;

        if (healthScript.ISDEAD)
        {
            StartCoroutine("DoDeath");
            bPlayerCanControl = false;
            return;
        }

        UpdateHUD();

        MovePlayer();

        if (bPlayerCanMove)
        {
            Aim();

            if (Input.GetButtonDown("movementskill"))
            {
                bMovementSkillAvailable = false;
                bPlayerCanMove = false;
                currentSkillTimer = movementSkillDuration;
                moveDirection *= movementSkillSpeed;
                dashTrail.enabled = true;
            }
        }
        else
        {
            currentSkillTimer -= Time.deltaTime;
            if (currentSkillTimer <= 0.0f)
            {
                bPlayerCanMove = true;
                dashTrail.enabled = false;

                if (bSkillHasCooldown)
                    StartCoroutine("updateCooldown");
                else
                    bMovementSkillAvailable = true;
            }
        }
    }
    IEnumerator DoDeath()
    {
        Debug.Log("DoDeath() started");
        if (animator)
        {
            animator.SetTrigger("die");
        }
        GameInstance._instance.DisplayBanner("YOU DIED!");
        yield return new WaitForSeconds(2.0f);
        GameInstance._instance.RestartLevel();
        Debug.Log("DoDeath() finished");
    }
    IEnumerator updateCooldown()
    {
        yield return new WaitForSeconds(movementSkillCooldown);
        bMovementSkillAvailable = true;

        yield return null;
    }

    void Aim()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            if (joysticks[0] != "")
            {
                float rsX = Input.GetAxis("HorizontalRS");
                float rsY = Input.GetAxis("VerticalRS");
                float angle = Mathf.Atan2(rsX, rsY);
                transform.rotation = Quaternion.EulerAngles(0.0f, angle, 0.0f);
            }
        }
        else
        {
            Vector3 objPos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 dir = Input.mousePosition - objPos;
            transform.rotation = Quaternion.Euler(new Vector3(0, Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg, 0));
        }
    }

    void MovePlayer()
    {
        if (controller.isGrounded && bPlayerCanMove)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection *= speed;
            if (Input.GetButton("Jump") && bCanJump)
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        animator.SetFloat("speed", controller.velocity.magnitude);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 8)
            return;
    }
}

