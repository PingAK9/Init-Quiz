using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultAlwayCorrect : Result {
   
    public override void CheckResult()
    {
        base.CheckResult();
        CheckResult(true);
    }

}
