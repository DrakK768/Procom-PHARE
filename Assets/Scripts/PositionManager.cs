using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PositionManager : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float pinchSpeed = 1f;
    [SerializeField] float rotationSpeed = 1f;

    bool twoFingers = false;
    float lastPosDst;
    float lastAngle;
    ImageTracker imTracker;
    //TODO: Double tap / long press (1s) to reset pos to center of screen

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (twoFingers) return;
        if (Touchscreen.current.touches.Count(t => t.isInProgress) != 1) return;
        if (imTracker.currentInstance == null) return;

        if (ctx.performed)
        {
            Transform camT = imTracker.arCamera.transform;
            Transform instanceT = imTracker.currentInstance.transform;
            Vector2 delta = ctx.ReadValue<Vector2>();// * moveSpeed / 100;

            Vector3 normal = (instanceT.position - camT.position).normalized;
            Vector3 right = Vector3.Cross(instanceT.up, normal).normalized;
            Vector3 forward = Vector3.Cross(normal, right).normalized;

            Vector3 movementDirection = (forward * delta.x + right * -delta.y).normalized;

            // Rotate position around the sphere’s center
            float moveDistance = moveSpeed * Time.deltaTime;
            Debug.Log($"Positionn | Moving: {moveDistance}");
            Debug.Log($"Positionn | Moving: {movementDirection}");
            instanceT.position = Quaternion.AngleAxis(moveDistance, movementDirection) * (instanceT.position - camT.position) + camT.position;
            imTracker.currentInstance.transform.position = instanceT.position;
        }
    }

    public void OnPinch(InputAction.CallbackContext ctx)
    {
        if (Touchscreen.current == null || Touchscreen.current.touches.Count(t => t.isInProgress) < 2) return;
        if (imTracker.currentInstance == null) return;

        if (ctx.phase == InputActionPhase.Performed)
        {
            var touches = Touchscreen.current.touches;

            if (touches.Count < 2) return;

            float posDst = math.distance(touches[0].position.ReadValue(), touches[1].position.ReadValue());
            if (!twoFingers || lastPosDst == -Mathf.Infinity)
            {
                Debug.Log("Positionn | Pinching first time");
                lastPosDst = math.distance(touches[0].startPosition.ReadValue(), touches[1].startPosition.ReadValue());
                twoFingers = true;
            }

            float distance = (lastPosDst - posDst) * Time.deltaTime * pinchSpeed / 100;

            Debug.Log($"Positionn | Pinching: {distance}");
            if (Mathf.Abs(distance) > 0.001f)
            {
                lastPosDst = posDst;

                imTracker.currentInstance.transform.position += imTracker.arCamera.transform.forward * distance;
            }
        }
    }

    public void OnRotate(InputAction.CallbackContext ctx)
    {
        if (Touchscreen.current == null || Touchscreen.current.touches.Count(t => t.isInProgress) < 2) return;
        if (imTracker.currentInstance == null) return;

        if (ctx.phase == InputActionPhase.Performed)
        {
            var touches = Touchscreen.current.touches;

            if (touches.Count < 2) return;

            Vector2 touch0 = touches[0].position.ReadValue();
            Vector2 touch1 = touches[1].position.ReadValue();

            float angle = Mathf.Atan2(touch1.y - touch0.y, touch1.x - touch0.x) * Mathf.Rad2Deg;
            if (lastAngle == -Mathf.Infinity)
            {
                Debug.Log("Positionn | Rotating first time");
                lastAngle = angle;
                twoFingers = true;
            }

            float angleDelta = (angle - lastAngle) * rotationSpeed * Time.deltaTime;
            lastAngle = angle;
            Debug.Log($"Positionn | Rotating: {angleDelta}");

            imTracker.currentInstance.transform.Rotate(Vector3.up, -angleDelta);
        }
    }

    void Start()
    {
        imTracker = ImageTracker.current;
    }

    void Update()
    {
        // Reset isPinching when no fingers are detected
        if (twoFingers && (Touchscreen.current == null || Touchscreen.current.touches.Count(t => t.isInProgress) < 2))
        {
            Debug.Log("Positionn | Update cancelled");
            twoFingers = false;
            lastPosDst = -Mathf.Infinity;
            lastAngle = -Mathf.Infinity;
        }
    }
}
