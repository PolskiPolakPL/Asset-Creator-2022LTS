using UnityEngine;

[CreateAssetMenu(fileName = "AudioSrc Setting", menuName = "ScriptableObject/Audio Source Settings")]
public class AudioSrcSettings : ScriptableObject
{
    [field: SerializeField][field: Range(0, 256)] public int Priority { get; private set; } = 128;
    [field: SerializeField][field: Range(0, 1)] public float Volume { get; private set; } = 1;
    [field: SerializeField][field: Range(-3, 3)] public float Pitch { get; private set; } = 1;
    [field: SerializeField][field: Range(-1, 1)] public float StereoPan { get; private set; } = 0;
    [field: SerializeField][field: Range(0, 1)] public float SpatialBlend { get; private set; } = 1;
    [field: SerializeField][field: Range(0, 1.1f)] public float ReverbZoneMix { get; private set; } = 1;
}
