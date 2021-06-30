using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
	public TMP_InputField inputName;	
	public Text BestScoreText;	

	private void Start()
    {
		//HighScoreManager.Instance.LoadHighScore();
		Events.Instance.onHighScoreChanged += NewHighScore;
		Events.Instance.NewHighScore();
		SetActualPlayer();
	}

	public void UpdateActualPlayer()
    {
		HighScoreManager.Instance.actualPlayer = inputName.text;
    }

	private void SetActualPlayer()
    {
        if (HighScoreManager.Instance.actualPlayer != null)
        {
			inputName.text = HighScoreManager.Instance.actualPlayer;
		}
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

	public void SeetingsMenu()
	{
		SceneManager.LoadScene(3);
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
