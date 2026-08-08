using GorillaLocomotion;
using UnityEngine;

namespace Console;

public class ConsoleUtils
{
    private static int? noInvisLayerMask;

    public static int NoInvisLayerMask()
    {
        noInvisLayerMask ??= ~(
                                  1 << LayerMask.NameToLayer("TransparentFX") |
                                  1 << LayerMask.NameToLayer("Ignore Raycast") |
                                  1 << LayerMask.NameToLayer("Zone") |
                                  1 << LayerMask.NameToLayer("Gorilla Trigger") |
                                  1 << LayerMask.NameToLayer("Gorilla Boundary") |
                                  1 << LayerMask.NameToLayer("GorillaCosmetics") |
                                  1 << LayerMask.NameToLayer("GorillaParticle"));

        return noInvisLayerMask ?? GTPlayer.Instance.locomotionEnabledLayers;
    }

    public static void TeleportPlayer(Vector3 destinationPosition)
    {
        GTPlayer.Instance.TeleportTo(FormatTeleportPosition(destinationPosition), GTPlayer.Instance.transform.rotation);
        VRRig.LocalRig.transform.position = destinationPosition;
    }

    public static Vector3 FormatTeleportPosition(Vector3 teleportPosition) =>
            teleportPosition - GorillaTagger.Instance.bodyCollider.transform.position +
            GorillaTagger.Instance.transform.position;

    public static void TeleportToMap(string mapName)
    {
        string mT = "";
        string nT = "";

        switch (mapName)
        {
            case "Forest":
                mT =
                        "Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/Regional Transition/TreeRoomSpawnForestZone";

                nT =
                        "Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Forest, Tree Exit";

                break;

            case "City":
                mT =
                        "Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/Regional Transition/ForestToCity";

                nT =
                        "Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - City Front";

                break;

            case "Canyons":
                mT =
                        "Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/Regional Transition/ForestCanyonTransition";

                nT =
                        "Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Canyon";

                break;

            case "Clouds":
                mT =
                        "Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/Regional Transition/CityToSkyJungle";

                nT =
                        "Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Clouds From Computer";

                break;

            case "Caves":
                mT =
                        "Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/Regional Transition/ForestToCave";

                nT =
                        "Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Cave";

                break;

            case "Beach":
                mT =
                        "Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/Regional Transition/BeachToForest";

                nT =
                        "Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Beach for Computer";

                break;

            case "Mountains":
                mT =
                        "Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/Regional Transition/CityToMountain";

                nT =
                        "Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Mountain";

                break;

            case "Basement":
                mT =
                        "Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/Regional Transition/CityToBasement";

                nT =
                        "Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Basement For Computer";

                break;

            case "Metropolis":
                mT =
                        "Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/Regional Transition/MetropolisOnly";

                nT =
                        "Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Metropolis from Computer";

                break;

            case "Arcade":
                mT =
                        "Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/Regional Transition/CityToArcade";

                nT =
                        "Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - City frm Arcade";

                break;

            case "Critters":
                mT =
                        "Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/Regional Transition/CityCrittersTransition";

                nT =
                        "Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - City from Critters";

                break;

            case "Rotating":
                mT =
                        "Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/Regional Transition/CityToRotating";

                nT =
                        "Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Rotating Map";

                break;

            case "Bayou":
                mT =
                        "Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/Regional Transition/BayouOnly";

                nT =
                        "Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - BayouComputer2";

                break;

            case "Virtual Stump":
                {
                    VirtualStumpTeleporter vstumpt = GameObject
                                                    .Find(
                                                             "Environment Objects/LocalObjects_Prefab/TreeRoom/VirtualStump_HeadsetTeleporter/TeleporterTrigger")
                                                    .GetComponent<VirtualStumpTeleporter>();

                    vstumpt.gameObject.transform.parent.parent.parent.parent.parent.parent.gameObject.SetActive(true);
                    vstumpt.gameObject.transform.parent.parent.parent.parent.gameObject.SetActive(true);
                    vstumpt.TeleportPlayer();

                    return;
                }

            case "Lava Forest":
                mT =
                        "Environment Objects/05Maze_PersistentObjects/GhostReactorElevatorManager/VIMForestLavaElevator/Triggers/VIMExp1_SetZoneTrigger";

                nT =
                        "Environment Objects/05Maze_PersistentObjects/GhostReactorElevatorManager/VIMForestLavaElevator/Triggers/JoinRoomTrigger";

                break;

            case "Skate Park":
                mT =
                        "Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/Regional Transition/ForestToHoverboard";

                nT =
                        "Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Hoverboard from Forest";

                break;

            case "Monke Blocks":
                mT =
                        "Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/Regional Transition/MonkeBlocksElevatorExit";

                nT =
                        "Environment Objects/05Maze_PersistentObjects/GhostReactorElevatorManager/MonkeBlocksElevator/Triggers/JoinRoomTrigger";

                break;
        }

        GameObject.Find(mT)?.GetComponent<GorillaSetZoneTrigger>()?.OnBoxTriggered();
        GameObject.Find(nT)?.SetActive(false);
        TeleportPlayer(GameObject.Find(mT)?.transform.position ?? VRRig.LocalRig.transform.position);
    }
}