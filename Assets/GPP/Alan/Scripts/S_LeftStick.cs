using UnityEngine;
using UnityEngine.EventSystems;

// Specify the axis options for the joystick
public enum AxisOptions { Both, Horizontal, Vertical }

// Using Unity's UI event handling system interfaces
// https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/SupportedEvents.html
public class S_LeftStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    // Access to the horizontal and vertical components of the joystick input
    public float Horizontal { get { return input.x; } }
    public float Vertical { get { return input.y; } }

    // Combined inputs
    public Vector2 Direction { get { return new Vector2(Horizontal, Vertical); } }

    // How far the handle can goes from the center of the joystick
    // If put to 1, the center of the handle will be on the joystick's circumference
    public float HandleRange
    {
        get { return handleRange; }
        set { handleRange = Mathf.Abs(value); }
    }

    // The zone that the movement starts based on the scale of the joystick
    // If put to zero, the movement starts from the center of the joystick
    public float DeadZone
    {
        get { return deadZone; }
        set { deadZone = Mathf.Abs(value); }
    }

    public AxisOptions AxisOptions { get { return AxisOptions; } set { axisOptions = value; } }
    
    [Header("Joystick options:")]
    [SerializeField] private float handleRange = 1;
    [SerializeField] private float deadZone = 0;
    [SerializeField] private AxisOptions axisOptions = AxisOptions.Both;

    [SerializeField] protected RectTransform background = null;
    [SerializeField] private RectTransform handle = null;

    // The reference to the joystick background image
    private RectTransform baseRect = null;

    private Canvas canvas;
    private Camera cam;

    private bool isLocked = false;

    private Vector2 input = Vector2.zero;

    protected virtual void Start()
    {
        HandleRange = handleRange;
        DeadZone = deadZone;
        baseRect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
            Debug.LogError("The Joystick is not placed inside a canvas");

        // Set the pivot and initial position of the background and handle
        Vector2 center = new Vector2(0.5f, 0.5f);
        background.pivot = center;
        handle.anchorMin = center;
        handle.anchorMax = center;
        handle.pivot = center;
        handle.anchoredPosition = Vector2.zero;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isLocked)
        {
            cam = null;
            if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
                cam = canvas.worldCamera;

            // Convert the background's world position to screen coordinates
            Vector2 position = RectTransformUtility.WorldToScreenPoint(cam, background.position);

            // Calculate the radius of the joystick's background
            Vector2 radius = background.sizeDelta / 2;

            // Calculate the normalized input vector based on the pointer's position
            input = (eventData.position - position) / (radius * canvas.scaleFactor);

            FormatInput();
            HandleInput(input.magnitude, input.normalized, radius, cam);

            // Update the anchored position of the handle
            handle.anchoredPosition = input * radius * handleRange;
        }
    }

    protected virtual void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        // Handle the input (apply dead zone, snapping, etc.)
        if (magnitude > deadZone)
        {
            if (magnitude > 1)
                input = normalised;
        }
        else
            input = Vector2.zero;
    }

    // Format the input based on the specified axis options
    private void FormatInput()
    {
        // Format the input based on the specified axis options
        if (axisOptions == AxisOptions.Horizontal)
            input = new Vector2(input.x, 0f);
        else if (axisOptions == AxisOptions.Vertical)
            input = new Vector2(0f, input.y);
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        // Reset the input vector to zero
        input = Vector2.zero;

        // Reset the anchored position of the handle to zero
        handle.anchoredPosition = Vector2.zero;
    }

    protected Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
    {
        // Initialize a local point variable
        Vector2 localPoint = Vector2.zero;

        // Check if the screen position can be converted to local point
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(baseRect, screenPosition, cam, out localPoint))
        {
            // Calculate pivot offset based on the pivot and size of the RectTransform
            Vector2 pivotOffset = baseRect.pivot * baseRect.sizeDelta;

            // Calculate the anchored position relative to the RectTransform
            return localPoint - (background.anchorMax * baseRect.sizeDelta) + pivotOffset;
        }
        
        // Return Vector2.zero if conversion fails
        return Vector2.zero;
    }

    public void changeAxis(AxisOptions ao)
    {
        axisOptions = ao;
    }

    public void Lock(bool locked)
    {
        isLocked = locked;
        input = Vector2.zero;
    }
}
