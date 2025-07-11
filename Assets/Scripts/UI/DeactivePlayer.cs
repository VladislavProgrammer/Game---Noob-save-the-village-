using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivePlayer : MonoBehaviour
{

    private void OnEnable()
    {
        EventManager.CallGameCompleted();
    }
}
