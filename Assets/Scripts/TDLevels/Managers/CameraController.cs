using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool isInvertScroll;
    public bool canMousePan;

    private Camera cam;

    public float panSpeed = 30f, panBorderThickness = 10f, scrollSpeed = 5f;
    public float scrollMin = 20f, scrollMax = 80f;
    public float clampExcess;
    public static float xMin, xMax, yMin, yMax;

    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GameIsOver)
        {
            this.enabled = false;
            return;
        }

        if (Input.GetKey("w") || (Input.mousePosition.y >= Screen.height - panBorderThickness && canMousePan))
        {
            transform.Translate(Vector3.up * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("a") || (Input.mousePosition.x <= panBorderThickness && canMousePan))
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("s") || (Input.mousePosition.y <= panBorderThickness && canMousePan))
        {
            transform.Translate(Vector3.down * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("d") || (Input.mousePosition.x >= Screen.width - panBorderThickness && canMousePan))
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        Vector3 clampedPosition = transform.position;
        if (xMax - clampExcess > xMin)
            clampedPosition.x = Mathf.Clamp(transform.position.x, xMin + clampExcess, xMax - clampExcess);
        else
            clampedPosition.x = Mathf.Clamp(transform.position.x, xMin, xMax);

        if (yMax - clampExcess > yMin)
            clampedPosition.y = Mathf.Clamp(transform.position.y, yMin + 2 * clampExcess, yMax - 2 * clampExcess);
        else
            clampedPosition.y = Mathf.Clamp(transform.position.y, yMin, yMax);

        transform.position = clampedPosition;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float finalScroll = scroll * scrollSpeed * -1000 * Time.deltaTime;
        if (isInvertScroll)
            finalScroll *= -1f;

        cam.orthographicSize += finalScroll;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, scrollMin, scrollMax);
    }
}
