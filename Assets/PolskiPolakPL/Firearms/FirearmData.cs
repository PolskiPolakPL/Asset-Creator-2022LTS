using UnityEngine;


[CreateAssetMenu(fileName ="Firearm Data", menuName = "ScriptableObject/Firearm Data")]
public class FirearmData : ScriptableObject
{
    [Header("Basic Stats")]
    public float damage; // basic damage
    public float criticalDamage; // "Headshot"
    public int magSize; // defines magazine size (how many bullets are shot before forced reload)
    public float fireCooldown; // How quickly a weapon fires
    public float farRange; // defines what distance is considered as "far"
    public float midRange; // defines what distance is considered as "mid"
    public float nearRange; // defines what distamce is considered as "near"
    public float farAccuracy; // How accurate weapon is on a "far" distance
    public float midAccuracy; // How accurate weapon is on a "mid" distance
    public float nearAccuracy; // How accurate weapon is on a "near" distance
    public float farPenetration; // How well weapon penetrates armor on a "far" distance
    public float midPenetration; // How well weapon penetrates armor on a "mid" distance
    public float nearPenetration; // How well weapon penetrates armor on a "near" distance

}