using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public TMP_Text WinnerText;

    public void Reset()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void VictoryStart()
    {
        StartCoroutine(Victory());
    }

    public IEnumerator Victory()
    {
        WinnerText.gameObject.SetActive(true);

        for (var i = 3; i >= 0; i--)
        {
            yield return new WaitForSeconds(1);
            WinnerText.color = Color.cyan;
            WinnerText.text = $"Restarting in {i} Seconds!";
        }

        SceneManager.LoadSceneAsync(0);
    }
}
