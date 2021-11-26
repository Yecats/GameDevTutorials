using UnityEngine;

/// <summary>
/// For example only
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private Rigidbody _rigidBody;

    private const float MoveSpeed = 5;
    private const float RotationSpeed = MoveSpeed / 2;
    private const string MouseInputAxis = "Mouse X";
    [SerializeField]
    private GameObject _bowModel;
    [SerializeField]
    private GameObject _quiverModel;

    private static Animator _animator;
    internal bool IsMoving => PlayerMovementDirection != Vector3.zero;

    public bool IsGrounded => Physics.Raycast(transform.position, Vector3.down, 1.1f, 1 << 6);
    public void ShowBow() => _bowModel.gameObject.SetActive(true);
    public void ShowQuiver() => _quiverModel.gameObject.SetActive(true);

    public Vector3 PlayerMovementDirection { private set; get; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start() 
    { 
        _rigidBody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }

    //update rotation
    private void Update() => _rigidBody.MoveRotation(_rigidBody.rotation * Quaternion.AngleAxis(Input.GetMouseButton(1) ? Input.GetAxis(MouseInputAxis) * RotationSpeed : 0, Vector3.up));

    //update position
    private void FixedUpdate()
    {
        PlayerMovementDirection = GetInputDirection();

        _rigidBody.MovePosition(_rigidBody.position + (transform.rotation * PlayerMovementDirection * Time.fixedDeltaTime * MoveSpeed));

        if (PlayerMovementDirection == Vector3.zero && IsGrounded)
        {
            _rigidBody.velocity = Vector3.zero;
            _rigidBody.angularVelocity = Vector3.zero;
        }

    }

    //returns vector of player input
    private static Vector3 GetInputDirection()
    {
        Vector3 nextMovement = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            nextMovement += Vector3.forward;
        }

        if (Input.GetKey(KeyCode.S))
        {
            nextMovement += Vector3.back;
        }

        if (Input.GetKey(KeyCode.A))
        {
            nextMovement += Vector3.left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            nextMovement += Vector3.right;
        }

        if (nextMovement == Vector3.zero && _animator.GetBool("Run"))
        {
            _animator.SetBool("Run", false);
        }
        else if (nextMovement != Vector3.zero && !_animator.GetBool("Run"))
        { 
            _animator.SetBool("Run", true);
        }


        return nextMovement;
    }
}
