using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField] int _initialSize = 4;
    [SerializeField] GameObject _segment;
    [SerializeField] GameObject _segmentContainer;

    private Vector2 _direction = Vector2.right;
    private List<GameObject> _segments = new List<GameObject>();

    public bool HasBody { get { return _segments.Count > 0; } }

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 1; i <= _initialSize; i++)
        {
            var body = Instantiate(_segment, transform.position - new Vector3(i, 0, 0), Quaternion.identity);
            _segments.Add(body);
            body.transform.SetParent(_segmentContainer.transform);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PauseUnpause();
        }

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

    private static void PauseUnpause()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            UILevel.Instance.Unpause();
        }
        else
        {
            Time.timeScale = 0f;
            UILevel.Instance.Pause();
        }
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

        if(_segments.Count == 0)
            segment.transform.position = transform.position;
        else
            segment.transform.position = _segments[_segments.Count - 1].transform.position;

        _segments.Add(segment);
        segment.transform.SetParent(_segmentContainer.transform);
    }

    public void Shrink()
    {
        //if (_segments.Count == 0)
        //    return;

        if (!HasBody)
            return;

        var lastsegment = _segments.Count - 1;
        Destroy(_segments[lastsegment]);
        _segments.Remove(_segments[lastsegment]);
    }
}