using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    private Vector3 _initialPosition;

    private bool _birdWasLaunched;
    private float _timeSittingAround;

    [SerializeField]
    private float _launchPower = 500;

    private void Awake()
    {
        _initialPosition = transform.position;
    }

    private void FixedUpdate()
    {
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        GetComponent<LineRenderer>().SetPosition(1, _initialPosition);

        if (_birdWasLaunched &&
            GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1)
            _timeSittingAround += Time.deltaTime;

        if (!(transform.position.y > 15) && !(transform.position.y < -15) && !(transform.position.x > 20) &&
            !(transform.position.x < -20) && !(_timeSittingAround > 3)) return;

        var currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadSceneAsync(currentSceneName);
    }

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;

        Vector2 directionToInitialPos = _initialPosition - transform.position;
        GetComponent<Rigidbody2D>().AddForce(directionToInitialPos * _launchPower);
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    private void OnMouseDrag()
    {
        var newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPos.x, newPos.y);
    }
}
