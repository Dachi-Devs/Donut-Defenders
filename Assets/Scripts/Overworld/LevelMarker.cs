using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Direction
{
    Up,
    Down,
    Right
}

public class LevelMarker : MonoBehaviour
{
    [Header("Level Settings")]
    public LevelSettings level;

    [Header("Markers")] //
    public LevelMarker UpMarker;
    public LevelMarker DownMarker;
    public LevelMarker RightMarker;

    [Header("Path Options")]
    public bool isPath;
    public LevelMarker nextMarker;

    private Dictionary<Direction, LevelMarker> _markerDirections;

    private void Start()
    {
        _markerDirections = new Dictionary<Direction, LevelMarker>
        {
            { Direction.Up, UpMarker },
            { Direction.Down, DownMarker },
            { Direction.Right, RightMarker }
        };
    }

    public LevelMarker GetMarkerInDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return UpMarker;
            case Direction.Down:
                return DownMarker;
            case Direction.Right:
                return RightMarker;
            default:
                throw new ArgumentOutOfRangeException("direction", direction, null);
        }
    }

    public LevelMarker GetNextMarker(LevelMarker marker)
    {
        return _markerDirections.FirstOrDefault(x => x.Value != null && x.Value != marker).Value;
    }

    private void OnDrawGizmos()
    {
        if (UpMarker != null) DrawLine(UpMarker);
        if (DownMarker != null) DrawLine(DownMarker);
        if (RightMarker != null) DrawLine(RightMarker);
        if (nextMarker != null) DrawLine(nextMarker);
    }

    protected void DrawLine(LevelMarker marker)
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, marker.transform.position);
    }
}
