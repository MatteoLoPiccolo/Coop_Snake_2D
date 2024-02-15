using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private int _initialSize = 4;
    [SerializeField] private GameObject _segment;
    [SerializeField] private GameObject _segmentContainer;

    private bool isPaused = false;
    private Vector2 _direction = Vector2.right;
    private List<GameObject> _segments = new List<GameObject>();

    public bool HasBody { get { return _segments.Count > 0; } }

    private int _score;
    public int Score { get { return _score; } }
    private int _scoreMultiplier = 2;

    [SerializeField] bool _isSnake2;
    public bool IsSnake2 { get {  return _isSnake2; } }

    private void Start()
    {
        _score = 0;

        SetInitialSizeOfTheSnake();
    }

    private void SetInitialSizeOfTheSnake()
    {
        for (int i = 1; i <= _initialSize; i++)
        {
            var body = Instantiate(_segment, transform.position - new Vector3(i, 0, 0), Quaternion.identity);
            _segments.Add(body);
            body.transform.SetParent(_segmentContainer.transform);
        }
    }

    private void Update()
    {
        if (!_isSnake2)
            HandleInputSnake1();
        else
            HandleInputSnake2();

        CheckBounds();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TogglePause();
            }
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            UILevel.Instance.Pause();
        }
        else
        {
            Time.timeScale = 1f;
            UILevel.Instance.Unpause();
        }
    }

    private void HandleInputSnake1()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _direction = Vector2.right;
        }
    }

    private void HandleInputSnake2()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _direction = Vector2.right;
        }
    }

    private void FixedUpdate()
    {
        MoveSnakeBody();
        Move();
    }

    private void Move()
    {
        float newX = Mathf.Round(transform.position.x) + _direction.x;
        float newY = Mathf.Round(transform.position.y) + _direction.y;

        transform.position = new Vector3(newX, newY, transform.position.z);
        //transform.position = new Vector3(Mathf.Round(transform.position.x) + _direction.x, Mathf.Round(transform.position.y) + _direction.y);
    }

    public bool IsHitHimself()
    {
        for (int i = 0; i < _segments.Count; i++)
        {
            if(transform.position == _segments[i].transform.position)
            {
                Debug.Log("I hit myself!");
                return true;
            }
        }

        return false;
    }

    private void CheckBounds()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        float rightSideOfTheScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x;
        float leftSideOfTheScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;

        float topOfScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).y;
        float bottomOfScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y;

        if (screenPos.x <= 0 && _direction == Vector2.left)
        {
            transform.position = new Vector2(rightSideOfTheScreenInWorld, transform.position.y);
        }
        else if (screenPos.x >= Screen.width && _direction == Vector2.right)
        {
            transform.position = new Vector2(leftSideOfTheScreenInWorld, transform.position.y);
        }
        else if (screenPos.y >= Screen.height && _direction == Vector2.up)
        {
            transform.position = new Vector2(transform.position.x, bottomOfScreenInWorld);
        }
        else if (screenPos.y <= 0 && _direction == Vector2.down)
        {
            transform.position = new Vector2(transform.position.x, topOfScreenInWorld);
        }
    }

    private void MoveSnakeBody()
    {
        if (_segments.Count == 0)
            return;

        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].transform.position = _segments[i - 1].transform.position;
        }

        _segments[0].transform.position = transform.position;
    }

    public void Grow()
    {
        GameObject segment = Instantiate(_segment);

        if (_segments.Count == 0)
            segment.transform.position = transform.position;
        else
            segment.transform.position = _segments[_segments.Count - 1].transform.position;

        _segments.Add(segment);
        segment.transform.SetParent(_segmentContainer.transform);

        ScoreMultiplier();
    }

    public void Shrink()
    {
        if (!HasBody)
            return;

        var lastsegment = _segments.Count - 1;
        Destroy(_segments[lastsegment]);
        _segments.Remove(_segments[lastsegment]);
    }

    public void UpdateScore(int amount)
    {
        _score += amount;
    }

    public bool IsDead()
    {
        return true;
    }

    private void ScoreMultiplier()
    {
        _score *= _scoreMultiplier;
        Invoke("ScoreMultiplierCooldown", 3.0f);
    }

    private void ScoreMultiplierCooldown()
    {
        _scoreMultiplier = 1;
        Debug.Log("My multiplier is now : " + _scoreMultiplier);
    }
}