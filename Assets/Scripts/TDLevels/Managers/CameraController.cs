using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool isInvertScroll;

    private Camera cam;

    public float panSpeed = 30f, panBorderThickness = 10f, scrollSpeed = 5f;
    public float scrollMin = 20f, scrollMax = 80f;

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

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.up * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.down * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(transform.position.x, -15f, 50f);
        clampedPosition.y = Mathf.Clamp(transform.position.y, 5f, 30f);

        transform.position = clampedPosition;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float finalScroll = scroll * scrollSpeed * -1000 * Time.deltaTime;
        if (isInvertScroll)
            finalScroll *= -1f;

        cam.orthographicSize += finalScroll;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, scrollMin, scrollMax);

    }
}
