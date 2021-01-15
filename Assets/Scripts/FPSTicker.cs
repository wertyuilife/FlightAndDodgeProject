using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FPSTicker : MonoBehaviour
{
    Text fpsText;
    // Start is called before the first frame update
    void Start()
    {
        fpsText = gameObject.GetComponent<Text>();
        fpsText.text = "FPS:0";
    }

    // Update is called once per frame
    void Update()
    {
		    int fps =(int)Mathf.Round( 1 / Time.deltaTime);
	  	  fpsText.text = "FPS:" + fps;
    }
}
