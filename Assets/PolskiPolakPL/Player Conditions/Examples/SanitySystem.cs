using TMPro;
using UnityEngine;

public class SanitySystem : MonoBehaviour
{
    public Condition sanity {  get; private set; }
    SanityState currentState;
    [SerializeField] float maxSanity = 100;
    [SerializeField] ConditionBar conditionBar;
    [SerializeField] float saneVal;
    [SerializeField] float paranoidVal;
    [SerializeField] float insaneVal;

    [SerializeField] TMP_Text sanityTextField;

    private void Awake()
    {
        sanity = new Condition(maxSanity);

        sanity.OnGained += HandleSanityGain;
        sanity.OnGained += DisplayCurrentState;

        sanity.OnLost += HandleSanityLoss;
        sanity.OnLost += DisplayCurrentState;

        sanity.OnCurrentValChanged += HandleSanityGain;
        sanity.OnCurrentValChanged += HandleSanityLoss;
        sanity.OnCurrentValChanged += DisplayCurrentState;
    }

    private void Start()
    {
        if (conditionBar)
            conditionBar.AttachCondition(sanity);
        DisplayCurrentState();
    }
    private void Update()
    {
        HandleDebug();
    }

    void DisplayCurrentState()
    {
        if (sanityTextField)
            sanityTextField.text = $"{currentState} \t ({sanity.CurrentVal}%)";
        else
            Debug.Log($"Current State: {currentState} \t ({sanity.CurrentVal}%)");
    }


    // Kolejnoœæ w obu poni¿szych metodach jest wa¿na, poniewa¿ pozwala ona na drastyczne spadki/odzyskanie poczytalnoœci.
    // Najpierw sprawdza najgorszy przypadek, a potem idzie stopniowo w górê.
    void HandleSanityLoss()
    {
        
        // INSANE -> MAD
        if (sanity.CheckBelowValue(insaneVal))
        {
            SetSanityState(SanityState.MAD);
            sanity.PrevValue = sanity.CurrentVal;
            return;
        }

        // PARANOID -> INSANE
        if (sanity.CheckBelowValue(paranoidVal))
        {
            SetSanityState(SanityState.INSANE);
            sanity.PrevValue = sanity.CurrentVal;
            return;
        }

        // SANE -> PARANOID
        if (sanity.CheckBelowValue(saneVal))
        {
            SetSanityState(SanityState.PARANOID);
            sanity.PrevValue = sanity.CurrentVal;
            return;
        }
    }

    void HandleSanityGain()
    {
        // PARANOID -> SANE
        if (sanity.CheckAboveVal(saneVal))
        {
            SetSanityState(SanityState.SANE);
            sanity.PrevValue = sanity.CurrentVal;
            return;
        }

        // INSANE -> PARANOID
        if (sanity.CheckAboveVal(paranoidVal))
        {
            SetSanityState (SanityState.PARANOID);
            sanity.PrevValue = sanity.CurrentVal;
            return;
        }

        // MAD -> INSANE
        if (sanity.CheckAboveVal(insaneVal))
        {
            SetSanityState(SanityState.INSANE);
            sanity.PrevValue = sanity.CurrentVal;
            return;
        }
    }

    void SetSanityState(SanityState newState)
    {
        currentState = newState;
    }

    void HandleDebug()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            sanity.Gain(3);
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            sanity.Loose(3);
        }

        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            sanity.SetCurrentValue(sanity.MinVal);
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            sanity.SetCurrentValue(sanity.MaxVal);
        }
    }

}

public enum SanityState
{
    SANE,
    PARANOID,
    INSANE,
    MAD
}