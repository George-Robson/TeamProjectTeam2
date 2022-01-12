using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canChangeLevel : MonoBehaviour
{
    private bool bCanChangeLevel = true;

    public void setCanChangeLevel(bool boolean) {
        bCanChangeLevel = boolean;
    }

    public bool getCanChangeLevel() {
        return bCanChangeLevel;
    }
}
