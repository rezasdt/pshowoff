using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField] private PopupWrapper _popupPrefab;

    public void Create(string text)
    {
        PopupWrapper popup = Instantiate(_popupPrefab, transform);
        popup.SetText(text);
    }
}
