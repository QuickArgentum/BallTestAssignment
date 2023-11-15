using Data;
using DataHolder;
using DG.Tweening;
using Input;
using UI;
using View;

namespace Handlers
{
    /// <summary>
    /// Responsible for triggering all the transition animations in the game
    /// </summary>
    public class GameTransitionHandler
    {
        private readonly GameStateHolder _gameStateHolder;
        private readonly FadePanelUI _fadePanelUI;
        private readonly GameFacade _facade;
        private readonly CameraView _cameraView;
        private readonly GameOverPanelUI _gameOverPanelUI;
        private readonly ITouchManager _touchManager;

        public GameTransitionHandler(GameStateHolder gameStateHolder, FadePanelUI fadePanelUI, GameFacade facade,
            CameraView cameraView, GameOverPanelUI gameOverPanelUI, ITouchManager touchManager)
        {
            _gameStateHolder = gameStateHolder;
            _fadePanelUI = fadePanelUI;
            _facade = facade;
            _cameraView = cameraView;
            _gameOverPanelUI = gameOverPanelUI;
            _touchManager = touchManager;
            _gameStateHolder.OnGameStateUpdated += OnGameStateUpdated;
        }

        private void OnGameStateUpdated(GameState value)
        {
            switch (value)
            {
                case GameState.FadingIn:
                    _fadePanelUI.SetEnabled(true);
                    DOTween.Sequence()
                        .Join(_fadePanelUI.CreateFadeOutTween())
                        .Join(_cameraView.CreateIntroTween())
                        .OnComplete(() =>
                        {
                            _gameStateHolder.GameState = GameState.Playing;
                            _fadePanelUI.SetEnabled(false);
                        })
                        .Play();
                    break;
                
                case GameState.Playing:
                    break;
                
                case GameState.GameOverScreenShowing:
                    _gameOverPanelUI.SetTitle(_gameStateHolder.PlayerEnergy <= 0 ? Strings.GameOver : Strings.Victory);
                    _gameOverPanelUI.SetEnabled(true);
                    _gameOverPanelUI.CreateFadeInTween()
                        .OnComplete(() => _gameStateHolder.GameState = GameState.GameOver)
                        .Play();
                    break;
                
                case GameState.GameOver:
                    void OnTouch()
                    {
                        _touchManager.OnTapEnd -= OnTouch;
                        _gameOverPanelUI.CreateFadeOutTween()
                            .OnComplete(() => _gameStateHolder.GameState = GameState.FadingOut)
                            .Play();
                    }
                    _touchManager.OnTapEnd += OnTouch;
                    break;
                
                case GameState.FadingOut:
                    _fadePanelUI.SetEnabled(true);
                    DOTween.Sequence()
                        .Join(_fadePanelUI.CreateFadeInTween())
                        .Join(_cameraView.CreateOutroTween())
                        .OnComplete(() => _facade.EndGame())
                        .Play();
                    break;
            }
        }
    }
}