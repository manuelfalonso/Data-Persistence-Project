using UnityEngine;
using TMPro;

public class ShowHighScores : MonoBehaviour
{
    private TextMeshProUGUI tmp;

    private void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        tmp.text = HighScoreManager.Instance.HighScoreTableToString();
        HighScoreManager.Instance.PrintHighScoreTable();
    }
}
