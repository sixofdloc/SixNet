using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Net_Data;
using Net_Data.Models;
using Net_Setup.Classes;

namespace Net_Setup
{
    public class MessageBaseSetup
    {
        private MessageBaseArea currentMessageBaseArea = null;
//        private MessageBase currentMessageBase = null;
        private List<MessageBaseArea> messageBaseAreas = null;
        private List<MessageBase> messageBases = null;

        private readonly BBSDataCore _core;

        public MessageBaseSetup(BBSDataCore core)
        {
            _core = core;
        }


        private void MessageBaseHeader()
        {
            messageBaseAreas = _core.MessageBaseAreas();
            messageBases = _core.MessageBases();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Setup Message Bases");
            Utils.Divider();
            Console.WriteLine((currentMessageBaseArea == null )?  "Currently in area: ROOT" : $"Current Area: ({currentMessageBaseArea?.Id}) {currentMessageBaseArea?.Title}");
            Console.WriteLine("Child Areas: ");

            if (!messageBaseAreas.Any(p => p.ParentAreaId == currentMessageBaseArea?.Id))
            {
                Console.WriteLine("None");
            }
            else
            {
                foreach (var childArea in messageBaseAreas.Where(p => p.ParentAreaId == currentMessageBaseArea?.Id))
                {
                    Console.WriteLine($"({childArea.Id}) { childArea.Title}");
                }
            }
            Console.WriteLine("Message Bases in this area: ");

            if (!messageBases.Any(p => p.MessageBaseAreaId == currentMessageBaseArea?.Id))
            {
                Console.WriteLine("None");
            }
            else
            {
                foreach (var messageBase in messageBases.Where(p => p.MessageBaseAreaId == currentMessageBaseArea?.Id))
                {
                    Console.WriteLine($"({messageBase.Id}) { messageBase.Title}");
                }
            }
            Utils.Divider();
        }

        public void SetupMessageBases()
        {
            var quitFlag = false;
            while (!quitFlag)
            {
                MessageBaseHeader();
                if (currentMessageBaseArea != null) Console.WriteLine("0. Navigate To Parent Area");
                Console.WriteLine("1. Add Child Area");
                if (currentMessageBaseArea != null)
                {
                    Console.WriteLine("2. Edit This Area");
                    Console.WriteLine("3. Delete This Area");
                }
                if (messageBaseAreas.Any(p => p.ParentAreaId == currentMessageBaseArea?.Id))
                {
                    Console.WriteLine("4. Navigate To Child Area");
                }
                Console.WriteLine("5. Add Message Base Here");
                if (messageBases.Any(p => p.MessageBaseAreaId == currentMessageBaseArea?.Id) )
                {
                    Console.WriteLine("6. Edit Message Base");
                }
                //Console.WriteLine("I. Import Here.");
                Console.WriteLine("Q. Quit To Main Menu");
                var choice = Console.ReadLine();
                switch (choice.ToString().ToUpper())
                {
                    case "0":
                        //Change to parent area
                        currentMessageBaseArea = messageBaseAreas.FirstOrDefault(p => p.Id == currentMessageBaseArea.ParentAreaId);
                        break;
                    case "1":
                        CreateMessageBaseArea();
                        break;
                    case "2":
                        //NOT IMPLEMENTED
                        break;
                    case "3":
                        //NOT IMPLEMENTED
                        break;
                    case "4":
                        int areaId = 0;
                        if (int.TryParse(Utils.Input("Enter the Id of the area to navigate to: ", ""), out areaId))
                        {
                            currentMessageBaseArea = messageBaseAreas.FirstOrDefault(p => p.Id == areaId);
                        }
                        break;
                    case "5":
                        CreateMessageBase();
                         break;
                    case "I":
                        //ImportGFiles();
                        break;
                    case "Q":
                        quitFlag = true;
                        break;
                    default:
                        break;
                }
            }

        }

        private void CreateMessageBaseArea()
        {
            MessageBaseHeader();
            Console.WriteLine("ADD NEW AREA");
            var areaTitle = Utils.Input("Enter a name for this new area", "");
            if (areaTitle != "")
            {
                var areaDescription = Utils.Input("Enter a description for this new area", "");
                if (areaDescription != "")
                {
                   var parentId = currentMessageBaseArea?.Id;
                    _core.CreateMessageBaseArea(new MessageBaseArea() { Title = areaTitle, Description = areaDescription, ParentAreaId = parentId });
                }
            }
        }

        private void CreateMessageBase()
        {
            MessageBaseHeader();
            Console.WriteLine("ADD NEW BASE");
            var baseName = Utils.Input("Enter a name for this new base", "");
            if (baseName != "")
            {
                var baseDescription = Utils.Input("Enter a description for this new base", "");
                if (baseDescription != "")
                {
                    _core.CreateMessageBase(new MessageBase() { Title = baseName, Description = baseDescription, MessageBaseAreaId = currentMessageBaseArea?.Id });
                }
            }
        }

    }
}
