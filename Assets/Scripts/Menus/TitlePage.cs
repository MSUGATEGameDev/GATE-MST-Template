using UnityEngine;

public class TitlePage : MenuPage
{
    public GameObject backButton;
    protected override void Start()
    {
        titlePage = this;
        prevPages.Add(this);
        menuPages.Add(gameObject);
        back = backButton;
        backButton.SetActive(false);
        gameObject.SetActive(true);
    }

}
