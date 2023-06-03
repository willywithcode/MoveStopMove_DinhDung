using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopupManager : Singleton<PopupManager>
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private GameUnit prefabTxtKillUI;
    [SerializeField] private Transform parentTxtKillUI;

    private float notificationDuration = 1f;
    private float scrollSpeed = 2f;

    private Coroutine scrollCoroutine;

    private void Start()
    {
        KillManager.Instance.OnKillEvent += ShowKillNotification;
        SimplePool.Preload(prefabTxtKillUI, 10, parentTxtKillUI, true, false);
    }

    private void ShowKillNotification(OriginWeapon weapon)
    {
        TxtKillUI notification = SimplePool.Spawn<TxtKillUI>(prefabTxtKillUI);
        notification.OnInit();
        notification.TxtContent = weapon.owner.namePlayer + Constant.killAlermMessage + weapon.victim.namePlayer;
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