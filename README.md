# **Instructions to Run the C# Console Quiz Application in Visual Studio**

This guide explains how to **open, configure, and run the application** using **Visual Studio** and PostgreSQL.

---

## **1. How to Run the Application in Visual Studio**

### **Step 1: Install Required Software**
Ensure the following are installed on your system:
- **Visual Studio (2022 or later)** – [Download Here](https://visualstudio.microsoft.com/)
- Install with the **.NET Core cross-platform development** workload.
- **PostgreSQL** – [Download Here](https://www.postgresql.org/download/)

### **Step 2: Open the Project in Visual Studio**
1. Open **Visual Studio**.
2. Click **"Open a project or solution"**.
3. Select the **QuizApp.sln** file from the project folder.

### **Step 3: Restore Dependencies**
1. Open the **Package Manager Console** (**Tools** → **NuGet Package Manager** → **Package Manager Console**).
2. Run the following command to restore packages:
```powershell
dotnet restore
```

### **Step 4: Apply Database Migrations**
1. Open the **Package Manager Console**.
2. Run the following command to apply migrations and create the database:
```powershell
dotnet ef database update
```

### **Step 5: Run the Application**
1. Press **F5** (or click **Start** in the Visual Studio toolbar).
2. The console will open with the **main menu**.

---

## **2. How to Change the Database Connection String**

### **Step 1: Open `AppDbContext.cs`**
1. In **Solution Explorer**, navigate to:
**`QuizApp` → `Data` → `AppDbContext.cs`**
2. Open the file in the editor.

### **Step 2: Modify the Connection String**
Locate the `OnConfiguring` method and update the PostgreSQL connection details:

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
optionsBuilder.UseNpgsql("Host=localhost;Database=quizapp;Username=postgres;Password=yourpassword");
}
```
- **`Host=localhost`** → Change if the database is hosted remotely.
- **`Database=quizapp`** → Set the correct database name.
- **`Username=postgres`** → Update with the correct database username.
- **`Password=yourpassword`** → Replace with the actual database password.

### **Step 3: Save and Restart the Application**
1. Save the file (**Ctrl + S**).
2. Press **F5** or click **Start** to run the application again.

---

Now the application is set up and ready to use in **Visual Studio**! 🚀

## **🎮 How to Use the Application**

### **🔹 Option 1: Create a Quiz**
📌 Select **1** and press **Enter**.
📌 Enter a **quiz title** (e.g., "General Knowledge").
📌 Enter **questions and answers**:

```
Enter question text (or 'done' to finish):
What is the capital of France?

Enter correct answer:
Paris
```
📌 Type **"done"** when finished.
✅ **Quiz saved successfully!**

---

### **🔹 Option 2: Play a Quiz**
📌 Select **2** and press **Enter**.
📌 Choose a quiz by entering its **ID**.
📌 Answer the questions:

```
What is the capital of France?
Your answer: Paris
Correct!
```
📌 Enter your **name for the high score**.
✅ **Your score is saved!**

---

### **🔹 Option 3: View High Scores**
📌 Select **3** and press **Enter**.
📌 Choose a quiz to see its **top scores**.

---

### **🔹 Option 4: Exit the App**
📌 Select **4** to **close** the application.

---

## **🎯 Summary**
- **Run the app**: Press **F5** in **Visual Studio**.
- **Create quizzes, play them, and track scores**.
- **Update connection string** in `AppDbContext.cs`.

Now you’re ready to use the **C# Console Quiz App** in **Visual Studio**!
