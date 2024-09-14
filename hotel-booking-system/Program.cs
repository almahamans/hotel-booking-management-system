    abstract class Room{
    public int RoomNumber{get; set;}
    public double Price{get; set;}
    public bool IsAvailable{get; set;}
    public Room(int roomNumber, double price){
        RoomNumber = roomNumber;
        Price = price; 
        IsAvailable = true;
    }
    public bool CheckIsAvailable(){
        return IsAvailable;
    }
    public abstract double CalculatePrice();
    public void CheckAvailability(int roomNumber){
        if(IsAvailable && RoomNumber == roomNumber){
            Console.WriteLine($"Room {RoomNumber} is Availble");
        }else{
            Console.WriteLine($"Room {RoomNumber} is not Availble");
        }        
    }
    public void ReleaseRoom(){
        IsAvailable = true;
        Console.WriteLine($"Room {RoomNumber} is now available.");
    }
    public void BookRoom(){
        if (IsAvailable == true){
            IsAvailable = false;
            Console.WriteLine($"Room {RoomNumber} has been booked.");
        }else{
            Console.WriteLine($"Room {RoomNumber} is already booked.");
        }
    }
    public virtual string RoomType(){
        return "A Room";
    }

}
class StandardRoom : Room
{
    public StandardRoom(int roomNumber, double price) : base(roomNumber, price)
    {      
    }
    public override double CalculatePrice(){
        return Price;
    }
    public override string RoomType(){
        return "Standard Room";
    }
}
class DeluxeRoom : Room
{
    public double AdditionalFeature{get; set;}
    public DeluxeRoom(int roomNumber, double price, double additionalFeature) : base(roomNumber, price)
    {
        AdditionalFeature = additionalFeature;
    }
    public override double CalculatePrice(){
        return AdditionalFeature + Price;
    }
    public override string RoomType(){
        return "Deluxe Room";
    }
}
class SuiteRoom : Room
{
    public double ExtraFeatures{get; set;}
    public SuiteRoom(int roomNumber, double price, double extraFeatures) : base(roomNumber, price)
    {
        ExtraFeatures = extraFeatures;
    }
    public override double CalculatePrice(){
        return ExtraFeatures * Price;
    }
    public override string RoomType(){
        return "Suite Room";
    }
}
class DisplayRoom{
    public void DisplayRoomInfo(Room room){  
       if(room != null){
        Console.WriteLine($"Type: {room.RoomType()} Number:{room.RoomNumber} Price:{room.Price}");
        room.CheckAvailability(room.RoomNumber);
       }else{
        Console.WriteLine($"Cannot finf the room");
       }
    }
}
class Customer{
    public string Name{get; set;}
    public string ContactInfo{get; set;}
    static int _customerID = 0;
    public int CustomerID { get; }
    public Customer(string name, string contactInfo){
         _customerID++;
        CustomerID = _customerID;
        Name = name;
        ContactInfo = contactInfo;
    }
    public void DisplayCustomerDetails(){
        Console.WriteLine($"Customer Name:{Name} Contact Info: {ContactInfo} Customer ID: {CustomerID}");   
    }
}
class Booking{
    static int _bookingID = 0;
    public int BookingID { get; }
    public Room RoomType{get; set;}
    public Customer CustomerName{get; set;}
    public double TotalAmount{get; set;}    
    public Booking(Room roomType, Customer customerName, double totalAmount){
        _bookingID++;
        BookingID = _bookingID;
        RoomType = roomType;
        CustomerName = customerName;
        TotalAmount = totalAmount;
    }
    public Booking(){}
    public static List<Booking> bookings = new List<Booking>();
    int nights;
    DateTime checkInDate;
    DateTime checkOutDate;
    public void CalculateTotalAmount(double price, DateTime checkIn, DateTime checkOut){
        checkInDate = checkIn;
        checkOutDate = checkOut;
        nights = (checkOut - checkIn).Days;
        TotalAmount = price * nights ;
        Console.WriteLine($"{TotalAmount}"); 
    }
    public void ConfirmBooking(Booking booking){
        Console.WriteLine($"Booking Confirmed");
        bookings.Add(booking); 
        booking.RoomType.BookRoom();
        booking.DisplayBooking();  
    }
    public void DisplayBooking(){
       Console.WriteLine($"Booking ID:{BookingID} \n Customer Name:{CustomerName.Name} \n Room Type:{RoomType.RoomType()} \n Room Number:{RoomType.RoomNumber} \n Check In Date:{checkInDate} \n Check Out Date:{checkOutDate} \n Nights: {nights} \n Total Amount:{TotalAmount}");     
    }
    public void CheckAvailability(int roomn){
       if(RoomType.RoomNumber == roomn && RoomType.IsAvailable){
        Console.WriteLine($"is available");
       }else{
        Console.WriteLine($"is not available");
       }
    }
}
class CancelBooking{
    public void CancelBookingById(int id){
        Booking removeBooking = Booking.bookings.Find(i => i.BookingID.Equals(id));
        if(removeBooking != null){
        Booking.bookings.Remove(removeBooking);
        removeBooking.RoomType.ReleaseRoom();
        Console.WriteLine($"Reservation Removed.");
        }else{
        Console.WriteLine($"Wrong ID number"); 
        }             
    }
        public void CancelBookingByName(string customerName){
        Booking removeBooking = Booking.bookings.Find(i => i.CustomerName.Name.Contains(customerName, StringComparison.OrdinalIgnoreCase));
        if(removeBooking != null){
        Booking.bookings.Remove(removeBooking);
        Console.WriteLine($"Reservation Removed.");
        removeBooking.RoomType.ReleaseRoom();
        }else{
        Console.WriteLine($"Wrong customer name"); 
        }             
    }
}
class DisplayBooking{
    public void DisplayCustomerBooking(string customerName){
        Booking customerInfo = Booking.bookings.Find(i => i.CustomerName.Name.Contains(customerName, StringComparison.OrdinalIgnoreCase));
        if(Booking.bookings.Count == 0){
            Console.WriteLine($"No bookings in the system");    
        }else if(customerInfo != null){
            Console.WriteLine($"Booking information for {customerName}");
            customerInfo.DisplayBooking();  
        }else{
            Console.WriteLine($"Wrong Customer Name."); 
        }
    }
}
interface IPayment{
    public void ProcessPayment(double amount);
    public void GenerateReceipt();
}
class CreditCardPayment : IPayment{
    public void ProcessPayment(double amount){
        Console.WriteLine($"Processing Credit Card payment of {amount}");
    }
    public void GenerateReceipt(){
        Console.WriteLine($"Receipt generated for Credit Card payment");
    }  
}
class PayPalPayment : IPayment{
    public void ProcessPayment(double amount){
        Console.WriteLine($"Processing Pay Pal payment of {amount}");
    }
    public void GenerateReceipt(){
        Console.WriteLine($"Receipt generated for Pay Pal payment");
    } 
}

namespace Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
        Room standardRoom = new StandardRoom(201,100);
        double roomPrice = standardRoom.CalculatePrice();
        Customer customer = new Customer("Jane Doe", "john@example.com");
        Booking booking = new Booking(standardRoom, customer, roomPrice);
        // while(true){
         if(standardRoom.CheckIsAvailable() == true){    

//display if the room available or not
            standardRoom.CheckAvailability(standardRoom.RoomNumber);
//calculate total price
            Console.WriteLine($"Enter Check in date:");
            DateTime checkIn = Convert.ToDateTime(Console.ReadLine()); 
            Console.WriteLine($"Enter Check out date:");
            DateTime checkOut = Convert.ToDateTime(Console.ReadLine()); 
            Console.WriteLine($"Total price:");
            booking.CalculateTotalAmount(roomPrice, checkIn, checkOut);
//confirm booking or cancle it
            Console.WriteLine($"Confirm the booking? (yes or no)");
            string confirming = Console.ReadLine();
        if(confirming.Equals("yes", StringComparison.OrdinalIgnoreCase)){
           booking.ConfirmBooking(booking);
           customer.DisplayCustomerDetails();
        }
        // else{
            // break;
        // }
//choose payment method
            Console.WriteLine($"Choose payment method:");
            string paymentMethod = Console.ReadLine();
            IPayment paymentCreditCard = new CreditCardPayment();
            IPayment paymentPayPal = new PayPalPayment();
        if(paymentMethod.Contains("credit card", StringComparison.OrdinalIgnoreCase)){
            paymentCreditCard.ProcessPayment(booking.TotalAmount);
            paymentCreditCard.GenerateReceipt();
            // break;
        }else if(paymentMethod.Contains("pay pal", StringComparison.OrdinalIgnoreCase)){
            paymentPayPal.ProcessPayment(booking.TotalAmount);
            paymentPayPal.GenerateReceipt();
            // break;
        }else{
            Console.WriteLine($"Invalid payment method");
            
        }
        }else{
            Console.WriteLine($"Room not available");   
        }
        // }  
        DisplayBooking displayBooking = new DisplayBooking();
        CancelBooking cancelBooking = new CancelBooking();
        DisplayRoom displayRoom = new DisplayRoom();
        // booking.CheckAvailability(201);
        // booking.DisplayBooking();
        displayBooking.DisplayCustomerBooking("Jane");
        // cancelBooking.CancelBookingById(1); 
        // displayRoom.DisplayRoomInfo(standardRoom);
        
        cancelBooking.CancelBookingByName("Jane");

        }
    }
}
