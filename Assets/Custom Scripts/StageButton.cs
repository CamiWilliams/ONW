using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StageButton : MonoBehaviour
{
    //Percentage of the button pressed necessary to trigger action
    [SerializeField] private float threshold = 0.1f;
    //dead zone for button to prevent double clicks
    [SerializeField] private float deadZone = 0.045f;

    //Stage Number
    public int stageNumber;
    public GameObject CurrentStage;
    public GameObject Stages;

    private bool isPressed;
    private Vector3 startPosition;
    private ConfigurableJoint joint;

    public UnityEvent onPressed, onReleased;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPressed && GetValue() + threshold >= 1)
        {
            Pressed();
        }
        if(isPressed && GetValue() - threshold <= 0)
        {
            Released();
        }
    }

    private float GetValue()
    {
        var value = Vector3.Distance(startPosition, transform.localPosition) / joint.linearLimit.limit;

        if (Mathf.Abs(value) < deadZone)
        {
            value = 0;
        }

        return Mathf.Clamp(value, -1f, 1f);
    }

    private void Pressed()
    {
        if (!isPressed)
        {
            isPressed = true;
            onPressed.Invoke();
            Debug.Log("Pressed");
            Stages.GetComponent<StageManager>().ActivateStage(stageNumber);
        }
    }

    private void Released()
    {
        if (isPressed)
        {
            isPressed = false;
            onReleased.Invoke();
            Debug.Log("Released");
        }
    }
}
