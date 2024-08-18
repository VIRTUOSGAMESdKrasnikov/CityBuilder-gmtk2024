using CityBuilder.DataStorage;
using UnityEngine;

namespace CityBuilder.Game.Camera
{
    public class OrbitalCameraController : ICameraController
    {
        private Transform _orbitalRig;
        private Transform _camTrans;
        private readonly OrbitalCameraStorage _orbitalCameraDataStorage;

        private float _zoomStep;

        private Vector3 _newPos;
        private float _yRotation;
        private float _newZoom;

        private Vector3 _dragCurrentPos, _dragStartPos;
        private Vector3 _rotateCurrentScreenPos, _rotateStartScreenPos;

        private UnityEngine.Camera _cam;

        public OrbitalCameraController(Transform rig, Transform camTrans, OrbitalCameraStorage orbitalCameraDataStorage)
        {
            _orbitalRig = rig;
            _camTrans = camTrans;
            _orbitalCameraDataStorage = orbitalCameraDataStorage;
            _cam = UnityEngine.Camera.main;
        }

        public void Init()
        {
            _newPos = _orbitalRig.position;
            _yRotation = _orbitalRig.rotation.eulerAngles.y;
            _newZoom = _orbitalCameraDataStorage.MaxZoomSize;
            _zoomStep = (_orbitalCameraDataStorage.MaxZoomSize - _orbitalCameraDataStorage.MinZoomSize) /
                        _orbitalCameraDataStorage.ZoomStepsCount;
        }

        public void Dispose()
        {
        }

        public void Tick()
        {
            // Setup start positions for mouse based functions.
            if (Input.GetMouseButtonDown(0))
            {
                if (GetPlaneRay(out var ray).Raycast(ray, out var entry))
                    _dragStartPos = ray.GetPoint(entry);
            }

            if (Input.GetMouseButtonDown(1))
                _rotateStartScreenPos = Input.mousePosition;

            // Handling functions.
            GetInputs(out var moveInput, out var yRotationInput, out var zoomInput);
            if (Input.GetMouseButton(0))
                HandleMouseBasedMovement();
            else
                HandleKeyboardBasedMovement(moveInput);
            if (Input.GetMouseButton(1))
                HandleMouseBasedRotation();
            else
                HandleKeyboardBasedRotation(yRotationInput);
            HandleZoom(zoomInput);

            if (Input.GetKeyDown(KeyCode.M))
                MaximizeZoom();

            ApplyPosition();
            ApplyRotation();
            ApplyZoom();
        }

        #region Movement

        private void HandleMouseBasedMovement()
        {
            if (GetPlaneRay(out var ray).Raycast(ray, out var entry))
            {
                _dragCurrentPos = ray.GetPoint(entry);
                _newPos += (_dragStartPos - _dragCurrentPos) * _orbitalCameraDataStorage.MovementMouseSensitivity;
            }
        }

        private void HandleKeyboardBasedMovement(Vector2 input)
        {
            var deltaSpeed = _orbitalCameraDataStorage.MovementKeyboardSpeed * Time.deltaTime;

            var fwdDir = _orbitalRig.forward;
            fwdDir.y = 0f;
            var rightDir = _orbitalRig.right;
            rightDir.y = 0f;

            var forward = fwdDir.normalized * (input.y * deltaSpeed);
            var horizontal = rightDir.normalized * (input.x * deltaSpeed);

            _newPos += horizontal + forward;
        }

        #endregion

        #region Rotation

        private void HandleMouseBasedRotation()
        {
            _rotateCurrentScreenPos = Input.mousePosition;

            float diffX = _rotateStartScreenPos.x - _rotateCurrentScreenPos.x;
            _rotateStartScreenPos = _rotateCurrentScreenPos;

            _yRotation += -diffX * _orbitalCameraDataStorage.RotationMouseSensitivity;
        }

        private void HandleKeyboardBasedRotation(float yRotationInput)
        {
            var yDeltaRotation = yRotationInput * _orbitalCameraDataStorage.RotationKeyboardSpeed * Time.deltaTime;
            _yRotation += yDeltaRotation;
        }

        #endregion

        #region Zoom

        private void HandleZoom(float input)
        {
            if (Mathf.Abs(input) < 0.1f)
                return;

            float zoomDelta = 0f;
            if (input < 0f)
                zoomDelta = -_zoomStep;
            else if (input > 0f)
                zoomDelta = _zoomStep;

            var worldZoomValue = Mathf.Abs(_camTrans.transform.localPosition.z);
            _newZoom = Mathf.Clamp(zoomDelta + worldZoomValue, _orbitalCameraDataStorage.MinZoomSize,
                _orbitalCameraDataStorage.MaxZoomSize);
        }

        private void MaximizeZoom() => _newZoom = _orbitalCameraDataStorage.MaxZoomSize;

        #endregion

        #region Applying

        private void ApplyPosition()
        {
            _orbitalRig.position = Vector3.Lerp(_orbitalRig.position, _newPos,
                Time.deltaTime * _orbitalCameraDataStorage.MovementSmoothness);
        }

        private void ApplyZoom()
        {
            var zoomVector = new Vector3(0f, 0f, -_newZoom);
            _camTrans.transform.localPosition = Vector3.Lerp(_camTrans.transform.localPosition, zoomVector,
                Time.deltaTime * _orbitalCameraDataStorage.ZoomSmoothness);
        }

        private void ApplyRotation()
        {
            var newRotation = Quaternion.Euler(_orbitalCameraDataStorage.XRotation, _yRotation, 0f);
            _orbitalRig.rotation =
                Quaternion.Slerp(_orbitalRig.rotation, newRotation,
                    Time.deltaTime * _orbitalCameraDataStorage.RotationSmoothness);
        }

        #endregion

        // TODO: Yeah, in future it should be remade.
        private void GetInputs(out Vector2 moveInput, out float yRotationInput, out float zoomInput)
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            yRotationInput = 0f;
            if (Input.GetKey(KeyCode.Q))
                yRotationInput = -1f;
            else if (Input.GetKey(KeyCode.E))
                yRotationInput = 1f;

            zoomInput = 0f;
            var scrollWheel = Input.GetAxisRaw("Mouse ScrollWheel");
            if (scrollWheel > 0f)
                zoomInput = -1f;
            else if (scrollWheel < 0f)
                zoomInput = 1f;

            if (Input.GetKey(KeyCode.R))
                zoomInput = -1f;
            if (Input.GetKey(KeyCode.F))
                zoomInput = 1f;
        }

        private Plane GetPlaneRay(out Ray ray)
        {
            var plane = new Plane(Vector3.up, Vector3.zero);
            ray = _cam.ScreenPointToRay(Input.mousePosition);
            return plane;
        }
    }
}