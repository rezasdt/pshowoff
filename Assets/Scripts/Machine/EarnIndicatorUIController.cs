using TMPro;
using UnityEngine;

public class EarnIndicatorUIController : MonoBehaviour
{
    [SerializeField]
    private RectTransform _rectTransform;
    [SerializeField]
    private TextMeshProUGUI _text;

    private MachineEarnHandler _earnHandler;

    private void Awake()
    {
        _earnHandler = FindObjectOfType<MachineEarnHandler>();
    }

    public void Init(int pAmount, Vector2 pPosition)
    {
        _text.text = $"+{pAmount}$";
        transform.position = pPosition;
        Invoke(nameof(ReturnToPool), 1.5f);
    }

    private void FixedUpdate()
    {
        _rectTransform.anchoredPosition += Vector2.up * 2;
    }

    private void ReturnToPool()
    {
        _earnHandler.ReleaseIndicator(this);
    }
}