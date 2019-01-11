
namespace ToyRobot
{
    public enum Heading
    {
        NORTH,
        SOUTH,
        EAST,
        WEST
    }
   

    public class Robot : IAction
    {
        public Position Location { get; set; }
        public Heading Heading { get; set; }

        public void Init()
        {

        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }

        public void Report()
        {
            throw new System.NotImplementedException();
        }
    }
}
