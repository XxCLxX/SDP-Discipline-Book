using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using asp_book.Entities;
using asp_book.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace asp_book.Controllers
{
    public class PlannerController : Controller
    {
        private readonly DataContext plannerData;
        private readonly ILogger<PlannerController> _logger;

        public PlannerController(ILogger<PlannerController> logger)
        {

            this.plannerData = new DataContext();
            _logger = logger;
        }

        public IActionResult Index(string room)
        {

            SchoolPlannerViewModel schoolTimeTable = new SchoolPlannerViewModel();
            schoolTimeTable.roomData = plannerData.schoolData;

            ViewBag.Rooms = new SelectList(plannerData.GetRooms(), "Value", "Text");

            if (room != null)
                schoolTimeTable.currentRoom = room;
            else
                schoolTimeTable.currentRoom = plannerData.GetRooms().Any() ? plannerData.GetRooms().First().Value : string.Empty;

            return View(schoolTimeTable);
        }

        public IActionResult UpdateRoom(string room)
        {
            if (String.IsNullOrEmpty(room))
                return RedirectToAction("Index");
            return RedirectToAction("Index", new { room = room });
        }

        [HttpPost]
        public IActionResult SaveDictionary(List<string> items, string dictionary)
        {
            if (items != null && items.Count != 0 && String.IsNullOrEmpty(dictionary))
            {
                plannerData.SaveDictionary(dictionary, items);
                return RedirectToAction("EditDictionary", new { dictionary = dictionary });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SaveEntry(string group, string clas, string teacher, string room, int? slot, string day)
        {
            if (String.IsNullOrEmpty(room) || String.IsNullOrEmpty(day) || !slot.HasValue || String.IsNullOrEmpty(group) || String.IsNullOrEmpty(clas) || String.IsNullOrEmpty(teacher))
                return RedirectToAction(nameof(Index));

            plannerData.AddActivity(room, slot.Value, day, group, clas, teacher);
            return RedirectToAction("Index", new { room = room });
        }

        public IActionResult UnassignEntry(string room, int? slot, string day)
        {
            if (String.IsNullOrEmpty(room) || String.IsNullOrEmpty(day) || !slot.HasValue)
                return RedirectToAction("Index");

            plannerData.RemoveActivity(room, slot.Value, day);
            return RedirectToAction("Index", new { room = room });
        }

        public IActionResult EditDictionary(string dictionary)
        {
            EditDictionaryViewModel editDictionary = new EditDictionaryViewModel();

            switch (dictionary)
            {
                case "teachers":
                    editDictionary.DictionaryItem = plannerData.schoolData.Teachers;
                    break;
                case "rooms":
                    editDictionary.DictionaryItem = plannerData.schoolData.Rooms;
                    break;
                case "lessons":
                    editDictionary.DictionaryItem = plannerData.schoolData.Lessons;
                    break;
                case "groups":
                    editDictionary.DictionaryItem = plannerData.schoolData.Groups;
                    break;
                default:
                    break;
            }

            editDictionary.DictionaryName = dictionary;
            return View(editDictionary);
        }

        public IActionResult EditDictionaryItem(string dictionary, string item)
        {
            EditDictionaryItemViewModel editItem = new EditDictionaryItemViewModel();

            editItem.DictionaryName = dictionary;
            editItem.Item = item;

            return View(editItem);
        }

        public IActionResult SaveDictionaryItem(string editedItem, string item, string dictionary)
        {
            if (String.IsNullOrEmpty(item))
                return RedirectToAction("EditDictionary", new { dictionary = dictionary });

            if (String.IsNullOrEmpty(editedItem))
                plannerData.AddDictionaryItem(dictionary, item);
            else
                plannerData.EditDictionaryItem(dictionary, editedItem, item);

            return RedirectToAction("EditDictionary", new { dictionary = dictionary });
        }

        public IActionResult RemoveDictionaryItem(string item, string dictionary)
        {
            plannerData.RemoveDictionaryItem(dictionary, item);
            return RedirectToAction("EditDictionary", new { dictionary = dictionary });
        }

        public IActionResult EditEntry(string room, int? slot, string day)
        {
            if (String.IsNullOrEmpty(room) || String.IsNullOrEmpty(day) || !slot.HasValue)
                return RedirectToAction(nameof(Index));

            EditEntryViewModel editEntry = new EditEntryViewModel();

            editEntry.LessonItems = plannerData.GetClasses();
            editEntry.GroupItems = plannerData.GetGroups();
            editEntry.TeacherItems = plannerData.GetTeachers();

            ActivityData selectedActivity = plannerData.getActivity(room, slot.Value, day);

            if (selectedActivity != null)
            {
                editEntry.Lesson = selectedActivity.Lesson;
                editEntry.Group = selectedActivity.Group;
                editEntry.Teacher = selectedActivity.Teacher;
            }
            else
            {
                editEntry.Lesson = "select class";
                editEntry.Group = "select group";
                editEntry.Teacher = "select teacher";
            }

            return View(editEntry);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
