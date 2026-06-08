using System.Numerics;
using Game.Core;
using Game.Actors;
using Raylib_cs;
namespace Game;

static class Game
{
    const int WIDTH = 800;
    const int HEIGHT = 600;
    const String TITLE = "Pusher";
    const int TARGET_FPS = 120;
    const int PHYSICS_FRAMERATE = 240; 
    static readonly int PHYSICS_FRAME_DURATION = 1000 / PHYSICS_FRAMERATE; // In Milliseconds
    
    static readonly Color CLEAR_COLOR = Color.Black;

    static bool PhysicsShouldStop = false;
    static float lastFrameEnd = 0f;

    static Scene currentScene = new([
        new Player(new(256,256))
    ]);

    static void Main ()
    {
        Raylib.InitWindow(WIDTH, HEIGHT, TITLE);
        Raylib.SetTargetFPS(TARGET_FPS);
        Raylib.SetExitKey(KeyboardKey.Escape);

        // Start physics thread
        Thread physicsThread = new Thread(PhysicsUpdate);

        currentScene.Start();

        physicsThread.Start();
        while(!Raylib.WindowShouldClose())
        {
            Update();
            Render();
        }

        PhysicsShouldStop = true;
        physicsThread.Join();

        Raylib.CloseWindow();
    }

    static void Update()
    {
        float delta = (float) Raylib.GetTime() - lastFrameEnd;

        /** UPDATE GAME OBJECTS HERE **/
        currentScene.Update(delta);

        lastFrameEnd = (float) Raylib.GetTime();
    }

    // Runs on a separate thread for physics objects, 
    // ran in a constant rate instead of the varying framerate.
    static void PhysicsUpdate()
    {
        float lastPhysicsFrameEnd = 0f;

        float lastPhysicsFrameStart;
        float delta = 1f;

        while(!PhysicsShouldStop)
        {
            lastPhysicsFrameStart = (float) Raylib.GetTime();
            delta = lastPhysicsFrameStart - lastPhysicsFrameEnd;

            /** CALL GAME PHYSICS HERE **/
            currentScene.PhysicsUpdate(delta);

            lastPhysicsFrameEnd = (float) Raylib.GetTime();
            Thread.Sleep(PHYSICS_FRAME_DURATION);
        }
        
    }

    static void Render()
    {
        Raylib.ClearBackground(CLEAR_COLOR);

        Raylib.BeginDrawing();

            /** CALL GAME RENDER CALLS HERE **/
            currentScene.Render();

            Raylib.DrawFPS(0, 0);

        Raylib.EndDrawing();
    }

    // Returns the scene a specific actor is in, returns null if none
    // Using this to avoid doing circular references with actors.
    public static Scene? GetScene(Actor actor)
    {
        foreach(Scene scene in Scene.Scenes)
        {
            if(scene.HasActor(actor)) return scene;
        }
        return null;
    }
}