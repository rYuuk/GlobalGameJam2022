using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject creditPanel;

    [SerializeField] private Button continueButton;
    [SerializeField] private Button startButton;
    [SerializeField] private Button creditButton;
    [SerializeField] private Button quitButton;

    [Header("Pause")] 
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button backToMenuButton;
    [SerializeField] private GameObject pausePanel;

    [Inject] private GameManager gameManager;

    private void OnEnable()
    {
        startButton.onClick.AddListener(StartGame);
        continueButton.onClick.AddListener(Continue);
        creditButton.onClick.AddListener(Credit);
        quitButton.onClick.AddListener(Quit);

        resumeButton.onClick.AddListener(Resume);
        backToMenuButton.onClick.AddListener(BackToMenu);
        gameManager.Paused += Pause;
    }

    private void OnDisable()
    {
        startButton.onClick.RemoveListener(StartGame);
        continueButton.onClick.RemoveListener(Continue);
        creditButton.onClick.RemoveListener(Credit);
        quitButton.onClick.RemoveListener(Quit);
        gameManager.Paused -= Pause;
    }

    private void Pause()
    {
        if (creditPanel.activeSelf)
        {
            creditPanel.SetActive(false);
            menuPanel.SetActive(true);
        }
        else
        {
            pausePanel.SetActive(true);
        }
    }

    private void BackToMenu()
    {
        pausePanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    private void Resume()
    {
        pausePanel.SetActive(false);
    }


    private void StartGame()
    {
        menuPanel.gameObject.SetActive(false);
    }

    private void Continue()
    {
        menuPanel.gameObject.SetActive(false);
    }

    private void Credit()
    {
        creditPanel.gameObject.SetActive(true);
    }

    private void Quit()
    {
        Application.Quit();
    }
}