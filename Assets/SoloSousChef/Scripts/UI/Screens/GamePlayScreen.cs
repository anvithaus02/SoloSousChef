using com.SoloSousChef.Manager;
using com.SoloSousChef.Order;
using com.SoloSousChef.UI.Components;
using UnityEngine;
namespace com.SoloSousChef.UI.Screens
{
    public class GamePlayScreen : BaseScreen
    {
        [SerializeField] private GamePlayHUD gamePlayHUD;

        public override void Show(bool animate = true)
        {
            base.Show(animate);
            Initialize();
        }

        private void Initialize()
        {
            ScoreManager.Instance.ResetScore();

            gamePlayHUD.Initialize();
            OrderManager.Instance.InitializeAllOrders();

        }
    }
}