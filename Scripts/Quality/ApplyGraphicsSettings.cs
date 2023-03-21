using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Softdrink;

public class ApplyGraphicsSettings : MonoBehaviour {

    public HardwareInfo hardwareInfo;

    void Start() {

        hardwareInfo.CalculateHardwareScore();

        if (hardwareInfo.userHardwareScore >= 50) {

            Graphics.activeTier = UnityEngine.Rendering.GraphicsTier.Tier3;

        } else if (hardwareInfo.userHardwareScore >= 20) {

            Graphics.activeTier = UnityEngine.Rendering.GraphicsTier.Tier2;

        } else {

            Graphics.activeTier = UnityEngine.Rendering.GraphicsTier.Tier1;

        }

    }

}
