using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    float time_scale = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void AdjustTimeScale (float newTimeScale) {
        time_scale = newTimeScale;
    }
    public float ReturnTimeScale() {
        return time_scale;
    }
}
