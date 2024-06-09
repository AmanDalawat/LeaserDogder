using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SimpleJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image bgImage;
    private Image joystickImage;
    public Vector3 InputDirection { get; private set; }

    private void Start()
    {
        bgImage = GetComponent<Image>();
        joystickImage = transform.GetChild(0).GetComponent<Image>();
        InputDirection = Vector3.zero;
    }

    public void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImage.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / bgImage.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImage.rectTransform.sizeDelta.y);

            float x = pos.x * 2 + 1;
            float y = pos.y * 2 - 1;
            InputDirection = new Vector3(x, 0, y);
            InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;

            joystickImage.rectTransform.anchoredPosition = new Vector3(InputDirection.x * (bgImage.rectTransform.sizeDelta.x / 3), InputDirection.z * (bgImage.rectTransform.sizeDelta.y / 3));
        }
    }

    public void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public void OnPointerUp(PointerEventData ped)
    {
        InputDirection = Vector3.zero;
        joystickImage.rectTransform.anchoredPosition = Vector3.zero;
    }
}