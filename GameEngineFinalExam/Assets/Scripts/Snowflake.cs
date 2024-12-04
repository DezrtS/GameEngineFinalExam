using UnityEngine;

public class Snowflake : MonoBehaviour
{
    private Rigidbody2D rig;

    [SerializeField] private float fireSpeed = 3;
    [SerializeField] private float lifetime = 10;
    private float lifetimeTimer = 0;
    private bool activated = false;

    public delegate void SnowflakeHandler(Snowflake snowflake);
    public event SnowflakeHandler OnSnowflakeFinish;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    public void Activate()
    {
        MegaMan megaMan = MegaMan.Instance;
        Vector2 direction = megaMan.transform.position - transform.position;
        rig.AddForce(fireSpeed * direction.normalized, ForceMode2D.Impulse);
        activated = true;
        lifetimeTimer = lifetime;
    }

    private void Update()
    {
        if (activated)
        {
            lifetimeTimer -= Time.deltaTime;
            if (lifetimeTimer <= 0)
            {
                OnSnowflakeFinish?.Invoke(this);
            }
        }
    }

    public void ResetSnowflake()
    {
        activated = false;
        rig.linearVelocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ICommand command = new InvertMegaManCommand(MegaMan.Instance);
            GameManager.Instance.InvokeCommand(command);
            OnSnowflakeFinish?.Invoke(this);
        }
    }
}
