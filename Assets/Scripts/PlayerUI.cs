using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    public Text DeadText { get; private set; }

	void Start () {
        DeadText = transform.Find("YouDiedText").GetComponent<Text>();
	}
	
}
