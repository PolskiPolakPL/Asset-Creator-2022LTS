using TMPro;
using UnityEngine;

public class SanitySystem : MonoBehaviour
{
    public Condition sanity {  get; private set; }
    SanityState currentState;
    [SerializeField] float maxSanity = 100;
    [SerializeField] float regenSpeed = 1;
    [SerializeField] ConditionBar conditionBar;
    [SerializeField] float saneVal;
    [SerializeField] float paranoidVal;
    [SerializeField] float insaneVal;
    [HideInInspector] public bool CanRegenerate = true;
    [SerializeField] TMP_Text sanityTextField;

    Camera cam;
    float normalFoV;
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
        cam = Camera.main;
        normalFoV = cam.fieldOfView;
    }
    private void Update()
    {
        if(CanRegenerate)
            sanity.Regen(regenSpeed);
    }

    void DisplayCurrentState()
    {
        float displayValue = Mathf.Round(sanity.CurrentVal * 100) / 100;
        if (sanityTextField)
            sanityTextField.text = $"{currentState} \t ({displayValue}%)";
        else
            Debug.Log($"Current State: {currentState} \t ({displayValue}%)");
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
            cam.fieldOfView = normalFoV/3;
            return;
        }

        // PARANOID -> INSANE
        if (sanity.CheckBelowValue(paranoidVal))
        {
            SetSanityState(SanityState.INSANE);
            sanity.PrevValue = sanity.CurrentVal;
            cam.fieldOfView = normalFoV/2;
            return;
        }

        // SANE -> PARANOID
        if (sanity.CheckBelowValue(saneVal))
        {
            SetSanityState(SanityState.PARANOID);
            sanity.PrevValue = sanity.CurrentVal;
            cam.fieldOfView = normalFoV*2/3;
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
            cam.fieldOfView = normalFoV;
            return;
        }

        // INSANE -> PARANOID
        if (sanity.CheckAboveVal(paranoidVal))
        {
            SetSanityState (SanityState.PARANOID);
            sanity.PrevValue = sanity.CurrentVal;
            cam.fieldOfView = normalFoV * 2 / 3;
            return;
        }

        // MAD -> INSANE
        if (sanity.CheckAboveVal(insaneVal))
        {
            SetSanityState(SanityState.INSANE);
            sanity.PrevValue = sanity.CurrentVal;
            cam.fieldOfView = normalFoV / 2;
            return;
        }
    }

    void SetSanityState(SanityState newState)
    {
        currentState = newState;
    }

}

public enum SanityState
{
    SANE,
    PARANOID,
    INSANE,
    MAD
}