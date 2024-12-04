using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    [SerializeField] private GameObject projectilePrefab;

    [SerializeField] private bool disableObjectPoolOptimization = false;
    [SerializeField] private GameObject snowflakePrefab;
    [SerializeField] private ObjectPool snowflakePool;
    private CommandInvoker commandInvoker;

    [SerializeField] private Vector2 upperRightBound;
    [SerializeField] private Vector2 lowerLeftBound;

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
        snowflakePool.InitializePool(snowflakePrefab);
        commandInvoker = new CommandInvoker();
    }

    public void SpawnProjectile(Vector3 position, Vector3 direction, float fireSpeed)
    {
        GameObject newProjectile = Instantiate(projectilePrefab, position, Quaternion.identity);
        newProjectile.GetComponent<Projectile>().FireProjectile(direction, fireSpeed, true);
    }

    public void SpawnSnowflake()
    {
        Vector2 randomPosition = new Vector2(Random.Range(lowerLeftBound.x, upperRightBound.x), Random.Range(lowerLeftBound.y, upperRightBound.y));
        GameObject snowflakeObject;
        if (disableObjectPoolOptimization)
        {
            snowflakeObject = Instantiate(snowflakePrefab);
        }
        else
        {
            snowflakeObject = snowflakePool.GetObject();
        }

        if (snowflakeObject != null)
        {
            snowflakeObject.transform.position = randomPosition;
            Snowflake snowflake = snowflakeObject.GetComponent<Snowflake>();
            snowflake.OnSnowflakeFinish += OnSnowflakeFinished;
            snowflake.Activate();
        }
    }

    public void OnSnowflakeFinished(Snowflake snowflake)
    {
        snowflake.OnSnowflakeFinish -= OnSnowflakeFinished;
        if (disableObjectPoolOptimization)
        {
            Destroy(snowflake.gameObject);
            return;
        }

        snowflake.ResetSnowflake();
        snowflakePool.ReturnToPool(snowflake.gameObject);
    }

    public void InvokeCommand(ICommand command)
    {
        commandInvoker.Execute(command);
    }
}
