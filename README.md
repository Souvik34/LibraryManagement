# 📘 Console-Based Library Management System 🖥️📚

A **beginner-friendly** C# console application to manage a library with basic features for **Admins** and **Users**.  
📁 Uses **CSV file storage** — no need for a database! Clean UI, simple logic, and great for learning File I/O and basic system design.

---

## 🧰 Features

### 👮 Admin Panel
🟢 Add New Books  
🟡 Update Existing Books  
🔴 Delete Books  
📖 View All Books  
👥 View Registered Users  
📄 View Borrowed Book Records  
🔍 Search Books & Users  

### 👤 User Panel
📝 Register / 🔐 Login  
📚 View Available Books  
🔍 Search by Title, Author, or Genre  
📥 Borrow a Book  
📤 Return a Book  

---

## 💾 Data Storage (CSV-Based)

| File Name       | Purpose                        |
|----------------|--------------------------------|
| `books.csv`     | Stores book details             |
| `users.csv`     | Stores user data and logins     |
| `borrowed.csv`  | Stores current borrow records   |

📌 No external databases needed – all logic is handled through file-based persistence.

---

## 🛠️ Tech Stack

- 🔤 **Language**: C#
- 🧱 **Framework**: .NET Console App
- 🗂️ **Storage**: CSV files (via `System.IO`)
- 💡 **Concepts Used**: File Handling, OOP, Menu-Driven Console UI

---

## ▶️ How to Run the Project

1. 🚀 **Clone the repository**:
   ```bash
   git clone https://github.com/your-username/ConsoleLibrarySystem.git
   cd ConsoleLibrarySystem
   
2. 🔧 Build & Run:
   ```bash
   dotnet build
   dotnet run
  Or simply open the project in Visual Studio and click Run ▶️

## 🗂️ Project Structure
```
📁 ConsoleLibrarySystem/
├── 📁 Models/
│   ├── 📄 Book.cs
│   ├── 📄 User.cs
│   └── 📄 BorrowRecord.cs
│
├── 📁 Services/
│   ├── 📄 BookService.cs
│   ├── 📄 UserService.cs
│   └── 📄 BorrowService.cs
│
├── 📁 Data/
│   ├── 📄 books.csv
│   ├── 📄 users.csv
│   └── 📄 borrowed.csv
│
└── 📄 Program.cs
```

## 📜 License
📝 MIT License © 2025 [Souvik Sural](https://github.com/Souvik34/LibraryManagement/blob/master/LICENSE.txt)


## 🤝 Contributing
👋 Contributions are welcome!
If you'd like to improve or add features, feel free to fork and submit a pull request.
Found a bug? 🐛 Open an issue!
