using UnityEngine;

public class UI_Controller: MonoBehaviour
{
    [SerializeField] private GameObject menuScreen;
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    public void OpenMenu()
    {
        menuScreen.SetActive(true);
        gameScreen.SetActive(false);
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }
    public void OpenGame()
    {
        menuScreen.SetActive(false);
        gameScreen.SetActive(true);
        winScreen.SetActive(false);
        loseScreen.SetActive(false);

    }
    public void OpenWin()
    {
        menuScreen.SetActive(false);
        gameScreen.SetActive(false);
        winScreen.SetActive(true);
        loseScreen.SetActive(false);

    }
    public void OpenLose()
    {
        menuScreen.SetActive(false);
        gameScreen.SetActive(false);
        winScreen.SetActive(false);
        loseScreen.SetActive(true);

    }
}
