using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;

class BusinessCard
{
    public string Name { get; set; }
    public string Profession { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Description { get; set; }
    public Dictionary<string, string> SocialMedia { get; set; } = new Dictionary<string, string>();
}

class Program
{
    static readonly string filePath = "business_card.json";
    static BusinessCard card = new BusinessCard();

    static void Main(string[] args)
    {
        // Завантажуємо дані
        LoadCard();

        // Ініціалізація тестових даних, якщо файл порожній
        if (string.IsNullOrEmpty(card.Name))
        {
            card.Name = "Олександр Коваль";
            card.Profession = "Розробник програмного забезпечення";
            card.Email = "o.koval@example.com";
            card.Phone = "+380671234567";
            card.Description = "Люблю кодити, слухати музику та подорожувати!";
            card.SocialMedia = new Dictionary<string, string>
            {
                { "LinkedIn", "linkedin.com/in/okoval" },
                { "GitHub", "github.com/okoval" }
            };
        }

        // Виводимо анімований заголовок
        AnimateTitle("Цифрова візитівка");

        while (true)
        {
            Console.Clear();
            DrawAsciiBorder("Моя візитівка");
            Console.WriteLine("| 1. Показати повну візитівку      |");
            Console.WriteLine("| 2. Про мене                      |");
            Console.WriteLine("| 3. Контакти                      |");
            Console.WriteLine("| 4. Соціальні мережі              |");
            Console.WriteLine("| 5. Редагувати візитівку          |");
            Console.WriteLine("| 6. Вихід                         |");
            DrawAsciiBorderBottom();
            Console.Write("Виберіть опцію: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1": ShowFullCard(); break;
                case "2": ShowAbout(); break;
                case "3": ShowContacts(); break;
                case "4": ShowSocialMedia(); break;
                case "5": EditCard(); break;
                case "6": SaveCard(); return;
                default: Console.WriteLine("Невірна опція! Натисніть будь-яку клавішу..."); Console.ReadKey(); break;
            }
        }
    }

    // Функція для анімації заголовка
    static void AnimateTitle(string title)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        foreach (char c in title)
        {
            Console.Write(c);
            Thread.Sleep(50); // Затримка 50 мс для кожної літери
        }
        Console.WriteLine("\n");
        Console.ResetColor();
        Console.WriteLine("Натисніть будь-яку клавішу, щоб продовжити...");
        Console.ReadKey();
    }

    // Функція для малювання ASCII-рамки
    static void DrawAsciiBorder(string title)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("=====================================");
        Console.WriteLine($"| {title.PadRight(34)}|");
        Console.WriteLine("=====================================");
        Console.ResetColor();
    }

    static void DrawAsciiBorderBottom()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("=====================================");
        Console.ResetColor();
    }

    // Функція для показу повної візитівки
    static void ShowFullCard()
    {
        Console.Clear();
        DrawAsciiBorder("Моя візитівка");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(@"
        ╔══════════════════════════════╗
        ║         ВІЗИТІВКА            ║
        ╚══════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine($"Ім'я...............: {card.Name}");
        Console.WriteLine($"Професія...........: {card.Profession}");
        Console.WriteLine($"Опис...............: {card.Description}");
        Console.WriteLine($"Електронна пошта...: {card.Email}");
        Console.WriteLine($"Телефон............: {card.Phone}");
        Console.WriteLine("Соціальні мережі:");
        foreach (var social in card.SocialMedia)
        {
            Console.WriteLine($"  {social.Key.PadRight(10)}: {social.Value}");
        }
        Console.WriteLine("\nНатисніть будь-яку клавішу...");
        Console.ReadKey();
    }

    // Функція для показу секції "Про мене"
    static void ShowAbout()
    {
        Console.Clear();
        DrawAsciiBorder("Про мене");
        Console.WriteLine($"Ім'я...............: {card.Name}");
        Console.WriteLine($"Професія...........: {card.Profession}");
        Console.WriteLine($"Опис...............: {card.Description}");
        Console.WriteLine("\nНатисніть будь-яку клавішу...");
        Console.ReadKey();
    }

    // Функція для показу контактів
    static void ShowContacts()
    {
        Console.Clear();
        DrawAsciiBorder("Контакти");
        Console.WriteLine($"Електронна пошта...: {card.Email}");
        Console.WriteLine($"Телефон............: {card.Phone}");
        Console.WriteLine("\nНатисніть будь-яку клавішу...");
        Console.ReadKey();
    }

    // Функція для показу соціальних мереж
    static void ShowSocialMedia()
    {
        Console.Clear();
        DrawAsciiBorder("Соціальні мережі");
        if (card.SocialMedia.Count == 0)
        {
            Console.WriteLine("Соціальні мережі не додані!");
        }
        else
        {
            foreach (var social in card.SocialMedia)
            {
                Console.WriteLine($"{social.Key.PadRight(10)}: {social.Value}");
            }
        }
        Console.WriteLine("\nНатисніть будь-яку клавішу...");
        Console.ReadKey();
    }

    // Функція для редагування візитівки
    static void EditCard()
    {
        Console.Clear();
        DrawAsciiBorder("Редагувати візитівку");
        Console.WriteLine("Введіть нові дані (залиште порожнім, щоб не змінювати):");

        Console.Write($"Ім'я ({card.Name}): ");
        string input = Console.ReadLine();
        if (!string.IsNullOrEmpty(input)) card.Name = input;

        Console.Write($"Професія ({card.Profession}): ");
        input = Console.ReadLine();
        if (!string.IsNullOrEmpty(input)) card.Profession = input;

        Console.Write($"Опис ({card.Description}): ");
        input = Console.ReadLine();
        if (!string.IsNullOrEmpty(input)) card.Description = input;

        Console.Write($"Електронна пошта ({card.Email}): ");
        input = Console.ReadLine();
        if (!string.IsNullOrEmpty(input)) card.Email = input;

        Console.Write($"Телефон ({card.Phone}): ");
        input = Console.ReadLine();
        if (!string.IsNullOrEmpty(input)) card.Phone = input;

        // Редагування соціальних мереж
        Console.WriteLine("Редагувати соціальні мережі? (y/n): ");
        if (Console.ReadLine().ToLower() == "y")
        {
            Console.WriteLine("Введіть назву мережі та URL (наприклад, 'LinkedIn linkedin.com/in/user'). Введіть порожній рядок для завершення:");
            while (true)
            {
                Console.Write("Назва мережі та URL: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) break;
                var parts = input.Split(' ', 2);
                if (parts.Length == 2)
                {
                    card.SocialMedia[parts[0]] = parts[1];
                }
                else
                {
                    Console.WriteLine("Невірний формат! Використовуйте: Назва URL");
                }
            }
        }

        SaveCard();
        Console.WriteLine("Візитівку оновлено!");
        Console.WriteLine("Натисніть будь-яку клавішу...");
        Console.ReadKey();
    }

    // Функція для збереження візитівки
    static void SaveCard()
    {
        try
        {
            string json = JsonSerializer.Serialize(card, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Помилка збереження: {ex.Message}");
        }
    }

    // Функція для завантаження візитівки
    static void LoadCard()
    {
        try
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                card = JsonSerializer.Deserialize<BusinessCard>(json) ?? new BusinessCard();
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Помилка завантаження: {ex.Message}");
        }
    }
}
