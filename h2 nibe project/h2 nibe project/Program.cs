﻿using System;
using System.Collections.Generic;
using System.IO;
namespace h2_nibe_project
{
    #region data controllers etc.
    class Stands
    {
        public string BodId { get; set; }
        public string Name { get; set; }
        public string SwitchAmount { get; set; }
        public string[] SwitchId { get; set; }
    }
    class NetworkSwitch
    {
        public string SwitchId { get; set; }
        public string PortAmount { get; set; }   
    }


    class SetupList
    {
        public List<NetworkSwitch> switchSetup = new List<NetworkSwitch>();
        public List<Stands> standSetup = new List<Stands>();
        public void AddSwitch(NetworkSwitch @switch)
        {
            switchSetup.Add(@switch);
        }
        public void RemoveSwitch(NetworkSwitch @switch)
        {
            switchSetup.Remove(@switch);
        }
        public void AddStand(Stands stand)
        {
            standSetup.Add(stand);
        }
        public void RemoveStand(Stands stand)
        {
            standSetup.Remove(stand);
        }
        
        public List<NetworkSwitch> GetSwitches()
        {
            return switchSetup;
        }

        public List<Stands> GetStands()
        {
            return standSetup;
        }
    }
    class Logging
    {
        readonly public string username = Environment.UserName;
        public void LogToFile(string text)
        {
            Directory.CreateDirectory("C:\\Users\\" + username + "\\Desktop\\nibe_log");
        
            File.AppendAllText("C:\\Users\\" + username + "\\Desktop\\nibe_log\\Log.txt", text);
        }
    }
    class SetupController
    {
        Logging log = new Logging();
        public SetupList setup = new SetupList();
        public void CreateSwitch(string id, string port)
        {
            NetworkSwitch @switch = new NetworkSwitch
            {
                SwitchId = id,
                PortAmount = port
            };
            log.LogToFile(DateTime.Now + " " + log.username + " Added switch with id " + id + " and port amount " + port + "\n");
            setup.AddSwitch(@switch);
        }
        public void CreateStand(string name, string bodId, string amount, string[] switchId)
        {
            string id = "";
            Stands stand = new Stands
            {
                Name = name,
                BodId = bodId,
                SwitchAmount = amount,
                SwitchId = switchId
            };
            foreach (string x in switchId)
            {
                id += x + " ";
            }
            log.LogToFile(DateTime.Now + " " + log.username + " Added stand with name " + name + " switch amount " + amount + " and switch id's " + id + "\n");
            setup.AddStand(stand);
        }
        public void RemoveStand(Stands stand)
        {
            setup.RemoveStand(stand);
        }
        public void RemoveSwitch(NetworkSwitch @switch)
        {
            setup.RemoveSwitch(@switch);
        }
    }
    #endregion
    class UserInput
    {
        protected int countSwitch = 1;
        protected int output;
        SetupController Controller = new SetupController();
        protected string input;
        public void CheckForNumber()
        {
            while(!Int32.TryParse(input, out output))
            {
                Console.WriteLine("Must be a number");
                input = Console.ReadLine();
            }
        }
        public void Clear()
        {
            Console.Clear();
        }
        public void Start()
        {
            Console.WriteLine("Press 1 to create or 2 to show");
            input = Console.ReadLine();
            CheckForNumber();
            if(input == "1")
            {
                input = "";
                Console.WriteLine("Press 1 to create a stand or press 2 to create a switch");
                input = Console.ReadLine();
                if(input == "1")
                {
                    Clear();
                    ShowAddStand();
                }
                else if(input == "2")
                {
                    Clear();
                    ShowAddSwitch();
                }
            }
            else if(input == "2")
            {
                input = "";
                Console.WriteLine("Press 1 to show stands or press 2 to show switches");
                input = Console.ReadLine();
                if(input == "1")
                {
                    Clear();
                    ShowStands();
                    Console.WriteLine("If you want to edit one of the elements press its number else press enter");

                }
                else if(input == "2")
                {
                    Clear();
                    ShowSwitch();
                    Console.WriteLine("If you want to edit one of the elements press its number else press enter");
                    if (Int32.TryParse(input, out output))
                    {
                        output = -1;

                    }
                }
            }
            
            Console.ReadKey(true);
            Start();
        }
        public void ShowAddStand()
        {
            Console.WriteLine("Write the following with your own input");
            Console.WriteLine("Id, name, amount of switches, id of the switches(this should be comma seperated without spaces if more than 1");
            string standInfo = Console.ReadLine();

            string[] standSplit = standInfo.Split(" ", 4);
            try
            {
                string[] idSplit = standSplit[3].Split(",");
                Controller.CreateStand(standSplit[0], standSplit[1], standSplit[2], idSplit);

            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Not enough parameters");
            }

        }
        public void ShowAddSwitch()
        {
            Console.WriteLine("Write the following with your own input");
            Console.WriteLine("Id, amount of ports");

            string switchInfo = Console.ReadLine();

            try
            {
                string[] switchSplit = switchInfo.Split(" ", 2);
                Controller.CreateSwitch(switchSplit[0], switchSplit[1]);
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Not enough parameters");
            }
        }
        public void ShowRemoveSwitch()
        {
            

        }
        public void ShowStands()
        {
            string id = "";
      
            List<Stands> stand = Controller.setup.GetStands();
            foreach(Stands i in stand)
            {
                foreach(string x in i.SwitchId)
                {
                    id += x + " ";
                }
                Console.WriteLine(i.BodId + " " + i.Name + " " + i.SwitchAmount + " " + id);
                id = "";
            }
        }
        public void ShowSwitch()
        {
            
            List<NetworkSwitch> @switch = Controller.setup.GetSwitches();
            foreach (NetworkSwitch i in @switch)
            {
                Console.WriteLine(countSwitch + " " + i.SwitchId + " " + i.PortAmount);
                countSwitch++;
            }
        }
    }
}
