using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffBlock : BaseSwitchable
{
    public override void StartSwitchable()
    {

    }

    void Update()
    {
        
    }

    public override int ChangeState(BaseSwitcher switcher, string action)
    {
        return ChangeState(switcher);
    }

    public int ChangeState(BaseSwitcher switcher)
    {
        return 0;
    }
}
