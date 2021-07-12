using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    private Animator _animator;
    
    private static readonly int Appear1 = Animator.StringToHash("appear");
    
    private static readonly int Disappear1 = Animator.StringToHash("disappear");

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Appear()
    {
        _animator.SetTrigger(Appear1);
    }
    
    public void Disappear()
    {
        _animator.SetTrigger(Disappear1);
    }
}
