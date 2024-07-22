using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Entities;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

    public void RestartGame(int sceneIndex)
    {
        var defaultWorld = World.DefaultGameObjectInjectionWorld;
        defaultWorld.EntityManager.CompleteAllTrackedJobs();
        foreach (var system in defaultWorld.Systems)
        {
            system.Enabled = false;
        }
        defaultWorld.Dispose();
        DefaultWorldInitialization.Initialize("Default World", false);
        if (!ScriptBehaviourUpdateOrder.IsWorldInCurrentPlayerLoop(World.DefaultGameObjectInjectionWorld))
        {
            ScriptBehaviourUpdateOrder.AppendWorldToCurrentPlayerLoop(World.DefaultGameObjectInjectionWorld);
        }   
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }
}
