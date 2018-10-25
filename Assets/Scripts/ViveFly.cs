using UnityEngine;
using System.Collections;

public class ViveFly : MonoBehaviour
{
    [SerializeField] Valve.VR.InteractionSystem.Player Player;
    [SerializeField] float speed = 5;

    Vector3 takeOffPoint;
    bool inFlight = false;

    //private void Start()
    //{
    //    HaspFeature feature = HaspFeature.Default;

    //    string vendorCode =
    //    "6J0GkP0keCnFT90S2TbYIjGgAmuifp2KG5IzAJ0/ea7rxDTMJHF/VcbuwvIgYTU5EZVTrmTboX0IYCzh" +
    //    "lASGU1C7UbgvhgOgqney2NO8Bk6QPkKJMHVHRye81iXXRrr2Tsf8wsMEE/dcr2J0aex1FiJd0bmEntiY" +
    //    "oEeTUwgijtWCOfazjkTSQ8DkP52GvENhbg6Au7AidG8ZlD7I8J+MfvNepRlk/c1V6r3jbyJYjl5XpzDi" +
    //    "UQKhYoElREe1j/QPeUKF9o1rvJnDdt+Z3yFO+QAsJrIW00PKmfvKeD8/hgR4nHRLxTiCkCNTRV3+CrNX" +
    //    "CidbGoAU9tBFabA8PLNDOq+2dv6xDdu+my8ggDegVVnrzQivthb7jPhKsIBfrWyPFnP0fHN7gesWrRQs" +
    //    "69/Wr7yCUHn3ncxlajvhpNilYo0T77fly04aFRD8UO06WzkoZYrXjHE6k/p5r9qURB69zyE6trLO7bVb" +
    //    "At+tMUjOCLi1y9AEHBDIOD9U49RpoF//mmk9monofNj0zoUmN/VjxCldECYXWa2eEpghKPDbrwarLTFV" +
    //    "NiWqYSASMCEHhwAUh3JMn1LYZxnoNbNVgbk5og/OgXFB0sfhjuBLJ90kNkuij4CAlSQlcqu2DBopblmt" +
    //    "gNFwRgF0Bm/oyRom69Kw5nKSFxc3KMXSoFmhsfZKjnFWVzPmE3eExK4HK2HCYpJAi845cHbzLSqC1HE0" +
    //    "SpI9o48CR85DBmxhUXJmq4PBkTFMtz1MdkDCyS/0oLODiHuopEfrjFxt0ahOKNeHW4BwSn3zNcpLWL/G" +
    //    "mjYy/2hm6h71LZzIbSHRshivpQ5xc/zHfpd1zm6f2bKuu4PDrPmFyBM3UGQMmEMq2VjPcLjolMWWmU/5" +
    //    "cjL/aK/eiJ4lCd00TYL2fF2BumrLDK7YwqBlJp0tWQx96xNu1TcCQou86iBIcFm5kyXaZOTElumJ25ko" +
    //    "bRTvvqioFubgrVdRDr79Ww==";

    //    Hasp hasp = new Hasp(feature);
    //    HaspStatus status = hasp.Login(vendorCode);

    //    if (HaspStatus.StatusOk != status)
    //    {
    //        //handle error
    //    }
    //}

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
