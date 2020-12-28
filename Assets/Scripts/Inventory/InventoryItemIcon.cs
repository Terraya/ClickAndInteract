using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemIcon : MonoBehaviour
{
    [SerializeField] GameObject textContainer = null;
    [SerializeField] TextMeshProUGUI itemNumber = null;
    
    public void SetItem(Sprite item)
    {
        var iconImage = GetComponent<Image>();
        if (item == null)
        {
            iconImage.enabled = false;
            return;
        }

        iconImage.enabled = true;
        iconImage.sprite = item;
    }

    public void SetItem(BaseInventoryItem item, int number)
    {
        var iconImage = GetComponent<Image>();
        if (item == null)
        {
            iconImage.enabled = false;
        }
        else
        {
            iconImage.enabled = true;
            iconImage.sprite = item.Icon;
        }

        if (itemNumber)
        {
            if (number <= 1)
            {
                textContainer.SetActive(false);
            }
            else
            {
                textContainer.SetActive(true);
                itemNumber.text = number.ToString();
            }
        }
    }
    
    public Sprite GetItem()
    {
        var iconImage = GetComponent<Image>();
        return !iconImage.enabled ? null : iconImage.sprite;
    }
}