using TMPro;
using UnityEngine;

public class NotificationUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;

    public void Init(string pTitle, string pDescription, float pDuration)
    {
        title.text = pTitle;
        description.text = pDescription;
        Destroy(gameObject, pDuration);
    }

    public void OnClick()
    {
        Destroy(gameObject);
    }
}
