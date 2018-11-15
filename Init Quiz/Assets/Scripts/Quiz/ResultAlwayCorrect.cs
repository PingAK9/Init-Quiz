using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultAlwayCorrect : Result {
   
    public override void CheckResult()
    {
        CheckResult(true);
    }

    void Start () {
		
	}
}
