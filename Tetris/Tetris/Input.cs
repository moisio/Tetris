using System.Collections;
using System.Windows.Forms;

namespace Tetris
{
    class Input
    {

        //Load table of available keys
        private static Hashtable keytbl = new Hashtable();

        //Checks if a key is pressed atm
        public static bool KeyPressed(Keys key)
        {
            if (keytbl[key] == null)
            {
                return false;
            }   
            return (bool)keytbl[key];
        }

        //Sets the active key's state
        public static void ChangeState(Keys key, bool state)
        {
            keytbl[key] = state;
        }
    }
}
