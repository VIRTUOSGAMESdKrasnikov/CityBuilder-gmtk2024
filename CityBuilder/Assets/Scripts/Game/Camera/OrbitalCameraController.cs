using UnityEngine;

namespace CityBuilder.Game.Camera
{
    public class OrbitalCameraController : ICameraController
    {
        private Transform _orbitalRig;
        private Transform _camTrans;

        // TODO: Move to RunTimeDataProvider 
        private float _movementKeyboardSpeed = 20f;
        private float _movementMouseSensitivity = .3f;
        private float _movementSmoothness = 50f;

        private float _rotationKeyboardSpeed = 70f;
        private float _rotationMouseSensitivity = .15f;
        private float _rotationSmoothness = 30f;

        private float _zoomSmoothness = 50f;
        private float _maxZoomSize = 30f;
        private float _minZoomSize = 5f;
        private float _zoomStepsCount = 20f;
        private float _zoomStep;

        private Vector3 _newPos;
        private float _yRotation;
        private float _newZoom;

        private Vector3 _dragCurrentPos, _dragStartPos;
        private Vector3 _rotateCurrentScreenPos, _rotateStartScreenPos;

        private UnityEngine.Camera _cam;

        public OrbitalCameraController(Transform rig, Transform camTrans)
        {
            _orbitalRig = rig;
            _camTrans = camTrans;
            _cam = UnityEngine.Camera.main;
        }

        public void Init()
        {
            _newPos = _orbitalRig.position;
            _yRotation = _orbitalRig.rotation.eulerAngles.y;
            _newZoom = _maxZoomSize;
            _zoomStep = (_maxZoomSize - _minZoomSize) / _zoomStepsCount;
        }

        public void Dispose()
        {
        }

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (GetPlaneRay(out var ray).Raycast(ray, out var entry))
                    _dragStartPos = ray.GetPoint(entry);
            }

            if (Input.GetMouseButtonDown(1))
                _rotateStartScreenPos = Input.mousePosition;

            if (Input.GetMouseButton(0))
                HandleMouseBasedMovement();

            if (Input.GetMouseButton(1))
                HandleMouseBasedRotation();

            var rotationInput = 0f;
            if (Input.GetKey(KeyCode.Q))
                rotationInput = -1f;
            if (Input.GetKey(KeyCode.E))
                rotationInput = 1f;

            var zoomInput = 0f;
            var scrollWheel = Input.GetAxisRaw("Mouse ScrollWheel");
            if (scrollWheel > 0f)
                zoomInput = -1f;
            else if (scrollWheel < 0f)
                zoomInput = 1f;

            if (Input.GetKey(KeyCode.R))
                zoomInput = -1f;
            if (Input.GetKey(KeyCode.F))
                zoomInput = 1f;

            var moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            HandleKeyboardBasedMovement(moveInput);
            HandleKeyboardBasedRotation(rotationInput);

            HandleZoom(zoomInput);
            if (Input.GetKeyDown(KeyCode.M))
                SetZoomCenter();

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
                _newPos += (_dragStartPos - _dragCurrentPos) * _movementMouseSensitivity;
            }
        }

        private void HandleKeyboardBasedMovement(Vector2 input)
        {
            var deltaSpeed = _movementKeyboardSpeed * Time.deltaTime;

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

            _yRotation += -diffX * _rotationMouseSensitivity;
        }

        private void HandleKeyboardBasedRotation(float yRotationInput)
        {
            var yDeltaRotation = yRotationInput * _rotationKeyboardSpeed * Time.deltaTime;
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
            _newZoom = Mathf.Clamp(zoomDelta + worldZoomValue, _minZoomSize, _maxZoomSize);
        }

        private void SetZoomCenter()
        {
            _newZoom = _maxZoomSize;
            _newPos = new Vector3(0, _orbitalRig.localPosition.y, 0);
            _yRotation = 0f;
        }

        #endregion

        #region Applying

        private void ApplyPosition()
        {
            _orbitalRig.position = Vector3.Lerp(_orbitalRig.position, _newPos,
                Time.deltaTime * _movementSmoothness);
        }

        private void ApplyZoom()
        {
            var zoomVector = new Vector3(0f, 0f, -_newZoom);
            _camTrans.transform.localPosition = Vector3.Lerp(_camTrans.transform.localPosition, zoomVector,
                Time.deltaTime * _zoomSmoothness);
        }

        // TODO: x should not be magic number.
        private void ApplyRotation()
        {
            var newRotation = Quaternion.Euler(45f, _yRotation, 0f);
            _orbitalRig.rotation =
                Quaternion.Slerp(_orbitalRig.rotation, newRotation, Time.deltaTime * _rotationSmoothness);
        }

        #endregion

        private Plane GetPlaneRay(out Ray ray)
        {
            var plane = new Plane(Vector3.up, Vector3.zero);
            ray = _cam.ScreenPointToRay(Input.mousePosition);
            return plane;
        }
    }
}