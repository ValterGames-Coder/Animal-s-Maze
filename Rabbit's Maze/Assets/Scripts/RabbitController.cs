using UnityEngine;

public class RabbitController : MonoBehaviour
{
    private Vector2 _nextPosition;
    [SerializeField] private float _speed;
    [SerializeField] private AnimationCurve _curve;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) _nextPosition = new Vector2(transform.position.x - 1, transform.position.y);
        if (Input.GetKeyDown(KeyCode.RightArrow)) _nextPosition = new Vector2(transform.position.x + 1, transform.position.y);
        if (Input.GetKeyDown(KeyCode.UpArrow)) _nextPosition = new Vector2(transform.position.x, transform.position.y + 1);
        if (Input.GetKeyDown(KeyCode.DownArrow)) _nextPosition = new Vector2(transform.position.x, transform.position.y - 1);
        transform.position = Vector2.Lerp(
            transform.position, 
            new Vector2(Mathf.Round(_nextPosition.x), Mathf.Round(_nextPosition.y)), 
            _curve.Evaluate(_speed * Time.deltaTime));
    }
}
