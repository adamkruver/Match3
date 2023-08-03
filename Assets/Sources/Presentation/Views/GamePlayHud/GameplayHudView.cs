using Match3.Presentation.Assets.Sources.Presentation.Views.Players;
using UnityEngine;

namespace Match3.Presentation.Assets.Sources.Presentation.Views.GamePlayHud
{
    public class GameplayHudView : MonoBehaviour
    {
        [field: SerializeField] public PlayerView LeftPlayer { get; private set; }
        [field: SerializeField] public PlayerView RightPlayer { get; private set; }
    }
}
