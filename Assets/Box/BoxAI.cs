using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxAI : MonoBehaviour
{
    // Stores the Box Rigidbody Component
    private Rigidbody2D BoxRigidbody;

    // Stores the Current GameObject Position
    public Vector3 CurrentPosition;

    // Stores the X-axis MovementAxis
    public float XMovementAxis = 3.0f;
    private float RandomMovementAxis;

    // Stores the Box Movement Speed
    public float MovementSpeed = 1.0f;

    // Bool determines if Box should move
    public bool FloatBox;
    public bool DropBox;

    // Gameobject which stores parachute
    private bool ParachuteOpened = false;
    public GameObject ParachuteHolder;
    public Sprite[] Parachute;

    // Bool determines if Box has Landed or GameOver
    private bool IgnoreBoxCollision;
	private bool IgnoreBoxTrigger;

    // Awake is used to initialise all GameObjects
    void Awake()
    {
        // Get Box Rigidbody Component
        BoxRigidbody = GetComponent<Rigidbody2D>();

        // Set Box Gravity
        BoxRigidbody.gravityScale = 0.1f;

        // Get Box Current Position
        CurrentPosition = this.gameObject.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
		// Assign Current Box AI Script and GameObject
		GameController.instance.CurrentBoxScript = this;
		GameController.instance.CurrentBox = this.gameObject;

        // Allow Box Movement by Default
        FloatBox = true;

        // Random IF Statement to determine whether the Box will move to the left or right
        if (Random.Range(0, 2) < 1)
        {
            // Move to the Left by Default
            RandomMovementAxis = -XMovementAxis;
            Debug.Log(this.gameObject.name + " is moving to the left by default!");
        }
        else
        {
            // Move to the Right by Default
            RandomMovementAxis = XMovementAxis;
            Debug.Log(this.gameObject.name + " is moving to the right by default!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Execute method to allow box to move left or right
        FloatingBox();
    }

    // Method to Float Box
    void FloatingBox()
    {
        // Check if Box can move is TRUE
        if (FloatBox == true && DropBox == false)
        {
            // Spawn New Parachute
            SpawnParachute();

            // Temporary Variable to Store the Box Spawn Position
            Vector3 BoxPosition = CurrentPosition;

            // Modify X-axis Box Positiom
            BoxPosition.x += RandomMovementAxis * Mathf.Sin(Time.time * MovementSpeed);

            // Apply Modified Transform Position
            transform.position = new Vector2(BoxPosition.x, transform.position.y);
        }
    }

    // Method to Drop Box
    public void FallingBox()
    {
		// Drop Box Status is Set to False
		if (DropBox == false)
		{
			// Execute Method to Drop Box
			DropBox = true;

			// Set FloatBox to False
			FloatBox = false;

	        // Destroy Parachute
	        DestroyParachute();

	        // Set Box Gravity to Zero
	        BoxRigidbody.gravityScale = Random.Range(2, 4);
		}
    }

    // Method to Show Parachute
    void SpawnParachute()
    {
        // Check if Parachute Opened
        if (ParachuteOpened == false)
        {
            // Select Random Parachute by Array Position
            int RandomParachute = Random.Range(0, Parachute.Length);

            // Get Sprite Renderer Position
            SpriteRenderer NewParachute = ParachuteHolder.GetComponent<SpriteRenderer>();

            // Assign New Parachute
            NewParachute.sprite = Parachute[RandomParachute];

            // Set Parachute Open Status to True
            ParachuteOpened = true;
        }
    }

    // Method to Destroy Parachute
    void DestroyParachute()
    {
        // Check if Parachute was Opened
        if (ParachuteOpened == true)
        {
            // Destroy Parachute
            Destroy(ParachuteHolder);
        }
    }


	// Method to Detect if Box Landed
	void BoxLanded()
	{
		// Set Collision and Trigger Status
		IgnoreBoxCollision = true;
		IgnoreBoxTrigger = true;

		// Award Player Score
		GameController.instance.AwardScore();

		// Spawn a New Box
		GameController.instance.SpawnNewBox();

		// Move the Camera
		GameController.instance.MoveCamera();
	}


	// Box Collision Enter 2D
	void OnCollisionEnter2D(Collision2D Other)
	{
		if(IgnoreBoxCollision == true)
			return;

		// Check Other GameObject by Tag
		if(Other.gameObject.tag == "Ground" || Other.gameObject.tag == "Box")
		{
			// Execute Box Landed Method after Given Time
			Invoke("BoxLanded", 2f);
			IgnoreBoxCollision = true;
		}
	}

	// Box Trigger Enter 2D
	void OnTriggerEnter2D(Collider2D Other)
	{
		if(IgnoreBoxTrigger == true)
			return;

		// Check Other GameObject by Tag
		if(Other.gameObject.tag == "Gameover")
		{
			// Execute Box Landed Method after Given Time
			GameController.instance.GameOver = true;

			// Cancel Invoke
			CancelInvoke("BoxLanded");
			IgnoreBoxTrigger = true;
		}
	}

}


