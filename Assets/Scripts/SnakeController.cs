using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField] int _initialSize = 4;
    [SerializeField] GameObject _segment;
    [SerializeField] Camera _mainCamera;

    private Vector2 _direction = Vector2.right;
    private List<GameObject> _segments = new List<GameObject>();

    // Start is called before the first frame update
    private void Start()
    {
        //_segments.Add(gameObject);

        for (int i = 1; i <= _initialSize; i++)
        {
            var body = Instantiate(_segment, transform.position - new Vector3(i, 0, 0), Quaternion.identity);
            _segments.Add(body);
        }
    }

    // Update is called once per frame
    private void Update()
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

        CheckBounds();
    }

    private void FixedUpdate()
    {
        MoveSnakeBody();
        Move();
    }

    private void Move()
    {
        transform.position = new Vector3(Mathf.Round(transform.position.x) + _direction.x, Mathf.Round(transform.position.y) + _direction.y);
    }

    private void CheckBounds()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        float rightSideOfTheScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x;
        float leftSideOfTheScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;

        float topOfScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).y;
        float bottomOfScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y;

        if(screenPos.x <= 0 && _direction == Vector2.left)
        {
            transform.position = new Vector2(rightSideOfTheScreenInWorld, transform.position.y);
        }
        else if(screenPos.x >= Screen.width && _direction == Vector2.right)
        {
            transform.position = new Vector2(leftSideOfTheScreenInWorld, transform.position.y);
        }
        else if(screenPos.y >= Screen.height && _direction == Vector2.up)
        {
            transform.position = new Vector2(transform.position.x, bottomOfScreenInWorld);
        }
        else if(screenPos.y <= 0 && _direction == Vector2.down)
        {
            transform.position = new Vector2(transform.position.x, topOfScreenInWorld);
        }
    }

    private void MoveSnakeBody()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].transform.position = _segments[i - 1].transform.position;
        }

        _segments[0].transform.position = transform.position;
    }

    public void Grow()
    {
        GameObject segment = Instantiate(_segment);
        segment.transform.position = _segments[_segments.Count - 1].transform.position;

        _segments.Add(segment);
    }

    public void Shrink()
    {
        if (_segments.Count == _initialSize)
            return;

        var lastsegment = _segments.Count - 1;
        Destroy(_segments[lastsegment]);
        _segments.Remove(_segments[lastsegment]);
    }
}