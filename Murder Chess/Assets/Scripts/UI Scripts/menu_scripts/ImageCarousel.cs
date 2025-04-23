//using Microsoft.Unity.VisualStudio.Editor;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageCarousel : MonoBehaviour
{
    public Image displayImage;
    public Image leftImage;
    public Image rightImage;
    ShopManager shopManager;

    public Text displayText;
    public Text costText;

    public List<ShopItem> items;
    private int currentIndex = 0;

    public SlotSelector slotSelector;

    void Start()
    {
        UpdateImage();
        shopManager = FindFirstObjectByType<ShopManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextImage();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousImage();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            slotSelector.gameObject.SetActive(true);
            slotSelector.UpdateImages();
        }
    }

    public void GetItem(int slot)
    {
        slotSelector.gameObject.SetActive(false);
        if (shopManager.AquireItem(items[currentIndex], slot))
        {
            items.RemoveAt(currentIndex);
            UpdateImage();
            slotSelector.UpdateImage(slot, items[currentIndex].image);
        }
    }

    void PreviousImage()
    {
        if (items.Count == 0) return;
        currentIndex = (currentIndex - 1 + items.Count) % items.Count;
        UpdateImage();
    }
    void NextImage()
    {
        if (items.Count == 0) return;
        currentIndex = (currentIndex + 1 + items.Count) % items.Count;
        UpdateImage();
    }

    public void UpdateImage()
    {
        if (items.Count == 0)
        {
            displayImage.sprite = null;
            leftImage.sprite = null;
            rightImage.sprite = null;
            displayText.text = "";
            costText.text = "";
            return;
        }

        currentIndex = (currentIndex + items.Count) % items.Count;

        displayImage.sprite = items[currentIndex].image;
        displayText.text = items[currentIndex].description;
        costText.text = items[currentIndex].price + " $MP";


        int previousIndex = (currentIndex - 1 + items.Count) % items.Count;
        leftImage.sprite = items[previousIndex].image;

        int nextIndex = (currentIndex + 1 + items.Count) % items.Count;
        rightImage.sprite = items[nextIndex].image;
    }

}
