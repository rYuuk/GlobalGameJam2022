using System;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject creditPanel;

    [SerializeField] private Button continueButton;
    [SerializeField] private Button startButton;
    [SerializeField] private Button creditButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private Button backButton;

    private void OnEnable()
    {
        startButton.onClick.AddListener(StartGame);
        continueButton.onClick.AddListener(Continue);
        creditButton.onClick.AddListener(Credit);
        quitButton.onClick.AddListener(Quit);
        backButton.onClick.AddListener(Back);
    }

    private void OnDisable()
    {
        startButton.onClick.RemoveListener(StartGame);
        continueButton.onClick.RemoveListener(Continue);
        creditButton.onClick.RemoveListener(Credit);
        quitButton.onClick.RemoveListener(Quit);
        backButton.onClick.RemoveListener(Back);
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

    private void Back()
    {
        creditPanel.SetActive(false);
    }
}