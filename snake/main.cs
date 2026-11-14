using System;
using System.Data;
using System.Numerics;
using Raylib_cs;

namespace Snake
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Raylib.InitWindow(800, 600, "Snake");
			Raylib.SetRandomSeed((uint)DateTime.Now.Microsecond);
			Raylib.SetTargetFPS(10);
			while (!Raylib.WindowShouldClose())
			{
				if (Raylib.IsKeyPressed(KeyboardKey.Up) && Variables.dx != 0 && Variables.dy != 1)
				{
					Variables.dx = 0;
					Variables.dy = -1;
				}
				else if (Raylib.IsKeyPressed(KeyboardKey.Down) && Variables.dx != 0 && Variables.dy != -1)
				{
					Variables.dx = 0;
					Variables.dy = 1;
				}
				else if (Raylib.IsKeyPressed(KeyboardKey.Left) && Variables.dx != 1 && Variables.dy != 0)
				{
					Variables.dx = -1;
					Variables.dy = 0;
				}
				else if (Raylib.IsKeyPressed(KeyboardKey.Right) && Variables.dx != -1 && Variables.dy != 0)
				{
					Variables.dx = 1;
					Variables.dy = 0;
				}
				
				if (Raylib.IsKeyPressed(KeyboardKey.Space) && Variables.dx == 0 && Variables.dy == 0)
					Variables.dx = 1;

				Vector2 head = Objects.snake[0];
				List<Vector2> body = Objects.snake[1..];
				Vector2 NewHead = new Vector2(head.X + Variables.dx, head.Y + Variables.dy);
				Objects.snake.Insert(0, NewHead);
				
				if ((NewHead[0], NewHead[1]) != (Objects.x_circle, Objects.y_circle))
					Objects.snake.RemoveAt(Objects.snake.Count - 1);
				
				Raylib.BeginDrawing();
				Raylib.ClearBackground(Color.Black);
				Raylib.DrawCircle(Objects.x_circle * Variables.GridSize +Variables.GridSize/2,
					Objects.y_circle * Variables.GridSize + Variables.GridSize/2, Variables.Radius, Color.Red);
				
				foreach (Vector2 coords in Objects.snake)
				{
					Raylib.DrawRectangle((int)coords[0] * Variables.GridSize, (int)coords[1] * Variables.GridSize
						, Variables.GridSize, Variables.GridSize, Color.White);
					Raylib.DrawCircle(Objects.x_circle * Variables.GridSize +Variables.GridSize/2,
							Objects.y_circle * Variables.GridSize +Variables.GridSize/2, Variables.Radius, Color.Red);
					}
				Raylib.EndDrawing();
				if ((NewHead[0], NewHead[1]) == (Objects.x_circle, Objects.y_circle))
				{
					Variables.PointCounter++;
					do
					{
						Objects.x_circle = Raylib.GetRandomValue(0, Variables.ArenaWidth - 1);
						Objects.y_circle = Raylib.GetRandomValue(0, Variables.ArenaHeight - 1);
					} while (Objects.snake.Contains(new Vector2(Objects.x_circle, Objects.y_circle)));
				}

				if (NewHead[0] < 0 || NewHead[1] < 0 || NewHead[1] >= Variables.ArenaHeight
				    || NewHead[0] >= Variables.ArenaWidth || body.Contains(NewHead))
				{
					Raylib.CloseWindow();
				}
			}
			
		}
	}

	class Objects
	{
		public static int x_circle = Raylib.GetRandomValue(0, Variables.ArenaWidth - 1);
		public static int y_circle = Raylib.GetRandomValue(0, Variables.ArenaHeight - 1);
		public static List<Vector2> snake = new List<Vector2>() { new Vector2(16, 12) };
	}
	
	class Variables
	{
		public static int PointCounter = 0;
		public static int GridSize = 20;
		public static int ArenaHeight = Raylib.GetScreenHeight() / GridSize;
		public static int ArenaWidth = Raylib.GetScreenWidth() / GridSize;
		public static int Radius = GridSize / 2;
		public static int dx = 0;
		public static int dy = 0;
	}
}