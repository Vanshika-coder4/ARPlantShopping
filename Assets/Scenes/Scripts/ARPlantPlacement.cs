using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlantPlacement : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public GameObject plantPrefab;

    private static readonly List<ARRaycastHit> hits =
        new List<ARRaycastHit>();

    private GameObject placedPlant;

    void Update()
    {
        if (Touchscreen.current == null)
            return;

        if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            Vector2 touchPosition =
                Touchscreen.current.primaryTouch.position.ReadValue();

            if (raycastManager.Raycast(
                touchPosition,
                hits,
                TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;

                if (placedPlant == null)
                {
                    placedPlant = Instantiate(
                        plantPrefab,
                        hitPose.position,
                        hitPose.rotation
                    );
                }
                else
                {
                    placedPlant.transform.position = hitPose.position;
                }
            }
        }
    }
}