using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveSettings : MonoBehaviour
{
    [SerializeField]
    private Slider paddleSlider;
    [SerializeField]
    private Slider ballSlider;

    [System.Serializable]
    public class SaveSettingsData
    {
        public float paddleValue = 0;
        public float ballValue = 0;

        public SaveSettingsData(float p, float b)
        {
            paddleValue = p;
            ballValue = b;
        }

        public float GetPaddleValue()
        {
            return paddleValue;
        }
        public float GetBallValue()
        {
            return ballValue;
        }
    }

    public void SaveSettingsButton()
    {
        SaveSettingsData data = null;

        if (paddleSlider == null || ballSlider == null)
        {
            Debug.LogError("Paddle or Ball Slider not found");
        }
        else
        {
            data = new SaveSettingsData(paddleSlider.value, ballSlider.value);
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + "/saveSettingsfile.json", json);
        }
    }
}
