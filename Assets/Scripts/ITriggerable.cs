using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public interface ITriggerable {
    int Layer { get; }
    GameObject GetGameObject();
    void PlayerEnterTrigger(WayfarerInputHandler wayfarerInputHandler);
    void PlayerExitTrigger(WayfarerInputHandler wayfarerInputHandler);
    void Activate(GameObject triggerObject);
}
