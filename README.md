# Project Management Application (WPF)

A **WPF-based Project Management Application** designed to streamline task management using a **Kanban-style board**. The application allows users to track the progress of tasks through various stages like "To Do," "In Progress," "Code Review," "QA," and "Done". It also features a modern login interface with support for third-party integrations.

---

## Features

### Login System
- **Secure Login**: Users can log in using their email and password.
- **Third-party Integration**: Buttons for Google, GitHub, and LinkedIn for quick redirection to respective accounts.
- **Stylish Design**: Modern UI with gradient themes and clean layouts.

### Task Management (Main Window)
- **Add Task**: Create tasks in the "To Do" column with a single click.
- **Drag & Drop**: Move tasks across columns to update their status dynamically.
- **Task Deletion**: Right-click on a task to delete it.
- **Task Description Editing**: Click into a task card to update its details.
- **Real-time Updates**: Tasks are saved to a local SQLite database for persistence.

### User Interface
- **Gradient Design**: A visually appealing gradient background and card styling.
- **Responsive Design**: Smooth interactions for task creation and movement.
- **Search Bar**: Placeholder for searching tasks (UI available but not fully implemented).

---

## Screenshots

### Task Board (Main Window)
![Task Board](./path_to_screenshot_1.png)

The task board is divided into five stages:
- **To Do**: For newly created tasks.
- **In Progress**: Tasks currently being worked on.
- **Code Review**: Tasks under review by peers.
- **QA**: Tasks under quality testing.
- **Done**: Completed tasks.

### Login Page
![Login Page](./path_to_screenshot_2.png)

The login page features:
- An option to sign in using an email and password.
- Buttons for Google, GitHub, and LinkedIn integration.
- A clean "Sign Up" section for potential future user registration.

---

## Technologies

- **WPF (Windows Presentation Foundation)** for building the UI.
- **SQLite** for local data storage using Entity Framework Core.
- **C#** as the core programming language.
- **Modern UI Design** with gradient colors and responsive components.

---

