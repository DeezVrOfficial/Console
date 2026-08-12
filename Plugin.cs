namespace Console;

[BepInEx.BepInPlugin(Constants.Guid, Constants.Name, Constants.Version)]
public class Plugin : BepInEx.BaseUnityPlugin
{
    /* keep all these methods in this example of plugin when adding my console to your mod ;)
     Contact me in discord @.kingofcode1 or email me at Deez@deez.uk */
    public static Plugin Instance;

    private void Start()
    {
        /* new Harmony(Constants.PluginGuid).PatchAll(); Remove this if you already have harmony patches */

        Console.LoadConsole();
        gameObject.AddComponent<HamburburData>();
        gameObject.AddComponent<TelemetryManagement>();
    }
}
