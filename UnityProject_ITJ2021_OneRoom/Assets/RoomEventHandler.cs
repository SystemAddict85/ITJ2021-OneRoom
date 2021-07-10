using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Room Event Handler",menuName="Handlers/Room Event")]
public class RoomEventHandler : ScriptableObject
{
    public Action<RoomController.RoomWall, float, float> onWallMove;

    [SerializeField] private float timeToMove = 2f;

    public void MoveWall(RoomController.RoomWall wall, float distance, float time)
    {
        onWallMove?.Invoke(wall, distance, time);
    }

    public void SetMoveTime(float newTime) => timeToMove = newTime;

    public void MoveWestWall(float distance)
    {
        MoveWall(RoomController.RoomWall.West, distance, timeToMove);
    }
    public void MoveEastWall(float distance)
    {
        MoveWall(RoomController.RoomWall.East, distance, timeToMove);
    }
    public void MoveNorthWall(float distance)
    {
        MoveWall(RoomController.RoomWall.North, distance, timeToMove);
    }
    public void MoveAllWalls(float distance)
    {
        MoveWall(RoomController.RoomWall.West, -distance, timeToMove);
        MoveWall(RoomController.RoomWall.East, distance, timeToMove);
        MoveWall(RoomController.RoomWall.North, distance, timeToMove);
    }
    
    
}