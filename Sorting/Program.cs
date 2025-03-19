using System;
using System.IO;

namespace Sorting
{
    class Program
    {
        static int compCount;
        static int swapCount;
        static Boolean optimised = false;
        static string[] names;
        string[] temp;
        
        static void Main(string[] args)
        {
            
            welcomeMessage();
            string[] names = CreateArray();
            string[] temp = new string[names.Length];
            Array.Copy(names, temp, names.Length);
            var watch = new System.Diagnostics.Stopwatch();//storing elapsed time using the built in diagnostics

            while (true) //loop for ever OR until 7 chosen
            {
                switch (menu())
                {
                    case 1://print the array
                        {
                            print_array(temp);
                            break;
                        }
                    case 2:
                        {
                            watch.Start();  
                            SpinAnimation.Start();
                            insertionSort(temp);
                            watch.Stop();//stop timer
                            SpinAnimation.Stop();
                            Array.Copy(names,temp,names.Length);//reload unsorted data
                            break;
                        }
                    case 3:
                        {
                            watch.Start();
                            SpinAnimation.Start();
                            bubbleSort(temp);
                            watch.Stop();//stop timer
                            SpinAnimation.Stop();
                            Array.Copy(names, temp, names.Length);//reload unsorted data
                            break;
                        }
                    case 4:
                        {
                            watch.Start();
                            SpinAnimation.Start();
                            MergeSort(temp, 0, temp.Length - 1);
                            watch.Stop();
                            SpinAnimation.Stop();
                            Array.Copy(names, temp, names.Length);
                            break;
                        }
                    case 5:
                        {
                            optimised = true;
                            watch.Start();
                            SpinAnimation.Start();
                            bubbleSort(temp);
                            watch.Stop();//stop timer
                            SpinAnimation.Stop();
                            Array.Copy(names, temp, names.Length);//reload unsorted data
                            break;
                        }
                    case 6:
                        {
                            watch.Start();
                            SpinAnimation.Start();
                            quickSort(temp,0,temp.Length-1);
                            watch.Stop();
                            SpinAnimation.Stop();
                            Array.Copy(names, temp, names.Length);//reload unsorted data
                            break;
                        }
                        case 7:
                        {
                             names = CreateArray();
                             temp = new string[names.Length];
                            Array.Copy(names, temp, names.Length);
                            break;
                        }
                    case 8:
                        {
                            Environment.Exit(0);
                            break;
                        }
                    default:
                        {
                            break;
                        }

                }
                Console.WriteLine($"\n Execution Time: {watch.ElapsedMilliseconds} ms \n");
                Console.WriteLine("Number of comparisons " + compCount);
                Console.WriteLine("Number of swaps " + swapCount);    
                watch.Reset();
                compCount = 0;
                swapCount = 0;
            }


        }
        
        static int menu()
        {
            int choice = 0;
            while (choice < 1 || choice > 7)
            {
                Console.WriteLine("1. Print the array so you can see it's random");
                Console.WriteLine("2. Insertion Sort");
                Console.WriteLine("3. Bubble Sort");
                Console.WriteLine("4. Merge Sort");
                Console.WriteLine("5. Optimised Bubble Sort");
                Console.WriteLine("6. Quick Sort");
                Console.WriteLine("7. Generate a new data set");
                Console.WriteLine("8. Exit");
                try
                { choice = Int32.Parse(Console.ReadLine()); }
                catch
                {
                    Console.WriteLine("Try again, this time please enter a number. Enter to continue.");
                    Console.ReadLine();
                }
                Console.Clear();
            }
            return choice;
        }
        static void welcomeMessage()
        {
            Console.WriteLine("The program will generate an array of random names. You can then choose different sorting algoritms to apply ");
            Console.WriteLine("Very large numbers (greater than 50,000) may take a long time to sort");
            
        }
        static string[] CreateArray()
        {
            int choice;
            int qty = 0;
            Console.WriteLine("How many names do you want (minimum 10, maximum 1000,000)?");
            Console.WriteLine("If you choose a value outside this you will get 10 names");

            try { qty = Int32.Parse(Console.ReadLine()); }

            catch { Console.WriteLine("OK, you get 10 names \n"); }

            if (qty < 10 || qty > 1000000)
            {
                Console.WriteLine("OK, you get 10 names \n");
                qty = 10; 
            }
            string[] names = new string[qty];
            Random randFirst = new Random();
            Random randLast = new Random();
            var firstLines = File.ReadAllLines("../../../Resources/firstNames.txt");
            var lastLines = File.ReadAllLines("../../../Resources/surNames.txt");
            for (int i = 0; i < qty; i++)
            {
                names[i] = firstLines[randFirst.Next(0, firstLines.Length)] + " " + lastLines[randLast.Next(0, lastLines.Length)];
            }
            Console.WriteLine("How do you want your names ordered?");
            Console.WriteLine("1. Random");
            Console.WriteLine("2. In order");
            Console.WriteLine("3. Reverse order");
            try { choice = Int32.Parse(Console.ReadLine()); }
            catch { Console.WriteLine("OK Random order \n");
                choice = 1;
            }
            switch(choice)
            {
                case 1:break;
                case 2: Array.Sort(names); break;
                case 3: Array.Sort(names); Array.Reverse(names);break;
                default:Console.WriteLine("Random then");break;

            }
            

            return names;
        }
        static void print_array(string[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(i.ToString() + ". " + arr[i]);
            }
        }
        static Boolean compareStrings(string str1, string str2) //returns true if str1 < str2
        {
            compCount++;
            int comp = string.Compare(str1, str2);
            if (comp == -1)
            { return true; }
            else
            { return false; }
        }
        //QUICK SORT ...O(log n)
        static void quickSort(string[] arr, int low, int high)
        {
            if(low<high)
            {

                // pi is partitioning index, arr[p]
                // is now at right place
                int pi = partition(arr, low, high);

                // Separately sort elements before
                // partition and after partition
                quickSort(arr, low, pi - 1);
                quickSort(arr, pi + 1, high);
            }
        }
        static int partition(string[] arr, int low, int high)
        {
            // pivot
            string pivot = arr[high];

            // Index of smaller element and
            // indicates the right position
            // of pivot found so far
            int i = (low - 1);

            for (int j = low; j <= high - 1; j++)
            {

                // If current element is smaller
                // than the pivot
                if (compareStrings(arr[j] , pivot))
                {

                    // Increment index of
                    // smaller element
                    i++;
                    swap(arr, i, j);
                }
            }
            swap(arr, i + 1, high);
            return (i + 1);
        }
        static void swap(string[] arr, int i, int j)
        {   swapCount++;
            string temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
        // BUBBLE SORT...O(n^2) because of the nested loops
        static void bubbleSort(string[] arr)
        {

            string temp;
            for (int i = 0; i <= arr.Length - 2; i++)//iterate through the array

            {
                Boolean swapped = false; //only used in the optimised version
                int opt = 0;
                if (optimised) { opt = i; }
                for (int j = 0; j <= arr.Length - 2 - opt; j++) // optimisation - reducing the step each time to avoid looking at bits which we already sorted
                {
                    if (compareStrings(arr[j + 1], arr[j]))
                    {
                        temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                        swapCount++;
                        swapped = true; //only used in the optimised version
                    }

                }
                if(!swapped && optimised) //check to see if the optimised version is being used
                    
                {
                    optimised = false; //reset the boolean
                    return; //we're done - return
                }
            }
        }
        //INSERTION SORT...O(n^2) because of the nested loops
        static void insertionSort(string[] arr)
        {

            string temp; //used for our value that we are going to swap
            Boolean done; // a flag to indicate if we have put the item in the correct place. We can stop when done
            for (int i = 1; i < arr.Length; i++) //NOTE we start at item 1 because item 0 is considered to be in the correct place already
            {
                temp = arr[i];
                done = false;
                for (int j = i - 1; j >= 0 && !done; j--) //loop as long as we still have items to compare (j hasn't reached -1) and we are not done comparing
                {
                    if (compareStrings(temp, arr[j])) //is the current item smaller than the next item in the j loop
                    {
                        arr[j + 1] = arr[j]; //swap - note one less step here then bubble sort because we already put the current item in temp at line 41
                        swapCount++;
                        arr[j] = temp;

                    }
                    else
                    {
                        done = true; //if the current item is bigger than the current item in the j loop it is in the right place and we are done
                    }
                }
            }
        }
        //MERGE SORT .....O(nlog n)
       static public string[] MergeSort(string[] array, int left, int right)//Merge sort recursive routine- needs MergeArray to rebuild.
        {
            if (left < right)
            {
                int middle = left + (right - left) / 2;

                MergeSort(array, left, middle);
                MergeSort(array, middle + 1, right);

                MergeArray(array, left, middle, right);
            }

            return array;
        }
       static public void MergeArray(string[] array, int left, int middle, int right)
        {
            var leftArrayLength = middle - left + 1;
            var rightArrayLength = right - middle;
            var leftTempArray = new string[leftArrayLength];
            var rightTempArray = new string[rightArrayLength];
            int i, j;

            for (i = 0; i < leftArrayLength; ++i)
                leftTempArray[i] = array[left + i];
            for (j = 0; j < rightArrayLength; ++j)
                rightTempArray[j] = array[middle + 1 + j];

            i = 0;
            j = 0;
            int k = left;

            while (i < leftArrayLength && j < rightArrayLength)
            {
                if (compareStrings(leftTempArray[i], rightTempArray[j]))
                {
                    array[k++] = leftTempArray[i++];
                }
                else
                {
                    array[k++] = rightTempArray[j++];
                }
            }

            while (i < leftArrayLength)
            {
                array[k++] = leftTempArray[i++];
            }

            while (j < rightArrayLength)
            {
                array[k++] = rightTempArray[j++];
            }
        }

    }
}