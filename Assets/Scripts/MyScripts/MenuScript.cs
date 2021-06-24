using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
	public TMP_InputField input;	
	public Text BestScoreText;

    private void Start()
    {
		//HighScoreManager.Instance.LoadHighScore();
		Events.Instance.onHighScoreChanged += NewHighScore;
		Events.Instance.NewHighScore();
	}

	public void UpdateActualPlayer()
    {
		HighScoreManager.Instance.actualPlayer = input.text;
    }

	void NewHighScore()
	{
		BestScoreText.text = $"Best Score : {HighScoreManager.Instance.bestHighScorePlayer} - {HighScoreManager.Instance.bestHighScore}";
	}

	public void HighScoreMenu()
	{
		SceneManager.LoadScene(2);
	}

	public void ResetHighScore()
    {
		HighScoreManager.Instance.ResetHighScores();
		NewHighScore();
    }

	public void StartGame()
    {		
		SceneManager.LoadScene(1);
    }

	public void QuitGame()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.ExitPlaymode();
#else
		Application.Quit();
#endif
	}

	void OnDestroy()
	{
		if (Events.Instance)
		{
			Events.Instance.onHighScoreChanged -= NewHighScore;
		}
	}
}
