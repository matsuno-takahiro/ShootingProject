using DG.Tweening;
using UnityEngine;

public class TimedEventListener : MonoBehaviour
{
    public float eventInterval = 5.0f; // イベントが発生する間隔（秒）
    private float timeSinceLastEvent = 0.0f;

    void Update()
    {
        timeSinceLastEvent += Time.deltaTime;

        if (timeSinceLastEvent >= eventInterval)
        {
            TriggerEvent();
            timeSinceLastEvent = 0.0f;
        }
    }

    private void TriggerEvent()
    {
        Debug.Log("イベントが発生しました！");
        // ここにイベント発生時の処理を追加
        
            transform.DOLocalMove(new Vector3(100f, 0, 0), 1f)
            .SetLoops(-1, LoopType.Yoyo)
          　.SetEase(Ease.OutQuad);
    }

    [ContextMenu("Manual Trigger Event")]
    public void ManualTriggerEvent()
    {
        TriggerEvent();
    }
}
