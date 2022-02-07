using UnityEngine;
using BoxScripts;

public class LookItem : MonoBehaviour
{
    private Transform _targetItem;
    private bool _onlyHorizontal;
    private bool _isActing;
    public ControllerData  _controller;

    public float DistanceToCamera = 0.5f;
    [SerializeField] private float zoomedDistanceToCamera = 0.25f;
    [SerializeField] private float zoomSpeed = 0.05f;
    private Vector3 zoomedVector = Vector3.zero;
    private float accumulatedZoomedFloat = 0;
    private Vector3 destination;

    public void SetItem(Transform tr, bool onlyHorizontal = false)
    {
        _targetItem = tr; _onlyHorizontal = onlyHorizontal; _isActing = true;
        destination = GameController.instance.gco.camera.transform.position +
                        (GameController.instance.gco.camera.transform.forward * DistanceToCamera);
    }

    private void Update() {
        if(!_isActing) return;

        if(_controller.Axis.x != 0)
        {
            float zoomDirection = 0f;
            Vector3 cameraPosition = GameController.instance.gco.camera.transform.position;
            Vector3 cameraForward = GameController.instance.gco.camera.transform.forward;
            
            if(_controller.Axis.x > 0 && accumulatedZoomedFloat < zoomedDistanceToCamera)
                zoomDirection = - zoomSpeed * Time.deltaTime;
            else if (_controller.Axis.x < 0 && accumulatedZoomedFloat > -zoomedDistanceToCamera)
                zoomDirection = zoomSpeed * Time.deltaTime;

            accumulatedZoomedFloat -= zoomDirection;
            zoomedVector += (cameraForward.normalized * zoomDirection);
            _targetItem.position = transform.position + zoomedVector;
        }
        if(_controller.isInput2Hold)
        {
            _targetItem.Rotate(Vector3.up, -_controller.CameraAxis.x);
            if(! _onlyHorizontal) _targetItem.Rotate(Vector3.right, -_controller.CameraAxis.y);
        }

        if(_controller.isEscapePressed) _targetItem.GetComponent<ActionBase>().Remove();
    }
}