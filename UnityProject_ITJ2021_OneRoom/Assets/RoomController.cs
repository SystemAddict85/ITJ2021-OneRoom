using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] private RoomEventHandler handler;
    [SerializeField] private Transform westWall, eastWall, northWall;
    public enum RoomWall { West, East, North }

    private float _moveDuration;

    private void Awake()
    {
        handler.onWallMove += ChangeWall;
    }

    private void ChangeWall(RoomWall wall, float change, float time)
    {
        Vector3 pos;
        _moveDuration = time;
        switch (wall)
        {
            case RoomWall.West:
                pos = westWall.position;
                pos.x += change;
                TweenWall(westWall, pos);
                break;
            case RoomWall.East:
                pos = eastWall.position;
                pos.x += change;
                TweenWall(eastWall, pos);
                break;
            case RoomWall.North:
                pos = northWall.position;
                pos.z += change;
                TweenWall(northWall, pos);               
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(wall), wall, null);
        }
    }

    private void TweenWall(Transform wall, Vector3 destination)
    {
        wall.DOLocalMove(destination, _moveDuration);
    }
}