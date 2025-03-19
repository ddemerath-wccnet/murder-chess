//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class ImageCarousel : MonoBehaviour
{
    public Image displayImage;
    public Image leftImage;
    public Image rightImage;

    public Text displayText;

    public Sprite[] images;
    public string[] imageDescriptions;
    private int currentIndex = 0;

    void Start()
    {
        UpdateImage();

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

    }

    void PreviousImage()
    {
        currentIndex = (currentIndex - 1 + images.Length) % images.Length;
        UpdateImage();
    }
    void NextImage()
    {
        currentIndex = (currentIndex + 1 + images.Length) % images.Length;
        UpdateImage();
    }

    void UpdateImage()
    {
        displayImage.sprite = images[currentIndex];
        displayText.text = imageDescriptions[currentIndex];


        int previousIndex = (currentIndex - 1 + images.Length) % images.Length;
        leftImage.sprite = images[previousIndex];

        int nextIndex = (currentIndex + 1 + images.Length) % images.Length;
        rightImage.sprite = images[nextIndex];
    }

}
