using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    private PlayerState state;
    [SerializeField]
    private float groundedHeight = 1.5f;
    [SerializeField]
    private float jumpHeight = 5;
    [SerializeField]
    private float slideHeight = .5f;
    [SerializeField]
    private Transform targetPos;
    private float targetY;
    private Quaternion targetRot = Quaternion.identity;
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float rotateSpeed = 2.5f;
    public Vector2 moveSpeedMultiplier = Vector2.one;
    private float multiplierFalloff = .5f;
    private Quaternion slideRotation;
    [SerializeField]
    private Vector3 slideRotEuler;
    private Quaternion jumpRotation;
    [SerializeField]
    private Vector3 jumpRotEuler;
    private Quaternion leftRotation;
    [SerializeField]
    private Vector3 leftRotEuler;
    private Quaternion rightRotation;
    [SerializeField]
    private Vector3 rightRotEuler;
    [SerializeField]
    private float sway = .5f;
    private float currentSway = 0f;
    public float intensity = .15f;

    void Start()
    {
        slideRotation.eulerAngles = slideRotEuler;
        jumpRotation.eulerAngles = jumpRotEuler;
        leftRotation.eulerAngles = leftRotEuler;
        rightRotation.eulerAngles = rightRotEuler;
        targetY = groundedHeight;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state.currentGroundState == PlayerState.GroundState.Grounded)
            Sway();
        else
            currentSway = 0;
        Vector3 positionNoise = Perlin2D() * intensity;
        transform.SetPositionAndRotation(new Vector3(
            Mathf.Lerp(transform.position.x, targetPos.position.x+positionNoise.x+currentSway, moveSpeedMultiplier.x * moveSpeed * Time.deltaTime),
            Mathf.Lerp(transform.position.y, targetY+positionNoise.y+Mathf.Abs(currentSway), moveSpeedMultiplier.y * moveSpeed * Time.deltaTime),
            Mathf.Lerp(transform.position.z, targetPos.position.z, moveSpeed * Time.deltaTime)), Quaternion.Lerp(transform.rotation, targetRot, rotateSpeed * Time.deltaTime));
        moveSpeedMultiplier = Vector2.MoveTowards(moveSpeedMultiplier, Vector2.one, multiplierFalloff * moveSpeedMultiplier.magnitude * Time.deltaTime);
    }

    private void Sway()
    {
        float sign = Mathf.Sign(sway);
        currentSway += sign * moveSpeed * Time.deltaTime;
        if ((sway < 0 && currentSway < sway) || (sway > 0 && currentSway > sway))
        {
            sway *= -1;
        }
    }

    private Vector3 Perlin2D(){
        return new Vector3(
            Mathf.PerlinNoise(Time.time*3, 0)-.5f,
            Mathf.PerlinNoise(Time.time*3, 10)-.5f
        ).normalized;
    }

    public void Slide(){
        targetY = slideHeight;
        moveSpeedMultiplier = new Vector2(1, 4f);
        targetRot = slideRotation;
    }
    public void EndSlide(){
        targetY = groundedHeight;
        moveSpeedMultiplier = new Vector2(1, 2f);
        targetRot = Quaternion.identity;
    }
    
    public void Jump(float time){
        targetRot = jumpRotation;
        targetY = jumpHeight;
        moveSpeedMultiplier = new Vector2(1, 2);
        IEnumerator LandJumpCoroutine = LandJump(time / 1.2f);
        StartCoroutine(LandJumpCoroutine);
    }
    
    public void EndJump(){
        targetRot = Quaternion.identity;
        targetY = groundedHeight;
        moveSpeedMultiplier = new Vector2(1, 3);
    }

    private IEnumerator LandJump(float time)
    {
        yield return new WaitForSeconds(time);
        if(state.currentGroundState == PlayerState.GroundState.Jumping)
            EndJump();
    }

    public void Move(int direction){
        if (direction >= 0){
            targetRot = rightRotation;
        }else{
            targetRot = leftRotation;
        }
        moveSpeedMultiplier = new Vector2(2, 1);
        StopAllCoroutines();
        IEnumerator endRoutine = EndRotation();
        StartCoroutine(endRoutine);
    }

    public IEnumerator EndRotation(){
        yield return new WaitForSeconds(.3f);
        targetRot = Quaternion.identity;
    }
}
