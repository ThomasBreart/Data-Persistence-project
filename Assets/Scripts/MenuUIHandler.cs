using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] GameObject playerNameField;
    [SerializeField] TMP_Text bestScoreText;

    private void Awake()
    {
        bestScoreText.text = ScoreManager.Instance.GetBestScoreTitle();
    }

    public void StartGame()
    {
        string playerName = playerNameField.GetComponent<TMP_InputField>().text;
        ScoreManager.Instance.playerName = playerName;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
