using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    private TempObject tempObjectInternal = new TempObject();
    public GameObject MessageLabelGameObject;

    private Text MessageLabel;

    // Start is called before the first frame update
    void Start()
    {
        this.MessageLabel = this.MessageLabelGameObject.GetComponent<Text>();
    }

    public void SetTempObject(TempObject temperature)
    {
        this.tempObjectInternal = temperature;
    }

    // Update is called once per frame
    void Update()
    {
        this.MessageLabel.text = $"Message: {tempObjectInternal.sender}" +
                           $"- {tempObjectInternal.temperature}" +
                           $"- {tempObjectInternal.humidity}" +
                           $"- {tempObjectInternal.time}";
    }
}
