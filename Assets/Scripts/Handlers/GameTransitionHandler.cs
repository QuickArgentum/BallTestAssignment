using Const;
using DataHolder;
using DG.Tweening;
using UI;
using View;

namespace Handlers
{
    public class GameTransitionHandler
    {
        private readonly GameStateHolder _gameStateHolder;
        private readonly FadePanelUI _fadePanelUI;
        private readonly GameFacade _facade;
        private readonly CameraView _cameraView;

        public GameTransitionHandler(GameStateHolder gameStateHolder, FadePanelUI fadePanelUI, GameFacade facade,
            CameraView cameraView)
        {
            _gameStateHolder = gameStateHolder;
            _fadePanelUI = fadePanelUI;
            _facade = facade;
            _cameraView = cameraView;
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
                    break;
                
                case GameState.GameOver:
                    break;
                
                case GameState.FadingOut:
                    _fadePanelUI.SetEnabled(true);
                    DOTween.Sequence()
                        .Join(_fadePanelUI.CreateFadeInTween())
                        .Join(_cameraView.CreateOutroTween())
                        .OnComplete(() => _facade.RestartGame())
                        .Play();
                    break;
            }
        }
    }
}