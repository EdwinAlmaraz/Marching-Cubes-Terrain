using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

namespace RTL.Atlas
{
    public class ChangeSceneState : State
    {
        int sceneIndex;

        public ChangeSceneState(int sceneIndex)
        {
            if (sceneIndex >= SceneManager.sceneCountInBuildSettings)
                throw new System.IndexOutOfRangeException();

            this.sceneIndex = sceneIndex;
        }

        public override async void Init()
        {
            await loadLevel();
        }

        private async Task loadLevel()
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(sceneIndex);

            while (!async.isDone)
                await Task.Yield();

            return;
        }

        public override void Update(float timer) { }
    }
}