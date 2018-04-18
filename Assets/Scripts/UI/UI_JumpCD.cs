using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_JumpCD : MonoBehaviour {

    Player player;
    GameObject UI;
    Image test;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        test=this.GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        test.fillAmount=(1-player.timer/Player.JumpCD);
	}
}
