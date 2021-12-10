using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] private GameObject menuScreen;
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject lostScreen;

    public void OpenMenu()
    {
        menuScreen.SetActive(true);
        gameScreen.SetActive(false);
        winScreen.SetActive(false);
        lostScreen.SetActive(false);
    }

    public void OpenGame()
    {
        menuScreen.SetActive(false);
        gameScreen.SetActive(true);
        winScreen.SetActive(false);
        lostScreen.SetActive(false);
    }

    public void OpenWin()
    {
        menuScreen.SetActive(false);
        gameScreen.SetActive(false);
        winScreen.SetActive(true);
        lostScreen.SetActive(false);
    }

    public void OpenLost()
    {
        menuScreen.SetActive(false);
        gameScreen.SetActive(false);
        winScreen.SetActive(false);
        lostScreen.SetActive(true);
    }

}
