using DarkRift;

namespace _debugHandle
{
    public class _debugHandle : Plugin
    {
        //Basic puglin data
        public override string name
        {
            get { return "_debughandle"; }
        }

        public override string version
        {
            get { return "0.1"; }
        }

        public override Command[] commands
        {
            get { return new Command[0]; }
        }

        public override string author
        {
            get { return "Luiz G. A. Roland"; }
        }

        public override string supportEmail
        {
            get { return "luizherege@gmail.com"; }
        }

        //Variable that
        public bool debugMode = true;

        public _debugHandle()
        { }
    }
}
