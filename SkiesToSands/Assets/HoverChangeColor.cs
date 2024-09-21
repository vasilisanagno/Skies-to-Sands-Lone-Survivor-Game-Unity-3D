using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonColorChangeOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color normalColor = Color.white;
    public Color hoverColor = Color.red;

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        // Set the initial color
        if (button != null)
        {
            button.image.color = normalColor;
        }
    }

    // This function is called when the mouse enters the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button != null)
        {
            button.image.color = hoverColor;
        }
    }

    // This function is called when the mouse exits the button
    public void OnPointerExit(PointerEventData eventData)
    {
        if (button != null)
        {
            button.image.color = normalColor;
        }
    }
}
