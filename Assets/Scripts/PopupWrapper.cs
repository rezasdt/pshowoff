using System.Collections;
using TMPro;
using UnityEngine;

public class PopupWrapper : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float slideDuration = 0.5f;
    [SerializeField] private float visibleDuration = 2.0f;

    private RectTransform _rectTransform;
    private float _width;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _width = _rectTransform.rect.width;
    }

    public void SetText(string text)
    {
        _text.text = text;
    }

    private IEnumerator Start()
    {
        //_rectTransform.anchoredPosition = new Vector2(-_width, _rectTransform.anchoredPosition.y);
        _rectTransform.anchoredPosition = new Vector2(0, 0);

        // Slide in
        yield return StartCoroutine(SlideToPosition(Vector2.zero, slideDuration));

        // Wait for the visible duration
        yield return new WaitForSeconds(visibleDuration);

        // Slide out (off-screen to the right)
        yield return StartCoroutine(SlideToPosition(new Vector2(_width, _rectTransform.anchoredPosition.y), slideDuration));

        Destroy(gameObject);
    }

    private IEnumerator SlideToPosition(Vector2 targetPosition, float duration)
    {
        Vector2 startPosition = _rectTransform.anchoredPosition;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            _rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _rectTransform.anchoredPosition = targetPosition;
    }
}
