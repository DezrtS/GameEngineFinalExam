using UnityEngine;

public class BlizzardMan : MonoBehaviour
{
    [SerializeField] private int snowflakesPerAttack = 3;
    [SerializeField] private float timeToAttack = 2;

    private float attackTimer = 0;
    private float rollTimer = 1;

    private void Start()
    {
        
        attackTimer = timeToAttack;
    }

    private void Update()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            attackTimer = timeToAttack;
            //if (Random.Range(0, 2) == 0)
            //{
                BlizzardAttack();
            //}
            //else
            //{
            //    RollAttack();
            //}
        }
    }

    public void BlizzardAttack()
    {
        GameManager instance = GameManager.Instance;

        for (int i = 0; i < snowflakesPerAttack; i++)
        {
            instance.SpawnSnowflake();
        }
    }

    public void RollAttack()
    {

    }
}
