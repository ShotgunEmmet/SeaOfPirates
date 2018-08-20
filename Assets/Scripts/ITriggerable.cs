using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerable {
    GameObject GetGameObject();
    void PlayerEnterTrigger(WayfarerInputHandler wayfarerInputHandler);
    void PlayerExitTrigger(WayfarerInputHandler wayfarerInputHandler);
}
