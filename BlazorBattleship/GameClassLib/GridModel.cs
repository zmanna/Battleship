using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorBattleship.GameClassLib
{

    public class GridModel
    {
        public class Square
        {
            public string Id {get; set;}
            public bool IsShip {get; set;}
            public bool IsDestroyed {get; set;}
            public bool IsEmpty {get; set;}
            public int X {get; set;}
            public int Y {get; set;}

        }
    }
}