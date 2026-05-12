using System;

namespace Assignment4
{
    // Q1
    /*
     * Static Binding:
     * Happens at compile time.
     * The method call is determined before the program runs.
     * Example: Method Overloading.
     * 
     * Dynamic Binding:
     * Happens at runtime.
     * The method call is determined while the program is running.
     * Example: Method Overriding.
     */

    // Q2
    /*
     * Method Overloading:
     * Same method name with different parameters.
     * Happens in the same class.
     * Used to perform similar operations in different ways.
     * 
     * Method Overriding:
     * Redefining a method in the child class.
     * Parent and child classes are required.
     * Used to change method behavior.
     */

    // Q3
    /*
     * virtual:
     * Allows the method to be overridden in child classes.
     * 
     * override:
     * Used in the child class to provide a new implementation.
     * 
     * base:
     * Used to call the parent class method.
     */

    // Q4

    class Ticket
    {
        public int TicketId { get; set; }
        public string MovieName { get; set; }
        public decimal Price { get; set; }

        public decimal PriceAfterTax
        {
            get
            {
                return Price + (Price * 0.14m);
            }
        }

        public Ticket()
        {
        }

        public Ticket(int id, string movieName, decimal price)
        {
            TicketId = id;
            MovieName = movieName;
            Price = price;
        }

        public virtual void PrintTicket()
        {
            Console.WriteLine("Ticket #" + TicketId +
                              " | " + MovieName +
                              " | Price: " + Price + " EGP" +
                              " | After Tax: " + PriceAfterTax.ToString("0.00") + " EGP");
        }

        public void SetPrice(decimal price)
        {
            Price = price;

            Console.WriteLine("Setting price directly: " + price);
        }

        public void SetPrice(decimal basePrice, decimal multiplier)
        {
            Price = basePrice * multiplier;

            Console.WriteLine("Setting price with multiplier: " +
                              basePrice + " x " + multiplier +
                              " = " + Price);
        }
    }

    class StandardTicket : Ticket
    {
        public string SeatNumber { get; set; }

        public StandardTicket(int id, string movieName, decimal price, string seat)
            : base(id, movieName, price)
        {
            SeatNumber = seat;
        }

        public override void PrintTicket()
        {
            base.PrintTicket();

            Console.WriteLine("  Seat: " + SeatNumber);
        }
    }

    class VIPTicket : Ticket
    {
        public bool LoungeAccess { get; set; }
        public decimal ServiceFee { get; set; }

        public VIPTicket(int id, string movieName, decimal price, bool lounge, decimal fee)
            : base(id, movieName, price)
        {
            LoungeAccess = lounge;
            ServiceFee = fee;
        }

        public override void PrintTicket()
        {
            base.PrintTicket();

            string loungeText = "No";

            if (LoungeAccess == true)
            {
                loungeText = "Yes";
            }

            Console.WriteLine("  Lounge: " + loungeText +
                              " | Service Fee: " + ServiceFee + " EGP");
        }
    }

    class IMAXTicket : Ticket
    {
        public bool Is3D { get; set; }

        public IMAXTicket(int id, string movieName, decimal price, bool is3D)
            : base(id, movieName, price)
        {
            Is3D = is3D;
        }

        public override void PrintTicket()
        {
            base.PrintTicket();

            string text = "No";

            if (Is3D == true)
            {
                text = "Yes";
            }

            Console.WriteLine("  IMAX 3D: " + text);
        }
    }

    class Cinema
    {
        Ticket[] tickets = new Ticket[10];
        int count = 0;

        public void OpenCinema()
        {
            Console.WriteLine("========== Cinema Opened ==========");
            Console.WriteLine("Projector started.");
        }

        public void CloseCinema()
        {
            Console.WriteLine();
            Console.WriteLine("========== Cinema Closed ==========");
            Console.WriteLine("Projector stopped.");
        }

        public void AddTicket(Ticket t)
        {
            tickets[count] = t;
            count++;
        }

        public void PrintAllTickets()
        {
            Console.WriteLine();
            Console.WriteLine("========== All Tickets ==========");

            for (int i = 0; i < count; i++)
            {
                tickets[i].PrintTicket();
            }
        }

        public static void ProcessTicket(Ticket t)
        {
            Console.WriteLine();
            Console.WriteLine("========== Process Single Ticket ==========");

            t.PrintTicket();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Cinema cinema = new Cinema();

            cinema.OpenCinema();

            StandardTicket standard =
                new StandardTicket(1, "Inception", 150, "A-5");

            VIPTicket vip =
                new VIPTicket(2, "Avengers", 200, true, 50);

            IMAXTicket imax =
                new IMAXTicket(3, "Dune", 180, false);

            Console.WriteLine();
            Console.WriteLine("========== SetPrice Test ==========");

            standard.SetPrice(150);

            standard.SetPrice(100, 1.5m);

            cinema.AddTicket(standard);
            cinema.AddTicket(vip);
            cinema.AddTicket(imax);

            cinema.PrintAllTickets();

            Cinema.ProcessTicket(vip);

            cinema.CloseCinema();
        }
    }
}