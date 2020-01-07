using System;
using System.Collections.Generic;
namespace h2_nibe_project
{

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
    class SetupController
    {
        public SetupList setup = new SetupList();
        public void CreateSwitch(string id, string port)
        {
            NetworkSwitch @switch = new NetworkSwitch
            {
                SwitchId = id,
                PortAmount = port
            };

            setup.AddSwitch(@switch);
        }
        public void CreateStand(string name, string bodId, string amount, string[] switchId)
        {
            Stands stand = new Stands
            {
                Name = name,
                BodId = bodId,
                SwitchAmount = amount,
                SwitchId = switchId
            };

            setup.AddStand(stand);
        }
    }
    class UserInput
    {
        protected int output;
        SetupController Controller = new SetupController();
        protected string input;
        public void Start()
        {
            Console.WriteLine("Press 1 to create or 2 to show");
            input = Console.ReadLine();

            Console.WriteLine("Press 1 to create a stand and press 2 to create a switch");
            input = Console.ReadLine();
            while(!Int32.TryParse(input, out output))
            {
                Console.WriteLine("Must be a number");
                input = Console.ReadLine();

            }
            if(input == "1")
            {
                ShowAddStand();
            }
            Console.ReadKey();
        }
        public void ShowAddStand()
        {

            Console.WriteLine("Write the following with your own input/n");
            Console.WriteLine("Name, Id, amount of switches, id of the switches(this should be comma seperated without spaces if more than 1");
            string standInfo = Console.ReadLine();

            string[] standSplit = standInfo.Split(" ", 4);
            string[] idSplit = standSplit[3].Split(",");

            Controller.CreateStand(standSplit[0], standSplit[1], standSplit[2], idSplit);
        }
        public void show_stands()
        {
            List<Stands> stand = Controller.setup.GetStands();
            foreach(Stands i in stand)
            {
                Console.WriteLine(i);
            }
        }
    }
  
}
