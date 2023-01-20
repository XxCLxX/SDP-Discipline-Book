using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using asp_book.Entities;

namespace asp_book.Models
{
    public class SchoolPlannerViewModel
    {
        public const string EMPTY_ENTRY = " ";
        public string currentRoom { get; set; }
        public SchoolData roomData;

        public string GetGroup(string room, int slot, string day)
        {
            foreach (var data in roomData.Activities)
            {
                if (data.Lesson == room && data.Slot == slot && data.DayofClass == day)
                    return data.Group;
            }

            return EMPTY_ENTRY;
        }

    }
}
