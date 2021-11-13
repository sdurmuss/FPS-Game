using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float hiz = 5f;
    CharacterController characterController;
    Vector3 move;
    const float gravity = 9.8f;
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        move = new Vector3(Input.GetAxis("Horizontal") * hiz * Time.deltaTime, 0f, Input.GetAxis("Vertical") * hiz * Time.deltaTime);
        move = transform.TransformDirection(move); // objenin global eksenini local eksenine çevirir. Bu sayede objeyi döndürdüğümüz yönde hareket eder.        
        //characterController.Move(move); buradayken alttaki if çalışmıyor
        if (!characterController.isGrounded)
        {
            move.y -= gravity * Time.deltaTime; 
        }
        characterController.Move(move);
        //characterController.SimpleMove(move); objenin y değerini kod ile değiştirebilmemizi engeller. yani karakter zıplayamaz. Zıplamadan oynanacak oyunlarda kullanılabilir.
    }
}
