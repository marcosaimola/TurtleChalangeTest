using System;
using System.Collections.Generic;
using System.Text;

namespace TurtleChallengeTest.Library
{
    public class Turn
    {
        public Action Action { get; set; }
        public ActionResult ActionResult { get; set; }
        public string Result { get; set; }
        public bool GameOver { get; set; }
        public bool Hit { get; set; }
    }
}
