using System.Collections;
using UnityEngine;
using VContainer;

public class Goal : MonoBehaviour
{

    [SerializeField] private Transform nextLevelStartPosition;
    [Inject] private Player player;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(MovePlayerToNextLevel());
    }

    private IEnumerator MovePlayerToNextLevel()
    {
        yield return new WaitForSeconds(3f);
        player.transform.position = nextLevelStartPosition.position;
    }
}
