using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    public Vector3 playerVelocity;
    private bool isGrounded;
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 1f;

    public int speedBoostActive = 0;

    public int jumpLockActive = 0;

    public int slowActive = 0;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;

        if (Input.GetKeyDown(KeyCode.K) && PlayerPrefs.GetInt("debug") == 1)
        {
            setSpeed(10f);
        }
        if (Input.GetKeyDown(KeyCode.J) && PlayerPrefs.GetInt("debug") == 1)
        {
            setSpeed(-10f);
        }
    }

    public void processMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
        //Debug.Log(playerVelocity.y);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }


    void OnTriggerEnter(Collider other){
        if (other.name == "Spring"){
            playerVelocity.y = Mathf.Sqrt(3 * jumpHeight * -3.0f * gravity);
        }

        if (other.name == "JumpLock" || other.name == "JumpLockDis" || other.name == "Walkway" || other.name == "Walkways"){
            jumpLockActive++;
            jumpHeight = 0f;
        }

        if (other.name == "JumpBoost"){
            jumpHeight = 3f;
        }

        if (other.name == "SpeedBoost"){
            speedBoostActive++;
            speed = 10f;
        }

        if (other.name == "Walkways"){
            slowActive++;
            speed = 2f;
        }

        if (other.name == "Walkways"){
            StartCoroutine(LoseJumpLock(0.2f));
        }

        if (other.name == "Disappear"){
            StartCoroutine(Disappear(1, other));
            StartCoroutine(Appear(4, other));
        }

        if (other.name == "JumpLockDis"){
            StartCoroutine(DisappearL(1, other));
            StartCoroutine(Appear(4, other));
        }
    }

    void OnTriggerExit(Collider other){
        if (other.name == "JumpLock" || other.name == "JumpLockDis" || other.name == "Walkway" || other.name == "Walkways"){
            StartCoroutine(LoseJumpLock(0.2f));
        }

        if (other.name == "JumpBoost"){
            jumpHeight = 1f;
        }

        if (other.name == "SpeedBoost"){
            StartCoroutine(LoseSpeedBoost(0.5f));
        }

        if (other.name == "Walkways"){
            StartCoroutine(LoseSlow(0.5f));
        }
    }

    IEnumerator Disappear(float secs, Collider other) {
        yield return new WaitForSeconds(secs);
        other.gameObject.SetActive(false);
     }

     IEnumerator DisappearL(float secs, Collider other) {
        yield return new WaitForSeconds(secs);
        other.gameObject.SetActive(false);
        jumpLockActive--;
     }

     IEnumerator Appear(float secs, Collider other) {
        yield return new WaitForSeconds(secs);
        other.gameObject.SetActive(true);
     }

     IEnumerator LoseSpeedBoost(float secs) {
        yield return new WaitForSeconds(secs);
        speedBoostActive--;
        if (speedBoostActive <= 0){
            speedBoostActive = 0;
            speed = 5f;
        }
     }

     IEnumerator LoseSlow(float secs) {
        yield return new WaitForSeconds(secs);
        slowActive--;
        if (slowActive <= 0){
            slowActive = 0;
            speed = 5f;
        }
     }

     IEnumerator LoseJumpLock(float secs) {
        yield return new WaitForSeconds(secs);
        jumpLockActive--;
        if (jumpLockActive <= 0){
            jumpLockActive = 0;
            jumpHeight = 1f;
        }
     }


    public void setSpeed(float v)
    {
        StartCoroutine(SpeedEntity(5f, v));
    }

    IEnumerator SpeedEntity(float secs, float v) {
        float oldSpeed = speed;
        speed = speed + v;
        if (speed > 15f)
        {
            speed = 15f;
        }
        else if(speed == 0)
        {
            speed = 5f;
        }
        else if (speed < -15f)
        {
            speed = -15f;
        }
        yield return new WaitForSeconds(secs);
        speed = oldSpeed;
    }

    public void resetSpeed(){
        StopAllCoroutines();
        speed = 5f;
    }
}
