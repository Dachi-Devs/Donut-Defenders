using UnityEngine;

public class OverworldPlayer : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LevelMarker startMarker;
    public bool canMove;

    public bool IsMoving { get; private set; }

    public LevelMarker CurrentMarker { get; private set; }
    private LevelMarker targetMarker;

    public ScreenFade fade;
    public UIOverworld ui;

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
        if ((Input.GetKeyDown(KeyCode.UpArrow)) || (Input.GetKeyDown(KeyCode.W)))
        {
            TrySetDirection(Direction.Up);
        }
        else if ((Input.GetKeyDown(KeyCode.DownArrow)) || (Input.GetKeyDown(KeyCode.S)))
        {
            TrySetDirection(Direction.Down);
        }
        else if ((Input.GetKeyDown(KeyCode.RightArrow)) || (Input.GetKeyDown(KeyCode.D)))
        {
            TrySetDirection(Direction.Right);
        }

        if ((Input.GetMouseButtonDown(0)) || (Input.GetKeyDown(KeyCode.A)))
        {
            TryLoadLevel();
        }
    }

    private void TryLoadLevel()
    {
        string lvl = CurrentMarker.level.levelToLoad.name;
        if (lvl != null)
        {
            Debug.Log("Load Scene " + lvl);
            fade.FadeTo(lvl);
        }
    }

    public void TrySetDirection(Direction direction)
    {
        var marker = CurrentMarker.GetMarkerInDirection(direction);

        if (marker == null)
            return;

        if(canMove)
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
        canMove = false;
        CurrentMarker = marker;
        targetMarker = null;
        transform.position = marker.transform.position;
        IsMoving = false;
        ui.SetLevelName(CurrentMarker.level.levelTitle.ToString());
    }

    public void OnLevelExit()
    {
        canMove = true;
    }
}
