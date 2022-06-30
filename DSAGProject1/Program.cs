using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace DSAGProject1
{
    class Attendance
    {
        public int intTime;

        public string strName;

        public int intID;

        public string strRemarks;

        public Attendance(int t, string n, int ID, string r)
        {
            strName = n;
            intTime = t;
            intID = ID;
            strRemarks = r;
        }

    }
    class Program
    {
        static int partitionID(Attendance[] arr, int low, int high)
        {
            // pivot - Element at right most position
            int pivot = arr[high].intID;
            int i = (low - 1);  // Index of smaller element
            for (int j = low; j <= high - 1; j++)
            {
                // If current element is smaller than the pivot, swap the element with pivot
                if (arr[j].intID < pivot)
                {
                    i++;    // increment index of smaller element

                    (arr[i], arr[j]) = (arr[j], arr[i]);
                }
            }
            (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);
            return (i + 1);
        }

        static void quickSortID(Attendance[] arr, int low, int high)
        {
            if (low < high)
            {
                // pivot_index is partitioning index, arr[pivot_index] is now at correct place in sorted array
                int pivot_index = partitionID(arr, low, high);

                quickSortID(arr, low, pivot_index - 1);  // Before pivot_index
                quickSortID(arr, pivot_index + 1, high); // After pivot_index
            }
        }

        static int partitionTime(Attendance[] arr, int low, int high)
        {
            // pivot - Element at right most position
            int pivot = arr[high].intTime;
            int i = (low - 1);  // Index of smaller element
            for (int j = low; j <= high - 1; j++)
            {
                // If current element is smaller than the pivot, swap the element with pivot
                if (arr[j].intTime < pivot)
                {
                    i++;    // increment index of smaller element

                    (arr[i], arr[j]) = (arr[j], arr[i]);
                }
            }
            (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);
            return (i + 1);
        }

        static void quickSortTime(Attendance[] arr, int low, int high)
        {
            if (low < high)
            {
                // pivot_index is partitioning index, arr[pivot_index] is now at correct place in sorted array
                int pivot_index = partitionTime(arr, low, high);

                quickSortTime(arr, low, pivot_index - 1);  // Before pivot_index
                quickSortTime(arr, pivot_index + 1, high); // After pivot_index
            }
        }
        public static string ArrayBinarySearch(Attendance[] data, int item)
        {
            int min = 0;
            int max = data.Count() - 1;
            while (min <= max)   //while min is less than max
            {
                int mid = (min + max) / 2;//mid is set to middle of array
                if (item > data[mid].intID)     //if item > the element in the middle
                    min = mid + 1;        //set min to the element after the middle
                else
                    max = mid - 1;        //set min to the element before the middle 
                if (data[mid].intID == item)    //if item is equal to element in the middle
                    return data[mid].strRemarks;           //return the index when item is found

            }
            return "Student ID Not Found!";  //return -1 when item is not found
        }

        public static int IntArrayBinarySearch(Attendance[] data, int item)
        {
            int min = 0;
            int max = data.Count() - 1;
            while (min <= max)   //while min is less than max
            {
                int mid = (min + max) / 2;//mid is set to middle of array
                if (item > data[mid].intID)     //if item > the element in the middle
                    min = mid + 1;        //set min to the element after the middle
                else
                    max = mid - 1;        //set min to the element before the middle 
                if (data[mid].intID == item)    //if item is equal to element in the middle
                    return mid;           //return the index when item is found

            }
            return -1 ;  //return -1 when item is not found
        }

        public static void StudentSearchPrompt(Attendance[] data)
        {
            Console.WriteLine("Please enter the ID of the student to edit his/hers attendance.");
            int IdOfStudent = int.Parse(Console.ReadLine());
            
            Console.WriteLine("\nStudent " + IdOfStudent + " Remark: " + ArrayBinarySearch(data, IdOfStudent));

            Console.WriteLine("What would you like to change \"" + ArrayBinarySearch(data, IdOfStudent) + "\" to? ");
            string newRemark = Console.ReadLine();

            data[IntArrayBinarySearch(data, IdOfStudent)].strRemarks = newRemark;

            Console.WriteLine("Do you have anymore to edit?(Y/N)");
            string choice = Console.ReadLine();

            if(choice == "Y")
            {
                StudentSearchPrompt(data);
            }
            Console.WriteLine("All edits have been done. You may upload the attendance file now.");      
        }
        

        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:RAWATTENDANCE.txt");
            Attendance[] attendances = new Attendance[lines.Count()];

            int i = 0;
            foreach (string line in lines)
            {
                try
                {
                    string[] words = line.Split(',');
                    int time = int.Parse(words[0]);
                    string name = words[1];
                    int ID = int.Parse(words[2]);
                    string remarks = words[3];
                    attendances[i] = new Attendance(time, name, ID, remarks);
                    i++;
                }
                catch
                {
                    Console.WriteLine("Error reading file");
                }

                
            }

            while (true)
            {
                Console.WriteLine("\n(1) Sort Attendance with ID\n(2) Sort Attendance with Time\n(3) Upload Attendance\n(4) Edit Attendance\n(E) Exit Program");
                string choice = Console.ReadLine();
                
                if (choice == "E")
                {
                    break;
                }

                if (choice == "1") //Display attendance after using quicksort(id) --> Console.WriteLine attendance 
                {
                    int n = attendances.Count();
                    quickSortID(attendances, 0, n - 1);
                    Console.WriteLine("{0, -25}  {1, 15} {2, 10} {3, 25}", "Time", "Student Name", "Student ID", "Remarks");
                    foreach (Attendance item in attendances)
                    {
                        Console.WriteLine("{0, -25}  {1, 15} {2, 10} {3, 25}", item.intTime, item.strName, item.intID, item.strRemarks);
                    }
                    Console.ReadKey();
                }

                else if (choice == "2") //quicksort(time) attendance
                {
                    int n = attendances.Count();
                    quickSortTime(attendances, 0, n - 1);
                    Console.WriteLine("{0, -25}  {1, 15} {2, 10} {3, 25}", "Time", "Student Name", "Student ID", "Remarks");

                    foreach (Attendance item in attendances)
                    {
                        Console.WriteLine("{0, -25}  {1, 15} {2, 10} {3, 25}", item.intTime, item.strName, item.intID, item.strRemarks);
                    }
                    Console.ReadKey();
                }

                else if (choice == "3") //quicksort(ID) attendance --> write into new textfile --> Display on Console as well
                {
                    int n = attendances.Count();
                    quickSortID(attendances, 0, n - 1);
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(@"C:uploadedattendance.txt"))
                        {
                            sw.WriteLine("{0, -25}  {1, 15} {2, 10} {3, 25}", "Time", "Student Name", "Student ID", "Remarks");
                            foreach (Attendance line in attendances)  //loops through each element in numbers
                            {
                                sw.WriteLine("{0, -25}  {1, 15} {2, 10} {3, 25}", line.intTime, line.strName, line.intID, line.strRemarks);//write to the file
                            }
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Error writing file");
                    }
                }

                else if (choice == "4") // Quicksort(ID) attendance, user would be able to use binary search to locate student attendance status and edit it.
                                        // Will prompt user if more edit is need with recursion method
                {
                    int n = attendances.Count();
                    quickSortID(attendances, 0, n - 1);

                    StudentSearchPrompt(attendances);
                    Console.ReadKey();
                }
            }
        }
    }
}