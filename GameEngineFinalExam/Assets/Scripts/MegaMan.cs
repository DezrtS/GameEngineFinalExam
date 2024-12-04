using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MegaMan : MonoBehaviour
{
    private static MegaMan instance;
    public static MegaMan Instance => instance;

    private Rigidbody2D rig;

    [SerializeField] private float fireSpeed = 3;
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float jumpPower = 10;

    public bool canJump = true;

    private int forward = 1;

    private void OnEnable()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = 0;
        if (Input.GetKey(KeyCode.D))
        {
            moveInput += 1;
            forward = 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveInput -= 1;
            forward = -1;
        }

        rig.linearVelocityX = moveInput * moveSpeed;
        Vector3 scale = transform.localScale;
        scale.x = forward * Mathf.Abs(scale.x);
        transform.localScale = scale;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            MegaBusterAttack();
        }
    }

    public void Jump()
    {
        if (canJump)
        {
            //if (Physics2D.Linecast())
            rig.AddForceY(jumpPower, ForceMode2D.Impulse);
        }
    }

    public void MegaBusterAttack()
    {
        GameManager.Instance.SpawnProjectile(transform.position, new Vector3(forward, 0, 0), fireSpeed);
    }
}
