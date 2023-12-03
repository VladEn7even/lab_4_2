using System;
using System.Collections.Generic;

// Базовий клас Комп'ютер
public class Computer
{
    public string IPAddress { get; set; }
    public int Power { get; set; }
    public string OperatingSystem { get; set; }

    public Computer(string ipAddress, int power, string operatingSystem)
    {
        IPAddress = ipAddress;
        Power = power;
        OperatingSystem = operatingSystem;
    }
}

// Спадкоємні класи: Сервер, Робоча станція, Маршрутизатор
public class Server : Computer
{
    public int StorageCapacity { get; set; }

    public Server(string ipAddress, int power, string operatingSystem, int storageCapacity)
        : base(ipAddress, power, operatingSystem)
    {
        StorageCapacity = storageCapacity;
    }
}

public class Workstation : Computer
{
    public string Department { get; set; }

    public Workstation(string ipAddress, int power, string operatingSystem, string department)
        : base(ipAddress, power, operatingSystem)
    {
        Department = department;
    }
}

public class Router : Computer
{
    public int Ports { get; set; }

    public Router(string ipAddress, int power, string operatingSystem, int ports)
        : base(ipAddress, power, operatingSystem)
    {
        Ports = ports;
    }
}

// Інтерфейс для з'єднання та передачі даних між комп'ютерами
public interface Connectable
{
    void Connect(Computer computer);
    void Disconnect(Computer computer);
    void TransmitData(Computer source, Computer destination, string data);
}

// Клас, що моделює Мережу
public class Network : Connectable
{
    private List<Computer> computers;
    private List<Tuple<Computer, Computer>> connections;

    public Network()
    {
        computers = new List<Computer>();
        connections = new List<Tuple<Computer, Computer>>();
    }

    public void AddComputer(Computer computer)
    {
        computers.Add(computer);
    }

    public void Connect(Computer computer1, Computer computer2)
    {
        connections.Add(new Tuple<Computer, Computer>(computer1, computer2));
        Console.WriteLine($"Connected {computer1.IPAddress} to {computer2.IPAddress}");
    }

    public void Disconnect(Computer computer1, Computer computer2)
    {
        connections.RemoveAll(c => (c.Item1 == computer1 && c.Item2 == computer2) || (c.Item1 == computer2 && c.Item2 == computer1));
        Console.WriteLine($"Disconnected {computer1.IPAddress} from {computer2.IPAddress}");
    }

    public void TransmitData(Computer source, Computer destination, string data)
    {
        Console.WriteLine($"Data transmitted from {source.IPAddress} to {destination.IPAddress}: {data}");
    }

    void Connectable.Connect(Computer computer)
    {
        throw new NotImplementedException();
    }

    void Connectable.Disconnect(Computer computer)
    {
        throw new NotImplementedException();
    }

    void Connectable.TransmitData(Computer source, Computer destination, string data)
    {
        throw new NotImplementedException();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Створення комп'ютерів
        Server server = new Server("192.168.1.1", 1000, "Windows Server", 5000);
        Workstation workstation = new Workstation("192.168.1.2", 500, "Windows 10", "HR Department");
        Router router = new Router("192.168.1.3", 200, "RouterOS", 8);

        // Додавання комп'ютерів до мережі
        Network network = new Network();
        network.AddComputer(server);
        network.AddComputer(workstation);
        network.AddComputer(router);

        // З'єднання комп'ютерів
        network.Connect(server, workstation);
        network.Connect(workstation, router);

        // Передача даних
        network.TransmitData(server, workstation, "Employee data");
        network.TransmitData(workstation, router, "Internet traffic");

        // Відключення комп'ютерів
        network.Disconnect(server, workstation);
        network.Disconnect(workstation, router);
    }
}
