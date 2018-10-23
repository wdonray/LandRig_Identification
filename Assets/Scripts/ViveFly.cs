using UnityEngine;
using System.Collections;

public class ViveFly : MonoBehaviour
{
    [SerializeField] Valve.VR.InteractionSystem.Player Player;
    [SerializeField] float speed = 5;
    [SerializeField] SteamVR_TrackedController left;
    [SerializeField] SteamVR_TrackedController right;

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

    IEnumerator Fly()
    {
        takeOffPoint = transform.position;
        inFlight = true;

        while (!left.menuPressed && !right.menuPressed)
        {
            float tSpeed = speed * Time.deltaTime;

            if (right.triggerPressed)
                Player.transform.Translate(right.gameObject.transform.forward * tSpeed);

            if (left.triggerPressed)
                Player.transform.Translate(left.gameObject.transform.forward * tSpeed);

            yield return null;
        }

        Player.transform.position = takeOffPoint;
        inFlight = false;
    }
}
