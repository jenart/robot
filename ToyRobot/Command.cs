namespace ToyRobot
{
    public enum Heading
    {
        // NOTE: the clockwise order is CRITICAL for processing commands
        IGNORE,
        NORTH,
        EAST,
        SOUTH,
        WEST
    }

    public enum OpCode
    {
        PLACE,
        MOVE,
        LEFT,
        RIGHT,
        REPORT
    }

    public class Command
    {
        public OpCode Operation { get; set; }
        public Position Location { get; set; }
        public Heading Facing { get; set; }

        private bool IsPlaceOpCode => Operation == OpCode.PLACE;

        public Command(OpCode operation, Position location = null, Heading facing = Heading.IGNORE)
        {
            Operation = operation;
            Location = location;
            Facing = facing;
        }


        public override string ToString()
        {
            return IsPlaceOpCode 
                ? $"OpCode [{Operation}] Position [{Location.X},{Location.Y}] Heading [{Facing}]"
                : $"OpCode [{Operation}]";
;        }

    }
}