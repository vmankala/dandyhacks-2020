using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine.Networking;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour {
    private float maxspeed; //walk speed
	Animator anim;
	private bool faceright; //face side of sprite activated
	// private bool jumping=false;
	// private bool isdead=false;
    private bool attacking;
    private bool colliding;


    // Start is called before the first frame update
    void Start() {
        maxspeed=4;//Set walk speed
		faceright=true;//Default right side
        attacking = false;
		anim = this.gameObject.GetComponent<Animator> ();
		anim.SetBool ("walk", false);//Walking animation is deactivated
		anim.SetBool ("dead", false);//Dying animation is deactivated
		anim.SetBool ("jump", false);//Jumping animation is deactivated
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log("Trigger!");
        if(attacking) {
            Destroy(collider.gameObject);
        }
    }

    // Update is called once per frame
    private int attackFrameCount = 0;
    void Update() {
        // Debug.Log(attacking);
        if (!isLocalPlayer) { return; }

        if (isLocalPlayer) {
            Camera.main.transform.position = this.transform.position + new Vector3(0,0,-10);
        }

        if (Input.GetMouseButtonDown(0)){
            Debug.Log("Mouse button down 0\n");
            anim.Play("attacking", -1, 0f);
            attacking = true;
        } else if (attackFrameCount > 60) {
            attacking = false;
            attackFrameCount = 0;
        }

        if(attacking) {
            attackFrameCount++;
        }


        //Horizontal movement
        float hMove = Input.GetAxis ("Horizontal");
        // GetComponent<Rigidbody2D>().velocity = new Vector2(hMove * maxspeed, GetComponent<Rigidbody2D>().velocity.y);
        if(hMove>0){//Go right
            anim.SetBool ("walk", true);//Walking animation is activated
            if(faceright==false){
                Flip ();
            }
        }
        if(hMove==0){//Stop
            anim.SetBool ("walk", false);
        }			
        if((hMove<0)){//Go left
            anim.SetBool ("walk", true);
            if(faceright==true){
                Flip ();
            }
        }

        //Vertical movement
        float vMove = Input.GetAxis("Vertical");
        // GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, vMove * maxspeed);
        if (vMove > 0 || vMove < 0) {
            anim.SetBool("walk", true);
        }
        if (vMove == 0 && hMove == 0) {
            anim.SetBool("walk", false);
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(hMove * maxspeed, vMove * maxspeed);
    }

    void Flip(){
		faceright=!faceright;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
