using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//MonoBehaviour is a base class that Unity scripts can derive from. 
//This allows the script to be added as a component to a GameObject and 
//therefore can receive messages from the engine.
public class WinMonitor : MonoBehaviour
{
    //References the ResultText GameObject's TextMeshPro - Text (UI) component
    public TMP_Text resultTextRef;

    //Will receive a message from the engine when another collider hits this objects collider.
    private void OnCollisionEnter(Collision collision)
    {
        //Changes the text property to display "Success!"
        resultTextRef.text = "Success!";
    }

}
