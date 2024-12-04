using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rig;
    bool friendly = true;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    public void FireProjectile(Vector3 direction, float fireSpeed, bool friendly)
    {
        this.friendly = friendly;
        rig.AddForce(fireSpeed * direction, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (friendly)
        {
            if (collision.CompareTag("Enemy"))
            {

            }
        }
    }
}
