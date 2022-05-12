# lab1-SD125
You will be provided with a simple premade system for tracking a car park. This program only has two classes, `Vehicle` and `VehicleTracker`. `VehicleTracker` has a `VehicleList Dictionary` with the key as the parking spot number and the value as the parked vehicle. 

When a customer wants to park, the `VehicleTracker.AddVehicle(Vehicle vehicleToPark)` method is called. It selects the first null value in the `VehicleList` and replaces it with the vehicle. 


You'll find the source code at the bottom of this text file.



Program Method Examples

/* declare a new VehicleTracker class, with a capacity for 10 cars, on 123 Fake St.

Construct a Dictionary of 10 key-value pairs {number, parked car}, where parked car is null and values are 1 - 10 */

VehicleTracker vt = new VehicleTracker(10, “123 Fake st”);

// declare a new Vehicle with the licence place “A01 T22”, and with a parking pass (bool true)

Vehicle customerOne = new Vehicle(“A01 T22”, true);

/* Add a vehicle to vt’s VehicleList property. It replaces the first “unoccupied” value in VehicleList, so we expect vt.VehicleList = {{1, customerOne}, {2, null}, {3, null}, etc} */

vt.AddVehicle(customerOne);

// Change the value of slot 1 in VehicleList to null 

vt.RemoveVehicle(“A0T T22”);

Task: Unit Testing

Your job is to write a number of unit tests for Vehicle Tracker, and correct those methods which are not functioning properly.

This class is very simple. Feel free to look at the properties and method signatures on the project, but try to avoid reading into the method bodies. Instead, try to find bugs solely through unit testing. Of course, once you have determined if a method is correct or not feel free to modify them.

When initialized, a VehicleTracker object should have empty slots [{SlotNumber, Vehicle}] from 1 - Capacity in VehicleTracker.VehicleList (ie. { {1, null}, {2, null}, {3,null}, //etc}

If the AddVehicle method is called, it should add the vehicle to the first slot in VehicleList that is not full. If there are no open slots, it should throw a generic exception with the VehicleTracker.AllSlotsFull message.

RemoveVehicle should accept either a licence or slot number, and set that slot’s vehicle to “null”.

RemoveVehicle should throw an exception if the licence passed to RemoveVehicle() is not found, if the slot number is invalid (greater than capacity or negative), or the slot is not filled.

VehicleTracker should track the proper number of slots available at all times with VehicleTracker.SlotsAvailable.

The VehicleTracker.ParkedPassholders() method should return a list of all parked vehicles that have a pass.

VehicleTracker.PassholderPercentage() method should return the percentage of vehicles that are parked which have passes. Note that this method uses the ParkedPassholders() method to get a count of passholders.

Try to follow this series of steps for each test:

Begin writing a single unit test. Create the conditions required to run the methods that are being tested (ie. initialize the class, provide values, etc).

Make an assertion on what you think the test should do (ie. “Assert that ‘RemoveAllCars’ sets all slots in VehicleTracker.VehicleList to null”).

Run your test. If it passes, ensure that your test is looking for exactly what you want, and ensure that it will fail if it finds any problems in the method.

If your test fails, check your test, and then the method you are testing. Use the test messages to find out why the method does not work properly.


The Program

class Program
    {
        static void Main(string[] args)
        {

        }
    }

    public class Vehicle
    {
        public string Licence { get; set; }
        public bool Pass { get; set; }
        public Vehicle(string licence, bool pass)
        {
            this.Licence = licence;
            this.Pass = pass;
        }
    }

    public class VehicleTracker
    {
        //PROPERTIES
        public string Address { get; set; }
        public int Capacity { get; set; }
        public int SlotsAvailable { get; set; }
        public Dictionary<int, Vehicle> VehicleList { get; set; }

        public VehicleTracker(int capacity, string address)
        {
            this.Capacity = capacity;
            this.Address = address;
            this.VehicleList = new Dictionary<int, Vehicle>();

            this.GenerateSlots();
        }

        // STATIC PROPERTIES
        public static string BadSearchMessage = "Error: Search did not yield any result.";
        public static string BadSlotNumberMessage = "Error: No slot with number ";
        public static string SlotsFullMessage = "Error: no slots available.";

        // METHODS
        public void GenerateSlots()
        {
            for (int i = 0; i <= this.Capacity; i++)
            {
                this.VehicleList.Add(i, null);
            }
        }

        public void AddVehicle(Vehicle vehicle)
        {
            foreach (KeyValuePair<int, Vehicle> slot in this.VehicleList)
            {
                if (slot.Value == null)
                {
                    this.VehicleList[slot.Key] = vehicle;
                    this.SlotsAvailable++;
                    return;
                }
            }
            throw new IndexOutOfRangeException(SlotsFullMessage);
        }

        public void RemoveVehicle(string licence)
        {
            try
            {
                int slot = this.VehicleList.First(v => v.Value.Licence == licence).Key;
                this.SlotsAvailable--;
                this.VehicleList[slot] = null;
            }
            catch
            {
                throw new NullReferenceException(BadSearchMessage);
            }
        }

        public bool RemoveVehicle(int slotNumber)
        {
            if (slotNumber > this.Capacity)
            {
                return false;
            }
            this.VehicleList[slotNumber] = null;
            this.SlotsAvailable++;
            return true;
        }

        public List<Vehicle> ParkedPassholders()
        {
            List<Vehicle> passHolders = new List<Vehicle>();
            passHolders.Add(this.VehicleList.FirstOrDefault(v => v.Value.Pass).Value);
            return passHolders;
        }

        public int PassholderPercentage()
        {
            int passHolders = ParkedPassholders().Count();
            int percentage = (passHolders / this.Capacity) * 100;
            return percentage;
        }
    }

}
