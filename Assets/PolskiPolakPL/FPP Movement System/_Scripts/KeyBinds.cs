using UnityEngine;
[CreateAssetMenu(fileName ="Player Key Binds", menuName ="ScriptableObject/Player Key Binds")]
public class KeyBinds : ScriptableObject
{
    public KeyCode JumpKey = KeyCode.Space;
    public KeyCode SprintKey = KeyCode.LeftShift;
    public KeyCode WalkKey = KeyCode.LeftAlt;
    public KeyCode CrouchKey = KeyCode.LeftControl;
    public KeyCode ProneKey = KeyCode.None;

    public KeyCode LeanLeftKey = KeyCode.Q;
    public KeyCode LeanRightKey = KeyCode.E;

    public KeyCode InteractKey = KeyCode.F;
    public KeyCode AltInteractKey = KeyCode.None;

    public KeyCode AttackKey = KeyCode.Mouse0;
    public KeyCode AimKey = KeyCode.Mouse1;
    public KeyCode MeleeKey = KeyCode.None;
    public KeyCode ThrowKey = KeyCode.G;

    public KeyCode ReloadKey = KeyCode.R;
    public KeyCode SwitchFireModeKey = KeyCode.V;
    public KeyCode FlashlightKey = KeyCode.F;

    public KeyCode InventoryKey = KeyCode.Tab;
    public KeyCode MapKey = KeyCode.M;
    public KeyCode PauseKey = KeyCode.Escape;
    public KeyCode NightVisionKey = KeyCode.N;
}
