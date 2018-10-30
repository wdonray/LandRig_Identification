using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloveAnimationController : MonoBehaviour
{
    [SerializeField]
    private Valve.VR.InteractionSystem.LinearMapping m_linearMapping;
    private Valve.VR.InteractionSystem.Hand m_parentHand;
    private Animator m_animator;

    // Use this for initialization
    void Start ()
    {
        m_parentHand = GetParentHand(gameObject);
        m_animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        m_animator.SetFloat("TriggerStrength",
            Mathf.Clamp(m_parentHand.controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).magnitude,
            0.01f, 0.99f));
    }

    private Valve.VR.InteractionSystem.Hand GetParentHand(GameObject child)
    {
        Valve.VR.InteractionSystem.Hand hand = child.GetComponent<Valve.VR.InteractionSystem.Hand>();

        if (hand == null)
            return GetParentHand(child.transform.parent.gameObject);
        else
            return hand;
    }
}
