using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpdateValueText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI value;
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void SetValue()
    {
        value.text = slider.value.ToString();
    }
}
