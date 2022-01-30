using UnityEngine;

public class LogoPanel : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Goal goal;
    
    private static readonly int PopUp = Animator.StringToHash("PopUp");

    private void Update()
    {
        if (goal.IsPlayerMoved)
        {
            animator.SetTrigger(PopUp);
        }
    }
}
