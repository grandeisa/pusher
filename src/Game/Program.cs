using Raylib_cs;

static class Game
{
    const int WIDTH = 800;
    const int HEIGHT = 600;
    const String TITLE = "Pusher";
    const int TARGET_FPS = 120;
    static readonly Color CLEAR_COLOR = Color.Black;


    static void Main ()
    {
        Raylib.InitWindow(WIDTH, HEIGHT, TITLE);
        Raylib.SetTargetFPS(TARGET_FPS);
        Raylib.SetExitKey(KeyboardKey.Escape);

        while(!Raylib.WindowShouldClose())
        {
            Update();
            Render();
        }

        Raylib.CloseWindow();
    }

    static void Update()
    {
        
    }

    static void PhysicsUpdate()
    {
        
    }

    static void Render()
    {
        Raylib.ClearBackground(CLEAR_COLOR);

        Raylib.BeginDrawing();

            /** CALL GAME RENDER CALLS HERE **/

            Raylib.DrawFPS(0, 0);

        Raylib.EndDrawing();
    }
}