namespace ToyRobot
{
    public interface IAction
    {
        void Init();
        void Execute();
        void Report();
    }
}