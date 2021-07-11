using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Room Event Handler",menuName="Handlers/Room Event")]
public class RoomEventHandler : ScriptableObject
{
    public Action<RoomController.RoomWall, float, float> onWallMove;
    public Action onLevelUp;
    public Action<RoomController.RoomWall, int, float, float> onPreviousLevelWallMove;

    [SerializeField] private float timeToMove = 2f;
    
    // Method group for changing previous levels
    private int _previousLevel = 0;
    public void UpdatePreviousLevelIndex(int index) => _previousLevel = index;

    private void MovePreviousWall(RoomController.RoomWall wall, int prevLevelIndex, float distance) =>
        onPreviousLevelWallMove?.Invoke(wall, prevLevelIndex, distance, timeToMove);
    public void MovePreviousWestWall(float distance) => MovePreviousWall(RoomController.RoomWall.West, _previousLevel, distance);
    public void MovePreviousEastWall(float distance) => MovePreviousWall(RoomController.RoomWall.East, _previousLevel, distance);

    // Method group for moving current level's walls
    private void MoveWall(RoomController.RoomWall wall, float distance, float time) => onWallMove?.Invoke(wall, distance, time);
    public void MoveWestWall(float distance) => MoveWall(RoomController.RoomWall.West, distance, timeToMove);
    public void MoveEastWall(float distance) => MoveWall(RoomController.RoomWall.East, distance, timeToMove);
    public void MoveNorthWall(float distance) => MoveWall(RoomController.RoomWall.North, distance, timeToMove);
    public void MoveAllWalls(float distance)
    {
        MoveWall(RoomController.RoomWall.West, -distance, timeToMove);
        MoveWall(RoomController.RoomWall.East, distance, timeToMove);
        MoveWall(RoomController.RoomWall.North, distance, timeToMove);
    }
    
    public void LevelUp() => onLevelUp?.Invoke();

    public void SetMoveTime(float newTime) => timeToMove = newTime;
    
}