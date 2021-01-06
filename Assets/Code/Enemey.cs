using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemey : MonoBehaviour
{
    [SerializeField] 
    private GameObject _cloudParticlePrefab;

    public static int Kills;

    public int TotalEnemies = 2;

    private string _lastKillName;

    public TMP_Text KillsText;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Bird.Victory) return;
        var bird = collision.collider.GetComponent<Bird>();
        if (bird != null)
        {
            Instantiate(_cloudParticlePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Kills++;
            if(!bird.name.Equals(_lastKillName))
                KillsText.text = $"Enemies Down: {Kills}";
            _lastKillName = collision.collider.name;

            if (TotalEnemies == Kills)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Bird.Victory = true;
                if (GameObject.Find("GameManager").GetComponent<GameManager>() != null)
                    GameObject.Find("GameManager").GetComponent<GameManager>().VictoryStart();
            }
            else Cursor.lockState = CursorLockMode.None;
            return;
        }

        var enemy = collision.collider.GetComponent<Enemey>();
        if (enemy != null)
            return;

        foreach (var contact in collision.contacts)
        {
            if (contact.normal.y < -0.5)
                Destroy(gameObject);
        }
    }

}
