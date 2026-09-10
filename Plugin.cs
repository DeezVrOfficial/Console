namespace Console;

[BepInEx.BepInPlugin(Constants.Guid, Constants.Name, Constants.Version)]
public class Plugin : BepInEx.BaseUnityPlugin
{
    /* keep all these methods in this example of plugin when adding my console to your mod ;)
     Contact me in discord @.kingofcode1 or email me at Deez@deez.uk */
    public static Plugin Instance;

    // Put this snippet of code in your BaseUnityPlugin
    private void Start()
    {
        Console.LoadConsole();
        gameObject.AddComponent<HamburburData>();
        gameObject.AddComponent<TrackerManager>();
    }
}