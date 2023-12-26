using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    private Transform targetPos;
    private Quaternion targetRot;
    [SerializeField]
    private float moveSpeed = 5f;
    private float rotateSpeed = 1.5f;
    public Vector2 moveSpeedMultiplier = Vector2.zero;
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
    public float intensity = .15f;

    void Start()
    {
        slideRotation.eulerAngles = slideRotEuler;
        jumpRotation.eulerAngles = jumpRotEuler;
        leftRotation.eulerAngles = leftRotEuler;
        rightRotation.eulerAngles = rightRotEuler;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 positionNoise = Perlin2D() * intensity;
        transform.position = new Vector3(
            Mathf.Lerp(transform.position.x, targetPos.position.x+positionNoise.x, moveSpeedMultiplier.x * moveSpeed * Time.deltaTime),
            Mathf.Lerp(transform.position.y, targetPos.position.y+positionNoise.y, moveSpeedMultiplier.y * moveSpeed * Time.deltaTime),
            Mathf.Lerp(transform.position.z, targetPos.position.z, moveSpeed * Time.deltaTime));
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
        moveSpeedMultiplier = Vector2.MoveTowards(moveSpeedMultiplier, Vector2.one, multiplierFalloff * Time.deltaTime);
    }

    private Vector3 Perlin2D(){
        return new Vector3(
            Mathf.PerlinNoise(Time.time*3, 0)-.5f,
            Mathf.PerlinNoise(Time.time*3, 10)-.5f
        ).normalized;
    }

    public void Slide(){
        moveSpeedMultiplier = new Vector2(1, 1.5f);
        targetRot = slideRotation;
    }
    public void EndSlide(){
        targetRot = Quaternion.identity;
    }
    
    public void Jump(){
        targetRot = jumpRotation;
    }
    
    public void EndJump(){
        targetRot = Quaternion.identity;
        moveSpeedMultiplier = new Vector2(1, 2);
    }

    public void Move(int direction){
        if (direction >= 0){
            targetRot = rightRotation;
        }else{
            targetRot = leftRotation;
        }
        StopAllCoroutines();
        IEnumerator endRoutine = EndRotation();
        StartCoroutine(endRoutine);
    }

    public IEnumerator EndRotation(){
        yield return new WaitForSeconds(.3f);
        targetRot = Quaternion.identity;
    }
}
