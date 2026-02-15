using UnityEngine;


[CreateAssetMenu(fileName ="Firearm Data", menuName = "ScriptableObject/Firearm Data")]
public class FirearmData : ScriptableObject
{
    [Header("Basic Stats")]
    public float Damage; // basic damage
    public float CriticalDamage; // "Headshot"
    public int MagSize; // defines magazine size (how many bullets are shot before forced reload)
    public float FireCooldown; // How quickly a weapon fires
    public float ReloadTime; // How long it takes to reload

    [Header("Far Range")]
    public float FarRange; // defines what distance is considered as "far"
    public float FarAccuracy; // How accurate weapon is on a "far" distance
    public float FarPenetration; // How well weapon penetrates armor on a "far" distance

    [Header("Mid Range")]
    public float MidRange; // defines what distance is considered as "mid"
    public float MidAccuracy; // How accurate weapon is on a "mid" distance
    public float MidPenetration; // How well weapon penetrates armor on a "mid" distance

    [Header("Near Range")]
    public float NearRange; // defines what distamce is considered as "near"
    public float NearAccuracy; // How accurate weapon is on a "near" distance
    public float NearPenetration; // How well weapon penetrates armor on a "near" distance

    [Header("Modifiers")]
    public float RunningAccuracy = 1;
    public float NoCoverDmg = 1;
    public float LightCoverDmg = 1;
    public float HeavyCoverDmg = 1;
    public float GarrisonedDmg = 1;

}