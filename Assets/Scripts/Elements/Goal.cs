using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

public class Goal : MonoBehaviour
{
    [SerializeField] private Transform nextLevelStartPosition;
    [SerializeField] private bool isGoalReached;

    [Header("Camera Shake")] [SerializeField]
    private float duration = 0.5f;

    [SerializeField] private float strength = 0.5f;
    [SerializeField] private float randomness = 90f;
    [SerializeField] private int vibrato = 10;
    public bool isLastCheckpoint;

    [Inject] private Player player;
    [Inject] private Camera mainCamera;

    public bool IsPlayerMoved => isPlayerMoved;
    private bool isPlayerMoved = false;


    private void OnTriggerEnter(Collider other)
    {
        if (!isLastCheckpoint)
        {
            if (isGoalReached)
            {
                return;
            }

            isGoalReached = true;

            StartCoroutine(MovePlayerToNextLevel());    
        }
        else
        {
            Invoke(nameof(Result),6f);
        }
        
    }

    void Result()
    {
        SceneManager.LoadScene(0);
    }
    
    private IEnumerator MovePlayerToNextLevel()
    {
        mainCamera.transform.DOShakePosition(duration: duration, strength: strength, vibrato: vibrato,
            randomness: randomness);
        mainCamera.transform.DOShakeRotation(duration, strength, vibrato, randomness);
        yield return new WaitForSeconds(3f);
        player.transform.position = nextLevelStartPosition.position;
        yield return new WaitForSeconds(1f);
        isPlayerMoved = true;
    }
}
