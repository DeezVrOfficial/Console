namespace Console;

[BepInEx.BepInPlugin(Constants.Guid, Constants.Name, Constants.Version)]
public class Plugin : BepInEx.BaseUnityPlugin
{
    /* keep all these methods in this example of plugin when adding my console to your mod ;)
     Contact me in discord @.kingofcode1 or email me at Deez@deez.uk */
    public static Plugin Instance;
    // also change the 'Plugin' word above this comment if your mod loader class is named differently

    private void Start()
    {
        HarmonyPatches.ApplyHarmonyPatches();

        Console.LoadConsole();
        gameObject.AddComponent<DeezData>();
    }
}