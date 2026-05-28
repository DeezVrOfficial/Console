using BepInEx;

namespace Console
{
    [BepInPlugin(Constants.Guid, Constants.Name, Constants.Version)]
    public class Plugin : BaseUnityPlugin
    {
        void Start() => Console.LoadConsole();
    }
}