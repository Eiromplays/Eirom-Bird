using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    private Vector3 _initialPosition;

    private bool _birdWasLaunched;
    private float _timeSittingAround;

    public float LaunchPower = 500;

    public TMP_Text CountdownText;

    public static bool Victory;

    private void Awake()
    {
        _initialPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (Victory) return;

        if (GetComponent<LineRenderer>().positionCount >= 2)
        {
            GetComponent<LineRenderer>().SetPosition(0, transform.position);
            GetComponent<LineRenderer>().SetPosition(1, _initialPosition);
        }

        if (_birdWasLaunched &&
            GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1)
        {
            _timeSittingAround += Time.deltaTime;
            CountdownText.gameObject.SetActive(true);
            CountdownText.text = $"Game restarting in {3-Mathf.RoundToInt(_timeSittingAround)} Seconds";
        }
        else 
            CountdownText.gameObject.SetActive(false);

        if (!(transform.position.y > 15) && !(transform.position.y < -15) && !(transform.position.x > 20) &&
            !(transform.position.x < -20) && !(_timeSittingAround > 3)) return;

        var currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadSceneAsync(currentSceneName);
    }

    private void OnMouseDown()
    {
        if (Victory) return;
        GetComponent<SpriteRenderer>().color = Color.blue;
        GetComponent<LineRenderer>().positionCount = 2;
        _birdWasLaunched = false;
    }

    private void OnMouseUp()
    {
        if (Victory) return;

        GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<LineRenderer>().positionCount = 0;

        Vector2 directionToInitialPos = _initialPosition - transform.position;

        GetComponent<Rigidbody2D>().AddForce(directionToInitialPos * LaunchPower);
        GetComponent<Rigidbody2D>().gravityScale = 1;

        _birdWasLaunched = true;
    }

    private void OnMouseDrag()
    {
        if (Victory) return;
        var newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPos.x, newPos.y);
    }
}
