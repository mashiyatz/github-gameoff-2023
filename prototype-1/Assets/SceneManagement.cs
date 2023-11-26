using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void ChangeToPlayScene()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void ChangeToBattleScene()
    {
        SceneManager.LoadScene("BattleScene");
    }

    public void ChangeToMainMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
