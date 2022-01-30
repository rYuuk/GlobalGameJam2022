using UnityEngine;

public class LogoPanel : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Prism prism;
    
    private static readonly int PopUp = Animator.StringToHash("PopUp");

    private void Update()
    {
        if (prism.IsTriggered)
        {
            animator.SetTrigger(PopUp);
        }
    }
}
