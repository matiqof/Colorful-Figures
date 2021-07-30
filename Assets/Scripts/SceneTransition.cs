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
        //Анимация исчезания сцены
        
        _animator.SetTrigger(Appear1);
    }
    
    public void Disappear()
    {
        //Анимация появления сцены
        
        _animator.SetTrigger(Disappear1);
    }
}
