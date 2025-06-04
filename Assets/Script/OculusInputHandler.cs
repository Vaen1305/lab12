using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class OculusInputHandler : MonoBehaviour
{
    public XRNode hand;

    InputDevice handController;

    public Vector2 joystick;
    public float triggerValue;
    public float gripValue;

    List<InputDevice> leftHandDevices = new List<InputDevice>();
    List<InputDevice> rightHandDevices = new List<InputDevice>();

    // Eventos para los botones
    public event Action OnPrimaryButtonJustPressed;
    public event Action OnSecondaryButtonJustPressed;
    public event Action OnMenuButtonJustPressed;
    public event Action OnGripButtonJustPressed;
    public event Action OnTriggerButtonJustPressed;

    // Estados internos
    private bool primaryButtonPressed;
    private bool secondaryButtonPressed;
    private bool menuButtonPressed;
    private bool gripButtonPressed;
    private bool triggerButtonPressed;

    private bool primaryButtonWasPressed;
    private bool secondaryButtonWasPressed;
    private bool menuButtonWasPressed;
    private bool gripButtonWasPressed;
    private bool triggerButtonWasPressed;

    private void Start()
    {
        LoadDevices();
    }

    private void LoadDevices()
    {
        if (hand == XRNode.LeftHand)
        {
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Left, leftHandDevices);
            if (leftHandDevices.Count > 0)
                handController = leftHandDevices[0];
        }
        else if (hand == XRNode.RightHand)
        {
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Right, rightHandDevices);
            if (rightHandDevices.Count > 0)
                handController = rightHandDevices[0];
        }
    }

    void Update()
    {
        if (hand == XRNode.LeftHand || hand == XRNode.RightHand)
        {
            if (!handController.isValid)
                LoadDevices();

            UpdateButtonStates();
            UpdateJoystickStates();
            UpdateTriggerValues();

            if (primaryButtonPressed && !primaryButtonWasPressed)
                OnPrimaryButtonJustPressed?.Invoke();
            if (secondaryButtonPressed && !secondaryButtonWasPressed)
                OnSecondaryButtonJustPressed?.Invoke();
            if (menuButtonPressed && !menuButtonWasPressed)
                OnMenuButtonJustPressed?.Invoke();
            if (gripButtonPressed && !gripButtonWasPressed)
                OnGripButtonJustPressed?.Invoke();
            if (triggerButtonPressed && !triggerButtonWasPressed)
                OnTriggerButtonJustPressed?.Invoke();

            primaryButtonWasPressed = primaryButtonPressed;
            secondaryButtonWasPressed = secondaryButtonPressed;
            gripButtonWasPressed = gripButtonPressed;
            triggerButtonWasPressed = triggerButtonPressed;
            menuButtonWasPressed = menuButtonPressed;
        }
    }

    private void UpdateButtonStates()
    {
        handController.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonPressed);
        handController.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButtonPressed);
        handController.TryGetFeatureValue(CommonUsages.gripButton, out gripButtonPressed);
        handController.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonPressed);
        handController.TryGetFeatureValue(CommonUsages.menuButton, out menuButtonPressed);
    }

    private void UpdateJoystickStates()
    {
        handController.TryGetFeatureValue(CommonUsages.primary2DAxis, out joystick);
    }

    private void UpdateTriggerValues()
    {
        handController.TryGetFeatureValue(CommonUsages.trigger, out triggerValue);
        handController.TryGetFeatureValue(CommonUsages.grip, out gripValue);
    }
}