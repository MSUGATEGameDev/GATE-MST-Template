using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public MenuPage titlePage;
    public GameObject backButton;
    private void Start()
    {
        MenuPage.titlePage = titlePage;
        MenuPage.back = backButton;
        MenuPage.prevPages.Add(titlePage);
        MenuPage.back.SetActive(false);
        MenuPage.titlePage.gameObject.SetActive(true);
    }
}
