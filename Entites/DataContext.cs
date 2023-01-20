using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace asp_book.Entities
{
    public class DataContext
    {
        private readonly string jsonFile = "data.json";
        public SchoolData schoolData;

        public DataContext()
        {
            DeserializeData();
        }

        public void DeserializeData()
        {
            if (File.Exists(jsonFile))
            {
                var jsonData = File.ReadAllText(jsonFile);
                if (String.IsNullOrEmpty(jsonData))
                {
                    schoolData = new SchoolData();
                    return;
                }

                try
                {
                    schoolData = JsonSerializer.Deserialize<SchoolData>(jsonData);
                    CheckDataCorrectness();
                }
                catch (JsonException)
                {
                    schoolData = new SchoolData();
                }
            }
            else
            {
                File.Create(jsonFile);
                schoolData = new SchoolData(); ;
                SerializeData();
            }
        }

        private void CheckDataCorrectness()
        {
            if (schoolData.Lessons == null || schoolData.Rooms == null || schoolData.Teachers == null || schoolData.Groups == null || schoolData.Activities == null)
            {
                schoolData = new SchoolData();
            }
        }

        public void SerializeData()
        {
            var jsonData = JsonSerializer.Serialize<SchoolData>(schoolData);
            File.WriteAllText(jsonFile, jsonData);
        }

        public ActivityData getActivity(string room, int slot, string day)
        {
            foreach (var activity in schoolData.Activities)
            {
                if (activity.BuildingofClass == room && activity.Slot == slot && activity.DayofClass == day)
                    return activity;
            }

            return null;
        }

        public bool RemoveActivity(string room, int slot, string day)
        {
            foreach (var activity in schoolData.Activities)
            {
                if (activity.BuildingofClass == room && activity.Slot == slot && activity.DayofClass == day)
                {
                    schoolData.Activities.Remove(activity);
                    SerializeData();
                    return true;
                }
            }

            return false;
        }

        public void AddActivity(string room, int slot, string day, string group, string lesson, string teacher)
        {
            ValidateNewActivity(room, slot, day, group, lesson, teacher);

            // Removes activity which is currently assigned to the edited slot, room and day so as data could be properly owerriten (otherwise the edited entry could be assigned to more than one activity). In case when the slot is being added new activity nothing will be removed.
            RemoveActivity(room, slot, day);
            schoolData.Activities.Add(new ActivityData(room, slot, day, group, lesson, teacher));
            SerializeData();
        }

        private bool ValidateNewActivity(string room, int slot, string day, string group, string clas, string teacher)
        {
            foreach (var activity in schoolData.Activities.ToList())
            {
                if (activity.BuildingofClass != room && activity.DayofClass == day && activity.Slot == slot && (activity.Group == group || activity.Teacher == teacher))
                    schoolData.Activities.Remove(activity);
            }
            return true;
        }

        public void SaveDictionary(string dictionaryName, List<string> dictionaryItems)
        {
            switch (dictionaryName)
            {
                case "Teachers":
                    schoolData.Teachers = dictionaryItems;
                    break;
                case "Rooms":
                    schoolData.Rooms = dictionaryItems;
                    break;
                case "Lessons":
                    schoolData.Lessons = dictionaryItems;
                    break;
                case "Groups":
                    schoolData.Groups = dictionaryItems;
                    break;
                default:
                    break;
            }
            SerializeData();
        }

        public void EditDictionaryItem(string dictionaryName, string oldItem, string newItem)
        {
            switch (dictionaryName)
            {
                case "Teachers":
                    EditTeacher(oldItem, newItem);
                    break;
                case "Rooms":
                    EditRoom(oldItem, newItem);
                    break;
                case "Lessons":
                    EditClass(oldItem, newItem);
                    break;
                case "Groups":
                    EditGroup(oldItem, newItem);
                    break;
                default:
                    break;
            }
            SerializeData();
        }

        public void AddDictionaryItem(string dictionaryName, string newItem)
        {
            switch (dictionaryName)
            {
                case "teachers":
                    AddTeacher(newItem);
                    break;
                case "Rooms":
                    AddRoom(newItem);
                    break;
                case "Lessons":
                    AddClass(newItem);
                    break;
                case "Groups":
                    AddGroup(newItem);
                    break;
                default:
                    break;
            }
            SerializeData();
        }


        private void AddTeacher(string newTeacher)
        {
            if (schoolData.Teachers.Contains(newTeacher))
                return;
            else
                schoolData.Teachers.Add(newTeacher);
        }

        private void AddClass(string newClass)
        {
            if (schoolData.Lessons.Contains(newClass))
                return;
            else
                schoolData.Lessons.Add(newClass);
        }

        private void AddRoom(string newRoom)
        {
            if (schoolData.Rooms.Contains(newRoom))
                return;
            else
                schoolData.Rooms.Add(newRoom);
        }

        private void AddGroup(string newGroup)
        {
            if (schoolData.Groups.Contains(newGroup))
                return;
            else
                schoolData.Groups.Add(newGroup);
        }


        public void RemoveDictionaryItem(string dictionaryName, string item)
        {
            switch (dictionaryName)
            {
                case "Teachers":
                    schoolData.Teachers.Remove(item);
                    RemoveTeacherActivities(item);
                    break;
                case "Rooms":
                    schoolData.Rooms.Remove(item);
                    RemoveRoomActivities(item);
                    break;
                case "Lessons":
                    schoolData.Lessons.Remove(item);
                    RemoveClassActivities(item);
                    break;
                case "Groups":
                    schoolData.Groups.Remove(item);
                    RemoveGroupActivities(item);
                    break;
                default:
                    break;
            }
            SerializeData();
        }

        public SelectList GetGroups()
        {
            IEnumerable<SelectListItem> groupsItems = schoolData.Groups.Select(m => new SelectListItem { Text = m, Value = m });
            return new SelectList(groupsItems, "Value", "Text");
        }

        public SelectList GetTeachers()
        {
            IEnumerable<SelectListItem> teacherItems = schoolData.Teachers.Select(m => new SelectListItem { Text = m, Value = m });
            return new SelectList(teacherItems, "Value", "Text");
        }

        public SelectList GetClasses()
        {
            IEnumerable<SelectListItem> classesItems = schoolData.Lessons.Select(m => new SelectListItem { Text = m, Value = m });
            return new SelectList(classesItems, "Value", "Text");
        }

        public SelectList GetRooms()
        {
            IEnumerable<SelectListItem> roomsItems = schoolData.Rooms.Select(m => new SelectListItem { Text = m, Value = m });
            return new SelectList(roomsItems, "Value", "Text");
        }

        private void EditTeacher(string oldTeacherName, string newTeacherName)
        {
            if (!schoolData.Teachers.Contains(oldTeacherName) || schoolData.Teachers.Contains(newTeacherName))
                return;

            schoolData.Teachers[schoolData.Teachers.FindIndex(i => i.Equals(oldTeacherName))] = newTeacherName;

            foreach (var activity in schoolData.Activities)
            {
                if (activity.Teacher == oldTeacherName)
                    activity.Teacher = newTeacherName;
            }
        }

        private void EditRoom(string oldRoomName, string newRoomName)
        {
            if (!schoolData.Rooms.Contains(oldRoomName) || schoolData.Rooms.Contains(newRoomName))
                return;

            schoolData.Rooms[schoolData.Rooms.FindIndex(i => i.Equals(oldRoomName))] = newRoomName;

            foreach (var activity in schoolData.Activities)
            {
                if (activity.BuildingofClass == oldRoomName)
                    activity.BuildingofClass = newRoomName;
            }
        }

        private void EditClass(string oldClassName, string newClassName)
        {
            if (!schoolData.Lessons.Contains(oldClassName) || schoolData.Lessons.Contains(newClassName))
                return;

            schoolData.Lessons[schoolData.Lessons.FindIndex(i => i.Equals(oldClassName))] = newClassName;

            foreach (var activity in schoolData.Activities)
            {
                if (activity.Teacher == oldClassName)
                    activity.Teacher = newClassName;
            }
        }

        private void EditGroup(string oldGroupName, string newGroupName)
        {
            if (!schoolData.Groups.Contains(oldGroupName) || schoolData.Groups.Contains(newGroupName))
                return;

            schoolData.Groups[schoolData.Groups.FindIndex(i => i.Equals(oldGroupName))] = newGroupName;

            foreach (var activity in schoolData.Activities.ToList())
            {
                if (activity.Group == oldGroupName)
                    activity.Group = newGroupName;
            }
        }

        private void RemoveTeacherActivities(string teacher)
        {
            foreach (var activity in schoolData.Activities.ToList())
            {
                if (activity.Teacher == teacher)
                    schoolData.Activities.Remove(activity);
            }
        }

        private void RemoveRoomActivities(string room)
        {
            foreach (var activity in schoolData.Activities.ToList())
            {
                if (activity.BuildingofClass == room)
                    schoolData.Activities.Remove(activity);
            }
        }

        private void RemoveClassActivities(string lesson)
        {
            foreach (var activity in schoolData.Activities.ToList())
            {
                if (activity.Lesson == lesson)
                    schoolData.Activities.Remove(activity);
            }
        }

        private void RemoveGroupActivities(string group)
        {
            foreach (var activity in schoolData.Activities.ToList())
            {
                if (activity.Group == group)
                    schoolData.Activities.Remove(activity);
            }
        }

    }
}