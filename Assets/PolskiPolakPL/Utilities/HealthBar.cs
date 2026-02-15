using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;
#if UNITY_EDITOR
    private void OnValidate()
    {
        slider = GetComponent<Slider>();
    }
#endif

    public void UpdateBarValue(float currentValue, float maxValue)
    {
        slider = gameObject.GetComponent<Slider>();
        slider.value = currentValue / maxValue;
    }
}
