using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 1)]
public class CheckpointData : ScriptableObject
{
    public int lastCheckpointID;
}