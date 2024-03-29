using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    [Header("Player1 reference")]
    [SerializeField] private SnakeController _snake1;

    [Space]
    [Header("Player2 reference")]
    [SerializeField] private SnakeController _snake2;

    [Header("Level Buonds Collider")]
    [SerializeField] BoxCollider2D _levelBounds;

    [Space]
    [Header("Mass Gainer reference")]
    [SerializeField] GameObject _massGainer;

    [Space]
    [Header("Mass Burner reference and time spawn delay")]
    [SerializeField] GameObject _massBurner;
    [SerializeField] float _massBurnerInitialDelay;
    [SerializeField] float _massBurnerSpawnInterval;
    [SerializeField] float _massBurnerCoroutineTimeCheck;
    [SerializeField] float _massBurnerDestroyTimer;

    private void Awake()
    {
        if (Instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        Time.timeScale = 1f;
        SpawnMassGainerAtRandomPosition();
        StartCoroutine(SpawnMassBurnerAtRandomPosition());
    }

    public void SpawnMassGainerAtRandomPosition()
    {
        float x, y;
        CalculateRandomPosition(out x, out y);

        GameObject massGainer = Instantiate(_massGainer);
        massGainer.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0f);
    }

    private void CalculateRandomPosition(out float x, out float y)
    {
        Bounds bounds = _levelBounds.bounds;

        x = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
        y = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);
    }

    private IEnumerator SpawnMassBurnerAtRandomPosition()
    {
        yield return new WaitForSeconds(_massBurnerInitialDelay);

        while (true)
        {
            if (!_snake1.HasBody)
                yield return new WaitForSeconds(_massBurnerCoroutineTimeCheck);

            float x, y;
            CalculateRandomPosition(out x, out y);

            GameObject massBurner = Instantiate(_massBurner);
            massBurner.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0f);
            Destroy(massBurner, _massBurnerDestroyTimer);
            yield return new WaitForSeconds(_massBurnerSpawnInterval);
        }
    }

    public void GameOverController()
    {
        if(_snake1.IsDead() || _snake2.IsDead())
            Time.timeScale = 0f;
    }
}