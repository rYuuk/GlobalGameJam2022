using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Exit : MonoBehaviour
{
    public Image fadePanel;
    public bool isPlayerInside;

    private void Update()
    {
        if (fadePanel.color.a >=0.99 && isPlayerInside)
        {
            Application.Quit();
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fadePanel.DOFade(1,3f);
            isPlayerInside = true;            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        fadePanel.DOKill(true);
        fadePanel.DOFade(0, 1f);
        isPlayerInside = false;
    }
}
