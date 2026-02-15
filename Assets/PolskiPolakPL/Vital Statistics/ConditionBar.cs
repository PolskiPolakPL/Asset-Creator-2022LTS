using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ConditionBar : MonoBehaviour
{
    Condition condition;
    Slider conditionBar;

    private void Awake()
    {
        conditionBar = GetComponent<Slider>();
    }

    public void AttachCondition(Condition condition)
    {
        if (condition != null)
            DetachCondition();
        this.condition = condition;
        this.condition.OnCurrentValChanged += UpdateBarValue;
        this.condition.OnGained += UpdateBarValue;
        this.condition.OnLost += UpdateBarValue;
        UpdateBarValue();
    }

    public void DetachCondition()
    {
        if (condition == null)
            return;
        condition.OnCurrentValChanged -= UpdateBarValue;
        condition.OnGained -= UpdateBarValue;
        condition.OnLost -= UpdateBarValue;
        condition = null;
    }

    void UpdateBarValue()
    {
        if(conditionBar)
            conditionBar.value = condition.GetNormalizedValue();
    }

    private void OnDestroy()
    {
        DetachCondition();
    }
}
