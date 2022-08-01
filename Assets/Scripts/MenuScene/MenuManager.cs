using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public RectTransform playButton;
    public RectTransform soundButton;
    public RectTransform exitButton;
    public Material menuPlayerColor;
    public GameObject[] menuPlayer;

    public float rotateSpeed;

    private void Start()
    {
        menuPlayerColor.color = Color.white;
    }
    private void Update()
    {
        foreach (GameObject humanoids in menuPlayer)
        {
            Vector3 humanRotate = new Vector3(0, rotateSpeed, 0);
            humanoids.transform.Rotate(humanRotate * Time.deltaTime);
        }
    }
    public void PlayButton()
    {
        menuPlayerColor.color = Color.green;
        playButton.DOScale(1.5f, 0.3f).OnComplete(() =>
        playButton.DOScale(0,1).OnComplete(()=> SceneManager.LoadScene(1)));
    }
    public void ExitButton()
    {

        Application.Quit();
    }
    public void SoundButton()
    {
        menuPlayerColor.color = new Color32(255, 125, 25,1);
        soundButton.DOScale(1.5f, 0.3f).OnComplete(() =>
        soundButton.DOScale(0, 1).OnComplete(() => soundButton.DOScale(1, 0.5f)));
        //Ses aç/kapat sonra yapılacak
    }
}
