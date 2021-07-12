using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pause : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Obstacles obstacles;
    
    [SerializeField] private Animator transitionAnimator;

    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private Score score;

    [SerializeField] private float time = 1f;

    [SerializeField] private DisplayScore displayScore;

    private bool _pause;

    private bool _inProgress;

    private float _lastTimeClick;

    private static readonly int Appear = Animator.StringToHash("appear");
    
    private static readonly int Disappear = Animator.StringToHash("disappear");

    public void OnPointerClick(PointerEventData eventData)
    {
        var currentTimeClick = eventData.clickTime;
        
        if(Mathf.Abs(currentTimeClick - _lastTimeClick) < 0.75f && !_inProgress) StartCoroutine(UpdatePauseMenu(true));
        
        _lastTimeClick = currentTimeClick;
    }
    
    public void ResumeGame()
    {
        StartCoroutine(UpdatePauseMenu(false));
    }

    private IEnumerator UpdatePauseMenu(bool pause)
    {
        if (pause)
        {
            _inProgress = true;
            
            displayScore.UpdatePauseScoreText();
            
            obstacles.UpdateMoveSpeed(0);

            score.SetTimerWork(false);
            
            transitionAnimator.SetTrigger(Appear);
        
            yield return new WaitForSeconds(time);

            pauseMenu.SetActive(true);

            transitionAnimator.SetTrigger(Disappear);

            _pause = true;
            
            _inProgress = false;
        } 
        else
        {
            _inProgress = true;
            
            transitionAnimator.SetTrigger(Appear);

            yield return new WaitForSeconds(time);

            pauseMenu.SetActive(false);

            transitionAnimator.SetTrigger(Disappear);

            yield return new WaitForSeconds(time);
            
            obstacles.UpdateMoveSpeed(obstacles.GETMoveSpeed());

            score.SetTimerWork(true);

            _pause = false;
            
            _inProgress = false;
            
        }
    }

    public bool GETPause()
    {
        return _pause;
    }
}
