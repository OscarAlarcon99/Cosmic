using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    public InputPlayer input;

	//If this is enabled, Unity's internal input smoothing is bypassed;
	public bool useRawInput = true;

	//If any input falls below this value, it is set to '0';
    //Use this to prevent any unwanted small movements of the joysticks ("jitter");
	public float deadZoneThreshold = 0.2f;
    public Vector2 deltaMove;
    public Vector2 deltaLook;

    float _horizontalInput;
    float _verticalInput;

    private void OnEnable()
    {
        input = new InputPlayer();
        input.Enable();
    }

    public void Desactivate()
    {
        _horizontalInput = 0;
        _verticalInput = 0;
        deltaLook = Vector2.zero;
        deltaMove = Vector2.zero;
    }

    public void FixedUpdateInput()
    {
        deltaMove = input.PlayerMain.Move.ReadValue<Vector2>();
        deltaLook = input.PlayerMain.Look.ReadValue<Vector2>();
    }

    public float GetHorizontalMovementInput()
	{
        if (useRawInput)
            _horizontalInput = deltaMove.x;
        else
            _horizontalInput = deltaMove.x;

		//Set any input values below threshold to '0';
		if(Mathf.Abs(_horizontalInput) < deadZoneThreshold)
			_horizontalInput = 0f;

		return _horizontalInput;
	}

	public  float GetVerticalMovementInput()
	{

		if(useRawInput)
			_verticalInput = deltaMove.y;
		else
            _verticalInput = deltaMove.y;
        
        //Set any input values below threshold to '0';
        if (Mathf.Abs(_verticalInput) < deadZoneThreshold)
			_verticalInput = 0f;

		return _verticalInput;
	}

	public  bool IsJumpKeyPressed()
	{
		return input.PlayerMain.Jump.triggered;
	}

}
