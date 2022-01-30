using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class RabbitController : MonoBehaviour
{
    private Vector2 _nextPosition, _oldPosition;
    [SerializeField] private float _speed;
    [SerializeField] private AnimationCurve _curve;
    private List<RaycastHit2D> _rays;
    private List<Vector2> _position = new List<Vector2>()
    {
        new Vector2(-1, 0),
        new Vector2(1, 0),
        new Vector2(0, 1),
        new Vector2(0, -1)
    };
    private List<KeyCode> _keys = new List<KeyCode>()
    {
        KeyCode.LeftArrow, 
        KeyCode.RightArrow, 
        KeyCode.UpArrow, 
        KeyCode.DownArrow
    };

    private MazeSpawner _mazeSpawner;

    private void Start()
    {
        _mazeSpawner = FindObjectOfType<MazeSpawner>();
    }

    private void Update()
    {
        for (int i = 0; i < _keys.Count; i++)
        {
            if (Input.GetKeyDown(_keys[i]) && _mazeSpawner.DistanceToStart > 0)
            {
                _oldPosition = transform.position;
                _nextPosition = new Vector2(transform.position.x + _position[i].x, transform.position.y + _position[i].y);
                _mazeSpawner.DistanceToStart--;
            }
        }
        transform.position = Vector2.Lerp(
            transform.position, 
            new Vector2(Mathf.Round(_nextPosition.x), Mathf.Round(_nextPosition.y)), 
            _curve.Evaluate(_speed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Wall"))
        {
            _nextPosition = _oldPosition;
            StartCoroutine(HittingTheWall(_speed));
        }
    }

    private IEnumerator HittingTheWall(float oldSpeed)
    {
        yield return new WaitForSeconds(0.3f);
        _speed = 0;
        yield return new WaitForSeconds(1);
        _speed = oldSpeed;
    }
}
