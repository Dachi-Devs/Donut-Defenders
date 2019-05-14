using UnityEngine;

public class LockPaths : MonoBehaviour
{
    public bool isLocked;

    // Start is called before the first frame update
    void Start()
    {
        isLocked = true;
    }

    public void UnlockPath()
    {
        isLocked = false;
    }
}
