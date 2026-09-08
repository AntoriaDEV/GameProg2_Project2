using UnityEngine;
using UnityEngine.Diagnostics;

public class NewMonoBehaviourScript : MonoBehaviour
{
    TS_Inputs inputs;
    CharacterController characterController;

    [Header("Player Controls")]
    [SerializeField] public float moveSpeed = 4f;
    [SerializeField] Vector2 moveInput;
    [SerializeField] Vector2 aimInput;

    [SerializeField] public bool isDead;

    private void Awake()
    {
        inputs = new TS_Inputs();
        characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    private void Update()
    {
        HandleInputs();
        HandleMovement();
        HandleRotation();
    }

    void HandleInputs()
    {
        moveInput = inputs.Player.Movement.ReadValue<Vector2>();
        aimInput = inputs.Player.Aiming.ReadValue<Vector2>();
    }

    void HandleMovement()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        characterController.Move(move * moveSpeed * Time.deltaTime);
    }

    void HandleRotation()
    {
        Ray ray = Camera.main.ScreenPointToRay(aimInput);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance; // from mouse position to ground plane

        if(groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            LookAt(point);
        }
    }

    void LookAt(Vector3 lookPoint)
    {
        Vector3 heightCorrectedPoint = new Vector3(
            lookPoint.x,
            transform.position.y,
            lookPoint.z);
        transform.LookAt(heightCorrectedPoint);
    }
}
