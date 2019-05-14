using UnityEngine;

public class OverworldPlayer : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LevelMarker startMarker;

    public bool IsMoving { get; private set; }

    public LevelMarker CurrentMarker { get; private set; }
    private LevelMarker targetMarker;

    public ScreenFade fade;

    void Start()
    {
        SetCurrentMarker(startMarker);
    }

    void Update()
    {
        if(!IsMoving)
        {
            CheckForInput();
        }

        if (targetMarker == null)
            return;

        MoveToLevel();
    }

    protected void DrawLine(LevelMarker marker)
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, marker.transform.position);
    }

    private void CheckForInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            TrySetDirection(Direction.Up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            TrySetDirection(Direction.Down);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            TrySetDirection(Direction.Right);
        }

        if (Input.GetMouseButtonDown(0))
        {
            TryLoadLevel();
        }

        if (Input.GetMouseButtonDown(1))
        {
            UnlockNextLevel();
        }
    }

    private void TryLoadLevel()
    {
        fade.FadeTo(CurrentMarker.level.levelToLoad);
    }

    public void TrySetDirection(Direction direction)
    {
        var marker = CurrentMarker.GetMarkerInDirection(direction);

        if (marker == null)
            return;

        if(marker.pathLock.isLocked == false)
        {
            MoveToMarker(marker);
        }
    }

    private void MoveToMarker(LevelMarker marker)
    {
        targetMarker = marker;
        IsMoving = true;
    }

    public void MoveToLevel()
    {
        var currentPosition = transform.position;
        var targetPosition = targetMarker.transform.position;

        if (Vector3.Distance(currentPosition, targetPosition) > .02f)
        {
            transform.position = Vector3.MoveTowards(currentPosition, targetPosition, Time.deltaTime * moveSpeed);
        }
        else
        {
            if(targetMarker.isPath)
            {
                MoveToMarker(targetMarker.nextMarker);
            }
            else
            {
                SetCurrentMarker(targetMarker); 
            }
        }
    }

    public void SetCurrentMarker(LevelMarker marker)
    {
        CurrentMarker = marker;
        targetMarker = null;
        transform.position = marker.transform.position;
        IsMoving = false;
    }

    public void OnLevelExit()
    {
        UnlockNextLevel();
    }

    void UnlockNextLevel()
    {
        if(CurrentMarker.UpMarker != null)
        {
            CurrentMarker.UpMarker.pathLock.UnlockPath();
        }
        if (CurrentMarker.DownMarker != null)
        {
            CurrentMarker.DownMarker.pathLock.UnlockPath();
        }
        if (CurrentMarker.RightMarker != null)
        {
            CurrentMarker.RightMarker.pathLock.UnlockPath();
        }
    }
}
