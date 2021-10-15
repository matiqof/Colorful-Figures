using UnityEngine;
using TMPro;
using System.Collections;

public class Begin : MonoBehaviour
{
    [SerializeField] private Obstacles obstacles;

    [SerializeField] private Score score;

    [SerializeField] private Player player;

    [SerializeField] private TextMeshProUGUI timer;

    [SerializeField] private float count = 3;

    private float _time = 1f;

    private bool _isBegin = false;

    private void Start() {
        obstacles.UpdateMoveSpeed(0);

        score.SetTimerWork(false);
    }

    public void StartGame() {

        StartCoroutine(TimerTick(_time));
    }

    private IEnumerator TimerTick(float time) {

        while (count >= 0) {
            yield return new WaitForSeconds(_time);

            count--;

            timer.SetText(count.ToString());
        }

        timer.enabled = false;

        obstacles.UpdateMoveSpeed(obstacles.GETMoveSpeed());

        score.SetTimerWork(true);

        player.enabled = true;

        _isBegin = true;
    }

    public bool GETBegin() {
        return _isBegin;
    }
}
