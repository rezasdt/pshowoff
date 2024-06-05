using TMPro;
using UnityEngine;

public class EarnIndicatorUIController : MonoBehaviour
{
    [SerializeField]
    private RectTransform _rectTransform;
    [SerializeField]
    private TextMeshProUGUI _text;

    public void Init(int pAmount, Vector2 pPosition)
    {
        _text.text = $"+{pAmount}$";
        transform.position = pPosition;
        Destroy(gameObject, 1.5f);
    }

    private void FixedUpdate()
    {
        _rectTransform.anchoredPosition += Vector2.up * 2;
    }
}
