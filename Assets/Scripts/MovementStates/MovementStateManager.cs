using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    [Header("General")]
    [SerializeField] public bool UpdateEnabled;

    #region Movement
    [Header("Movement Speeds")]
    public float currentMoveSpeed = 3f;
    public float walkSpeed = 3;
    public float walkBackSpeed = 2;
    public float runSpeed = 7;
    public float runBackSpeed = 5;
    public float crouchSpeed = 2;
    public float crouchBackSpeed = 1;

    [HideInInspector] public Vector3 dir;
    [HideInInspector] public float hzInput;
    [HideInInspector] public float vtInput;
    CharacterController controller;

    #region Controller constants
    float controllerHeightNormalSize = 1.8f;
    float controllerYCenterNormal = 0.9f;

    float controllerHeightCrouchSize = 1.06f;
    float controllerYCenterCrouch = 0.52f;

    #endregion


    #endregion

    #region Ground Check
    [Header("Ground check")]
    [SerializeField]
    float groundYOffset;
    [SerializeField]
    LayerMask groundMask;
    Vector3 spherePos;

    #endregion

    #region Gravity
    [Header("Gravity and jumping")]
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpForce = 6f;
    [SerializeField] public bool jumped;
    Vector3 velocity;

    #endregion

    #region Movement States
    [Header("Movement States")]
    public MovementBaseState previousState;
    public MovementBaseState currentState;

    public IdleState Idle = new IdleState();
    public WalkState Walk = new WalkState();
    public CrouchState Crouch = new CrouchState();
    public RunState Run = new RunState();
    public JumpState Jump = new JumpState();

    #endregion

    [HideInInspector] public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        SwitchState(Idle);
    }

    // Update is called once per frame
    void Update()
    {
        if (!UpdateEnabled)
        {
            return;
        }

        GetDirectionAndMove();
        Gravity();

        anim.SetFloat("hzInput", hzInput);
        anim.SetFloat("vtInput", vtInput);

        currentState.UpdateState(this);
    }

    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void GetDirectionAndMove()
    {
        hzInput = Input.GetAxis("Horizontal");
        vtInput = Input.GetAxis("Vertical");

        // Always moving relative to the way you are facing
        dir = transform.forward * vtInput + transform.right * hzInput;

        controller.Move(dir * currentMoveSpeed * Time.deltaTime);
    }

    public bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
        bool isGrounded = Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask);

        return isGrounded;
    }

    public void Gravity()
    {
        if (!IsGrounded())
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else if(velocity.y < 0)
        {
            velocity.y = -2;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(spherePos, controller.radius - 0.05f);
    }

    public void JumpForce() => velocity.y += jumpForce;

    public void Jumped() => jumped = true;

    void Falling() => anim.SetBool("Falling", !IsGrounded());

    public void ConfigureControllerNormal()
    {
        controller.height = controllerHeightNormalSize;
        controller.center = new Vector3(0, controllerYCenterNormal, 0);
    }

    public void ConfigureControllerCrouch()
    {
        controller.height = controllerHeightCrouchSize;
        controller.center = new Vector3(0, controllerYCenterCrouch, 0);
    }

}
