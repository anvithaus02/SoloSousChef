using System.Collections.Generic;
using System.Linq;
using com.SoloSousChef.UI.Managers;
using UnityEngine;
namespace com.SoloSousChef.UI.Screens
{
    public class BaseScreenManager : MonoBehaviour
    {
        public static BaseScreenManager Instance { get; private set; }

        [SerializeField] private List<BaseScreen> allScreens;
        private Dictionary<ScreenType, BaseScreen> _screenDictionary;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            _screenDictionary = allScreens.ToDictionary(s => s.screenType, s => s);
            InitializeUIState();
        }

        private void InitializeUIState()
        {
            foreach (var screen in allScreens)
            {
                screen.Hide(false);
            }

            ShowScreen(ScreenType.MainMenuScreen, false);
        }

        public void ShowScreen(ScreenType type, bool animate = true)
        {
            if (_screenDictionary.TryGetValue(type, out var screen))
                screen.Show(animate);
        }

        public void HideScreen(ScreenType type, bool animate = true)
        {
            if (_screenDictionary.TryGetValue(type, out var screen))
                screen.Hide(animate);
        }

        public void SwitchScreen(ScreenType hide, ScreenType show, bool animate = true)
        {
            HideScreen(hide, animate);
            ShowScreen(show, animate);
        }
    }
}