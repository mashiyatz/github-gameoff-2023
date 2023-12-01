using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public static void ChangeToPlayScene()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public static void ChangeToBattleScene()
    {
        SceneManager.LoadScene("BattleScene");
    }

    public static void ChangeToMainMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public static IEnumerator DelayToGameEnd()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("MainMenuScene");
    }
}
