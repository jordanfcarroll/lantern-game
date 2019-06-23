using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO
// Lantern spotlight must be behind player when facing up -- DONE
// idle animation states need to be preserved when lantern is active -- DONE
// Support for mouse controls
// Fix lantern up by default -- DONE
// Fix code reuse


public class PlayerControl : MonoBehaviour {

    private const float DefaultLightFullIntensity = 0.6f;
    private const float DefaultLightDimIntensity = 0.1f;
    private const float LanternLightFullIntensity = 1.4f;
    private const float LanternLightDimIntensity = 0.3f;
    private const float FlickerLightMaxIntensity = 3f;

    private const float SpotlightFullIntensity = 13f;
    private const float SpotlightStartAngle = 140f;
    private const float SpotlightEndAngle = 60f;
    private const float SpotlightAngleIncrement = 10f;
    private const float SpotlightIntensityIncrement = 1.2f;
    private const float FixedSpotlightY = 90f;
    private const float FixedSpotlightZ = 0f;

    private Vector2 direction;
    public float walkSpeed;
    public float runSpeed;
    public float focusSpeed;
    public float dimSpeed;
    public bool canMove = true;

    public float closestEnemyDistance = 1000f; 

    private float maxForwardSpeed;

    private string state ="FREE";
    private float xInput, yInput, xInputRight, yInputRight, LastMoveX = 0, LastMoveY = 1;
    private bool IsMoving;
    private bool IsTilting;
    

    private float flashlightAngle;
    private float unsignedAngle;

    private GameObject spotlightTriggerZone;


    private Rigidbody2D body;

    private Animator animator;

    private SpriteRenderer tempRend;

    private string ascendingTo = null;
    private float ascendingGrade = 0.0f;

    private Action interactCallback = null;
    private bool interactTooltip = false;
    
	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // StartCoroutine(LanternFlicker());
        tempRend = GetComponent<SpriteRenderer>();
        spotlightTriggerZone = GameObject.Find("Spotlight2dCollider");
        spotlightTriggerZone.GetComponent<BoxCollider2D>().enabled = false;
    }

    void Awake () {
        // Load to correct zone?
    }
 
    void LateUpdate () {
        // tempRend.sortingOrder = (int)Camera.main.WorldToScreenPoint (tempRend.bounds.min).y * -1;
    }
	
	void Update () {

        if (interactTooltip) {
            FindObjectOfType<InteractTooltip>().Display();
        } else {
            FindObjectOfType<InteractTooltip>().Hide();
        }


        // reset animator speed
        animator.speed = 1f;

        LayerMask tempMask;

        // get all objects?
        // add all those with higher y to tempmask
        //

        closestEnemyDistance = FindClosestEnemyDistance();

        if (state == "INTERACTING") {
            // facing down
            if (LastMoveY <= 0) {
                animator.Play("Idle");
            }
            else {
                animator.Play("Interacting");
            }
        }
        else if (state == "FORCED_MOVE") {
            animator.Play("Walking");
        }
        // if not locked into cutscene/interaction
        else if (canMove) {

            xInput = Input.GetAxisRaw("Horizontal");
            yInput = Input.GetAxisRaw("Vertical");
            xInputRight = Input.GetAxisRaw("Horizontal2");
            yInputRight = Input.GetAxisRaw("Vertical2");




            IsMoving = !(xInput == 0 && yInput == 0);
            IsTilting = !(xInputRight == 0 && yInputRight ==0 );


            if (IsMoving) {
                animator.SetFloat("FaceX", xInput);
                animator.SetFloat("FaceY", yInput);
                if (IsTilting) {
                    animator.SetFloat("TiltX", xInputRight);
                    animator.SetFloat("TiltY", yInputRight);
                } else {
                    animator.SetFloat("TiltX", xInput);
                    animator.SetFloat("TiltY", yInput);
                }
                LastMoveX = xInput;
                LastMoveY = yInput;
            }

            // Include or exclude Player in culling mask
            LayerMask mask;
            if (LastMoveY > 0 && LastMoveX == 0)
            {
                mask = LayerMask.GetMask("Default", "TilePathblocker");
            }
            else
            {
                mask = LayerMask.GetMask("Default", "Player", "TilePathblocker");
            }

          
            if (Input.GetButton("Fire3")) {
                // dim lantern
                maxForwardSpeed = focusSpeed;

                // animate
                if (IsMoving)
                {
                    animator.Play("Walking");
                }
                else
                {
                    animator.Play("Idle");
                }

                GameObject.Find("Player_Spotlight").GetComponent<Light>().intensity = 0f;
                GameObject.Find("Player_Spotlight").GetComponent<Light>().spotAngle = SpotlightStartAngle;
                GameObject.Find("Player_Default_Light").GetComponent<Light>().intensity = DefaultLightDimIntensity;
                GameObject.Find("Player_Lantern_Light").GetComponent<Light>().intensity = LanternLightDimIntensity;
            }


            // Using Lantern cone
            // Movement enabled? Only walk?
            else if (Input.GetButton("Fire2")) {
                maxForwardSpeed = focusSpeed;

                // Set animator state
                if (IsMoving) {
                    animator.Play("Walking Lantern");
                    animator.speed = 0.5f;
                } else {
                    // animator.Play("Standing Lantern");
                    animator.Play("Lantern");
                }

                // culling mask for light
                GameObject.Find("Player_Spotlight").GetComponent<Light>().cullingMask = mask;

                // Disable lantern
                // StopCoroutine(LanternFlicker());
                GameObject.Find("Player_Default_Light").GetComponent<Light>().intensity = DefaultLightFullIntensity;
                GameObject.Find("Player_Lantern_Light").GetComponent<Light>().intensity = 0;

                // Increment on spotlight
                if (GameObject.Find("Player_Spotlight").GetComponent<Light>().spotAngle > SpotlightEndAngle) {
                    GameObject.Find("Player_Spotlight").GetComponent<Light>().spotAngle -= SpotlightAngleIncrement;
                }
                if (GameObject.Find("Player_Spotlight").GetComponent<Light>().intensity < SpotlightFullIntensity) {
                    GameObject.Find("Player_Spotlight").GetComponent<Light>().intensity += SpotlightIntensityIncrement;
                }

                // compute lantern rotation
                float relevantX;
                float relevantY;
                if (IsTilting) {
                    relevantX = xInputRight;
                    relevantY = yInputRight;
                } else {
                    relevantX = LastMoveX;
                    relevantY = LastMoveY;
                }

                var tiltVector = new Vector3(relevantX, relevantY, 0);
                
                // Compare input vector with straight up and get angle
                unsignedAngle = Vector3.Angle(new Vector3(0f, 1f, 0f), tiltVector);
                
                if (relevantX < 0) {
                    flashlightAngle = 360f - unsignedAngle;
                } else {
                    flashlightAngle = unsignedAngle;
                }

                // Include or exclude Player in culling mask
                // LayerMask mask;
                // if (relevantY > 0) {
                //     mask = LayerMask.GetMask("Default");
                // } else {
                //     mask = LayerMask.GetMask("Default", "Player");
                // }
                // GameObject.Find("Player_Spotlight").GetComponent<Light>().cullingMask = mask;

                // Set rotation
                GameObject.Find("Player_Spotlight").transform.rotation = Quaternion.Euler(flashlightAngle - 90, FixedSpotlightY, FixedSpotlightZ);
                GameObject.Find("Spotlight2dCollider").transform.rotation = Quaternion.Euler(flashlightAngle - 90, FixedSpotlightY, FixedSpotlightZ);
                spotlightTriggerZone.GetComponent<BoxCollider2D>().enabled = true;


                LayerMask raymask = LayerMask.GetMask("Pickups");

                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(tiltVector), 2f, raymask);
              
                if (hit) {
                    hit.collider.gameObject.GetComponent<Pickup>().Reveal();
                }
            }
            else {
                // disable spotlight, enable lantern light
                GameObject.Find("Player_Spotlight").GetComponent<Light>().intensity = 0f;
                GameObject.Find("Player_Spotlight").GetComponent<Light>().spotAngle = SpotlightStartAngle;
                GameObject.Find("Player_Default_Light").GetComponent<Light>().intensity = DefaultLightFullIntensity;
                GameObject.Find("Player_Lantern_Light").GetComponent<Light>().intensity = LanternLightFullIntensity;
                spotlightTriggerZone.GetComponent<BoxCollider2D>().enabled = false;

                

                // Include or exclude Player in culling mask
                // LayerMask mask;
                // if (LastMoveY > 0)
                // {
                //     mask = LayerMask.GetMask("Default");
                // }
                // else
                // {
                //     mask = LayerMask.GetMask("Default", "Player");
                // }
                GameObject.Find("Player_Lantern_Light").GetComponent<Light>().cullingMask = mask;

                // animate
                if (IsMoving) {
                    animator.Play("Walking");
                } else {
                    animator.Play("Idle");
                }

                // lock movement if we're in a dialogue
                if (IsMoving) {

                    // work out move speed
                    if (Input.GetButton("Jump")) {
                        maxForwardSpeed = runSpeed;
                    } else {
    

                        // This is broken and not sure how to fix it atm -
                        // Calculates speed based on joystick position, but horizontal + vertical don't add up to 1 and nor do they
                        // seem to be proportional, so diagonal movement is too fast. Clamped for now.

                        var speedRatio = Math.Abs(Input.GetAxis("Horizontal")) + Math.Abs(Input.GetAxis("Vertical"));
                        maxForwardSpeed = Mathf.Clamp(walkSpeed, 0, walkSpeed * speedRatio);

                        animator.SetFloat("InputSpeed", speedRatio);
                    }

                }
            }
            // Handle movement
            if (ascendingTo == "RIGHT")
            {
                if (xInput > 0) {
                    yInput += ascendingGrade;
                    xInput -= ascendingGrade;
                }
                else if (xInput < 0) {
                    yInput -= ascendingGrade;
                    xInput += ascendingGrade;
                }
            }
            else if (ascendingTo == "LEFT")
            {
                if (xInput > 0)
                {
                    yInput -= ascendingGrade;
                    xInput -= ascendingGrade;
                }
                else if (xInput < 0)
                {
                    yInput += ascendingGrade;
                    xInput += ascendingGrade;
                }
            }

            var moveVector = new Vector2(xInput, yInput);
            
            moveVector.Normalize();

            // move character
            body.MovePosition(new Vector2((transform.position.x + moveVector.x * maxForwardSpeed * Time.deltaTime),
                transform.position.y + moveVector.y * maxForwardSpeed * Time.deltaTime));

        // Make sure animation idle for cutscene
        } else {
            animator.Play("Idle");
        }
    
        
		
    }

    public void lockPlayer() {
        canMove = false;
        GameObject.Find("Player_Spotlight").GetComponent<Light>().intensity = 0f;
        GameObject.Find("Player_Spotlight").GetComponent<Light>().spotAngle = SpotlightStartAngle;
        GameObject.Find("Player_Default_Light").GetComponent<Light>().intensity = DefaultLightFullIntensity;
        GameObject.Find("Player_Lantern_Light").GetComponent<Light>().intensity = LanternLightFullIntensity;
    }
    
    public void unlockPlayer() {
        canMove = true;
    }

    IEnumerator LanternFlicker() {
        while(true) {
            // if (GameObject.Find("Player_Lantern_Light").GetComponent<Light>().intensity < (LanternLightFullIntensity + 0.5f)) {
                // GameObject.Find("Player_Lantern_Light").GetComponent<Light>().intensity += Random.Range(0.3f, 0.4f);
            // } else {
                GameObject.Find("Player_Lantern_Light").GetComponent<Light>().intensity = LanternLightFullIntensity;
            // }
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.4f));
        }
    }

    public float FindClosestEnemyDistance()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                distance = curDistance;
            }
        }
        return distance;
    }

    // Receiver for animation event
    public void PlayFootstep () {
        FindObjectOfType<AudioManager>().Play("Player_Footstep");
    }

    public void setInteract() {
        state = "INTERACTING";
    }

    public void setFree() {
        state = "FREE";
    }

    public void TriggerInteract(Action callback) {
        interactCallback = callback;
        StartCoroutine(playInteract());
    }

    public void TriggerInteract() {
        StartCoroutine(playInteract());
    }

    public Transform getTransform() {
        return transform;
    }

    IEnumerator playInteract() {
        lockPlayer();
        setInteract();
        yield return new WaitForSeconds(1.5f);
        unlockPlayer();
        setFree();
        if (interactCallback != null) {
            interactCallback();
            interactCallback = null;
        }
    }

    public void setAscendDescend(string ascendsTo, float grade) {
        ascendingTo = ascendsTo;
        ascendingGrade = grade;
    }

    public void resetAscendDescend() {
        ascendingTo = null;
    }

    public void setUnderground () {
        GetComponent<SpriteRenderer>().sortingLayerName = "Underground";
    }

    public void resetUnderground () {
        GetComponent<SpriteRenderer>().sortingLayerName = "Player";
    }

    public void teleportPlayer (Transform destination) {
        transform.position = destination.position;
        Vector3 cameraPos = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().position;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().position = new Vector3(destination.position.x, destination.position.y, cameraPos.z);

    }

    public void forceMoveTo (Transform target, string endDirection, float speed) {
        state = "FORCED_MOVE";

        StartCoroutine(advanceToNextPoint(target.GetComponent<Transform>(), endDirection, speed));
    }

    IEnumerator advanceToNextPoint(Transform target, string endDirection, float speed)
    {
        float step = walkSpeed * speed * Time.deltaTime;



        Vector3 vector = target.position - transform.position;

        Vector3 normalized = Vector3.Normalize(vector);

        animator.SetFloat("FaceX", normalized.x);
        animator.SetFloat("FaceY", normalized.y);

        while (Vector3.Distance(transform.position, target.position) > step)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            yield return null;
        }

        state = null;
        if (endDirection == "UP")
        {
            faceUp();
        }
        else if (endDirection == "DOWN")
        {
            faceDown();
        }
        else if (endDirection == "RIGHT")
        {
            faceRight();
        }
        else if (endDirection == "LEFT")
        {
            faceLeft();
        }

    }

    public void faceUp()
    {
        animator.SetFloat("FaceY", 1);
        
    }

    public void faceDown()
    {
        animator.SetFloat("FaceY", -1);
    }

    public void faceRight()
    {
        animator.SetFloat("FaceX", 1);
    }

    public void faceLeft()
    {
        animator.SetFloat("FaceX", -1);
    }

    public void setInteractTooltip () {
        interactTooltip = true;
    }

    public void unsetInteractTooltip () {
        interactTooltip = false;
    }
}
