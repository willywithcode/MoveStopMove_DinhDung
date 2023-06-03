using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopupManager : Singleton<PopupManager>
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private GameUnit prefabTxtKillUI;
    [SerializeField] private Transform parentTxtKillUI;

    private string nameVictim;
    private string nameKiller;
    private float notificationDuration = 1f;
    private float scrollSpeed = 2f;

    public string NameVictim { get { return nameVictim; }  set { nameVictim = value; } }
    public string NameKiller { get { return nameKiller; } set { nameKiller = value; } }

    private Coroutine scrollCoroutine;

    private void Start()
    {
        KillManager.Instance.OnKillEvent += ShowKillNotification;
        SimplePool.Preload(prefabTxtKillUI, 10, parentTxtKillUI, true, false);
    }

    private void ShowKillNotification()
    {
        TxtKillUI notification = SimplePool.Spawn<TxtKillUI>(prefabTxtKillUI);
        notification.OnInit();
        notification.TxtContent = this.nameKiller + Constant.killAlermMessage + this.nameVictim;
        StartCoroutine(RemoveNotification(notification));
        if (scrollCoroutine != null)
        {
            StopCoroutine(scrollCoroutine);
        }
        scrollCoroutine = StartCoroutine(ScrollToBottom());
    }

    private IEnumerator RemoveNotification(GameUnit notification)
    {
        yield return new WaitForSeconds(notificationDuration);

        notification.OnDespawn();
    }

    private IEnumerator ScrollToBottom()
    {
        yield return new WaitForEndOfFrame();

        float targetPos = 0f;
        while (scrollRect.verticalNormalizedPosition > targetPos)
        {
            scrollRect.verticalNormalizedPosition -= Time.deltaTime * scrollSpeed;
            yield return null;
        }
        scrollCoroutine = null;
    }
}