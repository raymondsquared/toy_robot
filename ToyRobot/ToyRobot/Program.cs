﻿using System;
using ToyRobot.Abstractions;
using ToyRobot.Commands;
using ToyRobot.Helpers;
using ToyRobot.Models;
using ToyRobot.Providers;
using ToyRobot.Services;

namespace ToyRobot
{
    ///<summary>
    /// Entry point of the application
    ///</summary>
    ///<remarks>
    /// The application is a simulation of a toy robot moving on a square tabletop, of dimensions 5 units x 5 units.
    /// There are no other obstructions on the table surface.
    /// The robot is free to roam around the surface of the table, but must be prevented from falling to destruction.Any movement that would result in the robot falling from the table must be prevented, however further valid movement commands must still be allowed.
    ///</remarks>
    public class Program
    {
        public static Map Map;
        
        private static IApplicationService _applicationService;
        private static IProvider<Receiver> _receiverProvider;

        public static void Main(string[] args)
        {
            var mapWidth = ConfigHelper.LoadOrDefault(CONSTANTS.TEXTS.CONFIG_MAP_WIDTH, CONSTANTS.NUMBERS.TABLETOP_WIDTH);
            var mapLength = ConfigHelper.LoadOrDefault(CONSTANTS.TEXTS.CONFIG_MAP_LENGTH, CONSTANTS.NUMBERS.TABLETOP_WIDTH);
            Map = new TableTop(mapWidth, mapLength);

            _receiverProvider = new ReceiverProvider();
            _applicationService = new ApplicationService(Map, _receiverProvider.Provide());

            /*
             * handle file & keyboard input differently
             * Input can be from a file, or from standard input, as the developer chooses.
             */
            if (args.Length > 2 && string.Equals(args[0], CONSTANTS.TEXTS.ARGUMENT_FILE))
            {
                HandleFile(args);
            }
            else
            {
                HandleKeyboard();
            }
        }

        ///<summary>
        /// handle file input, pass the logic to application service layer
        ///</summary>    
        static void HandleFile(string[] args)
        {
            var file = args[1];

            Console.WriteLine("Reading file from : " + file + "\n");
            try
            {
                var lines = System.IO.File.ReadAllLines(file);

                foreach (var line in lines)
                {
                    _applicationService.Process(line);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error when reading file: " + ex.Message);
            }
        }

        ///<summary>
        /// handle keyboard inputs, pass the logic to application service layer
        ///</summary>    
        ///<remarks>
        /// command : -f "filename.txt" 
        ///</remarks>
        static void HandleKeyboard()
        {
            var success = false;

            string input;
            do
            {
                if (!success)
                    Console.WriteLine(CONSTANTS.TEXTS.WELCOME);

                input = Console.ReadLine();
                success = _applicationService.Process(input);

            } while (!string.Equals(input, CONSTANTS.TEXTS.COMMAND_EXIT));
        }
    }
}
