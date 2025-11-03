# MVR-Plugins

## About:
Simple plugin system for Mirror VR

## How To Use:

**Step 1.**
Make a empty GameObject

<img width="256" height="300" alt="image" src="https://github.com/user-attachments/assets/b9b3aac2-4f93-4724-a0be-70e558f43be7" />

**Step 2.**
Add a PluginManager component to this new GameObject

<img width="256" height="320" alt="image" src="https://github.com/user-attachments/assets/5058b4c3-f611-48ab-a1aa-82ad93da7754" />

**Step 3.**
Create Game Events (You can find an example in MVR Plugins\Scripts\Examples\ExampleEvent.cs):

```Csharp
[Event]
public class ExampleEvent
{
    public int number { get; }
    public ExampleEvent(int num)
    {
      number = num;
    }
}
```

**Step 4.**
Invoking Game Events:

```Csharp
PluginManager.currentAPI.FireEvent<ExampleEvent>(new ExampleEvent(48));
```

**Creating a Plugin**:

COMING SOON
