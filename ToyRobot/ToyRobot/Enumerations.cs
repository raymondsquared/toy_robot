﻿namespace ToyRobot
{
    ///<summary>
    /// collections of enumerations
    ///</summary>
    public static class ENUMERATIONS
    {
        public enum DIRECTIONS
        {
            NORTH,
            EAST,
            SOUTH,
            WEST,
            UNKNOWN
        }

        public enum TURNS
        {
            LEFT,
            RIGHT,
        }

        public enum LOG_LEVEL
        {
            DEBUG,
            INFO,
            WARN,
            ERROR
        }
    }
}
