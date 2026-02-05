#nullable disable
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestaurantManagement.DAL;
using RestaurantManagement.Models;

namespace RestaurantManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            // ==== HOST AND DB CONTEXT CONFIGURATION ====
            IHost _host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    var configuration = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=RestaurantDB;Integrated Security=True;Trusted_Connection=yes;TrustServerCertificate=True;";
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(configuration));
                })
                .Build();

            using var scope = _host.Services.CreateScope();
            var db = scope.ServiceProvider.GetService<ApplicationDbContext>();

            //if (db != null)
            //{
            //    db.Database.Migrate();
            //    db.Database.EnsureCreated();
            //}
            //else
            //{
            //    Console.WriteLine("Error: database context not found!");
            //    return;
            //}

            // =========== MAIN MENU ===========
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Restaurant Chain Management ===");
                Console.WriteLine("1. Add restaurant");
                Console.WriteLine("2. List restaurants");
                Console.WriteLine("3. Select restaurant");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1": AddRestaurant(db); break;
                    case "2": ShowRestaurants(db); break;
                    case "3": ManageRestaurant(db); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid option!"); break;
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }

        // ==================== RESTAURANTS ====================
        static void AddRestaurant(ApplicationDbContext db)
        {
            Console.Write("Restaurant name: ");
            string name = Console.ReadLine();

            Console.Write("Country: ");
            string country = Console.ReadLine();

            Console.Write("ZIP code: ");
            string zipCode = Console.ReadLine();
            if (zipCode.Length != 6 || !zipCode.Contains("-"))
            {
                Console.WriteLine("Invalid ZIP code format");
                return;
            }

            Console.Write("City: ");
            string city = Console.ReadLine();

            Console.Write("Street: ");
            string street = Console.ReadLine();

            Console.Write("Phone number: ");
            string phoneNumber = Console.ReadLine();
            if (phoneNumber.Length != 9)
            {
                Console.WriteLine("Invalid phone number");
                return;
            }

            Console.Write("Email: ");
            string email = Console.ReadLine();
            if (!email.Contains("@"))
            {
                Console.WriteLine("Invalid email");
                return;
            }

            Console.Write("Opening time (HH:mm): ");
            TimeOnly opening = TimeOnly.Parse(Console.ReadLine());

            Console.Write("Closing time (HH:mm): ");
            TimeOnly closing = TimeOnly.Parse(Console.ReadLine());

            var address = new Address(country, zipCode, city, street);

            var restaurant = new Restaurant
            {
                Name = name,
                Address = address,
                PhoneNumber = phoneNumber,
                Email = email,
                OpeningHours = opening,
                ClosingHours = closing,
                Menu = new List<MenuItem>(),
                Employees = new List<Employee>(),
            };

            db.Restaurant.Add(restaurant);
            db.SaveChanges();

            Console.WriteLine("Restaurant added and saved to the database!");
        }

        static void ShowRestaurants(ApplicationDbContext db)
        {
            var restaurants = db.Restaurant.Include(r => r.Address).ToList();

            if (!restaurants.Any())
            {
                Console.WriteLine("No restaurants available.");
                return;
            }

            var sorted = restaurants.OrderBy(r => r.Name).ToList();

            Console.WriteLine("\nRestaurant list:");
            for (int i = 0; i < sorted.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {sorted[i].Name} ({sorted[i].Address.City})");
            }
        }

        static void ManageRestaurant(ApplicationDbContext db)
        {
            var restaurants = db.Restaurant
                .Include(r => r.Address)
                .Include(r => r.Employees)
                .Include(r => r.Reservations)
                .Include(r => r.Menu)
                .ToList();

            if (!restaurants.Any())
            {
                Console.WriteLine("No restaurants available.");
                return;
            }

            ShowRestaurants(db);
            Console.Write("\nSelect restaurant: ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > restaurants.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            var selected = restaurants[choice - 1];

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== {selected.Name} ===");
                Console.WriteLine("1. Employees");
                Console.WriteLine("2. Reservations");
                Console.WriteLine("3. Menu");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                string opt = Console.ReadLine();

                switch (opt)
                {
                    case "1": ManageEmployees(selected, db); break;
                    case "2": ManageReservations(selected, db); break;
                    case "3": ManageMenu(selected, db); break;
                    case "0": return;
                }
            }
        }

        // ==================== EMPLOYEES ====================
        static void ManageEmployees(Restaurant restaurant, ApplicationDbContext db)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== {restaurant.Name} ===");
                Console.WriteLine("=== EMPLOYEES ===");
                Console.WriteLine("1. Add");
                Console.WriteLine("2. Remove");
                Console.WriteLine("3. List");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                string opt = Console.ReadLine();

                switch (opt)
                {
                    case "1": AddEmployee(restaurant, db); break;
                    case "2": RemoveEmployee(restaurant, db); break;
                    case "3": ShowEmployees(restaurant, db); break;
                    case "0": return;
                }
            }
        }

        static void AddEmployee(Restaurant restaurant, ApplicationDbContext db)
        {
            Console.Write("First name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last name: ");
            string lastName = Console.ReadLine();

            Console.Write("Phone number: ");
            string phoneNumber = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Date of birth (yyyy-MM-dd): ");
            DateTime.TryParse(Console.ReadLine(), out DateTime dateOfBirth);

            Console.Write("Country: ");
            string country = Console.ReadLine();

            Console.Write("ZIP code: ");
            string zipCode = Console.ReadLine();

            Console.Write("City: ");
            string city = Console.ReadLine();

            Console.Write("Street: ");
            string street = Console.ReadLine();

            var address = new Address(country, zipCode, city, street);

            var employee = new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                Email = email,
                DateOfBirth = dateOfBirth,
                Address = address,
                HiredOn = DateTime.Now,
                Restaurant = restaurant
            };

            db.Employee.Add(employee);
            db.SaveChanges();

            Console.WriteLine("Employee added and saved to the database!");
        }

        static void RemoveEmployee(Restaurant restaurant, ApplicationDbContext db)
        {
            Console.Write("Last name: ");
            string name = Console.ReadLine();

            var emp = db.Employee.FirstOrDefault(x =>
                x.LastName.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                x.RestaurantId == restaurant.Id);

            if (emp != null)
            {
                db.Employee.Remove(emp);
                db.SaveChanges();
                Console.WriteLine("Employee removed from the database!");
            }
        }

        static void ShowEmployees(Restaurant restaurant, ApplicationDbContext db)
        {
            var employees = db.Employee
                .Where(e => e.RestaurantId == restaurant.Id)
                .ToList();

            foreach (var e in employees)
            {
                Console.WriteLine($"{e.FirstName} {e.LastName}");
            }
        }

        // ==================== RESERVATIONS ====================
        static void ManageReservations(Restaurant restaurant, ApplicationDbContext db)
        {
            while (true)
            {
                Console.WriteLine($"=== {restaurant.Name} ===");
                Console.WriteLine("=== RESERVATIONS ===");
                Console.WriteLine("1. Add");
                Console.WriteLine("2. Remove");
                Console.WriteLine("3. List");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                string opt = Console.ReadLine();

                switch (opt)
                {
                    case "1": AddReservation(restaurant, db); break;
                    case "2": RemoveReservation(restaurant, db); break;
                    case "3": ShowReservation(restaurant, db); break;
                    case "0": return;
                }
            }
        }

        static void AddReservation(Restaurant restaurant, ApplicationDbContext db)
        {
            Console.Write("Customer last name: ");
            string name = Console.ReadLine();

            Console.Write("Number of people: ");
            int.TryParse(Console.ReadLine(), out int people);

            Console.Write("Phone: ");
            string phone = Console.ReadLine();

            Console.Write("Date (yyyy-MM-dd): ");
            DateTime.TryParse(Console.ReadLine(), out DateTime date);

            Console.Write("Time (HH:mm): ");
            TimeOnly.TryParse(Console.ReadLine(), out TimeOnly time);

            var reservation = new Reservation
            {
                CustomerName = name,
                NumberOfPeople = people,
                PhoneNumber = phone,
                Date = date,
                Time = time,
                Restaurant = restaurant
            };

            db.Reservation.Add(reservation);
            db.SaveChanges();

            Console.WriteLine("Reservation added and saved to the database!");
        }

        static void RemoveReservation(Restaurant restaurant, ApplicationDbContext db)
        {
            Console.Write("Customer last name: ");
            string name = Console.ReadLine();

            var res = db.Reservation.FirstOrDefault(x =>
                x.CustomerName.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                x.RestaurantId == restaurant.Id);

            if (res != null)
            {
                db.Reservation.Remove(res);
                db.SaveChanges();
                Console.WriteLine("Reservation removed from the database!");
            }
        }

        static void ShowReservation(Restaurant restaurant, ApplicationDbContext db)
        {
            var reservations = db.Reservation
                .Where(r => r.RestaurantId == restaurant.Id)
                .ToList();

            foreach (var r in reservations)
            {
                Console.WriteLine($"{r.CustomerName} {r.Date:yyyy-MM-dd} {r.Time}");
            }
        }

        // ==================== MENU ====================
        static void ManageMenu(Restaurant restaurant, ApplicationDbContext db)
        {
            while (true)
            {
                Console.WriteLine($"=== {restaurant.Name} ===");
                Console.WriteLine("=== MENU ===");
                Console.WriteLine("1. Add menu item");
                Console.WriteLine("2. Remove menu item");
                Console.WriteLine("3. Show menu");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                string opt = Console.ReadLine();

                switch (opt)
                {
                    case "1": AddMenuItem(restaurant, db); break;
                    case "2": RemoveMenuItem(restaurant, db); break;
                    case "3": ShowMenu(restaurant, db); break;
                    case "0": return;
                }
            }
        }

        static void AddMenuItem(Restaurant restaurant, ApplicationDbContext db)
        {
            Console.Write("Dish name: ");
            string name = Console.ReadLine();

            Console.Write("Description: ");
            string description = Console.ReadLine();

            Console.Write("Price: ");
            float.TryParse(Console.ReadLine(), out float price);

            var item = new MenuItem
            {
                Name = name,
                Description = description,
                Price = price
            };

            restaurant.Menu.Add(item);
            db.SaveChanges();

            Console.WriteLine("Menu item added!");
        }

        static void RemoveMenuItem(Restaurant restaurant, ApplicationDbContext db)
        {
            Console.Write("Dish name to remove: ");
            string name = Console.ReadLine();

            var item = restaurant.Menu
                .FirstOrDefault(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (item != null)
            {
                restaurant.Menu.Remove(item);
                db.SaveChanges();
                Console.WriteLine("Menu item removed!");
            }
        }

        static void ShowMenu(Restaurant restaurant, ApplicationDbContext db)
        {
            db.Entry(restaurant).Collection(r => r.Menu).Load();

            if (!restaurant.Menu.Any())
            {
                Console.WriteLine("No menu items available.");
                return;
            }

            foreach (var m in restaurant.Menu)
            {
                Console.WriteLine($"{m.Name} - {m.Price} PLN");
            }
        }
    }
}
