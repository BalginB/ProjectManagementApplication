# Project Management Application (WPF)

A **WPF (Windows Presentation Foundation)** application for basic project/task management. This project provides a simple **kanban-style board** to manage tasks across different statuses (To Do, In Progress, Code Review, QA, and Done). It also includes a **Login** screen and uses **SQLite** for local data storage.

---

## Table of Contents

- [Features](#features)
- [Technologies](#technologies)
- [Installation](#installation)
- [Usage](#usage)
- [Screenshots](#screenshots)
- [Folder Structure](#folder-structure)
- [Credits](#credits)

---

## Features

1. **Login Page**  
   - Simple login check (no real credential validation, just checks if the fields are not empty).
   - Social media buttons (Google, GitHub, LinkedIn) that open in a browser.

2. **Task Board (Main Window)**  
   - **Add Task**: Create new tasks in the **To Do** column.
   - **Task Columns**: To Do, In Progress, Code Review, QA, and Done.
   - **Drag and Drop**: Move tasks between columns by clicking and dragging the task cards.
   - **Delete Task**: Right-click (context menu) to delete a task.
   - **Task Description**: Click into the card text to edit the task description.
   - **Search Box**: Placeholder in the UI, not fully implemented in this snippet, but styled.

3. **Data Persistence**  
   - Tasks are saved in an **SQLite** database using Entity Framework Core (via `TaskBoardContext`).
   - When the application starts, it ensures the database is created (`EnsureCreated`).

---

## Technologies

- **.NET 6+ / .NET Framework** 
- **WPF (Windows Presentation Foundation)** for the UI
- **SQLite** (via [SQLitePCL](https://github.com/ericsink/SQLitePCL.raw) and EF Core)
- **C#** as the primary programming language



