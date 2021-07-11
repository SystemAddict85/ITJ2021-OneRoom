using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] private RoomEventHandler handler;
    [SerializeField] private Transform northWall;

    [SerializeField] private int currentLevel = 0;
    [SerializeField] private List<LevelWalls> levels;

    public enum RoomWall
    {
        West,
        East,
        North
    }

    private float _moveDuration;

    private void Awake()
    {
        handler.onWallMove += ChangeWall;
        handler.onLevelUp += LevelUp;
        handler.onPreviousLevelWallMove += ChangePreviousLevelWall;

        InitializeWalls();
    }

    private void InitializeWalls()
    {
        northWall.gameObject.SetActive(true);
        var pos = northWall.position;
        pos.z = 0;
        northWall.position = new Vector3(northWall.position.x, northWall.position.y, 0f);

        for (int i = 0; i < levels.Count; ++i)
        {
            levels[i].eastWall.gameObject.SetActive(true);
            levels[i].westWall.gameObject.SetActive(true);
            var zOffset = i == 0 ? -2 : levels[i - 1].westWall.position.z;
            levels[i].eastWall.position = new Vector3(28f, 4.75f, levels[i].northWallDistance + zOffset);
            levels[i].westWall.position = new Vector3(-26f, 4.75f, levels[i].northWallDistance + zOffset);
        }
    }

    private void LevelUp()
    {
        ++currentLevel;
        ChangeWall(RoomWall.North, levels[currentLevel].northWallDistance, _moveDuration);
    }

    private void ChangePreviousLevelWall(RoomWall wall, int prevLevelIndex, float change, float time)
    {
        prevLevelIndex = Mathf.Min(levels.Count - 1, prevLevelIndex);

        Vector3 pos;
        _moveDuration = time;
        switch (wall)
        {
            case RoomWall.West:
                pos = levels[prevLevelIndex].westWall.position;
                pos.x += change;
                TweenWall(levels[prevLevelIndex].westWall, pos);
                break;
            case RoomWall.East:
                pos = levels[prevLevelIndex].eastWall.position;
                pos.x += change;
                TweenWall(levels[prevLevelIndex].eastWall, pos);
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

    private void ChangeWall(RoomWall wall, float change, float time)
    {
        Vector3 pos;
        _moveDuration = time;
        switch (wall)
        {
            case RoomWall.West:
                pos = levels[currentLevel].westWall.position;
                pos.x += change;
                TweenWall(levels[currentLevel].westWall, pos);
                break;
            case RoomWall.East:
                pos = levels[currentLevel].eastWall.position;
                pos.x += change;
                TweenWall(levels[currentLevel].eastWall, pos);
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

    [Serializable]
    private struct LevelWalls
    {
        public Transform westWall, eastWall;
        public float northWallDistance;
    }
}