using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Drawing;
using Unity.VisualScripting;

public class ColorCursor : MonoBehaviour, IDragHandler
{
    private new bool enabled;

    public UnityEngine.Color color;

    [SerializeField] private Transform cursor;
    [SerializeField] private Image cursorColor;
    private RectTransform hsv;
    private Image image;
    [SerializeField] private Texture2D hsvTexture;
    [SerializeField] private Sprite whiteSprite;
    [SerializeField] private Sprite hsvSprite;
    [SerializeField] private Text inspector;

    private void Start()
    {
        hsv = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        color = UnityEngine.Color.white;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (IsInsideHSVMap(eventData.position) && enabled)
        {
            cursor.position = eventData.position;

            color = hsvTexture.GetPixel(
                (int)((eventData.position.x - (hsv.position.x - hsv.rect.size.x / 2)) * (hsvTexture.width / hsv.rect.size.x)), 
                (int)((eventData.position.y - (hsv.position.y - hsv.rect.size.y / 2)) * (hsvTexture.height / hsv.rect.size.y))
            );
            cursorColor.color = color;
            inspector.text = "#" + color.ToHexString();
        }
    }

    private bool IsInsideHSVMap(Vector2 point) => point.x > hsv.position.x - hsv.rect.size.x / 2 && 
                                                  point.x < hsv.position.x + hsv.rect.size.x / 2 &&
                                                  point.y > hsv.position.y - hsv.rect.size.y / 2 &&
                                                  point.y < hsv.position.y + hsv.rect.size.y / 2;

    public void Enable(bool state)
    {
        enabled = state;

        image.sprite = state ? hsvSprite : whiteSprite;
    }
}
