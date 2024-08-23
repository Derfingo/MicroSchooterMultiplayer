namespace Assets.Scripts.Save
{
    public interface ISaveSystem
    {
        void Save(SettingsData data);
        SettingsData Load();
    }
}
