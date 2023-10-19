using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private TextMeshProUGUI respawnTimer;

    private void Start()
    {
        playerData.onDeath += restart;
        playerMovement.onFinish += nextLevel;
    }

    private void OnDestroy()
    {
        playerData.onDeath -= restart;
        playerMovement.onFinish -= nextLevel;
    }

    private void nextLevel()
    {
        StartCoroutine("delayedRespawn");
    }

    private void restart()
    {
        StartCoroutine("delayedRespawn");
    }

    IEnumerator delayedRespawn()
    {
        respawnTimer.enabled = true;
        respawnTimer.SetText("5");
        for(int i = 1; i < 6; i++)
        {
            yield return new WaitForSeconds(1);
            respawnTimer.SetText((5 - i).ToString());
        }
        respawnTimer.enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
