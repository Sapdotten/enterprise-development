using Library.Domain.Entities;
using Library.Domain.Enums;

namespace Library.Domain.Data;

/// <summary>
/// Provides a test data fixtures for the Library domain entities.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Collection of preconfigured Book instances.
    /// Contains 11 books of various genres, authors, publishers, and publication years.
    /// </summary>
    public static List<Book> Books { get; } = [
        new Book
        {
            Id = 1,
            InventoryNumber = 1001,
            Code = "PEL101",
            Authors = "Виктор Пелевин",
            Title = "Чапаев и Пустота",
            PublisherType = PublisherType.Commercial,
            Publisher = Publisher.Eksmo,
            Year = 1996
        },
        new Book
        {
            Id = 2,
            InventoryNumber = 1002,
            Code = "PEL102",
            Authors = "Виктор Пелевин",
            Title = "Ампир V",
            PublisherType = PublisherType.Commercial,
            Publisher = Publisher.AST,
            Year = 2001
        },
        new Book
        {
            Id = 3,
            InventoryNumber = 1003,
            Code = "PEL103",
            Authors = "Виктор Пелевин",
            Title = "Трансгуманизм Inc.",
            PublisherType = PublisherType.Commercial,
            Publisher = Publisher.AdMarginem,
            Year = 2018
        },
        new Book
        {
            Id = 4,
            InventoryNumber = 1004,
            Code = "STR104",
            Authors = "Аркадий Стругацкий, Борис Стругацкий",
            Title = "Пикник на обочине",
            PublisherType = PublisherType.Commercial,
            Publisher = Publisher.Eksmo,
            Year = 1972
        },
        new Book
        {
            Id = 5,
            InventoryNumber = 1005,
            Code = "TAN105",
            Authors = "Эндрю Таненбаум",
            Title = "Архитектура компьютера",
            PublisherType = PublisherType.Academic,
            Publisher = Publisher.Binom,
            Year = 2007
        },
        new Book
        {
            Id = 6,
            InventoryNumber = 1006,
            Code = "LAV106",
            Authors = "Шандор ЛаВей",
            Title = "Библия Сатаны",
            PublisherType = PublisherType.Commercial,
            Publisher = Publisher.Veche,
            Year = 1969
        },
        new Book
        {
            Id = 7,
            InventoryNumber = 1007,
            Code = "MUR107",
            Authors = "Харуки Мураками",
            Title = "1Q84",
            PublisherType = PublisherType.Commercial,
            Publisher = Publisher.AST,
            Year = 2009
        },
        new Book
        {
            Id = 8,
            InventoryNumber = 1008,
            Code = "MAR108",
            Authors = "Джордж Р.Р. Мартин",
            Title = "Игра престолов",
            PublisherType = PublisherType.Commercial,
            Publisher = Publisher.AST,
            Year = 1996
        },
        new Book
        {
            Id = 9,
            InventoryNumber = 1009,
            Code = "KRA109",
            Authors = "Генрих Крамер, Яков Шпренгер",
            Title = "Молот ведьм",
            PublisherType = PublisherType.Academic,
            Publisher = Publisher.Nauka,
            Year = 1487
        },
        new Book
        {
            Id = 10,
            InventoryNumber = 1010,
            Code = "IRO110",
            Authors = "Иродов И.Е.",
            Title = "Задачник по физике",
            PublisherType = PublisherType.Educational,
            Publisher = Publisher.Mir,
            Year = 1988
        },
        new Book
        {
            Id = 11,
            InventoryNumber = 1011,
            Code = "IBS111",
            Authors = "Владимир Милованов, Андрей Гостев",
            Title = "Практическая информационная безопасность",
            PublisherType = PublisherType.Educational,
            Publisher = Publisher.Binom,
            Year = 2016
        }
    ];

    /// <summary>
    /// Collection of preconfigured Reader instances.
    /// Contains 10 student readers with full names, contact information, and registration dates.
    /// </summary>
    public static List<Reader> Readers { get; } = [
        new Reader
        {
            Id = 1,
            FirstName = "Иван",
            LastName = "Алехин",
            PatronymicName = "Игоревич",
            Address = "ул. Ленина, д. 15, кв. 42",
            PhoneNumber = "+7 (903) 123-45-67",
            RegistrationDate = new DateOnly(2025, 1, 15)
        },
        new Reader
        {
            Id = 2,
            FirstName = "Александр",
            LastName = "Волков",
            PatronymicName = "Юрьевич",
            Address = "пр. Победы, д. 8, кв. 112",
            PhoneNumber = "+7 (915) 234-56-78",
            RegistrationDate = new DateOnly(2025, 2, 10)
        },
        new Reader
        {
            Id = 3,
            FirstName = "Никита",
            LastName = "Гришин",
            PatronymicName = "Павлович",
            Address = "ул. Мира, д. 27, кв. 33",
            PhoneNumber = "+7 (926) 345-67-89",
            RegistrationDate = new DateOnly(2025, 2, 20)
        },
        new Reader
        {
            Id = 4,
            FirstName = "Никита",
            LastName = "Домнин",
            PatronymicName = "Михайлович",
            Address = "ш. Энтузиастов, д. 5, кв. 76",
            PhoneNumber = "+7 (937) 456-78-90",
            RegistrationDate = new DateOnly(2025, 3, 5)
        },
        new Reader
        {
            Id = 5,
            FirstName = "Даниил",
            LastName = "Иванов",
            PatronymicName = "Александрович",
            Address = "ул. Космонавтов, д. 11, кв. 29",
            PhoneNumber = "+7 (905) 567-89-01",
            RegistrationDate = new DateOnly(2025, 1, 30)
        },
        new Reader
        {
            Id = 6,
            FirstName = "Дмитрий",
            LastName = "Коновалов",
            PatronymicName = "Сергеевич",
            Address = "пр. Мира, д. 34, кв. 55",
            PhoneNumber = "+7 (916) 678-90-12",
            RegistrationDate = new DateOnly(2025, 3, 12)
        },
        new Reader
        {
            Id = 7,
            FirstName = "Родион",
            LastName = "Маркелов",
            PatronymicName = "Алексеевич",
            Address = "ул. Строителей, д. 7, кв. 18",
            PhoneNumber = "+7 (927) 789-01-23",
            RegistrationDate = new DateOnly(2025, 2, 25)
        },
        new Reader
        {
            Id = 8,
            FirstName = "Андрей",
            LastName = "Панявкин",
            PatronymicName = "Сергеевич",
            Address = "ул. Радужная, д. 4, кв. 63",
            PhoneNumber = "+7 (936) 890-12-34",
            RegistrationDate = new DateOnly(2025, 3, 18)
        },
        new Reader
        {
            Id = 9,
            FirstName = "Матвей",
            LastName = "Пихуров",
            PatronymicName = "Алексеевич",
            Address = "пр. Ленинский, д. 22, кв. 91",
            PhoneNumber = "+7 (904) 901-23-45",
            RegistrationDate = new DateOnly(2025, 3, 22)
        },
        new Reader
        {
            Id = 10,
            FirstName = "Артём",
            LastName = "Шикунов",
            PatronymicName = "Дмитриевич",
            Address = "ул. Трудовая, д. 19, кв. 5",
            PhoneNumber = "+7 (915) 012-34-56",
            RegistrationDate = new DateOnly(2025, 1, 20)
        }
    ];

    /// <summary>
    /// Collection of preconfigured LoanRecord instances representing active or historical book loans.
    /// Contains 12 records linking readers to books with specific issue dates and loan terms.
    /// </summary>
    public static List<LoanRecord> LoanRecords { get; } = [
        new LoanRecord
        {
            Id = 1,
            BookId = 1,
            ReaderId = 1,
            IssueDate = new DateOnly(2025, 3, 10),
            LoanTerm = 365
        },
        new LoanRecord
        {
            Id = 2,
            BookId = 5,
            ReaderId = 2,
            IssueDate = new DateOnly(2025, 2, 15),
            LoanTerm = 21
        },
        new LoanRecord
        {
            Id = 3,
            BookId = 11,
            ReaderId = 3,
            IssueDate = new DateOnly(2025, 4, 5),
            LoanTerm = 14
        },
        new LoanRecord
        {
            Id = 4,
            BookId = 8,
            ReaderId = 4,
            IssueDate = new DateOnly(2025, 3, 20),
            LoanTerm = 14
        },
        new LoanRecord
        {
            Id = 5,
            BookId = 10,
            ReaderId = 5,
            IssueDate = new DateOnly(2025, 1, 25),
            LoanTerm = 21
        },
        new LoanRecord
        {
            Id = 7,
            BookId = 11,
            ReaderId = 7,
            IssueDate = new DateOnly(2025, 4, 10),
            LoanTerm = 14
        },
        new LoanRecord
        {
            Id = 8,
            BookId = 4,
            ReaderId = 8,
            IssueDate = new DateOnly(2025, 2, 28),
            LoanTerm = 14
        },
        new LoanRecord
        {
            Id = 9,
            BookId = 3,
            ReaderId = 9,
            IssueDate = new DateOnly(2025, 3, 5),
            LoanTerm = 14
        },
        new LoanRecord
        {
            Id = 10,
            BookId = 7,
            ReaderId = 10,
            IssueDate = new DateOnly(2025, 4, 3),
            LoanTerm = 14
        },
        new LoanRecord
        {
            Id = 11,
            BookId = 2,
            ReaderId = 1,
            IssueDate = new DateOnly(2025, 4, 12),
            LoanTerm = 14
        },
        new LoanRecord
        {
            Id = 12,
            BookId = 10,
            ReaderId = 2,
            IssueDate = new DateOnly(2025, 3, 1),
            LoanTerm = 365
        }
    ];
    
}