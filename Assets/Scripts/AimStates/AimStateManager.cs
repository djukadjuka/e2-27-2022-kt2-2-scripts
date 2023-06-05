using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimStateManager : MonoBehaviour
{
    [Header("General Attributes")]
    [SerializeField] public bool UpdateEnabled;
    [SerializeField] float mouseSense = 1;
    public float xAxis;
    public float yAxis;

    [Header("Aim Zoom States")]
    [SerializeField] public AimBaseState currentState;
    public NormalAimState Normal = new NormalAimState();
    public ZoomAimState Zoom = new ZoomAimState();
    public float zoomFov = 30;
    public float normalFov;
    public float currentFov;
    public float fovSmoothSpeed = 20;

    [Header("Components")]
    [HideInInspector] public Animator anim;
    [HideInInspector] public CinemachineVirtualCamera vCam;
    [SerializeField] Transform camFollowPos;

    [Header("Aim Configuration")]
    [SerializeField] Transform aimPos;
    [SerializeField] float aimSmoothSpeed = 20;
    [SerializeField] LayerMask aimMask;

    // Start is called before the first frame update
    void Start()
    {
        vCam = GetComponentInChildren<CinemachineVirtualCamera>();
        normalFov = vCam.m_Lens.FieldOfView;
        anim = GetComponent<Animator>();

        SwitchState(Normal);
    }

    // Update is called once per frame
    void Update()
    {
        if (!UpdateEnabled)
        {
            return;
        }

        xAxis += Input.GetAxisRaw("Mouse X") * mouseSense;
        yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSense;
        yAxis = Mathf.Clamp(yAxis, -80, 80);

        vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, currentFov, fovSmoothSpeed * Time.deltaTime);

        Vector2 screenCneter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCneter);

        if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
        {
            aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);
            
        }

        currentState.UpdateState(this);
    }

    private void LateUpdate()
    {
        camFollowPos.localEulerAngles = new Vector3(yAxis, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
    }

    public void SwitchState(AimBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
