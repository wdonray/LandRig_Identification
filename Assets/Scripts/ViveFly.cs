using UnityEngine;
using System.Collections;

public class ViveFly : MonoBehaviour
{
    [SerializeField] Valve.VR.InteractionSystem.Player Player;
    [SerializeField] float speed = 5;

    Vector3 takeOffPoint;
    bool inFlight = false;

    // Update is called once per frame
    void Update ()
    {
        if(Player.leftController != null)
            if (Player.leftController.GetHairTrigger())
                Player.transform.Translate(Player.leftHand.gameObject.transform.forward * speed * Time.deltaTime);

        if (Player.rightController != null)
            if (Player.rightController.GetHairTrigger())
                Player.transform.Translate(Player.rightHand.gameObject.transform.forward * speed * Time.deltaTime);
    }
}
