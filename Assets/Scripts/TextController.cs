using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    private TempObject tempObjectInternal = new TempObject();
    private Text uiText;

    // Start is called before the first frame update
    void Start()
    {
        this.uiText = this.GetComponent<Text>();
    }

    public void SetTempObject(TempObject temperature)
    {
        this.tempObjectInternal = temperature;
    }

    // Update is called once per frame
    void Update()
    {
        this.uiText.text = $"Message: {tempObjectInternal.sender}" +
                           $"- {tempObjectInternal.temperature}" +
                           $"- {tempObjectInternal.humidity}" +
                           $"- {tempObjectInternal.time}";
    }
}
