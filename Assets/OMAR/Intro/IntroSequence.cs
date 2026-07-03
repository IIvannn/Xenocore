using UnityEngine;

[CreateAssetMenu(fileName = "IntroSequence", menuName = "Intro/Sequence")]
public class IntroSequence : ScriptableObject
{
    public IntroLine[] lines;
}
