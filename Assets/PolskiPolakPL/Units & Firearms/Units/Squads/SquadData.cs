using PolskiPolakPL.enums;
using UnityEngine;


[CreateAssetMenu(fileName = "SquadData", menuName = "ScriptableObject/Squad Data")]
public class SquadData : ScriptableObject
{
    [Min(1)] public int SquadSize = 1;
    public int HP = 100;
    public float MovementSpeed = 3.2f;
    public float Sight = 20;
    public float ReactionTime = 1.1f;
    public float AimTime = 0.5f;
}
