
# Task Manager (Диспетчер задач в Windows)

| Информация                  | WMI Запрос                                                | Пример параметров                         | Значение                                                                                                 |
|------------------------------|----------------------------------------------------------|-------------------------------------------|----------------------------------------------------------------------------------------------------------|
| Operating System details     | `SELECT * FROM Win32_OperatingSystem`                    | Name, Version, Manufacturer               | Name: Microsoft Windows 10 Pro, Version: 10.0.19042, Manufacturer: Microsoft                             | 
| Processor details            | `SELECT * FROM Win32_Processor`                          | Name, Manufacturer, MaxClockSpeed         | Name: Intel(R) Core(TM) i7-9700K CPU @ 3.60GHz, Manufacturer: GenuineIntel, MaxClockSpeed: 3600          |
| BIOS details                 | `SELECT * FROM Win32_BIOS`                               | Manufacturer, Version, ReleaseDate        | Manufacturer: American Megatrends Inc., Version: 5.14, ReleaseDate: 20200703000000.000000+000            |
| Memory details               | `SELECT * FROM Win32_PhysicalMemory`                     | Capacity, Manufacturer, Speed             | Capacity: 8589934592, Manufacturer: Kingston, Speed: 2666                                                |
| Disk drive details           | `SELECT * FROM Win32_DiskDrive`                          | Model, Size, InterfaceType                | Model: Samsung SSD 970 EVO Plus 500GB, Size: 500107862016, InterfaceType: NVMe                           |
| Network adapter details      | `SELECT * FROM Win32_NetworkAdapter`                     | AdapterType, MACAddress, Speed            | AdapterType: Ethernet 802.3, MACAddress: 00-0D-3A-78-C8-E2, Speed: 1000000000                            |
| Installed software details   | `SELECT * FROM Win32_Product`                            | Name, Version, InstallDate                | Name: Microsoft Office Professional Plus 2019, Version: 16.0.13530.20376, InstallDate: 20210101          |
| User account details         | `SELECT * FROM Win32_UserAccount WHERE LocalAccount=True`| Name, SID, Disabled                       | Name: Administrator, SID: S-1-5-21-2881115568-1604047378-1882543358-500, Disabled: False                 |
| Running processes            | `SELECT * FROM Win32_Process`                            | Name, ProcessId, ExecutablePath           | Name: chrome.exe, ProcessId: 1234, ExecutablePath: C:\ProgramFiles\Google\Chrome\Application\chrome.exe |
|Plug-and-Play устройства | `SELECT * FROM Win32_PnPEntity WHERE ConfigManagerErrorCode = 0`| - | Этот запрос используется для получения информации о всех устройствах Plug-and-Play (PnP), у которых код ошибки конфигурации равен 0, что означает, что устройство успешно сконфигурировано и готово к использованию. Он возвращает различную информацию о каждом устройстве, включая идентификатор устройства (DeviceID), идентификатор PnP (PNPDeviceID), описание (Description) и другие атрибуты.|
|USB устройства|`SELECT * FROM Win32_USBHub`| - |Этот запрос используется для получения информации о USB-устройствах, подключенных к компьютеру. Он возвращает различную информацию о каждом USB-устройстве, такую как идентификатор устройства (DeviceID), идентификатор PnP (PNPDeviceID), описание (Description) и другие атрибуты. Например, Model, Size, InterfaceType, и т.д.|

## Code example

```csharp
ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
foreach (ManagementObject obj in searcher.Get())
{q
    string processorName = obj["Name"].ToString();
    string processorManufacturer = obj["Manufacturer"].ToString();
    uint maxClockSpeed = (uint)obj["MaxClockSpeed"];

    Console.WriteLine("Processor Details:");
    Console.WriteLine("Name: " + processorName);
    Console.WriteLine("Manufacturer: " + processorManufacturer);
    Console.WriteLine("Max Clock Speed: " + maxClockSpeed + " MHz");
    Console.WriteLine();
}
```

# Класс Process

Класс `Process` является частью пространства имен `System.Diagnostics` в .NET и предоставляет возможность взаимодействия с процессами операционной системы. Он позволяет запускать новые процессы, получать информацию о запущенных процессах, а также управлять ими.

## Основные возможности

### Получение списка процессов

- С помощью метода `Process.GetProcesses()` можно получить массив всех процессов, запущенных на текущей машине.

### Запуск новых процессов

- Метод `Process.Start()` позволяет запускать новые процессы. Он принимает путь к исполняемому файлу и может также принимать аргументы командной строки.

### Получение информации о процессе

- Класс `Process` предоставляет множество свойств для получения информации о конкретном процессе, таких как имя процесса, идентификатор процесса, память, потребляемую процессом и другие характеристики.

### Управление процессами

- Класс `Process` также позволяет управлять процессами, например, завершать их выполнение с помощью метода `Kill()` или отправлять им сообщения с помощью метода `SendKeys()`.

## Пример использования

```csharp
using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        // Получаем список всех процессов
        Process[] processes = Process.GetProcesses();

        // Выводим информацию о каждом процессе
        foreach (Process process in processes)
        {
            Console.WriteLine($"Имя процесса: {process.ProcessName}");
            Console.WriteLine($"ID процесса: {process.Id}");
            Console.WriteLine($"Память: {process.WorkingSet64 / (1024 * 1024)} MB");
            Console.WriteLine("----------------------------------------");
        }
    }
}


