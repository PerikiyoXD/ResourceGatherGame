using Godot;
using System;

public partial class SceneCamera : Node
{
	// Camera properties
	private Camera3D camera;
	private const float RotationSpeed = 0.01f;
	private const float ZoomSpeed = 0.1f;
	private const float MaxZoom = 100.0f;
	private const float MinZoom = 0.1f;

	private bool isMouseGrabbed;
	private Vector2 lastMousePos;

	// Debug stuff
	//private Label cameraDebugLabelPress;
	//private Label cameraDebugLabelMotion;

	public override void _Ready()
	{
		camera = GetNode<Camera3D>("Camera3D");
		camera.Projection = Camera3D.ProjectionType.Orthogonal;
		camera.Size = 10;

		Transform3D transform = camera.Transform;
		{
			transform = transform.Translated(new Vector3(0, 0, 100));
			transform = transform.Rotated(Vector3.Up, Mathf.DegToRad(-45));
			transform = transform.Rotated(transform.Basis.X, Mathf.DegToRad(-35));
			transform = transform.Orthonormalized();
		}
		camera.Transform = transform;
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);

		// Handle middle mouse grab and move rotation
		if (@event is InputEventMouseButton eventMouseButton && eventMouseButton.ButtonIndex == MouseButton.Middle)
			if (eventMouseButton.Pressed)
				GrabMiddleMouse((InputEventMouseButton)@event);
			else
				ReleaseMiddleMouse();

		if (isMouseGrabbed && @event is InputEventMouseMotion eventMouseMotion)
		{
			Vector2 delta = (eventMouseMotion.GlobalPosition - lastMousePos);
			delta *= (float)RotationSpeed;
			OnMiddleMouseMotion(delta);
			lastMousePos = eventMouseMotion.GlobalPosition;
		}

		// Handle zoom (Mouse scrollwheel)
		if (@event is InputEventMouseButton eventMouseScroll && eventMouseScroll.ButtonIndex == MouseButton.WheelDown)
			ZoomCamera(ZoomSpeed);
		else if (@event is InputEventMouseButton eventMouseScrollUp && eventMouseScrollUp.ButtonIndex == MouseButton.WheelUp)
			ZoomCamera(-ZoomSpeed);

		// Handle WIN+ENTER to toggle fullscreen
		if (@event is InputEventKey eventKey && eventKey.Pressed && eventKey.AltPressed && eventKey.Keycode == Key.Enter)
		{
			DisplayServer.WindowSetMode(DisplayServer.WindowGetMode() == DisplayServer.WindowMode.Windowed ? DisplayServer.WindowMode.Fullscreen : DisplayServer.WindowMode.Windowed);
		}
	}

	private void GrabMiddleMouse(InputEventMouseButton @event)
	{
		//cameraDebugLabelPress.Text = "Middle mouse pressed";
		isMouseGrabbed = true;
		lastMousePos = @event.GlobalPosition;
	}

	private void ReleaseMiddleMouse()
	{
		//cameraDebugLabelPress.Text = "Middle mouse released";
		isMouseGrabbed = false;
	}

	public void OnMiddleMouseMotion(Vector2 delta)
	{
		// Camera Rotation Limits:
		// X: -33 to -75
		// Y: Full rotation
		// Z: 0

		const float minVerticalAngle = -75.0f;
		const float maxVerticalAngle = -33.0f;

		Transform3D transform = camera.Transform;

		// Delta on X axis is used to modify Y rotation on the camera
		// Delta on Y axis is used to modify X rotation on the camera according to the limits

		// Check if camera Y rotation is within the limits
		// If it is, rotate the camera around the Y axis
		// var currentVerticalAngle = Mathf.RadToDeg(Mathf.Atan2(transform.Basis[1][1], transform.Basis[1][2]));
		// if (currentVerticalAngle > minVerticalAngle && currentVerticalAngle < maxVerticalAngle)
		// 	transform = transform.Rotated(Vector3.Up, -delta.X); // Rotate around the Y axis according to the mouse delta on X 

		// Rotate the camera around the X axis according to the mouse delta on Y according to the limits
		var currentHorizontalAngle = Mathf.RadToDeg(Mathf.Atan2(transform.Basis[0][0], transform.Basis[0][2]));
		var clampedDeltaY = Mathf.Clamp(delta.Y, -1, 1);
		if (currentHorizontalAngle > minVerticalAngle && currentHorizontalAngle < maxVerticalAngle)
			transform = transform.Rotated(transform.Basis.X, -delta.Y); // Rotate around the X axis according to the mouse delta on Y


		// Orthonormalize the transform
		transform = transform.Orthonormalized();

		// Update the camera transform
		camera.Transform = transform;
	}

	private void ZoomCamera(double amount)
	{
		Transform3D transform = camera.Transform;
		Vector3 position = transform.Origin;
		float distance = position.Length() + (float)amount;

		if (distance < MinZoom)
		{
			distance = MinZoom;
		}
		else if (distance > MaxZoom)
		{
			distance = MaxZoom;
		}

		position = position.Normalized() * distance;
		transform.Origin = position;
		camera.Transform = transform;
	}
}
