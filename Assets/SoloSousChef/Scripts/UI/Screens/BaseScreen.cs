using UnityEngine;
using DG.Tweening;
using com.SoloSousChef.UI.Managers;

namespace com.SoloSousChef.UI.Screens
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class BaseScreen : MonoBehaviour
    {
        public ScreenType screenType;
        protected CanvasGroup canvasGroup;

        protected virtual void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public virtual void Show(bool animate = true)
        {
            gameObject.SetActive(true);
            if (animate)
            {
                canvasGroup.alpha = 0;
                canvasGroup.DOFade(1f, 0.3f).SetUpdate(true);
            }
            else
            {
                canvasGroup.alpha = 1f;
            }
        }

        public virtual void Hide(bool animate = true)
        {
            if (animate)
            {
                canvasGroup.DOFade(0f, 0.3f).SetUpdate(true).OnComplete(() => gameObject.SetActive(false));
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}