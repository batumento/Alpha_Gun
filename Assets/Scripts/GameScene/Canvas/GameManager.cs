using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public RectTransform gameOverPanel;
    public RectTransform pausePanel;
    public Button pauseButton;
    [Header("Model")]
    public GameObject model;
    [Header("Panels")]
    public GameObject envanterPanel;
    public GameObject magazaPanel;
    public GameObject beforeGamePanel;
    public GameObject settingsPanel;
    public GameObject levelPanel;
    public void PauseButton()
    {
            pausePanel.gameObject.SetActive(true);
            pausePanel.DOScale(1, 0.5f).SetEase(Ease.OutBack).OnComplete(()=> Time.timeScale = 0);
            pauseButton.interactable = false;
    }
    public void ContinueButton()
    {
        Time.timeScale = 1;
        pausePanel.transform.DOScale(0, 0.5f).OnComplete(() =>
        pausePanel.gameObject.SetActive(false));
        pauseButton.interactable = true;
    }
    public void AgainButton()
    {
        Time.timeScale = 1;
        pausePanel.transform.DOScale(0, 0.5f).OnComplete(() =>
        pausePanel.gameObject.SetActive(false));
        SceneManager.LoadScene(0);
    }
    public void MenuButton()
    {
        Time.timeScale = 1;
        pausePanel.transform.DOScale(0, 0.5f).OnComplete(() =>
        pausePanel.gameObject.SetActive(false));
        SceneManager.LoadScene(0);
    }

    public void GameOverPanel()
    {
        gameOverPanel.gameObject.SetActive(true);
        gameOverPanel.DOScale(1, 0.5f).SetEase(Ease.OutBack).OnComplete(() => Time.timeScale = 0);
    }
    public void LAgainButton()
    {
        Time.timeScale = 1;
        gameOverPanel.transform.DOScale(0, 0.5f).OnComplete(() =>
        gameOverPanel.gameObject.SetActive(false));
        SceneManager.LoadScene(0);
    }
    public void EnvanterButton()
    {
        envanterPanel.SetActive(true);
        model.SetActive(true);
        beforeGamePanel.SetActive(false);
        levelPanel.SetActive(false);
    }

    public void EnvanterExit()
    {
        envanterPanel.SetActive(false);
        model.SetActive(false);
        beforeGamePanel.SetActive(true);
        levelPanel.SetActive(true);
    }

    public void MagazaPanel()
    {
        magazaPanel.SetActive(true);
        beforeGamePanel.SetActive(false);
        levelPanel.SetActive(false);
    }

    public void MagazaExit()
    {
        magazaPanel.SetActive(false);
        beforeGamePanel.SetActive(true);
        levelPanel.SetActive(true);
    }

    public void SettingsPanel()
    {
        if (settingsPanel.activeSelf)
        {
            settingsPanel.SetActive(false);
        }
        else
        {
            settingsPanel.SetActive(true);
        }
    }
}
