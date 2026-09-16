using UnityEngine;

namespace Sumi
{
     public class VNRuntime:MonoBehaviour
     {
         private void Start()
         {
          StoryProgram story = new StoryProgram();

          story.AddDialogue("Sumi", "Hello!");
          story.AddDialogue("Sumi", "How are you?");

          StoryVM vm = new StoryVM(story);


          while (vm.HasNext())
          {
              vm.ExecuteNext();
          }
        }
    }
}
