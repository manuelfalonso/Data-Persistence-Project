using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float Speed = 2.0f;
    public float MaxMovement = 2.0f;

    private void Start()
    {
        LoadSettings();
    }

    // Update is called once per frame
    void Update()
    {
        float input = Input.GetAxis("Horizontal");

        Vector3 pos = transform.position;
        pos.x += input * Speed * Time.deltaTime;

        if (pos.x > MaxMovement)
            pos.x = MaxMovement;
        else if (pos.x < -MaxMovement)
            pos.x = -MaxMovement;

        transform.position = pos;
    }

    public void LoadSettings()
    {
        string path = Application.persistentDataPath + "/saveSettingsfile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveSettings.SaveSettingsData data = JsonUtility.FromJson<SaveSettings.SaveSettingsData>(json);

            Speed = data.GetPaddleValue();
        }
    }
}
