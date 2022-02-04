using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class RabbitController : MonoBehaviour
{
    [Header("Ходьба")] 
    [SerializeField] private float _speed;
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private AudioSource _stepAudio;
    private Vector2 _nextPosition, _oldPosition;
    [HideInInspector] public bool CanSwipe = true;

    private List<Vector2> _position = new List<Vector2>()
    {
        new Vector2(1, 0), // Вправо
        new Vector2(-1, 0), // Влево
        new Vector2(0, 1), // Вверх
        new Vector2(0, -1) // Вниз
    };
    
    //Свайпы
    private Vector2 _touchIn, _touchOut;
    private float _dragDistance;

    private MazeSpawner _mazeSpawner;
    private WinsController _winsController;

    [SerializeField] private GameObject _winPanel, _losePanel;

    private void Start()
    {
        _winsController = FindObjectOfType<WinsController>();
        _mazeSpawner = FindObjectOfType<MazeSpawner>();
    }

    private void Update()
    {
        _winPanel.SetActive(_winsController.Win);
        //Move
        transform.position = Vector2.Lerp(
            transform.position, 
            new Vector2(Mathf.Round(_nextPosition.x), Mathf.Round(_nextPosition.y)), 
            _curve.Evaluate(_speed * Time.deltaTime));
        
        if (_mazeSpawner.DistanceToStart <= 0 && _winsController.Win == false && 
            Math.Abs(transform.position.x - _nextPosition.x) < 0.05f && Math.Abs(transform.position.y - _nextPosition.y) < 0.05f)
        {
            _losePanel.SetActive(true);
            CanSwipe = false;
        }
    }

    public void TouchUp()
    {
        if (CanSwipe)
        {
            if (_mazeSpawner.DistanceToStart > 0)
            {
                _touchOut = Input.mousePosition;
                Vector2 delta = _touchIn - _touchOut;
                _oldPosition = transform.position;
                if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                {
                    _nextPosition = delta.x < 0
                        ? new Vector2(transform.position.x + _position[0].x, transform.position.y + _position[0].y)
                        : new Vector2(transform.position.x + _position[1].x, transform.position.y + _position[1].y);
                }
                else
                {
                    _nextPosition = delta.y < 0
                        ? new Vector2(transform.position.x + _position[2].x, transform.position.y + _position[2].y)
                        : new Vector2(transform.position.x + _position[3].x, transform.position.y + _position[3].y);
                }

                _mazeSpawner.DistanceToStart--;
                _stepAudio.Play();
            }
        }
    }

    public void TouchDown()
    {
        if (CanSwipe)
        {
            if (_mazeSpawner.DistanceToStart > 0)
            {
                _touchIn = Input.mousePosition;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Wall"))
        {
            _nextPosition = _oldPosition;
            StartCoroutine(HittingTheWall(_speed));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("IsMaze"))
        {
            if (_mazeSpawner.DistanceToStart >= 0) _winsController.Win = true;
            else _winsController.Win = false;
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
