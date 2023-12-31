using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Assets.Scripts;
using Assets.Scripts.Flight;
using System;
using ModApi;
using Object = System.Object;
using System.Windows.Forms;
using Assets.Scripts.Craft;
using ModApi.GameLoop;
using ModApi.Planet.Modifiers.VertexData;
using ModApi.Craft;
using ModApi.Craft.Parts;
using Assets.Scripts.Craft.Parts.Modifiers;
using Assets.Scripts.Flight.Sim;

public class RumblerScript : MonoBehaviour
{
    public float rumbleIntensity, rumbleDuration;
    public Player player;
    public CraftScript craftScript;

    void Start()
    {
        player = ReInput.players.GetPlayer(0);
        if (player == null) Debug.Log("Player not found");

        craftScript = FindObjectOfType<CraftScript>();
        if (craftScript == null) Debug.Log("CraftScript not found");

        craftScript.ActiveCommandPod.StageActivated += OnStageActivated;
        FlightSceneScript.Instance.ExplosionCreated += OnExplosionCreated;
        FlightSceneScript.Instance.CraftChanged += OnCraftChanged;
    }

    void Update()
    {
        //conditions during which rumble should be disabled
        if (!ModSettings.Instance.accelRumble) return;
        if (Game.Instance.FlightScene.TimeManager.CurrentMode.TimeMultiplier == 0) return;
        if (Game.Instance.FlightScene.CraftNode.IsDestroyed) return;
        if (Game.Instance.FlightScene.TimeManager.CurrentMode.TimeMultiplier >= 10) return;

        //acceleration rumble strength calculation
        float groundedSpeedMultiplier = Mathf.Clamp((float)Vector3d.Magnitude(craftScript.FlightData.SurfaceVelocity)/2,0,1);
        float flightAccel = craftScript.FlightData.Grounded ? groundedSpeedMultiplier*(float)craftScript.FlightData.AccelerationMagnitude : (float)Vector3d.Magnitude(craftScript.FlightData.Acceleration - craftScript.FlightData.Gravity);
        float accelRumbleIntensity = ModSettings.Instance.accelStrength * flightAccel / 100;

        RumblePulse(1, accelRumbleIntensity, Time.deltaTime);
    }

    private void OnStageActivated(ICommandPod source, int stageActivated) //staging rumble
    {
        if (ModSettings.Instance.stagingRumble)
        {
            RumblePulse(0,2f,0.25f);
        }
    }

    private void OnExplosionCreated(object sender, EventArgs e) //explosion rumble
    {
        if (ModSettings.Instance.explosionRumble)
        {
            RumblePulse(0,3f,0.35f);
        }
    }

    public void RumblePulse(int index, float intensity, float duration) //sending rumble to player
    {
        float intensityClamped = Mathf.Clamp(ModSettings.Instance.RumbleIntensity * intensity, 0, ModSettings.Instance.IntensityMax);
        player.SetVibration(index, intensityClamped, duration);
    }


    


    private void OnCraftChanged(ICraftNode craftnode)
    {
        craftScript = FindObjectOfType<CraftScript>();
    }

}
