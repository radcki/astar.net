using Astar.net.PathSolver;
using Astar.net.PathSolver.Parameters;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;


namespace Astar.net
{
    class Program
    {
        static void Main(string[] args)
        {
            for (var i = 0; i <= 6; i++)
            {
                var sizeX = 500;
                var sizeY = 500;
                var grid = new Grid(sizeX, sizeY, 25);
                var rnd = new Random();

                var start = new Position((short)(rnd.Next(0, sizeX / 4)), (short)rnd.Next(0, sizeY), 0);
                var end = new Position((short)rnd.Next(sizeX / 2, sizeX), (short)rnd.Next(0, sizeY), 0);

                switch (rnd.Next(1, 4))
                {
                    case 1:
                        // labirynth
                        for (var x = 0; x <= sizeX; x++)
                        {
                            for (var y = 0; y <= sizeY; y++)
                            {
                                if ((((y * 4) % 3) != 0) && (rnd.Next(0, 10) < 7))
                                {
                                    if (!start.Equals(x, y) && !end.Equals(x, y))
                                    {
                                        grid.BlockPosition(x, y);
                                    }
                                }
                            }
                        }
                        break;
                    case 2:
                        // blocks
                        var blockSize = 10;
                        for (var x = 0; x <= sizeX; x++)
                        {
                            for (var y = 0; y <= sizeY; y++)
                            {
                                if (rnd.Next(0, 900) < 2)
                                {
                                    try
                                    {
                                        for (var n = 0; n < blockSize * 2; n++)
                                        {
                                            for (var m = 0; m < blockSize; m++)
                                            {
                                                if (start.Equals(x + m, y + n) || end.Equals(x + m, y + n))
                                                {
                                                    throw new Exception();
                                                }
                                            }
                                        }

                                        for (var n = 0; n < blockSize * 2; n++)
                                        {
                                            for (var m = 0; m < blockSize; m++)
                                            {
                                                if (y + n < sizeY && x + m < sizeX)
                                                {
                                                    grid.BlockPosition(x + m, y + n);
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception)
                                    {
                                    }

                                }
                            }
                        }
                        break;
                    default:
                        // vertical blocks      

                        for (var x = 0; x <= sizeX; x++)
                        {
                            for (var y = 0; y <= sizeY; y++)
                            {
                                if (rnd.Next(0, 200) < 3)
                                {
                                    for (var n = 0; n < 20; n++)
                                    {
                                        if (!start.Equals(x, y + n) && !end.Equals(x, y + n) && y + n < sizeY)
                                        {
                                            grid.BlockPosition(x, y + n);
                                        }
                                    }
                                }
                            }
                        }
                        break;
                }


                var watch = Stopwatch.StartNew();

                var solver = new Solver(grid);
                var result = solver.FindPath(start, end, false);

                watch.Stop();
                Console.WriteLine("Found in: " + watch.Elapsed.TotalMilliseconds + " ms.");

                var bitmap = new Bitmap(sizeX, sizeY);
                var draw = Graphics.FromImage(bitmap);
                RectangleF textZone = new RectangleF(5, 5, 200, 200);
                RectangleF textShadowZone = new RectangleF(6, 6, 200, 200);

                var whitePen = new Pen(Color.White);
                var grayPen = new Pen(Color.Gray);
                var bluePen = new Pen(Color.Blue);
                var redPen = new Pen(Color.Red);
                var greenPen = new Pen(Color.LightGreen);
                var steps = 0;
                for (var y = 0; y <= sizeY; y++)
                {
                    for (var x = 0; x <= sizeX; x++)
                    {
                        if (start.Equals(x, y) || end.Equals(x, y))
                        {
                            draw.DrawRectangle(redPen, x, y, 1, 1);
                        }
                        else if (result.PathCoordinates[x, y] == 1)
                        {
                            draw.DrawRectangle(bluePen, x, y, 1, 1);
                            steps++;
                        }
                        else if (result.CheckedCoordinates[x, y] == 1)
                        {
                            draw.DrawRectangle(greenPen, x, y, 1, 1);
                        }
                        else if (grid.BlockedPositions[x, y] == 1)
                        {
                            draw.DrawRectangle(grayPen, x, y, 1, 1);
                        }
                        else
                        {
                            draw.DrawRectangle(whitePen, x, y, 1, 1);
                        }
                    }
                }
                draw.FillRectangle(Brushes.White, 5, 5, 160, 25);
                draw.DrawString("Steps: " + steps + Environment.NewLine + "found in " + watch.Elapsed.TotalMilliseconds + " ms.", new Font("Consolas", 8), Brushes.Red, textZone);

                bitmap.EnlargeImage(4).Save(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + Guid.NewGuid() + ".png", ImageFormat.Png);
                Console.WriteLine("File save with path: " + Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" +Guid.NewGuid() + ".png");
            }
        }

        
    }
}
