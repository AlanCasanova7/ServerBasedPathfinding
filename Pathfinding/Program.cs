using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding_2._0
{
    class Program
    {
        static void Main(string[] args)
        {
            Pathfinding.Init();

            #region Classic
            Stopwatch sw1 = new Stopwatch();
            sw1.Start();
            for (int i = 0; i < 50; i++)
            {
                Pathfinding_2._0.Pathfinding.GetPath(new Vector3(175.5f, 0f, 190f), new Vector3(51.9f, 0, 55.1f));
            }
            sw1.Stop();
            Console.WriteLine(sw1.Elapsed);
            #endregion
            #region Queue Based
            //for (int i = 0; i < 1000; i++)
            //{
            //    Enemy enemy = new Enemy();
            //    Pathfinding.RequestPath(enemy, new Vector3(175.5f, 0f, 190f), new Vector3(51.9f, 0, 55.1f));
            //}

            //Stopwatch sw1 = new Stopwatch();
            //Stopwatch sw = new Stopwatch();
            //sw1.Start();
            //while (true)
            //{
            //    sw.Reset();
            //    sw.Start();
            //    Pathfinding.Update();
            //    //for (int i = 0; i < 2500; i++)
            //    //{
            //    //Pathfinding_2._0.Pathfinding.GetPath(new Vector3(175.5f, 0f, 190f), new Vector3(51.9f, 0, 55.1f));
            //    //}
            //    sw.Stop();
            //    Console.WriteLine(sw.Elapsed);
            //    if(sw.Elapsed.Milliseconds < 1)
            //    {
            //        break;
            //    }
            //}
            //sw1.Stop();
            //Console.WriteLine(sw1.Elapsed);
#endregion

            Console.ReadLine();
        }
    }
}
