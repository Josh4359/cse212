/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService {
    public static void Run() {
        // Example code to see what's in the customer service queue:
        // var cs = new CustomerService(10);
        // Console.WriteLine(cs);

        // Test Cases

        // Test 1
        // Scenario: 
        // Expected Result: creating a CustomerService object with a size of 0 or less should default to 10
        Console.WriteLine("Test 1");

        CustomerService test1 = new(0);
        if (test1._maxSize == 10)
            Console.WriteLine("CustomerService initialized successfully");
        else
            Console.WriteLine("CustomerService initialization failed");

        // Defect(s) Found: none

        Console.WriteLine("=================");

        // Test 2
        // Scenario: 
        // Expected Result: AddNewCustomer should append a new cutomer to the queue
        Console.WriteLine("Test 2");

        CustomerService test2 = new(1);
        test2.AddNewCustomer();
        if (test2._queue.Count == 1)
            Console.WriteLine("Customer added successfully");
        else
            Console.WriteLine("AddNewCustomer failed");

        // Defect(s) Found: none

        Console.WriteLine("=================");

        // Test 3
        // Scenario: 
        // Expected Result: If the customer queue is full, attempting to add another customer should display an error.
        Console.WriteLine("Test 3");

        CustomerService test3 = new(1);
        test3.AddNewCustomer();
        test3.AddNewCustomer();
        if (test3._queue.Count == 1)
            Console.WriteLine("Queue limit enforced successfully");
        else
            Console.WriteLine("Queue limit failed");

        // Defect(s) Found: _queue.Count check should be >= _maxSize, not > _maxSize

        Console.WriteLine("=================");

        // Test 4
        // Scenario: 
        // Expected Result: When a customer is served, they should be removed from the queue
        Console.WriteLine("Test 4");

        CustomerService test4 = new(1);
        test4.AddNewCustomer();
        test4.ServeCustomer();
        if (test4._queue.Count == 0)
            Console.WriteLine("Customer served successfully");
        else
            Console.WriteLine("Customer dequeue failed");

        // Defect(s) Found: customer was being removed before being cached

        Console.WriteLine("=================");

        // Test 5
        // Scenario: 
        // Expected Result: If the queue is empty, display an error message
        Console.WriteLine("Test 5");

        CustomerService test5 = new(1);
        test5.ServeCustomer();

        // Defect(s) Found: there was no check for an empty queue

        Console.WriteLine("=================");
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize) {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer {
        public Customer(string name, string accountId, string problem) {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString() {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the 
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer() {
        // Verify there is room in the service queue
        if (_queue.Count >= _maxSize) {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer() {
        if (_queue.Count == 0)
        {
            Console.WriteLine("No customers to serve!");
            return;
        }

        var customer = _queue[0];
        _queue.RemoveAt(0);
        Console.WriteLine(customer);
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a 
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString() {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}