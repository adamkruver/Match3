using UnityEngine;

namespace Match3.Presentation.Sources.Presentation.Services
{
    public class CameraScaleService : MonoBehaviour
    {
        [SerializeField] private Canvas _horizontalCanvas;
        [SerializeField] private Canvas _verticalCanvas;

        private readonly float _baseSize = 6;

        private Vector2Int _currentScreenSize = Vector2Int.zero;
        private GameObject _verticalCanvasGameObject;
        private GameObject _horizontalCanvasGameObject;
        private Camera _camera;

        private Vector2Int ScreenSize => new Vector2Int(Screen.width, Screen.height);
        private bool IsHorizontalOrientation => _currentScreenSize.x > _currentScreenSize.y;

        private void Awake()
        {
            _camera = Camera.main;

            _horizontalCanvasGameObject = _horizontalCanvas.gameObject;
            _verticalCanvasGameObject = _verticalCanvas.gameObject;
        }

        private void LateUpdate()
        {
            Vector2Int screenSize = ScreenSize;

            if (screenSize == _currentScreenSize)
                return;

            _currentScreenSize = screenSize;

            if (IsHorizontalOrientation)
                UpdateHorizontalCanvas();
            else
                UpdateVerticalCanvas();
        }

        private void UpdateHorizontalCanvas()
        {
            _camera.orthographicSize = _baseSize;
            
            if (_horizontalCanvasGameObject.activeSelf)
                return;

            _verticalCanvasGameObject.SetActive(false);
            _horizontalCanvasGameObject.SetActive(true);
        }

        private void UpdateVerticalCanvas()
        {
            _camera.orthographicSize = _baseSize * _currentScreenSize.y / _currentScreenSize.x;
            
            if (_verticalCanvasGameObject.activeSelf)
                return;

            _horizontalCanvasGameObject.SetActive(false);
            _verticalCanvasGameObject.SetActive(true);
        }
    }
}