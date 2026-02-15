using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConditionUI : MonoBehaviour
{
    public Condition condition;
    [SerializeField] Slider conditionBar;
    [SerializeField] TMP_Text conditionTextField;
    [Tooltip("Optional prefix for condition text field")]
    [SerializeField] string prefix = "";
    [Tooltip("Optional suffix for condition text field")]
    [SerializeField] string suffix = "";
    private void Awake()
    {
        condition.OnCurrentValChanged += UpdateBarValue;
        condition.OnCurrentValChanged += UpdateText;
        condition.OnGained += UpdateBarValue;
        condition.OnGained += UpdateText;
        condition.OnLost += UpdateBarValue;
        condition.OnLost += UpdateText;
    }

    void UpdateBarValue()
    {
        if(conditionBar)
            conditionBar.value = condition.GetNormalizedValue();
    }

    void UpdateText()
    {
        if (conditionTextField)
            conditionTextField.text = prefix + condition.CurrentVal + suffix;
    }
}
